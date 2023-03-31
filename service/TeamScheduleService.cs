using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITeamScheduleService
    {
        // TeamSchedules Services
        Task<List<TeamSchedule>> GetTeamScheduleListByValue(int offset, int limit, string val); // GET All TeamScheduless
        Task<TeamSchedule> GetTeamSchedule(string TeamSchedule_name); // GET Single TeamSchedules        
        Task<List<TeamSchedule>> GetTeamScheduleList(string TeamSchedule_name); // GET List TeamSchedules        
        Task<TeamSchedule> AddTeamSchedule(TeamSchedule TeamSchedule); // POST New TeamSchedules
        Task<TeamSchedule> UpdateTeamSchedule(TeamSchedule TeamSchedule); // PUT TeamSchedules
        Task<(bool, string)> DeleteTeamSchedule(TeamSchedule TeamSchedule); // DELETE TeamSchedules
    }

    public class TeamScheduleService : ITeamScheduleService
    {
        private readonly XixsrvContext _db;

        public TeamScheduleService(XixsrvContext db)
        {
            _db = db;
        }

        #region TeamSchedules

        public async Task<List<TeamSchedule>> GetTeamScheduleListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TeamSchedules.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TeamSchedule> GetTeamSchedule(string TeamSchedule_id)
        {
            try
            {
                return await _db.TeamSchedules.FindAsync(TeamSchedule_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TeamSchedule>> GetTeamScheduleList(string TeamSchedule_id)
        {
            try
            {
                return await _db.TeamSchedules
                    .Where(i => i.TeamScheduleId == TeamSchedule_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TeamSchedule> AddTeamSchedule(TeamSchedule TeamSchedule)
        {
            try
            {
                await _db.TeamSchedules.AddAsync(TeamSchedule);
                await _db.SaveChangesAsync();
                return await _db.TeamSchedules.FindAsync(TeamSchedule.TeamScheduleId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TeamSchedule> UpdateTeamSchedule(TeamSchedule TeamSchedule)
        {
            try
            {
                _db.Entry(TeamSchedule).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TeamSchedule;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTeamSchedule(TeamSchedule TeamSchedule)
        {
            try
            {
                var dbTeamSchedule = await _db.TeamSchedules.FindAsync(TeamSchedule.TeamScheduleId);

                if (dbTeamSchedule == null)
                {
                    return (false, "TeamSchedule could not be found");
                }

                _db.TeamSchedules.Remove(TeamSchedule);
                await _db.SaveChangesAsync();

                return (true, "TeamSchedule got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TeamSchedules
    }
}
