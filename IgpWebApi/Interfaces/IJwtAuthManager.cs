using IgpDAL;
using Microsoft.AspNetCore.Identity;

public interface IJwtAuthManager
{
   // public Task<Tokens>      Authenticate(AUser users,IgpDbContext _dbctx);
   // public  Task<Tokens>      GenerateToken(IgpUser users,    UserManager<IgpUser> _userManager,RoleManager<IdentityRole> roleManager);
    //public  Task<Tokens>      GenerateToken(IgpUser users,    UserManager<IgpUser> _userManager);
    public  Task<Tokens>      GenerateToken(IgpUser users, string tokenownerid,string SkillId);//UserManager should be injected somewhere

    public  Task<IgpUser>     FindIgpUser(LoginDto users,    UserManager<IgpUser> _userManager);


}

