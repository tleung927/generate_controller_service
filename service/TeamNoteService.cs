using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITeamNoteService
    {
        // TeamNotes Services
        Task<List<TeamNote>> GetTeamNoteListByValue(int offset, int limit, string val); // GET All TeamNotess
        Task<TeamNote> GetTeamNote(string TeamNote_name); // GET Single TeamNotes        
        Task<List<TeamNote>> GetTeamNoteList(string TeamNote_name); // GET List TeamNotes        
        Task<TeamNote> AddTeamNote(TeamNote TeamNote); // POST New TeamNotes
        Task<TeamNote> UpdateTeamNote(TeamNote TeamNote); // PUT TeamNotes
        Task<(bool, string)> DeleteTeamNote(TeamNote TeamNote); // DELETE TeamNotes
    }

    public class TeamNoteService : ITeamNoteService
    {
        private readonly XixsrvContext _db;

        public TeamNoteService(XixsrvContext db)
        {
            _db = db;
        }

        #region TeamNotes

        public async Task<List<TeamNote>> GetTeamNoteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TeamNotes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TeamNote> GetTeamNote(string TeamNote_id)
        {
            try
            {
                return await _db.TeamNotes.FindAsync(TeamNote_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TeamNote>> GetTeamNoteList(string TeamNote_id)
        {
            try
            {
                return await _db.TeamNotes
                    .Where(i => i.TeamNoteId == TeamNote_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TeamNote> AddTeamNote(TeamNote TeamNote)
        {
            try
            {
                await _db.TeamNotes.AddAsync(TeamNote);
                await _db.SaveChangesAsync();
                return await _db.TeamNotes.FindAsync(TeamNote.TeamNoteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TeamNote> UpdateTeamNote(TeamNote TeamNote)
        {
            try
            {
                _db.Entry(TeamNote).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TeamNote;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTeamNote(TeamNote TeamNote)
        {
            try
            {
                var dbTeamNote = await _db.TeamNotes.FindAsync(TeamNote.TeamNoteId);

                if (dbTeamNote == null)
                {
                    return (false, "TeamNote could not be found");
                }

                _db.TeamNotes.Remove(TeamNote);
                await _db.SaveChangesAsync();

                return (true, "TeamNote got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TeamNotes
    }
}
