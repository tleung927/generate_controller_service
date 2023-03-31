using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderModifyService
    {
        // CderModifys Services
        Task<List<CderModify>> GetCderModifyListByValue(int offset, int limit, string val); // GET All CderModifyss
        Task<CderModify> GetCderModify(string CderModify_name); // GET Single CderModifys        
        Task<List<CderModify>> GetCderModifyList(string CderModify_name); // GET List CderModifys        
        Task<CderModify> AddCderModify(CderModify CderModify); // POST New CderModifys
        Task<CderModify> UpdateCderModify(CderModify CderModify); // PUT CderModifys
        Task<(bool, string)> DeleteCderModify(CderModify CderModify); // DELETE CderModifys
    }

    public class CderModifyService : ICderModifyService
    {
        private readonly XixsrvContext _db;

        public CderModifyService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderModifys

        public async Task<List<CderModify>> GetCderModifyListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderModifys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderModify> GetCderModify(string CderModify_id)
        {
            try
            {
                return await _db.CderModifys.FindAsync(CderModify_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderModify>> GetCderModifyList(string CderModify_id)
        {
            try
            {
                return await _db.CderModifys
                    .Where(i => i.CderModifyId == CderModify_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderModify> AddCderModify(CderModify CderModify)
        {
            try
            {
                await _db.CderModifys.AddAsync(CderModify);
                await _db.SaveChangesAsync();
                return await _db.CderModifys.FindAsync(CderModify.CderModifyId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderModify> UpdateCderModify(CderModify CderModify)
        {
            try
            {
                _db.Entry(CderModify).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderModify;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderModify(CderModify CderModify)
        {
            try
            {
                var dbCderModify = await _db.CderModifys.FindAsync(CderModify.CderModifyId);

                if (dbCderModify == null)
                {
                    return (false, "CderModify could not be found");
                }

                _db.CderModifys.Remove(CderModify);
                await _db.SaveChangesAsync();

                return (true, "CderModify got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderModifys
    }
}
