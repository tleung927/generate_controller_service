using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderBackupSclicdmService
    {
        // CderBackupSclicdms Services
        Task<List<CderBackupSclicdm>> GetCderBackupSclicdmListByValue(int offset, int limit, string val); // GET All CderBackupSclicdmss
        Task<CderBackupSclicdm> GetCderBackupSclicdm(string CderBackupSclicdm_name); // GET Single CderBackupSclicdms        
        Task<List<CderBackupSclicdm>> GetCderBackupSclicdmList(string CderBackupSclicdm_name); // GET List CderBackupSclicdms        
        Task<CderBackupSclicdm> AddCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm); // POST New CderBackupSclicdms
        Task<CderBackupSclicdm> UpdateCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm); // PUT CderBackupSclicdms
        Task<(bool, string)> DeleteCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm); // DELETE CderBackupSclicdms
    }

    public class CderBackupSclicdmService : ICderBackupSclicdmService
    {
        private readonly XixsrvContext _db;

        public CderBackupSclicdmService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderBackupSclicdms

        public async Task<List<CderBackupSclicdm>> GetCderBackupSclicdmListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderBackupSclicdms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderBackupSclicdm> GetCderBackupSclicdm(string CderBackupSclicdm_id)
        {
            try
            {
                return await _db.CderBackupSclicdms.FindAsync(CderBackupSclicdm_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderBackupSclicdm>> GetCderBackupSclicdmList(string CderBackupSclicdm_id)
        {
            try
            {
                return await _db.CderBackupSclicdms
                    .Where(i => i.CderBackupSclicdmId == CderBackupSclicdm_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderBackupSclicdm> AddCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm)
        {
            try
            {
                await _db.CderBackupSclicdms.AddAsync(CderBackupSclicdm);
                await _db.SaveChangesAsync();
                return await _db.CderBackupSclicdms.FindAsync(CderBackupSclicdm.CderBackupSclicdmId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderBackupSclicdm> UpdateCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm)
        {
            try
            {
                _db.Entry(CderBackupSclicdm).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderBackupSclicdm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderBackupSclicdm(CderBackupSclicdm CderBackupSclicdm)
        {
            try
            {
                var dbCderBackupSclicdm = await _db.CderBackupSclicdms.FindAsync(CderBackupSclicdm.CderBackupSclicdmId);

                if (dbCderBackupSclicdm == null)
                {
                    return (false, "CderBackupSclicdm could not be found");
                }

                _db.CderBackupSclicdms.Remove(CderBackupSclicdm);
                await _db.SaveChangesAsync();

                return (true, "CderBackupSclicdm got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderBackupSclicdms
    }
}
