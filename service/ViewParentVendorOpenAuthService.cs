using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewParentVendorOpenAuthService
    {
        // ViewParentVendorOpenAuths Services
        Task<List<ViewParentVendorOpenAuth>> GetViewParentVendorOpenAuthListByValue(int offset, int limit, string val); // GET All ViewParentVendorOpenAuthss
        Task<ViewParentVendorOpenAuth> GetViewParentVendorOpenAuth(string ViewParentVendorOpenAuth_name); // GET Single ViewParentVendorOpenAuths        
        Task<List<ViewParentVendorOpenAuth>> GetViewParentVendorOpenAuthList(string ViewParentVendorOpenAuth_name); // GET List ViewParentVendorOpenAuths        
        Task<ViewParentVendorOpenAuth> AddViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth); // POST New ViewParentVendorOpenAuths
        Task<ViewParentVendorOpenAuth> UpdateViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth); // PUT ViewParentVendorOpenAuths
        Task<(bool, string)> DeleteViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth); // DELETE ViewParentVendorOpenAuths
    }

    public class ViewParentVendorOpenAuthService : IViewParentVendorOpenAuthService
    {
        private readonly XixsrvContext _db;

        public ViewParentVendorOpenAuthService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewParentVendorOpenAuths

        public async Task<List<ViewParentVendorOpenAuth>> GetViewParentVendorOpenAuthListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewParentVendorOpenAuths.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewParentVendorOpenAuth> GetViewParentVendorOpenAuth(string ViewParentVendorOpenAuth_id)
        {
            try
            {
                return await _db.ViewParentVendorOpenAuths.FindAsync(ViewParentVendorOpenAuth_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewParentVendorOpenAuth>> GetViewParentVendorOpenAuthList(string ViewParentVendorOpenAuth_id)
        {
            try
            {
                return await _db.ViewParentVendorOpenAuths
                    .Where(i => i.ViewParentVendorOpenAuthId == ViewParentVendorOpenAuth_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewParentVendorOpenAuth> AddViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth)
        {
            try
            {
                await _db.ViewParentVendorOpenAuths.AddAsync(ViewParentVendorOpenAuth);
                await _db.SaveChangesAsync();
                return await _db.ViewParentVendorOpenAuths.FindAsync(ViewParentVendorOpenAuth.ViewParentVendorOpenAuthId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewParentVendorOpenAuth> UpdateViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth)
        {
            try
            {
                _db.Entry(ViewParentVendorOpenAuth).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewParentVendorOpenAuth;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth)
        {
            try
            {
                var dbViewParentVendorOpenAuth = await _db.ViewParentVendorOpenAuths.FindAsync(ViewParentVendorOpenAuth.ViewParentVendorOpenAuthId);

                if (dbViewParentVendorOpenAuth == null)
                {
                    return (false, "ViewParentVendorOpenAuth could not be found");
                }

                _db.ViewParentVendorOpenAuths.Remove(ViewParentVendorOpenAuth);
                await _db.SaveChangesAsync();

                return (true, "ViewParentVendorOpenAuth got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewParentVendorOpenAuths
    }
}
