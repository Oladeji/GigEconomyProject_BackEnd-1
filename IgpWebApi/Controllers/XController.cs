using System.Security.Claims;
using IgpDAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IgpWebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class XController : ControllerBase
{   
    
     public static IgpUser GetCurrentUser(ClaimsIdentity identity)
    {
       

         if (identity.IsAuthenticated != null)
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
    // public static  Client GetCurrentUser(ClaimsIdentity identity)
    //     { 
    //         if (identity.IsAuthenticated != null)
    //         {
    //             var userClaims = identity.Claims;
    //             //var allid= userClaims.Where(c =>c.Type=="CODE").ToList();
    //             // var allemail= userClaims.Where(c =>c.Type=="NOTE").ToList();

    //             return new Client
    //             {
                    
    //                 ClientId = int.Parse(userClaims.FirstOrDefault(o => o.Type == "CODE").Value),
    //                 email= userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
    //                 //ClientId = int.Parse(allid[0].Value),
    //                 //email =allemail[0].Value,

    //             };
    //         }
    //         return null;
    //     }

        public static string GetAClaim(ClaimsIdentity identity, string theclaimname)
         {
             if (identity.IsAuthenticated != null)
              {
           
                  var userClaims = identity.Claims;  
                  var such=   userClaims.FirstOrDefault(o => o.Type == theclaimname).Value;
                 
                 return such ;  
          
          
            
             } 
        return null;
        }
          

}





