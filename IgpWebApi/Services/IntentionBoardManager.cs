using IgpDAL;
using IgpWebApi.Libs;
using Microsoft.EntityFrameworkCore;

public class IntentionBoardManager : IIntentionBoardManager
{
    private readonly IgpDbContext _dbctx;
    public IntentionBoardManager(IgpDbContext dbctx)
    {
        _dbctx = dbctx;
    }

    public async Task<int> Create(int ProviderId, int jobid)

    {
        var job = await _dbctx.JobBoards.Where(p=>p.JobBoardId==jobid).FirstOrDefaultAsync();
               try
              {

                await _dbctx.IntentionBoards.AddAsync(new IntentionBoard
                {

                 JobId = job.JobBoardId,
                 ProviderId =ProviderId,
                 ClientId =job.ClientId,

                 State =JobState.New,
                 PostedDate = DateTimeOffset.Now

                });

                return await _dbctx.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

    }

    public Task<int> Delete(int IntentionBoardId)
    {
        throw new NotImplementedException();
    }

    public Task<IntentionBoard> Find(int ProviderId, int Jobid)
    {
        throw new NotImplementedException();
    }

    public async Task<List<IgpDAL.ServiceProvider>> ViewIntentionsForAJob(int JobId)
    {
        var Allintention= await _dbctx.IntentionBoards.Where(p=>p.JobId==JobId).ToListAsync();
        var listofprovider = new List<IgpDAL.ServiceProvider>();

        foreach(var intension in Allintention)
        {
           var prov = await _dbctx.ServiceProviders.Where(a=>a.ServiceProviderId== intension.ProviderId).ToListAsync();
           if (prov!=null)
           {
             listofprovider.AddRange(prov);
           }
        }
        return listofprovider;
    }
}