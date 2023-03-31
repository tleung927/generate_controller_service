using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderBackupSclicdrService
    {
        // CderBackupSclicdrs Services
        Task<List<CderBackupSclicdr>> GetCderBackupSclicdrListByValue(int offset, int limit, string val); // GET All CderBackupSclicdrss
        Task<CderBackupSclicdr> GetCderBackupSclicdr(string CderBackupSclicdr_name); // GET Single CderBackupSclicdrs        
        Task<List<CderBackupSclicdr>> GetCderBackupSclicdrList(string CderBackupSclicdr_name); // GET List CderBackupSclicdrs        
        Task<CderBackupSclicdr> AddCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr); // POST New CderBackupSclicdrs
        Task<CderBackupSclicdr> UpdateCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr); // PUT CderBackupSclicdrs
        Task<(bool, string)> DeleteCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr); // DELETE CderBackupSclicdrs
    }

    public class CderBackupSclicdrService : ICderBackupSclicdrService
    {
        private readonly XixsrvContext _db;

        public CderBackupSclicdrService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderBackupSclicdrs

        public async Task<List<CderBackupSclicdr>> GetCderBackupSclicdrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderBackupSclicdrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderBackupSclicdr> GetCderBackupSclicdr(string CderBackupSclicdr_id)
        {
            try
            {
                return await _db.CderBackupSclicdrs.FindAsync(CderBackupSclicdr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderBackupSclicdr>> GetCderBackupSclicdrList(string CderBackupSclicdr_id)
        {
            try
            {
                return await _db.CderBackupSclicdrs
                    .Where(i => i.CderBackupSclicdrId == CderBackupSclicdr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderBackupSclicdr> AddCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr)
        {
            try
            {
                await _db.CderBackupSclicdrs.AddAsync(CderBackupSclicdr);
                await _db.SaveChangesAsync();
                return await _db.CderBackupSclicdrs.FindAsync(CderBackupSclicdr.CderBackupSclicdrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderBackupSclicdr> UpdateCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr)
        {
            try
            {
                _db.Entry(CderBackupSclicdr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderBackupSclicdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderBackupSclicdr(CderBackupSclicdr CderBackupSclicdr)
        {
            try
            {
                var dbCderBackupSclicdr = await _db.CderBackupSclicdrs.FindAsync(CderBackupSclicdr.CderBackupSclicdrId);

                if (dbCderBackupSclicdr == null)
                {
                    return (false, "CderBackupSclicdr could not be found");
                }

                _db.CderBackupSclicdrs.Remove(CderBackupSclicdr);
                await _db.SaveChangesAsync();

                return (true, "CderBackupSclicdr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderBackupSclicdrs
    }
}
