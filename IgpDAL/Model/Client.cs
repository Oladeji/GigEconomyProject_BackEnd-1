using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;

namespace IgpDAL
{
  
public class Client
{
  public int ClientId { get; set; }

  public string Surname { get; set; }= string.Empty;
  public string Middlename  { get; set; }= string.Empty;
  public string Firstname { get; set; }= string.Empty;
  public string email { get; set; }= string.Empty;

	public string password { get; set; }= string.Empty;
  public string PhoneNo { get; set; }= string.Empty;
  public string PostCode  { get; set; }= string.Empty;
  public string HouseNo  { get; set; }= string.Empty;
  public string Address { get; set; }= string.Empty;
  public string City { get; set; }= string.Empty;
  public string Country { get; set; }= string.Empty;
 // public IFormFile Image { get; set; }
  public string ImageUrl  { get; set; }
  public Point Location { get; set; } 
  [JsonIgnore]
  public List<JobBoard>? JobBoards { get; set; } 
}



public enum TypeOfUser 
{
  ADMIN,

  PROVIDER,
  CLIENT,
  UNKNOWN
}
}