using System.Xml; 
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;

[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapEnvelope
{
    [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapBody? Body { get; set; }

    public SoapEnvelope() {}
}

public class SoapBody
{
    //[XmlElement("ShippingLoginSession", Namespace = "http://www.talend.org/service/")]
    [XmlAnyElement]
    public XmlElement? Any { get; set; }

    public SoapBody() {}
}


[XmlType(Namespace = "http://www.talend.org/service/")]
public class ShippingSessionLogin
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Account its mandatory")]
    [XmlElement("Account", IsNullable = false)]
    public string? Account { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "User its mandatory")]
    [XmlElement("User", IsNullable = false)]
    public string? User { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Password its mandatory")]
    [XmlElement("Password", IsNullable = false)]
    public string? Password { get; set; }

    public ShippingSessionLogin() {}
}