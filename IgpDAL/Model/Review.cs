
namespace IgpDAL
{
public class Review
{
  public int ReviewId {get;set;}
  public int  ProviderId {get;set;}
  public int JobBoardId {get;set;}
  public int ClientId {get; set;}
  // public int IgpUserId { get; set; }
  public string Rating {get;set;}
  public  string Comment {get;set;}
  public   DateTimeOffset  ReviewDate {get;set;}= DateTimeOffset.Now;
}
}