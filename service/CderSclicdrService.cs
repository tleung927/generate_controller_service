using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderSclicdrService
    {
        // CderSclicdrs Services
        Task<List<CderSclicdr>> GetCderSclicdrListByValue(int offset, int limit, string val); // GET All CderSclicdrss
        Task<CderSclicdr> GetCderSclicdr(string CderSclicdr_name); // GET Single CderSclicdrs        
        Task<List<CderSclicdr>> GetCderSclicdrList(string CderSclicdr_name); // GET List CderSclicdrs        
        Task<CderSclicdr> AddCderSclicdr(CderSclicdr CderSclicdr); // POST New CderSclicdrs
        Task<CderSclicdr> UpdateCderSclicdr(CderSclicdr CderSclicdr); // PUT CderSclicdrs
        Task<(bool, string)> DeleteCderSclicdr(CderSclicdr CderSclicdr); // DELETE CderSclicdrs
    }

    public class CderSclicdrService : ICderSclicdrService
    {
        private readonly XixsrvContext _db;

        public CderSclicdrService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderSclicdrs

        public async Task<List<CderSclicdr>> GetCderSclicdrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderSclicdrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderSclicdr> GetCderSclicdr(string CderSclicdr_id)
        {
            try
            {
                return await _db.CderSclicdrs.FindAsync(CderSclicdr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderSclicdr>> GetCderSclicdrList(string CderSclicdr_id)
        {
            try
            {
                return await _db.CderSclicdrs
                    .Where(i => i.CderSclicdrId == CderSclicdr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderSclicdr> AddCderSclicdr(CderSclicdr CderSclicdr)
        {
            try
            {
                await _db.CderSclicdrs.AddAsync(CderSclicdr);
                await _db.SaveChangesAsync();
                return await _db.CderSclicdrs.FindAsync(CderSclicdr.CderSclicdrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderSclicdr> UpdateCderSclicdr(CderSclicdr CderSclicdr)
        {
            try
            {
                _db.Entry(CderSclicdr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderSclicdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderSclicdr(CderSclicdr CderSclicdr)
        {
            try
            {
                var dbCderSclicdr = await _db.CderSclicdrs.FindAsync(CderSclicdr.CderSclicdrId);

                if (dbCderSclicdr == null)
                {
                    return (false, "CderSclicdr could not be found");
                }

                _db.CderSclicdrs.Remove(CderSclicdr);
                await _db.SaveChangesAsync();

                return (true, "CderSclicdr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderSclicdrs
    }
}
