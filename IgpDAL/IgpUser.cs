using Microsoft.AspNetCore.Identity;
namespace IgpDAL;

public class IgpUser:IdentityUser
{
    public  string Petname {get;set;} =string.Empty;
    public  int  UserCode {get;set;} =0;
    public  string  Usertype {get;set;} ="CLIENT";// OR PROVIDER
    public  string  Role {get;set;} ="Client";// OR PROVIDER

   public Client? Client { get; set; }
   public ServiceProvider? ServiceProvider { get; set; }
    
}