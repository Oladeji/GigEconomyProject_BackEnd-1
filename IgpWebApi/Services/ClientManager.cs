

using IgpDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class Clientmanager : IClientManager
{

 // , IgpDbContext dbctx, UserManager<IgpUser> _userManager, IJwtAuthManager _jwtAuthManager)
 

    private readonly UserManager<IgpUser> _userManager;
     private readonly IgpDbContext _dbctx;

    private readonly IJwtAuthManager _jwtAuthManager;

	public Clientmanager(
            UserManager<IgpUser> userManager,
            IgpDbContext ctx,
            IJwtAuthManager jwtAuthManager
           
            )

    {
     
        _dbctx = ctx;
        _jwtAuthManager = jwtAuthManager;
        _userManager = userManager;
	}




    public async Task<Client> CreateAsync(ClientRegisterDto user)
    {
        
     var loc = new NetTopologySuite.Geometries.Point( user.Location.lonx, user.Location.laty) { SRID = 4326 };
//new NetTopologySuite.Geometries.Point(1.536553, 49.823796) { SRID = 4326 }
        try{
        await _dbctx.Clients.AddAsync(new Client
        {
            email = user.Email,
            Surname = user.Surname,
            Firstname = user.Firstname,
            Middlename= user.Middlename,
            PhoneNo=user.PhoneNo,
            PostCode=user.PostCode,
            HouseNo= user.HouseNo,
            Address=user.Address,
            City=user.City,
            Country=user.Country,
            ImageUrl=user.ImageUrl,
            Location=   loc
           //  
                            
        });

            
        await _dbctx.SaveChangesAsync();
        return    await FindAsync(user); ;
        }catch(Exception ex){
            throw new Exception(ex.Message.ToString()+ " Add Client ");

        }


    }

    public async Task<Client> FindAsync(ClientRegisterDto user)
    {
        var client = await _dbctx.Clients.Where(i => i.email == user.Email).FirstOrDefaultAsync();
        return client;
    }



 //   public async Task<CustomReturnType> RegisterClient(IFormFile iformFileValue,  ClientRegisterDto model, IgpDbContext dbctx, UserManager<IgpUser> _userManager, IJwtAuthManager _jwtAuthManager)
  public async Task<CustomReturnType> RegisterClient( ClientRegisterDto model )

 
    {

        var client = await FindAsync(model);
        if (client == null)
        {   model.ImageUrl= await FileHelper.UploadImage(model.Image );
            
            client = await CreateAsync(model);
            if (client == null)
                return new CustomReturnType
                {
                    code = StatusCodes.Status500InternalServerError,

                    message = "Problem Creating User"

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
            UserCode = client.ClientId,
            Usertype = TypeOfUser.CLIENT,
            Client=client,
            PhoneNumber=client.PhoneNo
            
            
            


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
                message = error + "User creation failed! Please check user details and try again."
            };
        }
   
          // var  loggedinuser = await  _userManager.FindByEmailAsync(user.Email); //await FindAsync(user);
          
          //await _userManager.AddToRoleAsync(loggedinuser,UserRoles.Client);
          await _userManager.AddToRoleAsync(user,UserRoles.Client);
        var tokenresult = await _jwtAuthManager.GenerateToken(user);
        if (tokenresult == null) {throw new Exception(" Problem generation Token"); }
       
            
       
        return new CustomReturnType
        {
            code = StatusCodes.Status201Created,
            token=tokenresult,
            message = "User created successfully!",
            Usertype = TypeOfUser.CLIENT.ToString(),
            Username = model.Email
        };

    }


}