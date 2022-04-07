
namespace IgpDAL
{
public class Invoice
{
 public int InvoiceId {get; set;}
 public int JobId {get; set;}
  public int ProviderId {get; set;}
  public float Amount {get;set;}
  public DateTimeOffset submitteddate {get;set;}

  // public virtual JobStatus JobStatus { get; set; }
}
}