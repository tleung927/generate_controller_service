using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILoginStatService
    {
        // LoginStats Services
        Task<List<LoginStat>> GetLoginStatListByValue(int offset, int limit, string val); // GET All LoginStatss
        Task<LoginStat> GetLoginStat(string LoginStat_name); // GET Single LoginStats        
        Task<List<LoginStat>> GetLoginStatList(string LoginStat_name); // GET List LoginStats        
        Task<LoginStat> AddLoginStat(LoginStat LoginStat); // POST New LoginStats
        Task<LoginStat> UpdateLoginStat(LoginStat LoginStat); // PUT LoginStats
        Task<(bool, string)> DeleteLoginStat(LoginStat LoginStat); // DELETE LoginStats
    }

    public class LoginStatService : ILoginStatService
    {
        private readonly XixsrvContext _db;

        public LoginStatService(XixsrvContext db)
        {
            _db = db;
        }

        #region LoginStats

        public async Task<List<LoginStat>> GetLoginStatListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.LoginStats.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LoginStat> GetLoginStat(string LoginStat_id)
        {
            try
            {
                return await _db.LoginStats.FindAsync(LoginStat_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LoginStat>> GetLoginStatList(string LoginStat_id)
        {
            try
            {
                return await _db.LoginStats
                    .Where(i => i.LoginStatId == LoginStat_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<LoginStat> AddLoginStat(LoginStat LoginStat)
        {
            try
            {
                await _db.LoginStats.AddAsync(LoginStat);
                await _db.SaveChangesAsync();
                return await _db.LoginStats.FindAsync(LoginStat.LoginStatId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<LoginStat> UpdateLoginStat(LoginStat LoginStat)
        {
            try
            {
                _db.Entry(LoginStat).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return LoginStat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLoginStat(LoginStat LoginStat)
        {
            try
            {
                var dbLoginStat = await _db.LoginStats.FindAsync(LoginStat.LoginStatId);

                if (dbLoginStat == null)
                {
                    return (false, "LoginStat could not be found");
                }

                _db.LoginStats.Remove(LoginStat);
                await _db.SaveChangesAsync();

                return (true, "LoginStat got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion LoginStats
    }
}
