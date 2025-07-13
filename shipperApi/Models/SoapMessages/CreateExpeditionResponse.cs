using System.Xml.Serialization;

namespace shipperApi.Models;



[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapExpeditionResponseEnvelope
{
    [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapExpeditionResponseBody? ResponseBody { get; set; }

    public SoapExpeditionResponseEnvelope() { }
}

public class SoapExpeditionResponseBody
{
    // AQUI ESTÁ A PROPRIEDADE QUE FALTAVA
    [XmlElement("CreateExpeditionResponse", Namespace = "http://www.talend.org/service/")]
    public CreateExpeditionResponse? CreateExpeditionResponse { get; set; }

    public SoapExpeditionResponseBody() { }
}

public class CreateExpeditionResponse
{
    // Adicionei as propriedades que você estava a tentar usar no seu endpoint
    [XmlElement("p_errorCode")]
    public string? PerrorCode { get; set; }
    
    [XmlElement("labelB64")]
    public string? LabelB64 { get; set; }

    public CreateExpeditionResponse() { }
}