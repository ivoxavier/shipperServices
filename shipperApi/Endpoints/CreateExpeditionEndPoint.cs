using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using shipperApi.Models;
using shipperApi.Models.DataBase;
using MySqlConnector;
using System.Data;
using MiniValidation;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using shipperApi.Services;

namespace shipperApi.Endpoints;

public static class CreateExpeditionEndpoint
{
    public static IEndpointRouteBuilder MapCreateExpeditionEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/services/ShippingServices");

        group.MapPost("/CreateExpeditionRequest", async (HttpRequest request, [FromServices] ShippingDevContex db,[FromServices] IReportGeneratorService reportService) =>
        {
            SoapExpeditionEnvelope? envelope;
            CreateExpeditionRequest? expeditionRequest;

            
            try
            {
                string requestBody;
                using (var streamReader = new StreamReader(request.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }   

                Console.WriteLine(requestBody);

    
                var envelopeSerializer = new XmlSerializer(typeof(SoapExpeditionEnvelope));
                using (var stringReader = new StringReader(requestBody))
                {
                    envelope = envelopeSerializer.Deserialize(stringReader) as SoapExpeditionEnvelope;
                }

                if (envelope?.Body?.Any == null)
                {
                    return Results.BadRequest("Body is invalid or body is empty");
                }

                var contentSerializer = new XmlSerializer(typeof(CreateExpeditionRequest), new XmlRootAttribute("CreateExpeditionRequest") { Namespace = "http://www.talend.org/service/" });
                using (var nodeReader = new XmlNodeReader(envelope.Body.Any))
                {
                    expeditionRequest = contentSerializer.Deserialize(nodeReader) as CreateExpeditionRequest;
                }

                if (expeditionRequest == null)
                {
                    return Results.BadRequest("Elements invalid inside Body");
                }
            }
            catch (Exception ex)
            {
                
                return Results.BadRequest($"Erro during processing XML: {ex.Message}");
            }


            if (!MiniValidator.TryValidate(expeditionRequest, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            //var account = expeditionRequest.Account;
            //var user = expeditionRequest.User;
            //var password = expeditionRequest.Password;

            var connection = db.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();
                

                 await using (var cmdLogin = connection.CreateCommand())
                {       
                    cmdLogin.CommandText = "admin_login";
                    cmdLogin.CommandType = CommandType.StoredProcedure;
                    cmdLogin.Parameters.Add(new MySqlParameter("@p_login", 33));
                    cmdLogin.Parameters.Add(new MySqlParameter("@p_pass", 44));

                    var p_errorCode_login = new MySqlParameter("@p_errorCode", MySqlDbType.Int64)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmdLogin.Parameters.Add(p_errorCode_login);

        
                    await cmdLogin.ExecuteNonQueryAsync();

                    if (Convert.ToInt64(p_errorCode_login.Value) != 0)
                    {
                        return Results.BadRequest("Codigo errado");
                    }
                }


                await using (var cmdLogin = connection.CreateCommand())
                {

                    cmdLogin.CommandText = "create_expedition";
                    cmdLogin.CommandType = CommandType.StoredProcedure;
                    cmdLogin.Parameters.Add(new MySqlParameter("@p_customerAccount", 388));



                    var o_fastReportPath = new MySqlParameter("@o_fastReportPath", MySqlDbType.VarChar, 255)
                    {
                        Direction = ParameterDirection.Output
                    };
                    //var o_sessionMessage = new MySqlParameter("@o_sessionMessage", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                    cmdLogin.Parameters.Add(o_fastReportPath);
                    //cmdLogin.Parameters.Add(o_sessionCode);
                    //cmdLogin.Parameters.Add(o_sessionMessage);

                    await cmdLogin.ExecuteNonQueryAsync();

                    var reportFileName = "demo.frx";
                    //Convert.ToString(o_fastReportPath.Value);

                    string? labelBase64 = null;

                    if (!string.IsNullOrEmpty(reportFileName))
                    {
                        try
                        {

                            byte[] pdfBytes = await reportService.GeneratePdfAsync(reportFileName, expeditionRequest);
                            labelBase64 = Convert.ToBase64String(pdfBytes);
                        }
                        catch (Exception reportEx)
                        {
                            Console.WriteLine($"Erro ao gerar a etiqueta do FastReport: {reportEx.Message}");
                        }
                    }

                    //if (Convert.ToInt64(p_errorCode.Value) != 0)
                    // {
                    //    return Results.BadRequest(Convert.ToString(o_sessionMessage.Value) ?? "Utilizador inexistente ou credenciais inválidas.");
                    // }

                    var expeditionresponseEnvelope = new SoapExpeditionResponseEnvelope
                    {
                        ResponseBody = new SoapExpeditionResponseBody
                        {
                            CreateExpeditionResponse = new CreateExpeditionResponse
                            {
                                PerrorCode = "wef", //Convert.ToString(o_fastReportPath),
                                LabelB64 = Convert.ToString(labelBase64)
                            }
                        }
                    };
                    return Results.Ok(expeditionresponseEnvelope);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return Results.Problem("Erro no servidor", statusCode: 500);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        })
        .Accepts<SoapExpeditionEnvelope>("application/xml")
        .Produces<SoapExpeditionResponseEnvelope>(StatusCodes.Status200OK, "application/xml")
        .Produces<string>(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Create Expedition");

        return app;
    }
}
