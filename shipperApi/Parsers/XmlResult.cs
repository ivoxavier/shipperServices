using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Http;

namespace shipperApi.ToXML; // Se o seu projeto tiver outro nome, ajuste o namespace aqui

public static class XmlResults
{
  
    public static IResult Send(object data)
    {
        return new XmlResult(data);
    }
}


public class XmlResult : IResult
{
    private readonly object _data;

    public XmlResult(object data)
    {
        _data = data;
    }

    public async Task ExecuteAsync(HttpContext httpContext)
    {
        // Define o tipo de conteúdo da resposta para XML
        httpContext.Response.ContentType = "application/xml; charset=utf-8";

        // Cria o serializador para o tipo do objeto que estamos a enviar
        var serializer = new XmlSerializer(_data.GetType());

        // Para respostas SOAP, é importante definir os namespaces para que o XML
        // seja formatado corretamente com os prefixos (ex: <soap:Envelope>)
        var namespaces = new XmlSerializerNamespaces();
        namespaces.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");

        // Escrevemos o XML para um stream em memória primeiro
        await using var memoryStream = new MemoryStream();
        serializer.Serialize(memoryStream, _data, namespaces);

        // Resetamos a posição do stream para o início antes de o copiar
        memoryStream.Seek(0, SeekOrigin.Begin);

        // Copiamos o conteúdo do stream em memória para o corpo da resposta final
        await memoryStream.CopyToAsync(httpContext.Response.Body);
    }
}