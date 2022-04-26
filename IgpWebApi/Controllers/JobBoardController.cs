using System.Security.Claims;
using IgpDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgpWebApi.Controllers;


public class JobBoardController : XController
{
     private readonly IgpDbContext _dbctx;
     private readonly IJobManager _jobManager;
    public JobBoardController(IgpDbContext ctx, IJobManager jobManager)
    {
        _dbctx = ctx;
        _jobManager= jobManager;
    }


  
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
    public   async  Task<IActionResult>  Create([FromBody] JobDto job)
    {
      
                
          //  var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
              var ClientId =GetAClaim((ClaimsIdentity)User.Identity,"TOKENOWNERID");
                var jb= await  _jobManager.Create(int.Parse(ClientId),job);
                 if (jb == -1)
                 {
           
                     return StatusCode(StatusCodes.Status500InternalServerError,"Problem Creating User" );
                
                 }
             
                  

              return new OkObjectResult(new CustomReturnType{ message="Job Successfully Placed  On JobBoard"});
    }

   [HttpGet("ViewJobsWithClientId")]// service providers wants to check jobs that has their skill id on jobboard // they hve paid for
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.Client) ]
    public   async  Task<IActionResult>  ViewJobsWithClientId()
    {
      
                
         //  var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
         //  var SkillId= GetAClaim((ClaimsIdentity)User.Identity,"SKILLID");
           var clientid =GetAClaim( (ClaimsIdentity)User.Identity,"TOKENOWNERID");
          // var theclient = await _dbctx.Clients.FindAsync(clientid);
          var theclient =  _dbctx.Clients.Where(p=> p.ClientId==int.Parse(clientid)).FirstOrDefault();
           var result =await  _jobManager.ViewJobsPostedbyAClient(clientid);

         var finalresult = _jobManager.ChangeToDto2(theclient.Location,result);
       
                
           
            return new OkObjectResult( finalresult);
     }

   [HttpGet("ViewJobsWithSkillId")]// service providers wants to check jobs that has their skill id on jobboard // they hve paid for
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.ServiceProvider) ]
    public   async  Task<IActionResult>  ViewJobsWithSkillId()
    {
      
                
         //  var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
           var SkillId= GetAClaim((ClaimsIdentity)User.Identity,"SKILLID");
           var providerId =GetAClaim( (ClaimsIdentity)User.Identity,"TOKENOWNERID");
         //to get provider location
           var provider =  _dbctx.ServiceProviders.Where(p=> p.ServiceProviderId==int.Parse(providerId)).FirstOrDefault();
           var result =await  _jobManager.ViewJobsWithparticularSkillIdId(SkillId);

           var finalresult = _jobManager.ChangeToDto2(provider.Location,result);
       
                
           
            return new OkObjectResult( finalresult);
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




   