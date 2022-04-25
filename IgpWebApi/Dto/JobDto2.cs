
using IgpDAL;

public class JobDto2
{
    public string JobDescription { get; set; } = string.Empty;

    public int SkillTypeId { get; set; }
    public float JobinitialBudget { get; set; }
   // public string Image { get; set; }
    public string ImageUrl { get; set; } = string.Empty;

    public double joblonx{ get; set; } 
    public double joblaty { get; set; } 

    public double mylonx{ get; set; } 
    public double mylaty { get; set; } 
    public double distancetojoblocation { get; set; } 
    public  JobState State{get;set;}
    public int JobBoardId { get; set; }
    public int ClientId { get; set; }

    public DateTimeOffset ProposedeffectiveDate { get; set; }
    public DateTimeOffset PostedDate { get; set; }


}










