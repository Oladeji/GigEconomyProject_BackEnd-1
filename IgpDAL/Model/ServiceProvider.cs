using NetTopologySuite.Geometries;


namespace IgpDAL
{
public class  ServiceProvider
{
  public int  ServiceProviderId {set;get;}
//  public int  ProviderId {set;get;}
  public string Surname {set;get;}
  public string Middlename {set;get;}
  public string Firstname {set;get;}
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
  ///
  // we have a dedicated service provide skillset that is supposed to 
  //take care of skill , but due to time constraint
  // I have to assume a provider will have only one skill set
  // And Also A provide will have only one outlet so 
  //service provider details is not implemented for now
  ///
  public int SkillTypeId { get; set; } 
  
public Point Location { get; set; } 

  public string ImageUrl  { get; set; } = string.Empty;

}
}