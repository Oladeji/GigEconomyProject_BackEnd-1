//.cs
using System.Security.Claims;
using IgpDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgpWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class SkillTypeController : ControllerBase
{
     private readonly IgpDbContext _dbctx;
     private readonly ISkillTypeManager _skillTypeManager;
    public SkillTypeController(IgpDbContext ctx, ISkillTypeManager skillTypeManager)
    {
        _dbctx = ctx;
        _skillTypeManager= skillTypeManager;
    }


  
    [HttpGet("GetAllSkillTypes")]
    public async  Task<ActionResult< IEnumerable<SkillType>>> Get()
    {  
        

        var jb =  await _dbctx.SkillTypes.ToListAsync();
        return jb;
    }

    [HttpGet]
    public   ActionResult< SkillType> Get(string Id)
    {
        var user =   _dbctx.SkillTypes.FindAsync( Id);
        if (user!=null)
        {
            return Ok(user) ;
        }

        return NotFound("SkillTypes Not Found with Id");
    }

    [HttpPost("Create")]

    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.Admin) ]
    public   async  Task<IActionResult>  Create([FromBody] SkillType skill)
    {
        var currentuser=  General.GetCurrentUser((ClaimsIdentity)User.Identity);
        var jb= await  _skillTypeManager.CreateAsync(skill);
                if (jb == null)
                {
           
                    return StatusCode(StatusCodes.Status500InternalServerError,"Problem Creating User" );
                
                }

       return Ok("Skill  Created ");
    }


    [HttpDelete()]
    //[Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme,Roles =UserRoles.Admin) ]

    public   async  Task<IActionResult>  Delete(int id)
    {
        var skill =   _dbctx.SkillTypes.Where( i => i.SkillTypeId==id);
        if (skill!=null)
        {
            _dbctx.Remove(skill);
           await  _dbctx.SaveChangesAsync();
           return Ok(" skill Removed");
        } 

       return Ok("skill Does Not Exist");
    }






}
