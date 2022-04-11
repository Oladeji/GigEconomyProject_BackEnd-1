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
public class ClientController : ControllerBase
{

    private readonly IgpDbContext _dbctx;
    private readonly UserManager<IgpUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IConfiguration _iconfiguration;
    private readonly IJwtAuthManager _jwtAuthManager;
    private readonly IClientManager _clientManager;
    public ClientController(
            UserManager<IgpUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,

           IgpDbContext ctx,
           IConfiguration iconfiguration,
           IJwtAuthManager jwtAuthManager,
           IClientManager clientManager)
    {
        this._dbctx = ctx;
        this._iconfiguration = iconfiguration;
        this._jwtAuthManager = jwtAuthManager;

        _userManager = userManager;
        _roleManager = roleManager;
        _clientManager = clientManager;


    }


    private IgpUser GetCurrentUser()
    {
        var identity = HttpContext.User.Identity as ClaimsIdentity;

        if (identity != null)
        {
            var userClaims = identity.Claims;

            return new IgpUser
            {
                UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
                Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
                PhoneNumber = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
                //Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
                //Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
            };
        }
        return null;
    }





    // [HttpPost("Search")]
    // //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles="Admin") ]
    // public async Task<ActionResult<IdentityUser>> SearchByEmail( LoginDto auser)
    // {

    //     var user = await _jwtAuthManager.FindIgpUser(auser, _userManager);
    //     if (user != null)
    //     {
    //         return Ok(user);
    //     }

    //     return BadRequest("User Not Found");
    // }



    [HttpPost("RegisterUser")]
    // This must take in several data not just AUSER model
    public async Task<IActionResult> RegisterUser([FromForm] ClientRegisterDto model)
    {

        return new OkObjectResult(await _clientManager.RegisterClient(model));
    }


 

    [HttpGet("SampleGetDistanceBtwPoints")]
    // This must take in several data not just AUSER model
    public  ActionResult GetDistance()
    {
        
        var x = _dbctx.ServiceProviders.Where(i => i.Email == "Akomspatrick7@yahoo.com").First().Location;
        var y = _dbctx.Clients.Where(i => i.email == "akomspatrick2@yahoo.com").First().Location;
        var distanceInMeters = CalculateDistance.getdistance(x, y);
        return Ok(distanceInMeters);
    }


}
