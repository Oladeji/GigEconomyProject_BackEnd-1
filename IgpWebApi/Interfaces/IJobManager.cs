using IgpDAL;

public interface IJobManager{

    Task<JobBoard> Find(Client user , JobBoard job);
    Task<int> Create(int   ClientId, JobDto job);

    Task<int> Delete(JobBoard job);

    Task<List<JobBoard>> ViewJobsWithparticularSkillIdId(string SkillId);
    List<JobDto2> ChangeToDto2(NetTopologySuite.Geometries.Point location, List<JobBoard> result);
     Task<List<JobBoard>> ViewJobsPostedbyAClient(string clientid);
}