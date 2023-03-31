using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILoginService
    {
        // Logins Services
        Task<List<Login>> GetLoginListByValue(int offset, int limit, string val); // GET All Loginss
        Task<Login> GetLogin(string Login_name); // GET Single Logins        
        Task<List<Login>> GetLoginList(string Login_name); // GET List Logins        
        Task<Login> AddLogin(Login Login); // POST New Logins
        Task<Login> UpdateLogin(Login Login); // PUT Logins
        Task<(bool, string)> DeleteLogin(Login Login); // DELETE Logins
    }

    public class LoginService : ILoginService
    {
        private readonly XixsrvContext _db;

        public LoginService(XixsrvContext db)
        {
            _db = db;
        }

        #region Logins

        public async Task<List<Login>> GetLoginListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Logins.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Login> GetLogin(string Login_id)
        {
            try
            {
                return await _db.Logins.FindAsync(Login_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Login>> GetLoginList(string Login_id)
        {
            try
            {
                return await _db.Logins
                    .Where(i => i.LoginId == Login_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Login> AddLogin(Login Login)
        {
            try
            {
                await _db.Logins.AddAsync(Login);
                await _db.SaveChangesAsync();
                return await _db.Logins.FindAsync(Login.LoginId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Login> UpdateLogin(Login Login)
        {
            try
            {
                _db.Entry(Login).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Login;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLogin(Login Login)
        {
            try
            {
                var dbLogin = await _db.Logins.FindAsync(Login.LoginId);

                if (dbLogin == null)
                {
                    return (false, "Login could not be found");
                }

                _db.Logins.Remove(Login);
                await _db.SaveChangesAsync();

                return (true, "Login got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Logins
    }
}
