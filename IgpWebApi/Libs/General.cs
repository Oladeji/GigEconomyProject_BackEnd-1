using System.Security.Claims;
using IgpDAL;

public static class General
{
    
    public static  Client GetCurrentUser(ClaimsIdentity identity)
        { 
            if (identity.IsAuthenticated != null)
            {
                var userClaims = identity.Claims;
                //var allid= userClaims.Where(c =>c.Type=="CODE").ToList();
                // var allemail= userClaims.Where(c =>c.Type=="NOTE").ToList();

                return new Client
                {
                    
                    ClientId = int.Parse(userClaims.FirstOrDefault(o => o.Type == "CODE").Value),
                    email= userClaims.FirstOrDefault(o => o.Type == ClaimTypes.Email)?.Value
                    //ClientId = int.Parse(allid[0].Value),
                    //email =allemail[0].Value,

                };
            }
            return null;
        }

}
