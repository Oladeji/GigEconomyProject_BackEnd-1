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


public class ServiceProviderController : XController
{

    private readonly IgpDbContext _dbctx;
    private readonly UserManager<IgpUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    private readonly IConfiguration _iconfiguration;
    private readonly IJwtAuthManager _jwtAuthManager;
    //private readonly IClientManager _clientManager;
    private readonly IServiceProviderManager _serviceProviderManager;
    public ServiceProviderController(
            UserManager<IgpUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration,

           IgpDbContext ctx,
           IConfiguration iconfiguration,
           IJwtAuthManager jwtAuthManager,
           //IClientManager clientManager,
           IServiceProviderManager serviceProviderManager
           )
    {
        this._dbctx = ctx;
        this._iconfiguration = iconfiguration;
        this._jwtAuthManager = jwtAuthManager;

        _userManager = userManager;
        _roleManager = roleManager;
        _serviceProviderManager = serviceProviderManager;


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








    [HttpPost("RegisterProvider")]
    // This must take in several data not just AUSER model
    public async Task<IActionResult> RegisterProvider([FromBody] ProviderRegisterDto model)
    {

        return new OkObjectResult(await _serviceProviderManager.RegisterProvider(model));
    }




}
