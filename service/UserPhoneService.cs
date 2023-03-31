using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IUserPhoneService
    {
        // UserPhones Services
        Task<List<UserPhone>> GetUserPhoneListByValue(int offset, int limit, string val); // GET All UserPhoness
        Task<UserPhone> GetUserPhone(string UserPhone_name); // GET Single UserPhones        
        Task<List<UserPhone>> GetUserPhoneList(string UserPhone_name); // GET List UserPhones        
        Task<UserPhone> AddUserPhone(UserPhone UserPhone); // POST New UserPhones
        Task<UserPhone> UpdateUserPhone(UserPhone UserPhone); // PUT UserPhones
        Task<(bool, string)> DeleteUserPhone(UserPhone UserPhone); // DELETE UserPhones
    }

    public class UserPhoneService : IUserPhoneService
    {
        private readonly XixsrvContext _db;

        public UserPhoneService(XixsrvContext db)
        {
            _db = db;
        }

        #region UserPhones

        public async Task<List<UserPhone>> GetUserPhoneListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.UserPhones.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserPhone> GetUserPhone(string UserPhone_id)
        {
            try
            {
                return await _db.UserPhones.FindAsync(UserPhone_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UserPhone>> GetUserPhoneList(string UserPhone_id)
        {
            try
            {
                return await _db.UserPhones
                    .Where(i => i.UserPhoneId == UserPhone_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<UserPhone> AddUserPhone(UserPhone UserPhone)
        {
            try
            {
                await _db.UserPhones.AddAsync(UserPhone);
                await _db.SaveChangesAsync();
                return await _db.UserPhones.FindAsync(UserPhone.UserPhoneId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<UserPhone> UpdateUserPhone(UserPhone UserPhone)
        {
            try
            {
                _db.Entry(UserPhone).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return UserPhone;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUserPhone(UserPhone UserPhone)
        {
            try
            {
                var dbUserPhone = await _db.UserPhones.FindAsync(UserPhone.UserPhoneId);

                if (dbUserPhone == null)
                {
                    return (false, "UserPhone could not be found");
                }

                _db.UserPhones.Remove(UserPhone);
                await _db.SaveChangesAsync();

                return (true, "UserPhone got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion UserPhones
    }
}
