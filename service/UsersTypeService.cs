using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IUsersTypeService
    {
        // UsersTypes Services
        Task<List<UsersType>> GetUsersTypeListByValue(int offset, int limit, string val); // GET All UsersTypess
        Task<UsersType> GetUsersType(string UsersType_name); // GET Single UsersTypes        
        Task<List<UsersType>> GetUsersTypeList(string UsersType_name); // GET List UsersTypes        
        Task<UsersType> AddUsersType(UsersType UsersType); // POST New UsersTypes
        Task<UsersType> UpdateUsersType(UsersType UsersType); // PUT UsersTypes
        Task<(bool, string)> DeleteUsersType(UsersType UsersType); // DELETE UsersTypes
    }

    public class UsersTypeService : IUsersTypeService
    {
        private readonly XixsrvContext _db;

        public UsersTypeService(XixsrvContext db)
        {
            _db = db;
        }

        #region UsersTypes

        public async Task<List<UsersType>> GetUsersTypeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.UsersTypes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsersType> GetUsersType(string UsersType_id)
        {
            try
            {
                return await _db.UsersTypes.FindAsync(UsersType_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UsersType>> GetUsersTypeList(string UsersType_id)
        {
            try
            {
                return await _db.UsersTypes
                    .Where(i => i.UsersTypeId == UsersType_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<UsersType> AddUsersType(UsersType UsersType)
        {
            try
            {
                await _db.UsersTypes.AddAsync(UsersType);
                await _db.SaveChangesAsync();
                return await _db.UsersTypes.FindAsync(UsersType.UsersTypeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<UsersType> UpdateUsersType(UsersType UsersType)
        {
            try
            {
                _db.Entry(UsersType).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return UsersType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUsersType(UsersType UsersType)
        {
            try
            {
                var dbUsersType = await _db.UsersTypes.FindAsync(UsersType.UsersTypeId);

                if (dbUsersType == null)
                {
                    return (false, "UsersType could not be found");
                }

                _db.UsersTypes.Remove(UsersType);
                await _db.SaveChangesAsync();

                return (true, "UsersType got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion UsersTypes
    }
}
