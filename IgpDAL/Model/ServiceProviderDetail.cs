using NetTopologySuite.Geometries;


namespace IgpDAL
{
public class  ServiceProviderDetail
{
  public int ServiceProviderDetailId {get; set;}
  public int  ProviderId {set;get;}
  public string Petname {set;get;}
  public string MissionStatement {set;get;}
  public string Email {set;get;}
  public string PhoneNo {set;get;}
  public string AlternatePhoneNo {set;get;}
  public string PostCode {set;get;}
  public string HouseNo {set;get;}
  public string Address {set;get;}
  public string City {set;get;}
  public string Country {set;get;}
  public string QuoteInvoiceHeader {set;get;} 
  public Point Location { get; set; } 
}
}