using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGroupService
    {
        // Groups Services
        Task<List<Group>> GetGroupListByValue(int offset, int limit, string val); // GET All Groupss
        Task<Group> GetGroup(string Group_name); // GET Single Groups        
        Task<List<Group>> GetGroupList(string Group_name); // GET List Groups        
        Task<Group> AddGroup(Group Group); // POST New Groups
        Task<Group> UpdateGroup(Group Group); // PUT Groups
        Task<(bool, string)> DeleteGroup(Group Group); // DELETE Groups
    }

    public class GroupService : IGroupService
    {
        private readonly XixsrvContext _db;

        public GroupService(XixsrvContext db)
        {
            _db = db;
        }

        #region Groups

        public async Task<List<Group>> GetGroupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Groups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Group> GetGroup(string Group_id)
        {
            try
            {
                return await _db.Groups.FindAsync(Group_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Group>> GetGroupList(string Group_id)
        {
            try
            {
                return await _db.Groups
                    .Where(i => i.GroupId == Group_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Group> AddGroup(Group Group)
        {
            try
            {
                await _db.Groups.AddAsync(Group);
                await _db.SaveChangesAsync();
                return await _db.Groups.FindAsync(Group.GroupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Group> UpdateGroup(Group Group)
        {
            try
            {
                _db.Entry(Group).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Group;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGroup(Group Group)
        {
            try
            {
                var dbGroup = await _db.Groups.FindAsync(Group.GroupId);

                if (dbGroup == null)
                {
                    return (false, "Group could not be found");
                }

                _db.Groups.Remove(Group);
                await _db.SaveChangesAsync();

                return (true, "Group got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Groups
    }
}
