

using System.Security.Claims;
using IgpDAL;
using IgpWebApi.Dto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgpWebApi.Controllers;


public class IntentionBoardController : XController
{
     private readonly IgpDbContext _dbctx;
     private readonly IIntentionBoardManager _intentionBoardManager;
    public IntentionBoardController(IgpDbContext ctx, IIntentionBoardManager intentionBoardManager)
    {
        _dbctx = ctx;
        _intentionBoardManager= intentionBoardManager;
    }


  
    [HttpGet("GetAll")]
    public async  Task<ActionResult< IEnumerable<IntentionBoard>>> Get()
    {  
        

        var jb =  await _dbctx.IntentionBoards.ToListAsync();
        return jb;
    }

    [HttpGet]
    public   ActionResult< IntentionBoard> Get(string Id)
    {
        var user =   _dbctx.IntentionBoards.FindAsync( Id);
        if (user!=null)
        {
            return Ok(user) ;
        }

        return NotFound("Job Not Found with Id");
    }



   [HttpPost("Create")]// This is an expression of interest in a Job by a Service Provider
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.ServiceProvider) ]
    public   async  Task<IActionResult>  Create([FromBody] JobDto3 job)
    {
      
                
          //  var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
              var ClientId =GetAClaim((ClaimsIdentity)User.Identity,"TOKENOWNERID");
                var jb= await  _intentionBoardManager.Create(int.Parse(ClientId),int.Parse(job.JobId));
                 if (jb == -1)
                 {
           
                     return StatusCode(StatusCodes.Status500InternalServerError,"Problem Creating User" );
                
                 }
             
                  

              return new OkObjectResult(new CustomReturnType{ message="Job Successfully Placed  On JobBoard"});
    }

   [HttpGet("ViewJobs")]// service providers wants to check jobs that has their skill id on jobboard // they hve paid for
   [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.ServiceProvider) ]
    public   async  Task<IActionResult>  ViewJobs()
    {
      return Ok();
                
        //  //  var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
        //    var SkillId= GetAClaim((ClaimsIdentity)User.Identity,"SKILLID");
        //    var providerId =GetAClaim( (ClaimsIdentity)User.Identity,"TOKENOWNERID");
        //  //  var provider = await _dbctx.ServiceProviders.FindAsync(providerId);
        //    var provider =  _dbctx.ServiceProviders.Where(p=> p.ServiceProviderId==int.Parse(providerId)).FirstOrDefault();
        //    var result =await  _jobManager.ViewJobsWithparticularSkillIdId(SkillId);

        //    var finalresult = _jobManager.ChangeToDto2(provider.Location,result);
       
                
           
        //     return new OkObjectResult( finalresult);
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





}







