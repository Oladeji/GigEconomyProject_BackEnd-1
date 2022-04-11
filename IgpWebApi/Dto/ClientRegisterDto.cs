using NetTopologySuite.Geometries;

public class ClientRegisterDto//: ILoginDto

{
    public string Email { get; set; }
	public string Password { get; set; }
  public string Surname { get; set; }= string.Empty;
  public string Middlename  { get; set; }= string.Empty;
  public string Firstname { get; set; }= string.Empty;

  public string PhoneNo { get; set; }= string.Empty;
  public string PostCode  { get; set; }= string.Empty;
  public string HouseNo  { get; set; }= string.Empty;
  public string Address { get; set; }= string.Empty;
  public string City { get; set; }= string.Empty;
  public string Country { get; set; }= string.Empty;
  public IFormFile Image { get; set; }
  public string ImageUrl  { get; set; } = string.Empty;
  public GeolocationDto Location { get; set; }
   }

