using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderBackupSclisupService
    {
        // CderBackupSclisups Services
        Task<List<CderBackupSclisup>> GetCderBackupSclisupListByValue(int offset, int limit, string val); // GET All CderBackupSclisupss
        Task<CderBackupSclisup> GetCderBackupSclisup(string CderBackupSclisup_name); // GET Single CderBackupSclisups        
        Task<List<CderBackupSclisup>> GetCderBackupSclisupList(string CderBackupSclisup_name); // GET List CderBackupSclisups        
        Task<CderBackupSclisup> AddCderBackupSclisup(CderBackupSclisup CderBackupSclisup); // POST New CderBackupSclisups
        Task<CderBackupSclisup> UpdateCderBackupSclisup(CderBackupSclisup CderBackupSclisup); // PUT CderBackupSclisups
        Task<(bool, string)> DeleteCderBackupSclisup(CderBackupSclisup CderBackupSclisup); // DELETE CderBackupSclisups
    }

    public class CderBackupSclisupService : ICderBackupSclisupService
    {
        private readonly XixsrvContext _db;

        public CderBackupSclisupService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderBackupSclisups

        public async Task<List<CderBackupSclisup>> GetCderBackupSclisupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderBackupSclisups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderBackupSclisup> GetCderBackupSclisup(string CderBackupSclisup_id)
        {
            try
            {
                return await _db.CderBackupSclisups.FindAsync(CderBackupSclisup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderBackupSclisup>> GetCderBackupSclisupList(string CderBackupSclisup_id)
        {
            try
            {
                return await _db.CderBackupSclisups
                    .Where(i => i.CderBackupSclisupId == CderBackupSclisup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderBackupSclisup> AddCderBackupSclisup(CderBackupSclisup CderBackupSclisup)
        {
            try
            {
                await _db.CderBackupSclisups.AddAsync(CderBackupSclisup);
                await _db.SaveChangesAsync();
                return await _db.CderBackupSclisups.FindAsync(CderBackupSclisup.CderBackupSclisupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderBackupSclisup> UpdateCderBackupSclisup(CderBackupSclisup CderBackupSclisup)
        {
            try
            {
                _db.Entry(CderBackupSclisup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderBackupSclisup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderBackupSclisup(CderBackupSclisup CderBackupSclisup)
        {
            try
            {
                var dbCderBackupSclisup = await _db.CderBackupSclisups.FindAsync(CderBackupSclisup.CderBackupSclisupId);

                if (dbCderBackupSclisup == null)
                {
                    return (false, "CderBackupSclisup could not be found");
                }

                _db.CderBackupSclisups.Remove(CderBackupSclisup);
                await _db.SaveChangesAsync();

                return (true, "CderBackupSclisup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderBackupSclisups
    }
}
