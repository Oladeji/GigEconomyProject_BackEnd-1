namespace IgpDAL
{
public class IntentionBoard
{
public int IntentionBoardId {get; set;}
  public int JobId {get; set;}
  public int ProviderId {get; set;}
  public int ClientId {get; set;}
 //  public int IgpUserId { get; set; }
  public  JobState State{get;set;}= JobState.New;  
  public DateTimeOffset  PostedDate {get; set;}
}
}