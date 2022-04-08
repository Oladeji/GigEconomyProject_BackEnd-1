using IgpDAL;
using Microsoft.EntityFrameworkCore;

public class JobManager : IJobManager
{
      private readonly IgpDbContext _dbctx;
    public JobManager( IgpDbContext dbctx)
    {
        _dbctx=dbctx;
    }

    public async Task<int> Create(Client user, AJob job)
    {
        await _dbctx.JobBoards.AddAsync(new JobBoard{
             JobDescription=job.JobDescription,
             ClientId = user.ClientId
        });

       return await _dbctx.SaveChangesAsync();

        
    }

    public async Task<int> Delete(JobBoard job)
    {
           if(job.State==JobState.New)
              _dbctx.JobBoards.Remove(job);
           
           return  await _dbctx.SaveChangesAsync();



    }

    public async Task<JobBoard> Find(Client user, JobBoard job)
    {

        // JobBoard? jobBoard = await dbctx.JobBoards.Where(u => (u.ClientId == user.ClientId) && (u.JobBoardId == job.JobBoardId)).FirstOrDefaultAsync();
       // return
       // jobBoard;
        return 
        await _dbctx.JobBoards.Where(u=> (u.ClientId==user.ClientId) && (u.JobBoardId== job.JobBoardId)).FirstOrDefaultAsync();
    }
}