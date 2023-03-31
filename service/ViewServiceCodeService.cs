using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewServiceCodeService
    {
        // ViewServiceCodes Services
        Task<List<ViewServiceCode>> GetViewServiceCodeListByValue(int offset, int limit, string val); // GET All ViewServiceCodess
        Task<ViewServiceCode> GetViewServiceCode(string ViewServiceCode_name); // GET Single ViewServiceCodes        
        Task<List<ViewServiceCode>> GetViewServiceCodeList(string ViewServiceCode_name); // GET List ViewServiceCodes        
        Task<ViewServiceCode> AddViewServiceCode(ViewServiceCode ViewServiceCode); // POST New ViewServiceCodes
        Task<ViewServiceCode> UpdateViewServiceCode(ViewServiceCode ViewServiceCode); // PUT ViewServiceCodes
        Task<(bool, string)> DeleteViewServiceCode(ViewServiceCode ViewServiceCode); // DELETE ViewServiceCodes
    }

    public class ViewServiceCodeService : IViewServiceCodeService
    {
        private readonly XixsrvContext _db;

        public ViewServiceCodeService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewServiceCodes

        public async Task<List<ViewServiceCode>> GetViewServiceCodeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewServiceCodes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewServiceCode> GetViewServiceCode(string ViewServiceCode_id)
        {
            try
            {
                return await _db.ViewServiceCodes.FindAsync(ViewServiceCode_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewServiceCode>> GetViewServiceCodeList(string ViewServiceCode_id)
        {
            try
            {
                return await _db.ViewServiceCodes
                    .Where(i => i.ViewServiceCodeId == ViewServiceCode_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewServiceCode> AddViewServiceCode(ViewServiceCode ViewServiceCode)
        {
            try
            {
                await _db.ViewServiceCodes.AddAsync(ViewServiceCode);
                await _db.SaveChangesAsync();
                return await _db.ViewServiceCodes.FindAsync(ViewServiceCode.ViewServiceCodeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewServiceCode> UpdateViewServiceCode(ViewServiceCode ViewServiceCode)
        {
            try
            {
                _db.Entry(ViewServiceCode).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewServiceCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewServiceCode(ViewServiceCode ViewServiceCode)
        {
            try
            {
                var dbViewServiceCode = await _db.ViewServiceCodes.FindAsync(ViewServiceCode.ViewServiceCodeId);

                if (dbViewServiceCode == null)
                {
                    return (false, "ViewServiceCode could not be found");
                }

                _db.ViewServiceCodes.Remove(ViewServiceCode);
                await _db.SaveChangesAsync();

                return (true, "ViewServiceCode got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewServiceCodes
    }
}
