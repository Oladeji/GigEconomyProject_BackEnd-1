namespace IgpDAL
{
public class IntentionBoard
{
public int IntentionBoardId {get; set;}
  public int JobId {get; set;}
  public int ProviderId {get; set;}
  public int ClientId {get; set;}
 //  public int IgpUserId { get; set; }
  public string JobState {get; set;}
  public DateTimeOffset  PostedDate {get; set;}
}
}