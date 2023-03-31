using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderBackupService
    {
        // CderBackups Services
        Task<List<CderBackup>> GetCderBackupListByValue(int offset, int limit, string val); // GET All CderBackupss
        Task<CderBackup> GetCderBackup(string CderBackup_name); // GET Single CderBackups        
        Task<List<CderBackup>> GetCderBackupList(string CderBackup_name); // GET List CderBackups        
        Task<CderBackup> AddCderBackup(CderBackup CderBackup); // POST New CderBackups
        Task<CderBackup> UpdateCderBackup(CderBackup CderBackup); // PUT CderBackups
        Task<(bool, string)> DeleteCderBackup(CderBackup CderBackup); // DELETE CderBackups
    }

    public class CderBackupService : ICderBackupService
    {
        private readonly XixsrvContext _db;

        public CderBackupService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderBackups

        public async Task<List<CderBackup>> GetCderBackupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderBackups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderBackup> GetCderBackup(string CderBackup_id)
        {
            try
            {
                return await _db.CderBackups.FindAsync(CderBackup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderBackup>> GetCderBackupList(string CderBackup_id)
        {
            try
            {
                return await _db.CderBackups
                    .Where(i => i.CderBackupId == CderBackup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderBackup> AddCderBackup(CderBackup CderBackup)
        {
            try
            {
                await _db.CderBackups.AddAsync(CderBackup);
                await _db.SaveChangesAsync();
                return await _db.CderBackups.FindAsync(CderBackup.CderBackupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderBackup> UpdateCderBackup(CderBackup CderBackup)
        {
            try
            {
                _db.Entry(CderBackup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderBackup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderBackup(CderBackup CderBackup)
        {
            try
            {
                var dbCderBackup = await _db.CderBackups.FindAsync(CderBackup.CderBackupId);

                if (dbCderBackup == null)
                {
                    return (false, "CderBackup could not be found");
                }

                _db.CderBackups.Remove(CderBackup);
                await _db.SaveChangesAsync();

                return (true, "CderBackup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderBackups
    }
}
