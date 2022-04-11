using IgpDAL;

public interface ISkillTypeManager
{
    Task<SkillType> FindAsync(SkillType model );
    Task<SkillType> CreateAsync(SkillType model );
    Task<CustomReturnType> RegisterSkill(SkillType model);
}