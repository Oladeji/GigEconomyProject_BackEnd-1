using IgpDAL;
using Microsoft.EntityFrameworkCore;

public class JobManager : IJobManager
{
      private readonly IgpDbContext _dbctx;
    public JobManager( IgpDbContext dbctx)
    {
        _dbctx=dbctx;
    }

    public async Task<int> Create(Client user, JobDto job)
    {
        try{
        //save image to get the image url
        var loc = new NetTopologySuite.Geometries.Point( job.Location.lonx, job.Location.laty) { SRID = 4326 };

         job.ImageUrl= await FileHelper.UploadImage(job.Image );
        await _dbctx.JobBoards.AddAsync(new JobBoard{
             JobDescription=job.JobDescription,
            SkillTypeId = job.SkillTypeId,
            ImageUrl = job.ImageUrl,
            Location =loc,
            JobinitialBudget= job.JobinitialBudget,
             
//           
             ClientId = user.ClientId
        });

       return await _dbctx.SaveChangesAsync();
        }catch(Exception ex)
        {
            throw ex;
        }
        
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