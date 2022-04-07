
namespace IgpDAL
{
public class Quote
{
 public int QuoteId {get; set;}
 public int JobBoardId {get; set;}
  public int ProviderId {get; set;}
  public float Amount {get;set;}
  public DateTimeOffset submitteddate {get;set;}
}
}