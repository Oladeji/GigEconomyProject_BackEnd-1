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


    [HttpPost("Authenticate")]
    public async  Task<ActionResult<int>> Authenticate([FromBody ]ClientRegisterDto user)
    {
       var loggedinuser = await  _jwtAuthManager.FindIgpUser( user,  _userManager);
            if (loggedinuser !=null)
            {
               var result=  await _jwtAuthManager.GenerateToken(loggedinuser);
                if( result !=null)  
                {
                    return Ok(result); 
                }
            }
          return NotFound("User Not Found");
      
     }



}
