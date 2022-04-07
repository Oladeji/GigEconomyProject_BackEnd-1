
namespace IgpDAL
{
public class FinancedProject
{
  public int FinancedProjectId {get; set;}
  public int JobBoardId {get; set;}
  public int FinanceHouseId  {get; set;}
  public float FinanceAmount  {get; set;}
  public string FinanceState  {get; set;}
  public string Comment  {get; set;}
  public DateTimeOffset  ApprovalDate {get; set;}
  public DateTimeOffset  RequestDate {get; set;}

}
}