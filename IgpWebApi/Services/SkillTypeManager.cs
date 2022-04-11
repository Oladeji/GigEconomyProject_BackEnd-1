using IgpDAL;

using Microsoft.EntityFrameworkCore;
public class SkillTypeManager : ISkillTypeManager
{


    private readonly IgpDbContext _dbctx;

    // private readonly IJwtAuthManager _jwtAuthManager;

    public SkillTypeManager(

            IgpDbContext ctx
          //  IJwtAuthManager jwtAuthManager

            )

    {

        _dbctx = ctx;
        //  _jwtAuthManager = jwtAuthManager;
        //   _userManager = userManager;
    }

    public async Task<SkillType> CreateAsync(SkillType model)
    {
        try
        {
            await _dbctx.SkillTypes.AddAsync(new SkillType
            {
                Description = model.Description,
                Requirement = model.Requirement,
                Certifications = model.Certifications

            });
            await _dbctx.SaveChangesAsync();
            return await FindAsync(model);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message.ToString() + " Add Sync ");

        }
    }

    public async Task<SkillType> FindAsync(SkillType model)
    {
      var client = await _dbctx.SkillTypes.Where(i => i.SkillTypeId == model.SkillTypeId).FirstOrDefaultAsync();
        return client;
    }

    public Task<CustomReturnType> RegisterSkill(SkillType model)
    {
        throw new NotImplementedException("This may not be needed");
    }
}