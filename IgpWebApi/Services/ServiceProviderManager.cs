using IgpDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class ServiceProviderManager : IServiceProviderManager
{

    private readonly IgpDbContext _dbctx;
    private readonly UserManager<IgpUser> _userManager;
    private readonly IJwtAuthManager _jwtAuthManager;

	public ServiceProviderManager(
            UserManager<IgpUser> userManager,
            IgpDbContext ctx,
            IJwtAuthManager jwtAuthManager
           
            )

    {
     
        _dbctx = ctx;
        _jwtAuthManager = jwtAuthManager;
        _userManager = userManager;
	}





    public async Task<IgpDAL.ServiceProvider> CreateAsync(ProviderRegisterDto user)
    {
        try 
        {
         var loc = new NetTopologySuite.Geometries.Point( user.Location.lonx, user.Location.laty) { SRID = 4326 };
        await _dbctx.ServiceProviders.AddAsync(new IgpDAL.ServiceProvider
        {
            Surname = user.Surname,
            Middlename = user.Middlename,
            Firstname = user.Firstname,
            Petname = user.Petname,
            MissionStatement = user.MissionStatement,
            Email = user.Email,
            PhoneNo = user.PhoneNo,
            AlternatePhoneNo = user.AlternatePhoneNo,
            PostCode = user.PostCode,
            HouseNo = user.HouseNo,
            Address = user.Address,
            City = user.City,
            Country = user.Country,
            ImageUrl=user.ImageUrl,
            Location = loc


        });
        // await _dbctx.SaveChangesAsync();
        // var  loggedinuser = await  _userManager.FindByEmailAsync(user.Email); //await FindAsync(user);
        //      await _userManager.AddToRoleAsync(loggedinuser,UserRoles.ServiceProvider);
        await _dbctx.SaveChangesAsync();
        return    await FindAsync(user); ;
        }catch(Exception ex){
            throw new Exception(ex.Message.ToString()+ " Add Client ");

        }

    }

    public async Task<IgpDAL.ServiceProvider> FindAsync(ProviderRegisterDto user)
    {
              var client = await _dbctx.ServiceProviders.Where(i => i.Email == user.Email).FirstOrDefaultAsync();
        return client;
    }


    public async Task<CustomReturnType> RegisterProvider(ProviderRegisterDto model)
    {
        
        var client = await FindAsync(model);
        if (client == null)
        {   model.ImageUrl= await FileHelper.UploadImage(model.Image );
            
            client = await CreateAsync(model);
            if (client == null)
                return new CustomReturnType
                {
                    code = StatusCodes.Status500InternalServerError,

                    message = "Problem Creating Sevice Provider"

                };

        }

        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null) return new CustomReturnType { 
            code = StatusCodes.Status500InternalServerError,
            message = "User already exists!" ,Username=model.Email
            };

        IgpUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email,
            UserCode = client.ServiceProviderId,
            Usertype = TypeOfUser.PROVIDER


        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var error = string.Empty;
            foreach (var err in result.Errors)
            {
                error += err.Code + " : " + err.Description + "  \n";
            }
            return new CustomReturnType
            {
                code = StatusCodes.Status500InternalServerError,
                message = error + "Service Provider  failed! Please check Providers details and try again."
            };
        }
        await _userManager.AddToRoleAsync(user,UserRoles.ServiceProvider);
        var tokenresult = await _jwtAuthManager.GenerateToken(user);
        if (tokenresult == null) {throw new Exception(" Problem generation Token"); }
       
            
       
        return new CustomReturnType
        {
            code = StatusCodes.Status201Created,
            token=tokenresult,
            message = "Provider created successfully,Please Provide Details!",
            Usertype = TypeOfUser.PROVIDER.ToString(),
            Username = model.Email
        };
    }
}