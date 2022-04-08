
using NetTopologySuite.Geometries;


namespace IgpDAL
{


public class JobStatus
{

 
 public int JobStatusId  { get ; set ;}
 public int JobBoardId { get ; set ;}
 public int ProviderId { get ; set ;}
 public int ClientId {get; set;}
  // public int IgpUserId { get; set; }
 public string Comment { get ; set ;}= string.Empty;
 public DateTimeOffset PostedDate { get ; set ;}= DateTimeOffset.Now;
 public DateTimeOffset StatedDate { get ; set ;}
 public DateTimeOffset  FinishedDate { get ; set ;} 
public Point Location { get; set; } 

public  Invoice Invoice { get; set; }
public JobState  State{get;set;}= JobState.New;
}

  public enum JobState
    {
        InProgress, 
        Completed_Paid, 
        Completed_NotYetPaid,
        New
    };
}