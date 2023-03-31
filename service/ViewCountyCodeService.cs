using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewCountyCodeService
    {
        // ViewCountyCodes Services
        Task<List<ViewCountyCode>> GetViewCountyCodeListByValue(int offset, int limit, string val); // GET All ViewCountyCodess
        Task<ViewCountyCode> GetViewCountyCode(string ViewCountyCode_name); // GET Single ViewCountyCodes        
        Task<List<ViewCountyCode>> GetViewCountyCodeList(string ViewCountyCode_name); // GET List ViewCountyCodes        
        Task<ViewCountyCode> AddViewCountyCode(ViewCountyCode ViewCountyCode); // POST New ViewCountyCodes
        Task<ViewCountyCode> UpdateViewCountyCode(ViewCountyCode ViewCountyCode); // PUT ViewCountyCodes
        Task<(bool, string)> DeleteViewCountyCode(ViewCountyCode ViewCountyCode); // DELETE ViewCountyCodes
    }

    public class ViewCountyCodeService : IViewCountyCodeService
    {
        private readonly XixsrvContext _db;

        public ViewCountyCodeService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewCountyCodes

        public async Task<List<ViewCountyCode>> GetViewCountyCodeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewCountyCodes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewCountyCode> GetViewCountyCode(string ViewCountyCode_id)
        {
            try
            {
                return await _db.ViewCountyCodes.FindAsync(ViewCountyCode_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewCountyCode>> GetViewCountyCodeList(string ViewCountyCode_id)
        {
            try
            {
                return await _db.ViewCountyCodes
                    .Where(i => i.ViewCountyCodeId == ViewCountyCode_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewCountyCode> AddViewCountyCode(ViewCountyCode ViewCountyCode)
        {
            try
            {
                await _db.ViewCountyCodes.AddAsync(ViewCountyCode);
                await _db.SaveChangesAsync();
                return await _db.ViewCountyCodes.FindAsync(ViewCountyCode.ViewCountyCodeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewCountyCode> UpdateViewCountyCode(ViewCountyCode ViewCountyCode)
        {
            try
            {
                _db.Entry(ViewCountyCode).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewCountyCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewCountyCode(ViewCountyCode ViewCountyCode)
        {
            try
            {
                var dbViewCountyCode = await _db.ViewCountyCodes.FindAsync(ViewCountyCode.ViewCountyCodeId);

                if (dbViewCountyCode == null)
                {
                    return (false, "ViewCountyCode could not be found");
                }

                _db.ViewCountyCodes.Remove(ViewCountyCode);
                await _db.SaveChangesAsync();

                return (true, "ViewCountyCode got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewCountyCodes
    }
}
