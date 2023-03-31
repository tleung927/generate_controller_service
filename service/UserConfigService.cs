using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IUserConfigService
    {
        // UserConfigs Services
        Task<List<UserConfig>> GetUserConfigListByValue(int offset, int limit, string val); // GET All UserConfigss
        Task<UserConfig> GetUserConfig(string UserConfig_name); // GET Single UserConfigs        
        Task<List<UserConfig>> GetUserConfigList(string UserConfig_name); // GET List UserConfigs        
        Task<UserConfig> AddUserConfig(UserConfig UserConfig); // POST New UserConfigs
        Task<UserConfig> UpdateUserConfig(UserConfig UserConfig); // PUT UserConfigs
        Task<(bool, string)> DeleteUserConfig(UserConfig UserConfig); // DELETE UserConfigs
    }

    public class UserConfigService : IUserConfigService
    {
        private readonly XixsrvContext _db;

        public UserConfigService(XixsrvContext db)
        {
            _db = db;
        }

        #region UserConfigs

        public async Task<List<UserConfig>> GetUserConfigListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.UserConfigs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UserConfig> GetUserConfig(string UserConfig_id)
        {
            try
            {
                return await _db.UserConfigs.FindAsync(UserConfig_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UserConfig>> GetUserConfigList(string UserConfig_id)
        {
            try
            {
                return await _db.UserConfigs
                    .Where(i => i.UserConfigId == UserConfig_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<UserConfig> AddUserConfig(UserConfig UserConfig)
        {
            try
            {
                await _db.UserConfigs.AddAsync(UserConfig);
                await _db.SaveChangesAsync();
                return await _db.UserConfigs.FindAsync(UserConfig.UserConfigId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<UserConfig> UpdateUserConfig(UserConfig UserConfig)
        {
            try
            {
                _db.Entry(UserConfig).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return UserConfig;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUserConfig(UserConfig UserConfig)
        {
            try
            {
                var dbUserConfig = await _db.UserConfigs.FindAsync(UserConfig.UserConfigId);

                if (dbUserConfig == null)
                {
                    return (false, "UserConfig could not be found");
                }

                _db.UserConfigs.Remove(UserConfig);
                await _db.SaveChangesAsync();

                return (true, "UserConfig got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion UserConfigs
    }
}
