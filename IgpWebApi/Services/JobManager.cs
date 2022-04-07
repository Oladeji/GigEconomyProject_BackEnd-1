using IgpDAL;
using Microsoft.EntityFrameworkCore;

public class JobManager : IJobManager
{
    public async Task<int> Create(Client user, AJob job, IgpDbContext dbctx)
    {
        await dbctx.JobBoards.AddAsync(new JobBoard{
             JobDescription=job.JobDescription,
             ClientId = user.ClientId
        });

       return await dbctx.SaveChangesAsync();

        
    }

    public async Task<int> Delete(JobBoard job,IgpDbContext dbctx)
    {
           if(job.JobState=="New")
              dbctx.JobBoards.Remove(job);
           
           return  await dbctx.SaveChangesAsync();



    }

    public async Task<JobBoard> Find(Client user, JobBoard job, IgpDbContext dbctx)
    {
        return 
        await dbctx.JobBoards.Where(u=> (u.ClientId==user.ClientId) && (u.JobBoardId== job.JobBoardId)).FirstOrDefaultAsync();
    }
}