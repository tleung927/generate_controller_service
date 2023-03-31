using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSprovidUpdateService
    {
        // SandisSprovidUpdates Services
        Task<List<SandisSprovidUpdate>> GetSandisSprovidUpdateListByValue(int offset, int limit, string val); // GET All SandisSprovidUpdatess
        Task<SandisSprovidUpdate> GetSandisSprovidUpdate(string SandisSprovidUpdate_name); // GET Single SandisSprovidUpdates        
        Task<List<SandisSprovidUpdate>> GetSandisSprovidUpdateList(string SandisSprovidUpdate_name); // GET List SandisSprovidUpdates        
        Task<SandisSprovidUpdate> AddSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate); // POST New SandisSprovidUpdates
        Task<SandisSprovidUpdate> UpdateSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate); // PUT SandisSprovidUpdates
        Task<(bool, string)> DeleteSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate); // DELETE SandisSprovidUpdates
    }

    public class SandisSprovidUpdateService : ISandisSprovidUpdateService
    {
        private readonly XixsrvContext _db;

        public SandisSprovidUpdateService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSprovidUpdates

        public async Task<List<SandisSprovidUpdate>> GetSandisSprovidUpdateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSprovidUpdates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSprovidUpdate> GetSandisSprovidUpdate(string SandisSprovidUpdate_id)
        {
            try
            {
                return await _db.SandisSprovidUpdates.FindAsync(SandisSprovidUpdate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSprovidUpdate>> GetSandisSprovidUpdateList(string SandisSprovidUpdate_id)
        {
            try
            {
                return await _db.SandisSprovidUpdates
                    .Where(i => i.SandisSprovidUpdateId == SandisSprovidUpdate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSprovidUpdate> AddSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate)
        {
            try
            {
                await _db.SandisSprovidUpdates.AddAsync(SandisSprovidUpdate);
                await _db.SaveChangesAsync();
                return await _db.SandisSprovidUpdates.FindAsync(SandisSprovidUpdate.SandisSprovidUpdateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSprovidUpdate> UpdateSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate)
        {
            try
            {
                _db.Entry(SandisSprovidUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSprovidUpdate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSprovidUpdate(SandisSprovidUpdate SandisSprovidUpdate)
        {
            try
            {
                var dbSandisSprovidUpdate = await _db.SandisSprovidUpdates.FindAsync(SandisSprovidUpdate.SandisSprovidUpdateId);

                if (dbSandisSprovidUpdate == null)
                {
                    return (false, "SandisSprovidUpdate could not be found");
                }

                _db.SandisSprovidUpdates.Remove(SandisSprovidUpdate);
                await _db.SaveChangesAsync();

                return (true, "SandisSprovidUpdate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSprovidUpdates
    }
}
