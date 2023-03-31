using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSclientUpdateService
    {
        // SandisSclientUpdates Services
        Task<List<SandisSclientUpdate>> GetSandisSclientUpdateListByValue(int offset, int limit, string val); // GET All SandisSclientUpdatess
        Task<SandisSclientUpdate> GetSandisSclientUpdate(string SandisSclientUpdate_name); // GET Single SandisSclientUpdates        
        Task<List<SandisSclientUpdate>> GetSandisSclientUpdateList(string SandisSclientUpdate_name); // GET List SandisSclientUpdates        
        Task<SandisSclientUpdate> AddSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate); // POST New SandisSclientUpdates
        Task<SandisSclientUpdate> UpdateSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate); // PUT SandisSclientUpdates
        Task<(bool, string)> DeleteSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate); // DELETE SandisSclientUpdates
    }

    public class SandisSclientUpdateService : ISandisSclientUpdateService
    {
        private readonly XixsrvContext _db;

        public SandisSclientUpdateService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSclientUpdates

        public async Task<List<SandisSclientUpdate>> GetSandisSclientUpdateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSclientUpdates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSclientUpdate> GetSandisSclientUpdate(string SandisSclientUpdate_id)
        {
            try
            {
                return await _db.SandisSclientUpdates.FindAsync(SandisSclientUpdate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSclientUpdate>> GetSandisSclientUpdateList(string SandisSclientUpdate_id)
        {
            try
            {
                return await _db.SandisSclientUpdates
                    .Where(i => i.SandisSclientUpdateId == SandisSclientUpdate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSclientUpdate> AddSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate)
        {
            try
            {
                await _db.SandisSclientUpdates.AddAsync(SandisSclientUpdate);
                await _db.SaveChangesAsync();
                return await _db.SandisSclientUpdates.FindAsync(SandisSclientUpdate.SandisSclientUpdateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSclientUpdate> UpdateSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate)
        {
            try
            {
                _db.Entry(SandisSclientUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSclientUpdate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSclientUpdate(SandisSclientUpdate SandisSclientUpdate)
        {
            try
            {
                var dbSandisSclientUpdate = await _db.SandisSclientUpdates.FindAsync(SandisSclientUpdate.SandisSclientUpdateId);

                if (dbSandisSclientUpdate == null)
                {
                    return (false, "SandisSclientUpdate could not be found");
                }

                _db.SandisSclientUpdates.Remove(SandisSclientUpdate);
                await _db.SaveChangesAsync();

                return (true, "SandisSclientUpdate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSclientUpdates
    }
}
