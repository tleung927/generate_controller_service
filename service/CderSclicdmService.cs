using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderSclicdmService
    {
        // CderSclicdms Services
        Task<List<CderSclicdm>> GetCderSclicdmListByValue(int offset, int limit, string val); // GET All CderSclicdmss
        Task<CderSclicdm> GetCderSclicdm(string CderSclicdm_name); // GET Single CderSclicdms        
        Task<List<CderSclicdm>> GetCderSclicdmList(string CderSclicdm_name); // GET List CderSclicdms        
        Task<CderSclicdm> AddCderSclicdm(CderSclicdm CderSclicdm); // POST New CderSclicdms
        Task<CderSclicdm> UpdateCderSclicdm(CderSclicdm CderSclicdm); // PUT CderSclicdms
        Task<(bool, string)> DeleteCderSclicdm(CderSclicdm CderSclicdm); // DELETE CderSclicdms
    }

    public class CderSclicdmService : ICderSclicdmService
    {
        private readonly XixsrvContext _db;

        public CderSclicdmService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderSclicdms

        public async Task<List<CderSclicdm>> GetCderSclicdmListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderSclicdms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderSclicdm> GetCderSclicdm(string CderSclicdm_id)
        {
            try
            {
                return await _db.CderSclicdms.FindAsync(CderSclicdm_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderSclicdm>> GetCderSclicdmList(string CderSclicdm_id)
        {
            try
            {
                return await _db.CderSclicdms
                    .Where(i => i.CderSclicdmId == CderSclicdm_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderSclicdm> AddCderSclicdm(CderSclicdm CderSclicdm)
        {
            try
            {
                await _db.CderSclicdms.AddAsync(CderSclicdm);
                await _db.SaveChangesAsync();
                return await _db.CderSclicdms.FindAsync(CderSclicdm.CderSclicdmId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderSclicdm> UpdateCderSclicdm(CderSclicdm CderSclicdm)
        {
            try
            {
                _db.Entry(CderSclicdm).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderSclicdm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderSclicdm(CderSclicdm CderSclicdm)
        {
            try
            {
                var dbCderSclicdm = await _db.CderSclicdms.FindAsync(CderSclicdm.CderSclicdmId);

                if (dbCderSclicdm == null)
                {
                    return (false, "CderSclicdm could not be found");
                }

                _db.CderSclicdms.Remove(CderSclicdm);
                await _db.SaveChangesAsync();

                return (true, "CderSclicdm got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderSclicdms
    }
}
