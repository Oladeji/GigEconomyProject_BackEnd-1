using IgpDAL;
using Microsoft.EntityFrameworkCore;

public class JobManager : IJobManager
{
    private readonly IgpDbContext _dbctx;
    public JobManager(IgpDbContext dbctx)
    {
        _dbctx = dbctx;
    }

    public List<JobDto2> ChangeToDto2( NetTopologySuite.Geometries.Point location,List<JobBoard> result)
    {
        var data = new List<JobDto2>();
        foreach (var res in result)
        {
            var distance = CalculateDistance.getdistance(location,res.Location);
            data.Add( new JobDto2()
            {
                JobDescription = res.JobDescription,

                SkillTypeId = res.SkillTypeId,

                JobinitialBudget = res.JobinitialBudget,

                ImageUrl = res.ImageUrl,
                mylonx = location.Coordinate.X,
                mylaty= location.Coordinate.Y,
                State= res.State,
                joblonx = res.Location.Coordinate.X,
                joblaty= res.Location.Coordinate.Y,
                distancetojoblocation = distance,
                JobBoardId = res.JobBoardId,
                ClientId = res.ClientId,
                ProposedeffectiveDate = res.ProposedeffectiveDate,
                PostedDate = res.PostedDate

            });
    }
    return data;
    }

        public async Task<int> Create(int ClientId, JobDto job)
        {
            try
            {
                //save image to get the image url
                var loc = new NetTopologySuite.Geometries.Point(job.Location.lonx, job.Location.laty) { SRID = 4326 };

                job.ImageUrl = await FileHelper.UploadImage(job.Image);
                await _dbctx.JobBoards.AddAsync(new JobBoard
                {
                    JobDescription = job.JobDescription,
                    SkillTypeId = job.SkillTypeId,
                    ImageUrl = job.ImageUrl,
                    Location = loc,
                    JobinitialBudget = job.JobinitialBudget,

                    //           
                    ClientId = ClientId
                });

                return await _dbctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<int> Delete(JobBoard job)
        {
            if (job.State == JobState.New)
                _dbctx.JobBoards.Remove(job);

            return await _dbctx.SaveChangesAsync();



        }

        public async Task<JobBoard> Find(Client user, JobBoard job)
        {

            // JobBoard? jobBoard = await dbctx.JobBoards.Where(u => (u.ClientId == user.ClientId) && (u.JobBoardId == job.JobBoardId)).FirstOrDefaultAsync();
            // return
            // jobBoard;
            return
            await _dbctx.JobBoards.Where(u => (u.ClientId == user.ClientId) && (u.JobBoardId == job.JobBoardId)).FirstOrDefaultAsync();
        }

        public async Task<List<JobBoard>> ViewJobsWithparticularSkillIdId(string SkillId)
        {
            return
                await _dbctx.JobBoards.Where(u =>
                     (u.SkillTypeId == int.Parse(SkillId)) &&
                    (u.State == JobState.New)
                ).ToListAsync();

        }
    }