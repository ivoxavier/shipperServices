// Ficheiro: Endpoints/ShippingSessionLoginEndpoint.cs

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

namespace shipperApi.Endpoints;

public static class ShippingSessionLoginEndpoint
{
    public static IEndpointRouteBuilder MapShippingSessionLoginEndpoint(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/services");

        group.MapPost("/ShippingServices", async (HttpRequest request, [FromServices] ShippingDevContex db) =>
        {
            SoapEnvelope? envelope;
            ShippingSessionLogin? loginRequest;

            
            try
            {
                string requestBody;
                using (var streamReader = new StreamReader(request.Body))
                {
                    requestBody = await streamReader.ReadToEndAsync();
                }

    
                var envelopeSerializer = new XmlSerializer(typeof(SoapEnvelope));
                using (var stringReader = new StringReader(requestBody))
                {
                    envelope = envelopeSerializer.Deserialize(stringReader) as SoapEnvelope;
                }

                if (envelope?.Body?.Any == null)
                {
                    return Results.BadRequest("Body is invalid or body is empty");
                }

                var contentSerializer = new XmlSerializer(typeof(ShippingSessionLogin), new XmlRootAttribute("ShippingSessionLogin") { Namespace = "http://www.talend.org/service/" });
                using (var nodeReader = new XmlNodeReader(envelope.Body.Any))
                {
                    loginRequest = contentSerializer.Deserialize(nodeReader) as ShippingSessionLogin;
                }

                if (loginRequest == null)
                {
                    return Results.BadRequest("Elements invalid inside Body");
                }
            }
            catch (Exception ex)
            {
                
                return Results.BadRequest($"Erro during processing XML: {ex.Message}");
            }
           


    
            if (!MiniValidator.TryValidate(loginRequest, out var errors))
            {
                return Results.ValidationProblem(errors);
            }

            var account = loginRequest.Account;
            var user = loginRequest.User;
            var password = loginRequest.Password;

            var connection = db.Database.GetDbConnection();

            try
            {
                await connection.OpenAsync();

                await using (var cmdLogin = connection.CreateCommand())
                {
                    cmdLogin.CommandText = "check_user";
                    cmdLogin.CommandType = CommandType.StoredProcedure;
                    cmdLogin.Parameters.Add(new MySqlParameter("@p_Account", account));
                    cmdLogin.Parameters.Add(new MySqlParameter("@p_User", user));
                    cmdLogin.Parameters.Add(new MySqlParameter("@p_Password", password));

                    var p_errorCode = new MySqlParameter("@p_errorCode", MySqlDbType.Int64) { Direction = ParameterDirection.Output };
                    var o_sessionCode = new MySqlParameter("@o_sessionCode", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };
                    var o_sessionMessage = new MySqlParameter("@o_sessionMessage", MySqlDbType.VarChar, 255) { Direction = ParameterDirection.Output };

                    cmdLogin.Parameters.Add(p_errorCode);
                    cmdLogin.Parameters.Add(o_sessionCode);
                    cmdLogin.Parameters.Add(o_sessionMessage);

                    await cmdLogin.ExecuteNonQueryAsync();

                    if (Convert.ToInt64(p_errorCode.Value) != 0)
                    {
                        return Results.BadRequest(Convert.ToString(o_sessionMessage.Value) ?? "Utilizador inexistente ou credenciais inválidas.");
                    }

                    var responseEnvelope = new SoapResponseEnvelope
                    {
                        ResponseBody = new SoapResponseBody
                        {
                            LoginResponse = new ShippingLoginResponse
                            {
                                SessionCode = Convert.ToString(o_sessionCode.Value),
                                SessionMessage = Convert.ToString(o_sessionMessage.Value)
                            }
                        }
                    };
                    return Results.Ok(responseEnvelope);
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
        .Accepts<SoapEnvelope>("application/xml")
        .Produces<SoapResponseEnvelope>(StatusCodes.Status200OK, "application/xml")
        .Produces<string>(StatusCodes.Status400BadRequest)
        .ProducesValidationProblem()
        .Produces(StatusCodes.Status500InternalServerError)
        .WithTags("Shipping Session");

        return app;
    }
}