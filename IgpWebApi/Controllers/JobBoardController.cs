using System.Security.Claims;
using IgpDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgpWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class JobBoardController : ControllerBase
{
     private readonly IgpDbContext _dbctx;
     private readonly IJobManager _jobManager;
    public JobBoardController(IgpDbContext ctx, IJobManager jobManager)
    {
        _dbctx = ctx;
        _jobManager= jobManager;
    }

    // private Client GetCurrentUser()
    //     {
    //         var identity = HttpContext.User.Identity as ClaimsIdentity;

    //         if (identity != null)
    //         {
    //             var userClaims = identity.Claims;

    //             return new Client
    //             {
                    
    //                // UserCode = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.)?.Value,
    //                 UserName = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Name)?.Value,
    //                 Email = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value,
    //                 PhoneNumber = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.MobilePhone)?.Value,
    //                // Surname = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Surname)?.Value,
    //                 Role = userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Role)?.Value
    //             };
    //         }
    //         return null;
    //     }

  
    [HttpGet("GetAll")]
    public async  Task<ActionResult< IEnumerable<JobBoard>>> Get()
    {  
        

        var jb =  await _dbctx.JobBoards.ToListAsync();
        return jb;
    }

    [HttpGet]
    public   ActionResult< JobBoard> Get(string Id)
    {
        var user =   _dbctx.JobBoards.FindAsync( Id);
        if (user!=null)
        {
            return Ok(user) ;
        }

        return NotFound("Job Not Found with Id");
    }

    [HttpPost("Create")]

    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.Client) ]
    public   async  Task<IActionResult>  Create([FromBody] AJob job)
    {
        var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
        var jb= await  _jobManager.Create(currentuser,job);
                if (jb == -1)
                {
           
                    return StatusCode(StatusCodes.Status500InternalServerError,"Problem Creating User" );
                
                }

       return Ok("Job Created On JobBoard");
    }


    [HttpDelete()]
    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.Client) ]

    public   async  Task<IActionResult>  Delete(string email)
    {
        var user =   _dbctx.Clients.Where( i => i.email==email);
        if (user!=null)
        {
            _dbctx.Remove(user);
           await  _dbctx.SaveChangesAsync();
           return Ok(" User Removed");
        } 

       return Ok("User Does Not Exist");
    }




  [HttpGet("IamClient")]

    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.Client) ]
    public   async  Task<IActionResult>  IamClient()
    {

//var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
       return Ok("Client");
    }

  [HttpGet("IamProvider")]

    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.ServiceProvider) ]
    public   async  Task<IActionResult>  IamProvider()
    {


       return Ok("Provider");
    }

}
