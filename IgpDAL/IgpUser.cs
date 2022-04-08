using Microsoft.AspNetCore.Identity;
namespace IgpDAL;

public class IgpUser:IdentityUser
{
    public  string Petname {get;set;} =string.Empty;
    public  int  UserCode {get;set;} =0;
    public  TypeOfUser  Usertype {get;set;} =TypeOfUser.UNKNOWN;/// OR PROVIDER
    public  TypeOfUser  Role {get;set;} =TypeOfUser.UNKNOWN;// OR PROVIDER

   public Client? Client { get; set; }
   public ServiceProvider? ServiceProvider { get; set; }
    
}