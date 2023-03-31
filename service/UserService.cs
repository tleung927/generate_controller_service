using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IUserService
    {
        // Users Services
        Task<List<User>> GetUserListByValue(int offset, int limit, string val); // GET All Userss
        Task<User> GetUser(string User_name); // GET Single Users        
        Task<List<User>> GetUserList(string User_name); // GET List Users        
        Task<User> AddUser(User User); // POST New Users
        Task<User> UpdateUser(User User); // PUT Users
        Task<(bool, string)> DeleteUser(User User); // DELETE Users
    }

    public class UserService : IUserService
    {
        private readonly XixsrvContext _db;

        public UserService(XixsrvContext db)
        {
            _db = db;
        }

        #region Users

        public async Task<List<User>> GetUserListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Users.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<User> GetUser(string User_id)
        {
            try
            {
                return await _db.Users.FindAsync(User_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<User>> GetUserList(string User_id)
        {
            try
            {
                return await _db.Users
                    .Where(i => i.UserId == User_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<User> AddUser(User User)
        {
            try
            {
                await _db.Users.AddAsync(User);
                await _db.SaveChangesAsync();
                return await _db.Users.FindAsync(User.UserId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<User> UpdateUser(User User)
        {
            try
            {
                _db.Entry(User).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return User;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUser(User User)
        {
            try
            {
                var dbUser = await _db.Users.FindAsync(User.UserId);

                if (dbUser == null)
                {
                    return (false, "User could not be found");
                }

                _db.Users.Remove(User);
                await _db.SaveChangesAsync();

                return (true, "User got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Users
    }
}
