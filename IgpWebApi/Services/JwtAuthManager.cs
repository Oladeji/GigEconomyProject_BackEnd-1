using IgpDAL;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;

public class JwtAuthManager :IJwtAuthManager
{
   private readonly IConfiguration _configuration;
       private readonly UserManager<IgpUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
	public JwtAuthManager(IConfiguration iconfig  ,
            UserManager<IgpUser> userManager,
            RoleManager<IdentityRole> roleManager)

    {
     
        _userManager = userManager;
        _roleManager = roleManager;
        this._configuration= iconfig;
	}



   // public async Task<Tokens> GenerateToken(IgpUser loggedinuser,    UserManager<IgpUser> _userManager,RoleManager<IdentityRole> roleManager)
       public async Task<Tokens> GenerateToken(IgpUser loggedinuser,string tokenownerid, string SkillId)//,    UserManager<IgpUser> _userManager)
   
    {
           // var loggedinuser = await Find(auser, _userManager);

            if (loggedinuser != null )
            {
                var userRoles = await _userManager.GetRolesAsync(loggedinuser);
                //when creating this user , i should have added the roles if i want to use this

                var authClaims = new List<Claim>
                {
                   new Claim("CODE", loggedinuser.UserCode.ToString()),
                   new Claim("NOTE", "I cant still add info from client or provider inside generatetoken"),
                   new Claim( ClaimTypes.Email,loggedinuser.Email),
                  //  new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                  
                   // new Claim( ClaimTypes.MobilePhone,loggedinuser.PhoneNumber),
                   // new Claim( ClaimTypes.MobilePhone,loggedinuser.PhoneNo),
                   // new Claim( ClaimTypes.PostalCode,loggedinuser.PostCode),
                  //  new Claim( ClaimTypes.PostalCode,loggedinuser.p),
                    new Claim( ClaimTypes.Role,loggedinuser.Role.ToString()),
                    new Claim( "USERTYPE",loggedinuser.Usertype.ToString()),
                    new Claim( "EMAIL",loggedinuser.Email),
                    new Claim( "SKILLID",SkillId),
                    new Claim( "TOKENOWNERID",tokenownerid)
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

              return GetToken(authClaims);


            }
            return  null;
    }

  public async  Task<IgpUser> FindIgpUser(LoginDto auser,    UserManager<IgpUser> _userManager)
  {
      
            var loggedinuser = await  _userManager.FindByEmailAsync(auser.Email);
            if (loggedinuser != null && await _userManager.CheckPasswordAsync(loggedinuser, auser.Password))
            {
            return loggedinuser ;
            } return null;

  }





   private Tokens GetToken(List<Claim> authClaims)
   {

       try {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:JwtKey"]));

            var token = new JwtSecurityToken(
                issuer:_configuration["Jwt:JwtIssuer"],
                audience: _configuration["Jwt:JwtAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );
            return new Tokens { Token =new JwtSecurityTokenHandler().WriteToken(token)   };
          } 
          catch (Exception ex)
          {
              throw ex; 
          }


    }


}

