using IgpDAL;

public interface IJobManager{

    Task<JobBoard> Find(Client user , JobBoard job,  IgpDbContext dbctx);
    Task<int> Create(Client user , AJob job,  IgpDbContext dbctx);

    Task<int> Delete(JobBoard job,IgpDbContext dbctx);
    
}