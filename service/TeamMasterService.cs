using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITeamMasterService
    {
        // TeamMasters Services
        Task<List<TeamMaster>> GetTeamMasterListByValue(int offset, int limit, string val); // GET All TeamMasterss
        Task<TeamMaster> GetTeamMaster(string TeamMaster_name); // GET Single TeamMasters        
        Task<List<TeamMaster>> GetTeamMasterList(string TeamMaster_name); // GET List TeamMasters        
        Task<TeamMaster> AddTeamMaster(TeamMaster TeamMaster); // POST New TeamMasters
        Task<TeamMaster> UpdateTeamMaster(TeamMaster TeamMaster); // PUT TeamMasters
        Task<(bool, string)> DeleteTeamMaster(TeamMaster TeamMaster); // DELETE TeamMasters
    }

    public class TeamMasterService : ITeamMasterService
    {
        private readonly XixsrvContext _db;

        public TeamMasterService(XixsrvContext db)
        {
            _db = db;
        }

        #region TeamMasters

        public async Task<List<TeamMaster>> GetTeamMasterListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TeamMasters.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TeamMaster> GetTeamMaster(string TeamMaster_id)
        {
            try
            {
                return await _db.TeamMasters.FindAsync(TeamMaster_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TeamMaster>> GetTeamMasterList(string TeamMaster_id)
        {
            try
            {
                return await _db.TeamMasters
                    .Where(i => i.TeamMasterId == TeamMaster_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TeamMaster> AddTeamMaster(TeamMaster TeamMaster)
        {
            try
            {
                await _db.TeamMasters.AddAsync(TeamMaster);
                await _db.SaveChangesAsync();
                return await _db.TeamMasters.FindAsync(TeamMaster.TeamMasterId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TeamMaster> UpdateTeamMaster(TeamMaster TeamMaster)
        {
            try
            {
                _db.Entry(TeamMaster).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TeamMaster;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTeamMaster(TeamMaster TeamMaster)
        {
            try
            {
                var dbTeamMaster = await _db.TeamMasters.FindAsync(TeamMaster.TeamMasterId);

                if (dbTeamMaster == null)
                {
                    return (false, "TeamMaster could not be found");
                }

                _db.TeamMasters.Remove(TeamMaster);
                await _db.SaveChangesAsync();

                return (true, "TeamMaster got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TeamMasters
    }
}
