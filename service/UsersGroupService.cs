using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IUsersGroupService
    {
        // UsersGroups Services
        Task<List<UsersGroup>> GetUsersGroupListByValue(int offset, int limit, string val); // GET All UsersGroupss
        Task<UsersGroup> GetUsersGroup(string UsersGroup_name); // GET Single UsersGroups        
        Task<List<UsersGroup>> GetUsersGroupList(string UsersGroup_name); // GET List UsersGroups        
        Task<UsersGroup> AddUsersGroup(UsersGroup UsersGroup); // POST New UsersGroups
        Task<UsersGroup> UpdateUsersGroup(UsersGroup UsersGroup); // PUT UsersGroups
        Task<(bool, string)> DeleteUsersGroup(UsersGroup UsersGroup); // DELETE UsersGroups
    }

    public class UsersGroupService : IUsersGroupService
    {
        private readonly XixsrvContext _db;

        public UsersGroupService(XixsrvContext db)
        {
            _db = db;
        }

        #region UsersGroups

        public async Task<List<UsersGroup>> GetUsersGroupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.UsersGroups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<UsersGroup> GetUsersGroup(string UsersGroup_id)
        {
            try
            {
                return await _db.UsersGroups.FindAsync(UsersGroup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<UsersGroup>> GetUsersGroupList(string UsersGroup_id)
        {
            try
            {
                return await _db.UsersGroups
                    .Where(i => i.UsersGroupId == UsersGroup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<UsersGroup> AddUsersGroup(UsersGroup UsersGroup)
        {
            try
            {
                await _db.UsersGroups.AddAsync(UsersGroup);
                await _db.SaveChangesAsync();
                return await _db.UsersGroups.FindAsync(UsersGroup.UsersGroupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<UsersGroup> UpdateUsersGroup(UsersGroup UsersGroup)
        {
            try
            {
                _db.Entry(UsersGroup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return UsersGroup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteUsersGroup(UsersGroup UsersGroup)
        {
            try
            {
                var dbUsersGroup = await _db.UsersGroups.FindAsync(UsersGroup.UsersGroupId);

                if (dbUsersGroup == null)
                {
                    return (false, "UsersGroup could not be found");
                }

                _db.UsersGroups.Remove(UsersGroup);
                await _db.SaveChangesAsync();

                return (true, "UsersGroup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion UsersGroups
    }
}
