using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderBackupSclicdeService
    {
        // CderBackupSclicdes Services
        Task<List<CderBackupSclicde>> GetCderBackupSclicdeListByValue(int offset, int limit, string val); // GET All CderBackupSclicdess
        Task<CderBackupSclicde> GetCderBackupSclicde(string CderBackupSclicde_name); // GET Single CderBackupSclicdes        
        Task<List<CderBackupSclicde>> GetCderBackupSclicdeList(string CderBackupSclicde_name); // GET List CderBackupSclicdes        
        Task<CderBackupSclicde> AddCderBackupSclicde(CderBackupSclicde CderBackupSclicde); // POST New CderBackupSclicdes
        Task<CderBackupSclicde> UpdateCderBackupSclicde(CderBackupSclicde CderBackupSclicde); // PUT CderBackupSclicdes
        Task<(bool, string)> DeleteCderBackupSclicde(CderBackupSclicde CderBackupSclicde); // DELETE CderBackupSclicdes
    }

    public class CderBackupSclicdeService : ICderBackupSclicdeService
    {
        private readonly XixsrvContext _db;

        public CderBackupSclicdeService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderBackupSclicdes

        public async Task<List<CderBackupSclicde>> GetCderBackupSclicdeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderBackupSclicdes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderBackupSclicde> GetCderBackupSclicde(string CderBackupSclicde_id)
        {
            try
            {
                return await _db.CderBackupSclicdes.FindAsync(CderBackupSclicde_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderBackupSclicde>> GetCderBackupSclicdeList(string CderBackupSclicde_id)
        {
            try
            {
                return await _db.CderBackupSclicdes
                    .Where(i => i.CderBackupSclicdeId == CderBackupSclicde_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderBackupSclicde> AddCderBackupSclicde(CderBackupSclicde CderBackupSclicde)
        {
            try
            {
                await _db.CderBackupSclicdes.AddAsync(CderBackupSclicde);
                await _db.SaveChangesAsync();
                return await _db.CderBackupSclicdes.FindAsync(CderBackupSclicde.CderBackupSclicdeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderBackupSclicde> UpdateCderBackupSclicde(CderBackupSclicde CderBackupSclicde)
        {
            try
            {
                _db.Entry(CderBackupSclicde).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderBackupSclicde;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderBackupSclicde(CderBackupSclicde CderBackupSclicde)
        {
            try
            {
                var dbCderBackupSclicde = await _db.CderBackupSclicdes.FindAsync(CderBackupSclicde.CderBackupSclicdeId);

                if (dbCderBackupSclicde == null)
                {
                    return (false, "CderBackupSclicde could not be found");
                }

                _db.CderBackupSclicdes.Remove(CderBackupSclicde);
                await _db.SaveChangesAsync();

                return (true, "CderBackupSclicde got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderBackupSclicdes
    }
}
