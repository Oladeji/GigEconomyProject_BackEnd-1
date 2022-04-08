using IgpDAL;

public interface IJobManager{

    Task<JobBoard> Find(Client user , JobBoard job);
    Task<int> Create(Client user , AJob job);

    Task<int> Delete(JobBoard job);


    
}