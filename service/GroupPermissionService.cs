using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGroupPermissionService
    {
        // GroupPermissions Services
        Task<List<GroupPermission>> GetGroupPermissionListByValue(int offset, int limit, string val); // GET All GroupPermissionss
        Task<GroupPermission> GetGroupPermission(string GroupPermission_name); // GET Single GroupPermissions        
        Task<List<GroupPermission>> GetGroupPermissionList(string GroupPermission_name); // GET List GroupPermissions        
        Task<GroupPermission> AddGroupPermission(GroupPermission GroupPermission); // POST New GroupPermissions
        Task<GroupPermission> UpdateGroupPermission(GroupPermission GroupPermission); // PUT GroupPermissions
        Task<(bool, string)> DeleteGroupPermission(GroupPermission GroupPermission); // DELETE GroupPermissions
    }

    public class GroupPermissionService : IGroupPermissionService
    {
        private readonly XixsrvContext _db;

        public GroupPermissionService(XixsrvContext db)
        {
            _db = db;
        }

        #region GroupPermissions

        public async Task<List<GroupPermission>> GetGroupPermissionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.GroupPermissions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GroupPermission> GetGroupPermission(string GroupPermission_id)
        {
            try
            {
                return await _db.GroupPermissions.FindAsync(GroupPermission_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GroupPermission>> GetGroupPermissionList(string GroupPermission_id)
        {
            try
            {
                return await _db.GroupPermissions
                    .Where(i => i.GroupPermissionId == GroupPermission_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<GroupPermission> AddGroupPermission(GroupPermission GroupPermission)
        {
            try
            {
                await _db.GroupPermissions.AddAsync(GroupPermission);
                await _db.SaveChangesAsync();
                return await _db.GroupPermissions.FindAsync(GroupPermission.GroupPermissionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<GroupPermission> UpdateGroupPermission(GroupPermission GroupPermission)
        {
            try
            {
                _db.Entry(GroupPermission).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return GroupPermission;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGroupPermission(GroupPermission GroupPermission)
        {
            try
            {
                var dbGroupPermission = await _db.GroupPermissions.FindAsync(GroupPermission.GroupPermissionId);

                if (dbGroupPermission == null)
                {
                    return (false, "GroupPermission could not be found");
                }

                _db.GroupPermissions.Remove(GroupPermission);
                await _db.SaveChangesAsync();

                return (true, "GroupPermission got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion GroupPermissions
    }
}
