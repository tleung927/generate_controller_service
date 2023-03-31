using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMwcDevBackupService
    {
        // MwcDevBackups Services
        Task<List<MwcDevBackup>> GetMwcDevBackupListByValue(int offset, int limit, string val); // GET All MwcDevBackupss
        Task<MwcDevBackup> GetMwcDevBackup(string MwcDevBackup_name); // GET Single MwcDevBackups        
        Task<List<MwcDevBackup>> GetMwcDevBackupList(string MwcDevBackup_name); // GET List MwcDevBackups        
        Task<MwcDevBackup> AddMwcDevBackup(MwcDevBackup MwcDevBackup); // POST New MwcDevBackups
        Task<MwcDevBackup> UpdateMwcDevBackup(MwcDevBackup MwcDevBackup); // PUT MwcDevBackups
        Task<(bool, string)> DeleteMwcDevBackup(MwcDevBackup MwcDevBackup); // DELETE MwcDevBackups
    }

    public class MwcDevBackupService : IMwcDevBackupService
    {
        private readonly XixsrvContext _db;

        public MwcDevBackupService(XixsrvContext db)
        {
            _db = db;
        }

        #region MwcDevBackups

        public async Task<List<MwcDevBackup>> GetMwcDevBackupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.MwcDevBackups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MwcDevBackup> GetMwcDevBackup(string MwcDevBackup_id)
        {
            try
            {
                return await _db.MwcDevBackups.FindAsync(MwcDevBackup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<MwcDevBackup>> GetMwcDevBackupList(string MwcDevBackup_id)
        {
            try
            {
                return await _db.MwcDevBackups
                    .Where(i => i.MwcDevBackupId == MwcDevBackup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<MwcDevBackup> AddMwcDevBackup(MwcDevBackup MwcDevBackup)
        {
            try
            {
                await _db.MwcDevBackups.AddAsync(MwcDevBackup);
                await _db.SaveChangesAsync();
                return await _db.MwcDevBackups.FindAsync(MwcDevBackup.MwcDevBackupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<MwcDevBackup> UpdateMwcDevBackup(MwcDevBackup MwcDevBackup)
        {
            try
            {
                _db.Entry(MwcDevBackup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return MwcDevBackup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMwcDevBackup(MwcDevBackup MwcDevBackup)
        {
            try
            {
                var dbMwcDevBackup = await _db.MwcDevBackups.FindAsync(MwcDevBackup.MwcDevBackupId);

                if (dbMwcDevBackup == null)
                {
                    return (false, "MwcDevBackup could not be found");
                }

                _db.MwcDevBackups.Remove(MwcDevBackup);
                await _db.SaveChangesAsync();

                return (true, "MwcDevBackup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion MwcDevBackups
    }
}
