using System.Xml; 
using System.Xml.Serialization;
using System.ComponentModel.DataAnnotations;


namespace shipperApi.Models;

[XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
public class SoapExpeditionEnvelope
{
    [XmlElement("Body", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public SoapBody? Body { get; set; }

    public SoapExpeditionEnvelope() { }
}


public class SoapBody
{
    
    [XmlAnyElement]
    public XmlElement? Any { get; set; }

    public SoapBody() {}
}

[XmlType(Namespace = "http://www.talend.org/service/")]
public class CreateExpeditionRequest
{
    [Required(ErrorMessage = "CostumerAccount is mandatory")]
    [XmlElement("customerAccount")]
    public int CustomerAccount { get; set; }

    [XmlElement("departmentCode")]
    public string? DepartmentCode { get; set; }

    [XmlElement("customerReference")]
    public string? CustomerReference { get; set; }

    [XmlElement("senderReference")]
    public string? SenderReference { get; set; }

    [XmlElement("sendDate")]
    public DateTime? SendDate { get; set; }

    [XmlElement("divisionCode")]
    public string? DivisionCode { get; set; }

    [XmlElement("serviceType")]
    public string? ServiceType { get; set; }


    [XmlElement("senderName")]
    public string? SenderName { get; set; }

    [XmlElement("senderAddress")]
    public string? SenderAddress { get; set; }

    [XmlElement("senderPostalCode")]
    public string? SenderPostalCode { get; set; }

    [XmlElement("senderPostalCodeName")]
    public string? SenderPostalCodeName { get; set; }

    [XmlElement("senderCountryCode")]
    public string? SenderCountryCode { get; set; }

    [XmlElement("senderCountryName")]
    public string? SenderCountryName { get; set; }

    [XmlElement("senderFiscalNumber")]
    public string? SenderFiscalNumber { get; set; }

    [XmlElement("senderPhone")]
    public string? SenderPhone { get; set; }

    [XmlElement("senderEmail")]
    public string? SenderEmail { get; set; }

    [XmlElement("senderCode")]
    public string? SenderCode { get; set; }


    [XmlElement("addresseeName")]
    public string? AddresseeName { get; set; }

    [XmlElement("addresseeAddress")]
    public string? AddresseeAddress { get; set; }

    [XmlElement("addresseePostalCode")]
    public string? AddresseePostalCode { get; set; }

    [XmlElement("addresseePostalCodeName")]
    public string? AddresseePostalCodeName { get; set; }

    [XmlElement("addresseeCountryCode")]
    public string? AddresseeCountryCode { get; set; }

    [XmlElement("addresseeCountryName")]
    public string? AddresseeCountryName { get; set; }

    [XmlElement("addresseeFiscalNumber")]
    public string? AddresseeFiscalNumber { get; set; }

    [XmlElement("addresseePhone")]
    public string? AddresseePhone { get; set; }

    [XmlElement("addresseeEmail")]
    public string? AddresseeEmail { get; set; }


    [XmlElement("receiverName")]
    public string? ReceiverName { get; set; }

    [XmlElement("receiverAddress")]
    public string? ReceiverAddress { get; set; }

    [XmlElement("receiverPostalCode")]
    public string? ReceiverPostalCode { get; set; }

    [XmlElement("receiverPostalCodeName")]
    public string? ReceiverPostalCodeName { get; set; }

    [XmlElement("receiverCountryCode")]
    public string? ReceiverCountryCode { get; set; }

    [XmlElement("receiverCountryName")]
    public string? ReceiverCountryName { get; set; }

    [XmlElement("receiverFiscalNumber")]
    public string? ReceiverFiscalNumber { get; set; }

    [XmlElement("receiverPhone")]
    public string? ReceiverPhone { get; set; }

    [XmlElement("receiverEmail")]
    public string? ReceiverEmail { get; set; }


    [XmlElement("requestDeliveryDate")]
    public DateTime? RequestDeliveryDate { get; set; }

    [XmlElement("booking")]
    public string?  Booking { get; set; }

    [XmlElement("largeSurface")]
    public string?  LargeSurface { get; set; }

    [XmlElement("pod")]
    public string?  Pod { get; set; }

    [XmlElement("reimbursementValue")]
    public string?  ReimbursementValue { get; set; }

    [XmlElement("safeValue")]
    public string?  SafeValue { get; set; }

    [XmlElement("comments")]
    public string? Comments { get; set; }


    [XmlElement("sessionCode")]
    public string? SessionCode { get; set; }

    [XmlElement("user")]
    public string? User { get; set; }

    [XmlElement("labelFormat")]
    public string? LabelFormat { get; set; }


    [XmlElement("totalNumberPallets")]
    public int? TotalNumberPallets { get; set; }

    [XmlElement("totalNumberPackages")]
    public int? TotalNumberPackages { get; set; }

    [XmlElement("totalWeight")]
    public string? TotalWeight { get; set; }

    [XmlElement("totalM3")]
    public string?  TotalM3 { get; set; }


    public CreateExpeditionRequest() { }
}