public class ProviderRegisterDto

{
    public string Surname { set; get; }
    public string Middlename { set; get; }
    public string Firstname { set; get; }
    public string Petname { set; get; }
    public string MissionStatement { set; get; }
    public string Email { set; get; }
    public string PhoneNo { set; get; }
    public string AlternatePhoneNo { set; get; }
    public string PostCode { set; get; }
    public string HouseNo { set; get; }
    public string Address { set; get; }
    public string City { set; get; }
    public string Country { set; get; }


    public string ImageUrl { get; set; } = string.Empty;
    public string Password { get; set; }

    public string Image { get; set; }
   public int SkillTypeId { get; set; } 
    public GeolocationDto Location { get; set; }





}

