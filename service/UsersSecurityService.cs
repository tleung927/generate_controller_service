using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IUsersSecurityService
    {
        // UsersSecuritys Services
        Task<List<UsersSecurity>> GetUsersSecurityListByValue(int offset, int limit, string val); // GET All UsersSecurityss
        Task<UsersSecurity> GetUsersSecurity(string UsersSecurity_name); // GET Single UsersSecuritys        
        Task<List<UsersSecurity>> GetUsersSecurityList(string UsersSecurity_name); // GET List UsersSecuritys        
        Task<UsersSecurity> AddUsersSecurity(UsersSecurity UsersSecurity); // POST New UsersSecuritys
        Task<UsersSecurity> UpdateUsersSecurity(UsersSecurity UsersSecurity); // PUT UsersSecuritys
        Task<(bool, string)> DeleteUsersSecurity(UsersSecurity UsersSecurity); // DELETE UsersSecuritys
    }

    public class UsersSecurityService : IUsersSecurityService
    {
        private readonly XixsrvContext _db;

        public UsersSecurityService(XixsrvContext db)
        {
            _db = db;
        }

        #region UsersSecuritys

        public async Task<List<UsersSecurity>> GetUsersSecurityListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.UsersSecuritys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsersSecurity> GetUsersSecurity(string UsersSecurity_id)
        {
            try
            {
                return await _db.UsersSecuritys.FindAsync(UsersSecurity_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UsersSecurity>> GetUsersSecurityList(string UsersSecurity_id)
        {
            try
            {
                return await _db.UsersSecuritys
                    .Where(i => i.UsersSecurityId == UsersSecurity_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<UsersSecurity> AddUsersSecurity(UsersSecurity UsersSecurity)
        {
            try
            {
                await _db.UsersSecuritys.AddAsync(UsersSecurity);
                await _db.SaveChangesAsync();
                return await _db.UsersSecuritys.FindAsync(UsersSecurity.UsersSecurityId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<UsersSecurity> UpdateUsersSecurity(UsersSecurity UsersSecurity)
        {
            try
            {
                _db.Entry(UsersSecurity).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return UsersSecurity;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUsersSecurity(UsersSecurity UsersSecurity)
        {
            try
            {
                var dbUsersSecurity = await _db.UsersSecuritys.FindAsync(UsersSecurity.UsersSecurityId);

                if (dbUsersSecurity == null)
                {
                    return (false, "UsersSecurity could not be found");
                }

                _db.UsersSecuritys.Remove(UsersSecurity);
                await _db.SaveChangesAsync();

                return (true, "UsersSecurity got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion UsersSecuritys
    }
}
