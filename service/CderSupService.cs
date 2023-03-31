using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderSupService
    {
        // CderSups Services
        Task<List<CderSup>> GetCderSupListByValue(int offset, int limit, string val); // GET All CderSupss
        Task<CderSup> GetCderSup(string CderSup_name); // GET Single CderSups        
        Task<List<CderSup>> GetCderSupList(string CderSup_name); // GET List CderSups        
        Task<CderSup> AddCderSup(CderSup CderSup); // POST New CderSups
        Task<CderSup> UpdateCderSup(CderSup CderSup); // PUT CderSups
        Task<(bool, string)> DeleteCderSup(CderSup CderSup); // DELETE CderSups
    }

    public class CderSupService : ICderSupService
    {
        private readonly XixsrvContext _db;

        public CderSupService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderSups

        public async Task<List<CderSup>> GetCderSupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderSups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderSup> GetCderSup(string CderSup_id)
        {
            try
            {
                return await _db.CderSups.FindAsync(CderSup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderSup>> GetCderSupList(string CderSup_id)
        {
            try
            {
                return await _db.CderSups
                    .Where(i => i.CderSupId == CderSup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderSup> AddCderSup(CderSup CderSup)
        {
            try
            {
                await _db.CderSups.AddAsync(CderSup);
                await _db.SaveChangesAsync();
                return await _db.CderSups.FindAsync(CderSup.CderSupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderSup> UpdateCderSup(CderSup CderSup)
        {
            try
            {
                _db.Entry(CderSup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderSup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderSup(CderSup CderSup)
        {
            try
            {
                var dbCderSup = await _db.CderSups.FindAsync(CderSup.CderSupId);

                if (dbCderSup == null)
                {
                    return (false, "CderSup could not be found");
                }

                _db.CderSups.Remove(CderSup);
                await _db.SaveChangesAsync();

                return (true, "CderSup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderSups
    }
}
