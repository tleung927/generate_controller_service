using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IActiveConsumerCmService
    {
        // ActiveConsumerCms Services
        Task<List<ActiveConsumerCm>> GetActiveConsumerCmListByValue(int offset, int limit, string val); // GET All ActiveConsumerCmss
        Task<ActiveConsumerCm> GetActiveConsumerCm(string ActiveConsumerCm_name); // GET Single ActiveConsumerCms        
        Task<List<ActiveConsumerCm>> GetActiveConsumerCmList(string ActiveConsumerCm_name); // GET List ActiveConsumerCms        
        Task<ActiveConsumerCm> AddActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm); // POST New ActiveConsumerCms
        Task<ActiveConsumerCm> UpdateActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm); // PUT ActiveConsumerCms
        Task<(bool, string)> DeleteActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm); // DELETE ActiveConsumerCms
    }

    public class ActiveConsumerCmService : IActiveConsumerCmService
    {
        private readonly XixsrvContext _db;

        public ActiveConsumerCmService(XixsrvContext db)
        {
            _db = db;
        }

        #region ActiveConsumerCms

        public async Task<List<ActiveConsumerCm>> GetActiveConsumerCmListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ActiveConsumerCms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ActiveConsumerCm> GetActiveConsumerCm(string ActiveConsumerCm_id)
        {
            try
            {
                return await _db.ActiveConsumerCms.FindAsync(ActiveConsumerCm_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ActiveConsumerCm>> GetActiveConsumerCmList(string ActiveConsumerCm_id)
        {
            try
            {
                return await _db.ActiveConsumerCms
                    .Where(i => i.ActiveConsumerCmId == ActiveConsumerCm_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ActiveConsumerCm> AddActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm)
        {
            try
            {
                await _db.ActiveConsumerCms.AddAsync(ActiveConsumerCm);
                await _db.SaveChangesAsync();
                return await _db.ActiveConsumerCms.FindAsync(ActiveConsumerCm.ActiveConsumerCmId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ActiveConsumerCm> UpdateActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm)
        {
            try
            {
                _db.Entry(ActiveConsumerCm).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ActiveConsumerCm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteActiveConsumerCm(ActiveConsumerCm ActiveConsumerCm)
        {
            try
            {
                var dbActiveConsumerCm = await _db.ActiveConsumerCms.FindAsync(ActiveConsumerCm.ActiveConsumerCmId);

                if (dbActiveConsumerCm == null)
                {
                    return (false, "ActiveConsumerCm could not be found");
                }

                _db.ActiveConsumerCms.Remove(ActiveConsumerCm);
                await _db.SaveChangesAsync();

                return (true, "ActiveConsumerCm got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ActiveConsumerCms
    }
}
