using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILoginTypeService
    {
        // LoginTypes Services
        Task<List<LoginType>> GetLoginTypeListByValue(int offset, int limit, string val); // GET All LoginTypess
        Task<LoginType> GetLoginType(string LoginType_name); // GET Single LoginTypes        
        Task<List<LoginType>> GetLoginTypeList(string LoginType_name); // GET List LoginTypes        
        Task<LoginType> AddLoginType(LoginType LoginType); // POST New LoginTypes
        Task<LoginType> UpdateLoginType(LoginType LoginType); // PUT LoginTypes
        Task<(bool, string)> DeleteLoginType(LoginType LoginType); // DELETE LoginTypes
    }

    public class LoginTypeService : ILoginTypeService
    {
        private readonly XixsrvContext _db;

        public LoginTypeService(XixsrvContext db)
        {
            _db = db;
        }

        #region LoginTypes

        public async Task<List<LoginType>> GetLoginTypeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.LoginTypes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LoginType> GetLoginType(string LoginType_id)
        {
            try
            {
                return await _db.LoginTypes.FindAsync(LoginType_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LoginType>> GetLoginTypeList(string LoginType_id)
        {
            try
            {
                return await _db.LoginTypes
                    .Where(i => i.LoginTypeId == LoginType_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<LoginType> AddLoginType(LoginType LoginType)
        {
            try
            {
                await _db.LoginTypes.AddAsync(LoginType);
                await _db.SaveChangesAsync();
                return await _db.LoginTypes.FindAsync(LoginType.LoginTypeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<LoginType> UpdateLoginType(LoginType LoginType)
        {
            try
            {
                _db.Entry(LoginType).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return LoginType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLoginType(LoginType LoginType)
        {
            try
            {
                var dbLoginType = await _db.LoginTypes.FindAsync(LoginType.LoginTypeId);

                if (dbLoginType == null)
                {
                    return (false, "LoginType could not be found");
                }

                _db.LoginTypes.Remove(LoginType);
                await _db.SaveChangesAsync();

                return (true, "LoginType got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion LoginTypes
    }
}
