using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using IgpDAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

namespace IgpWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
 
    private readonly IgpDbContext _dbctx;
    private readonly UserManager<IgpUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
 
    private readonly IConfiguration _iconfiguration;
    private readonly   IJwtAuthManager  _jwtAuthManager;
    public TokenController(
              UserManager<IgpUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,

        IgpDbContext ctx,IConfiguration iconfiguration,IJwtAuthManager jwtAuthManager)
    {
        this._dbctx = ctx;
        this._iconfiguration=iconfiguration;
        this._jwtAuthManager = jwtAuthManager;
        _userManager = userManager;
        _roleManager = roleManager;// inverstigate the use of thsis 
        

    }


    // [HttpPost("Authenticate")]
    // public async  Task<ActionResult<int>> Authenticate([FromBody ]ClientRegisterDto user)
    // {
    //    var loggedinuser = await  _jwtAuthManager.FindIgpUser( user,  _userManager);
    //         if (loggedinuser !=null)
    //         {
    //            var result=  await _jwtAuthManager.GenerateToken(loggedinuser);
    //             if( result !=null)  
    //             {
    //                 return Ok(result); 
    //             }
    //         }
    //       return NotFound("User Not Found");
      
    //  }

    [HttpPost("Login")]
    public async  Task<ActionResult<int>> Login([FromBody ] LoginDto user)
    {
       var loggedinuser = await  _jwtAuthManager.FindIgpUser( user,  _userManager);
            if (loggedinuser !=null)
            {
               var result=  await _jwtAuthManager.GenerateToken(loggedinuser);
                if( result !=null)  
                {

                   if (loggedinuser.Usertype==TypeOfUser.CLIENT ){
                    var theclient =  _dbctx.Clients.Where(i => i.email == loggedinuser.Email).FirstOrDefault();
                    if( theclient == null){NotFound("Client  Not Found - Register Again");}
                    
                                    return  Ok(  new CustomReturnType { 
                                code = StatusCodes.Status200OK, 
                                Usertype=loggedinuser.Usertype.ToString(),
                                message = "User succesfully Login",
                                token= result,
                                Username=theclient.Firstname,
                                ImageUrl= theclient.ImageUrl });
                   }
                   else
                   if (loggedinuser.Usertype==TypeOfUser.PROVIDER ){
                    var theclient =  _dbctx.ServiceProviders.Where(i => i.Email == loggedinuser.Email).FirstOrDefault();
                    if( theclient == null){NotFound("Provider  Not Found - Register Again");}
                    
                                    return  Ok(  new CustomReturnType { 
                                code = StatusCodes.Status200OK, 
                                Usertype=loggedinuser.Usertype.ToString(),
                                message = "User succesfully Login",
                                token= result,
                                Username=theclient.Firstname,
                                ImageUrl= theclient.ImageUrl });
                   }

                  //  return Ok(result); 
                }
            }
          return NotFound("User Not Found- Register Again");
      
     }


    [HttpPost("SearchUserByEmail")]
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles="Admin") ]
    public async Task<ActionResult<IdentityUser>> SearchByEmail( LoginDto auser)
    {

        var user = await _jwtAuthManager.FindIgpUser(auser, _userManager);
        if (user != null)
        {
            return Ok(user);
        }

        return BadRequest("User Not Found");
    }




}
