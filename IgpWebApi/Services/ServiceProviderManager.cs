using IgpDAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

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

    public object ChangeToProviderRegisterDto2(Point location, List<IgpDAL.ServiceProvider> result)
    {





        var data = new List<ProviderRegisterDto2>();
        foreach (var res in result)
        {
            var distance = CalculateDistance.getdistance(location, res.Location);
            var idanddesc= res.Country.Split('£');
            data.Add(new ProviderRegisterDto2()
            {


                SkillTypeId = res.SkillTypeId,
                Surname = res.Surname,
                Middlename = res.Middlename,
                Firstname = res.Firstname,
                Petname = res.Petname,
                MissionStatement = res.MissionStatement,
                Email = res.Email,
                PhoneNo = res.PhoneNo,
                AlternatePhoneNo = res.AlternatePhoneNo,
                ImageUrl = res.ImageUrl,
                mylonx = location.Coordinate.X,
                mylaty = location.Coordinate.Y,
                joblonx = res.Location.Coordinate.X,
                joblaty = res.Location.Coordinate.Y,
                distancetojoblocation = distance,
                PostCode = res.PostCode,
                HouseNo = res.HouseNo,
                City = res.City,
                Country = res.Country,
                JobId=  idanddesc[0],
                Jobdescription=  idanddesc[1]

            });
        }
        return data;






    }

    public async Task<IgpDAL.ServiceProvider> CreateAsync(ProviderRegisterDto user)
    {
        try
        {
            var loc = new NetTopologySuite.Geometries.Point(user.Location.lonx, user.Location.laty) { SRID = 4326 };
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
                ImageUrl = user.ImageUrl,
                Location = loc,
                SkillTypeId = user.SkillTypeId


            });
            // await _dbctx.SaveChangesAsync();
            // var  loggedinuser = await  _userManager.FindByEmailAsync(user.Email); //await FindAsync(user);
            //      await _userManager.AddToRoleAsync(loggedinuser,UserRoles.ServiceProvider);
            await _dbctx.SaveChangesAsync();
            return await FindAsync(user); ;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString() + " Add Client ");

        }

    }

    public async Task<IgpDAL.ServiceProvider> FindAsync(ProviderRegisterDto user)
    {
        var client = await _dbctx.ServiceProviders.Where(i => i.Email == user.Email).FirstOrDefaultAsync();
        return client;
    }


    public async Task<CustomReturnType> RegisterProvider(ProviderRegisterDto model)
    {

        var serviceprovider = await FindAsync(model);
        if (serviceprovider == null)
        {
            model.ImageUrl = await FileHelper.UploadImage(model.Image);

            serviceprovider = await CreateAsync(model);
            if (serviceprovider == null)
                return new CustomReturnType
                {
                    code = StatusCodes.Status500InternalServerError,

                    message = "Problem Creating Sevice Provider"

                };

        }

        var userExists = await _userManager.FindByEmailAsync(model.Email);
        if (userExists != null) return new CustomReturnType
        {
            code = StatusCodes.Status500InternalServerError,
            message = "User already exists!",
            Username = model.Email
        };

        IgpUser user = new()
        {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Email,
            UserCode = serviceprovider.ServiceProviderId,
            Usertype = TypeOfUser.PROVIDER,
            ServiceProvider = serviceprovider


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
        await _userManager.AddToRoleAsync(user, UserRoles.ServiceProvider);
        var tokenresult = await _jwtAuthManager.GenerateToken(user, serviceprovider.ServiceProviderId.ToString(), serviceprovider.SkillTypeId.ToString());
        if (tokenresult == null) { throw new Exception(" Problem generation Token"); }



        return new CustomReturnType
        {
            code = StatusCodes.Status201Created,
            token = tokenresult,
            message = "Provider created successfully,Please Provide Details!",
            Usertype = TypeOfUser.PROVIDER.ToString(),
            Username = model.Email,
            ImageUrl = model.ImageUrl
        };
    }

    public async Task<List<IgpDAL.ServiceProvider>> ViewallProvidersthatShowedIntension4aCustomer(string clientId)
    {
        var Allintention = await _dbctx.IntentionBoards.Where(p => p.ClientId == int.Parse(clientId)).ToListAsync();
        var listofprovider = new List<IgpDAL.ServiceProvider>();

        foreach (var intension in Allintention)
        {
             var jobdesc = await _dbctx.JobBoards.Where(p => p.JobBoardId == intension.JobId).ToListAsync();
             if(jobdesc ==null)
             {
                continue;
             } 
             var jobdescription= jobdesc[0].JobDescription;
            var allprovider = await _dbctx.ServiceProviders.Where(a => a.ServiceProviderId == intension.ProviderId).ToListAsync();
            if (allprovider != null)
            {
                    foreach (var provider in allprovider)
                    {
                    provider.Country= intension.JobId.ToString()+"£"+jobdescription;
                    
                    listofprovider.Add(provider);
                }
            }
        }
        return listofprovider;
    }

    public async Task<List<IgpDAL.ServiceProvider>> ViewIntentionsForAJob(int JobId) //This limits ViewallProvidersthatShowedIntension4aCustomer to a particular job
    {
        var Allintention = await _dbctx.IntentionBoards.Where(p => p.JobId == JobId).ToListAsync();
        var listofprovider = new List<IgpDAL.ServiceProvider>();

        foreach (var intension in Allintention)
        {
            var prov = await _dbctx.ServiceProviders.Where(a => a.ServiceProviderId == intension.ProviderId).ToListAsync();
            if (prov != null)
            {
                listofprovider.AddRange(prov);
            }
        }
        return listofprovider;
    }

}
