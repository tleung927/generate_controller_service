using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSprotaxUpdateService
    {
        // SandisSprotaxUpdates Services
        Task<List<SandisSprotaxUpdate>> GetSandisSprotaxUpdateListByValue(int offset, int limit, string val); // GET All SandisSprotaxUpdatess
        Task<SandisSprotaxUpdate> GetSandisSprotaxUpdate(string SandisSprotaxUpdate_name); // GET Single SandisSprotaxUpdates        
        Task<List<SandisSprotaxUpdate>> GetSandisSprotaxUpdateList(string SandisSprotaxUpdate_name); // GET List SandisSprotaxUpdates        
        Task<SandisSprotaxUpdate> AddSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate); // POST New SandisSprotaxUpdates
        Task<SandisSprotaxUpdate> UpdateSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate); // PUT SandisSprotaxUpdates
        Task<(bool, string)> DeleteSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate); // DELETE SandisSprotaxUpdates
    }

    public class SandisSprotaxUpdateService : ISandisSprotaxUpdateService
    {
        private readonly XixsrvContext _db;

        public SandisSprotaxUpdateService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSprotaxUpdates

        public async Task<List<SandisSprotaxUpdate>> GetSandisSprotaxUpdateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSprotaxUpdates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSprotaxUpdate> GetSandisSprotaxUpdate(string SandisSprotaxUpdate_id)
        {
            try
            {
                return await _db.SandisSprotaxUpdates.FindAsync(SandisSprotaxUpdate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSprotaxUpdate>> GetSandisSprotaxUpdateList(string SandisSprotaxUpdate_id)
        {
            try
            {
                return await _db.SandisSprotaxUpdates
                    .Where(i => i.SandisSprotaxUpdateId == SandisSprotaxUpdate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSprotaxUpdate> AddSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate)
        {
            try
            {
                await _db.SandisSprotaxUpdates.AddAsync(SandisSprotaxUpdate);
                await _db.SaveChangesAsync();
                return await _db.SandisSprotaxUpdates.FindAsync(SandisSprotaxUpdate.SandisSprotaxUpdateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSprotaxUpdate> UpdateSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate)
        {
            try
            {
                _db.Entry(SandisSprotaxUpdate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSprotaxUpdate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSprotaxUpdate(SandisSprotaxUpdate SandisSprotaxUpdate)
        {
            try
            {
                var dbSandisSprotaxUpdate = await _db.SandisSprotaxUpdates.FindAsync(SandisSprotaxUpdate.SandisSprotaxUpdateId);

                if (dbSandisSprotaxUpdate == null)
                {
                    return (false, "SandisSprotaxUpdate could not be found");
                }

                _db.SandisSprotaxUpdates.Remove(SandisSprotaxUpdate);
                await _db.SaveChangesAsync();

                return (true, "SandisSprotaxUpdate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSprotaxUpdates
    }
}
