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


public class ClientController : XController
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




    [HttpPost("RegisterUser")]
    public async Task<IActionResult> RegisterUser([FromBody] ClientRegisterDto model)
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
