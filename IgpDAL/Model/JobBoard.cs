using NetTopologySuite.Geometries;


namespace IgpDAL
{
public class JobBoard
{
  public int  JobBoardId {set;get;}
  public int ClientId { get; set; }
  public string JobDescription {get;set;} = string.Empty;
 // public Point  JobLocation {get;set;}
  public DateTimeOffset ProposedeffectiveDate {get;set;}= DateTimeOffset.Now;
  public string JobState {get;set;}="New";
  public string FinanceOption  {get;set;} ="None";
  
  public DateTimeOffset PostedDate {get;set;}= DateTimeOffset.Now;
  public float JobinitialBudget {get;set; } =0;

  public Client Client {get;set;}


  public SkillType? SkillType { get; set; }  

}
}