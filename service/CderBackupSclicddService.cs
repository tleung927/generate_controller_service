using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderBackupSclicddService
    {
        // CderBackupSclicdds Services
        Task<List<CderBackupSclicdd>> GetCderBackupSclicddListByValue(int offset, int limit, string val); // GET All CderBackupSclicddss
        Task<CderBackupSclicdd> GetCderBackupSclicdd(string CderBackupSclicdd_name); // GET Single CderBackupSclicdds        
        Task<List<CderBackupSclicdd>> GetCderBackupSclicddList(string CderBackupSclicdd_name); // GET List CderBackupSclicdds        
        Task<CderBackupSclicdd> AddCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd); // POST New CderBackupSclicdds
        Task<CderBackupSclicdd> UpdateCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd); // PUT CderBackupSclicdds
        Task<(bool, string)> DeleteCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd); // DELETE CderBackupSclicdds
    }

    public class CderBackupSclicddService : ICderBackupSclicddService
    {
        private readonly XixsrvContext _db;

        public CderBackupSclicddService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderBackupSclicdds

        public async Task<List<CderBackupSclicdd>> GetCderBackupSclicddListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderBackupSclicdds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderBackupSclicdd> GetCderBackupSclicdd(string CderBackupSclicdd_id)
        {
            try
            {
                return await _db.CderBackupSclicdds.FindAsync(CderBackupSclicdd_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderBackupSclicdd>> GetCderBackupSclicddList(string CderBackupSclicdd_id)
        {
            try
            {
                return await _db.CderBackupSclicdds
                    .Where(i => i.CderBackupSclicddId == CderBackupSclicdd_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderBackupSclicdd> AddCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd)
        {
            try
            {
                await _db.CderBackupSclicdds.AddAsync(CderBackupSclicdd);
                await _db.SaveChangesAsync();
                return await _db.CderBackupSclicdds.FindAsync(CderBackupSclicdd.CderBackupSclicddId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderBackupSclicdd> UpdateCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd)
        {
            try
            {
                _db.Entry(CderBackupSclicdd).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderBackupSclicdd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderBackupSclicdd(CderBackupSclicdd CderBackupSclicdd)
        {
            try
            {
                var dbCderBackupSclicdd = await _db.CderBackupSclicdds.FindAsync(CderBackupSclicdd.CderBackupSclicddId);

                if (dbCderBackupSclicdd == null)
                {
                    return (false, "CderBackupSclicdd could not be found");
                }

                _db.CderBackupSclicdds.Remove(CderBackupSclicdd);
                await _db.SaveChangesAsync();

                return (true, "CderBackupSclicdd got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderBackupSclicdds
    }
}
