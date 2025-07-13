using System.Xml.Serialization;

namespace shipperApi.Models;



[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapManifestResponseEnvelope
{
    [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapManifestResponseBody? ResponseBody { get; set; }

    public SoapManifestResponseEnvelope() { }
}

public class SoapManifestResponseBody
{
    
    [XmlElement("CreateManifestResponse", Namespace = "http://www.talend.org/service/")]
    public CreateManifestResponse? CreateManifestResponse { get; set; }

    public SoapManifestResponseBody() { }
}

public class CreateManifestResponse
{
    
    [XmlElement("p_errorCode")]
    public string? PerrorCode { get; set; }
    
    [XmlElement("labelB64")]
    public string? LabelB64 { get; set; }

    public CreateManifestResponse() { }
}