using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILoginPermissionService
    {
        // LoginPermissions Services
        Task<List<LoginPermission>> GetLoginPermissionListByValue(int offset, int limit, string val); // GET All LoginPermissionss
        Task<LoginPermission> GetLoginPermission(string LoginPermission_name); // GET Single LoginPermissions        
        Task<List<LoginPermission>> GetLoginPermissionList(string LoginPermission_name); // GET List LoginPermissions        
        Task<LoginPermission> AddLoginPermission(LoginPermission LoginPermission); // POST New LoginPermissions
        Task<LoginPermission> UpdateLoginPermission(LoginPermission LoginPermission); // PUT LoginPermissions
        Task<(bool, string)> DeleteLoginPermission(LoginPermission LoginPermission); // DELETE LoginPermissions
    }

    public class LoginPermissionService : ILoginPermissionService
    {
        private readonly XixsrvContext _db;

        public LoginPermissionService(XixsrvContext db)
        {
            _db = db;
        }

        #region LoginPermissions

        public async Task<List<LoginPermission>> GetLoginPermissionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.LoginPermissions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LoginPermission> GetLoginPermission(string LoginPermission_id)
        {
            try
            {
                return await _db.LoginPermissions.FindAsync(LoginPermission_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LoginPermission>> GetLoginPermissionList(string LoginPermission_id)
        {
            try
            {
                return await _db.LoginPermissions
                    .Where(i => i.LoginPermissionId == LoginPermission_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<LoginPermission> AddLoginPermission(LoginPermission LoginPermission)
        {
            try
            {
                await _db.LoginPermissions.AddAsync(LoginPermission);
                await _db.SaveChangesAsync();
                return await _db.LoginPermissions.FindAsync(LoginPermission.LoginPermissionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<LoginPermission> UpdateLoginPermission(LoginPermission LoginPermission)
        {
            try
            {
                _db.Entry(LoginPermission).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return LoginPermission;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLoginPermission(LoginPermission LoginPermission)
        {
            try
            {
                var dbLoginPermission = await _db.LoginPermissions.FindAsync(LoginPermission.LoginPermissionId);

                if (dbLoginPermission == null)
                {
                    return (false, "LoginPermission could not be found");
                }

                _db.LoginPermissions.Remove(LoginPermission);
                await _db.SaveChangesAsync();

                return (true, "LoginPermission got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion LoginPermissions
    }
}
