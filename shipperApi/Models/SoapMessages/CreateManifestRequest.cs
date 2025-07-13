using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System;

namespace shipperApi.Models;



[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapManifestEnvelope
{
    [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapManifestBody? Body { get; set; }

    public SoapManifestEnvelope() { }
}

public class SoapManifestBody
{
    
    [XmlAnyElement]
    public XmlElement? Any { get; set; }

    public SoapManifestBody() { }
}


[XmlType(Namespace = "http://www.talend.org/service/")]
public class CreateManifestRequest
{
    [XmlElement("user",Namespace="")]
    public string? User { get; set; }

    [XmlElement("sessionCode",Namespace="")]
    public string? SessionCode { get; set; }

    [XmlElement("manifestdetails",Namespace="")]
    public List<ManifestDetail> ManifestDetails { get; set; }

    [XmlElement("manifestSendDate",Namespace="")]
    public string? ManifestSendDate { get; set; }
    
    [XmlElement("labelFormat",Namespace="")]
    public string? LabelFormat { get; set; }

    [XmlElement("customerAccount",Namespace="")]
    public string? CustomerAccount { get; set; }

    [XmlElement("senderPoint",Namespace="")]
    public string? SenderPoint { get; set; }

    public CreateManifestRequest()
    {
        ManifestDetails = new List<ManifestDetail>();
    }
}


public class ManifestDetail
{
    [XmlElement("customerReference",Namespace="")]
    public string? CustomerReference { get; set; }
    
    [XmlElement("trackingNumber",Namespace="")]
    public string? TrackingNumber { get; set; }

    public ManifestDetail() { }
}