using System.Xml.Serialization;


[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapResponseEnvelope
{

    [XmlNamespaceDeclarations]
    public XmlSerializerNamespaces Namespaces
    {
        get
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("soap", "http://schemas.xmlsoap.org/soap/envelope/");
            return ns;
        }
        set { }
    }

    [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapResponseBody? ResponseBody { get; set; }
    
    public SoapResponseEnvelope() {}
}


public class SoapResponseBody
{
    [XmlElement("ShippingLoginResponse", Namespace = "http://www.talend.org/service/")]
    public ShippingLoginResponse? LoginResponse { get; set; }
    public SoapResponseBody() {}
}


public class ShippingLoginResponse
{
    [XmlElement(Namespace = "")]
    public string? SessionCode { get; set; }

    [XmlElement(Namespace = "")]
    public string? SessionMessage { get; set; }
    public ShippingLoginResponse() {}
}