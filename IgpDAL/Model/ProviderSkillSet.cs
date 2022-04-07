
namespace IgpDAL
{
public class ProviderSkillSet
{
 public int ProviderSkillSetId {set;get;}
  public int ProviderId {set;get;}
  public int  SkillId {set;get;}

  public SkillType SkillType {get;set;}
  public ServiceProvider ServiceProvider {get;set;}
}
}