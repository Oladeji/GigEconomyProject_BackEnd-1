using IgpDAL;
using IgpWebApi.Libs;

public interface IIntentionBoardManager{

    Task<IntentionBoard> Find(int ProviderId  , int Jobid);
    Task<int> Create(int ProviderId, int jobId);

    Task<int> Delete(int IntentionBoardId );

   Task<List<IgpDAL.ServiceProvider>> ViewIntentionsForAJob(int  JobId);
  //  List<JobDto2> ChangeToDto2(NetTopologySuite.Geometries.Point location, List<JobBoard> result);
}