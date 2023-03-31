using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILoginTrxXrfService
    {
        // LoginTrxXrfs Services
        Task<List<LoginTrxXrf>> GetLoginTrxXrfListByValue(int offset, int limit, string val); // GET All LoginTrxXrfss
        Task<LoginTrxXrf> GetLoginTrxXrf(string LoginTrxXrf_name); // GET Single LoginTrxXrfs        
        Task<List<LoginTrxXrf>> GetLoginTrxXrfList(string LoginTrxXrf_name); // GET List LoginTrxXrfs        
        Task<LoginTrxXrf> AddLoginTrxXrf(LoginTrxXrf LoginTrxXrf); // POST New LoginTrxXrfs
        Task<LoginTrxXrf> UpdateLoginTrxXrf(LoginTrxXrf LoginTrxXrf); // PUT LoginTrxXrfs
        Task<(bool, string)> DeleteLoginTrxXrf(LoginTrxXrf LoginTrxXrf); // DELETE LoginTrxXrfs
    }

    public class LoginTrxXrfService : ILoginTrxXrfService
    {
        private readonly XixsrvContext _db;

        public LoginTrxXrfService(XixsrvContext db)
        {
            _db = db;
        }

        #region LoginTrxXrfs

        public async Task<List<LoginTrxXrf>> GetLoginTrxXrfListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.LoginTrxXrfs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LoginTrxXrf> GetLoginTrxXrf(string LoginTrxXrf_id)
        {
            try
            {
                return await _db.LoginTrxXrfs.FindAsync(LoginTrxXrf_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LoginTrxXrf>> GetLoginTrxXrfList(string LoginTrxXrf_id)
        {
            try
            {
                return await _db.LoginTrxXrfs
                    .Where(i => i.LoginTrxXrfId == LoginTrxXrf_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<LoginTrxXrf> AddLoginTrxXrf(LoginTrxXrf LoginTrxXrf)
        {
            try
            {
                await _db.LoginTrxXrfs.AddAsync(LoginTrxXrf);
                await _db.SaveChangesAsync();
                return await _db.LoginTrxXrfs.FindAsync(LoginTrxXrf.LoginTrxXrfId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<LoginTrxXrf> UpdateLoginTrxXrf(LoginTrxXrf LoginTrxXrf)
        {
            try
            {
                _db.Entry(LoginTrxXrf).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return LoginTrxXrf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLoginTrxXrf(LoginTrxXrf LoginTrxXrf)
        {
            try
            {
                var dbLoginTrxXrf = await _db.LoginTrxXrfs.FindAsync(LoginTrxXrf.LoginTrxXrfId);

                if (dbLoginTrxXrf == null)
                {
                    return (false, "LoginTrxXrf could not be found");
                }

                _db.LoginTrxXrfs.Remove(LoginTrxXrf);
                await _db.SaveChangesAsync();

                return (true, "LoginTrxXrf got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion LoginTrxXrfs
    }
}
