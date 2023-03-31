using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspparticipantService
    {
        // FormIfspparticipants Services
        Task<List<FormIfspparticipant>> GetFormIfspparticipantListByValue(int offset, int limit, string val); // GET All FormIfspparticipantss
        Task<FormIfspparticipant> GetFormIfspparticipant(string FormIfspparticipant_name); // GET Single FormIfspparticipants        
        Task<List<FormIfspparticipant>> GetFormIfspparticipantList(string FormIfspparticipant_name); // GET List FormIfspparticipants        
        Task<FormIfspparticipant> AddFormIfspparticipant(FormIfspparticipant FormIfspparticipant); // POST New FormIfspparticipants
        Task<FormIfspparticipant> UpdateFormIfspparticipant(FormIfspparticipant FormIfspparticipant); // PUT FormIfspparticipants
        Task<(bool, string)> DeleteFormIfspparticipant(FormIfspparticipant FormIfspparticipant); // DELETE FormIfspparticipants
    }

    public class FormIfspparticipantService : IFormIfspparticipantService
    {
        private readonly XixsrvContext _db;

        public FormIfspparticipantService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspparticipants

        public async Task<List<FormIfspparticipant>> GetFormIfspparticipantListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspparticipants.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfspparticipant> GetFormIfspparticipant(string FormIfspparticipant_id)
        {
            try
            {
                return await _db.FormIfspparticipants.FindAsync(FormIfspparticipant_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfspparticipant>> GetFormIfspparticipantList(string FormIfspparticipant_id)
        {
            try
            {
                return await _db.FormIfspparticipants
                    .Where(i => i.FormIfspparticipantId == FormIfspparticipant_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfspparticipant> AddFormIfspparticipant(FormIfspparticipant FormIfspparticipant)
        {
            try
            {
                await _db.FormIfspparticipants.AddAsync(FormIfspparticipant);
                await _db.SaveChangesAsync();
                return await _db.FormIfspparticipants.FindAsync(FormIfspparticipant.FormIfspparticipantId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfspparticipant> UpdateFormIfspparticipant(FormIfspparticipant FormIfspparticipant)
        {
            try
            {
                _db.Entry(FormIfspparticipant).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfspparticipant;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfspparticipant(FormIfspparticipant FormIfspparticipant)
        {
            try
            {
                var dbFormIfspparticipant = await _db.FormIfspparticipants.FindAsync(FormIfspparticipant.FormIfspparticipantId);

                if (dbFormIfspparticipant == null)
                {
                    return (false, "FormIfspparticipant could not be found");
                }

                _db.FormIfspparticipants.Remove(FormIfspparticipant);
                await _db.SaveChangesAsync();

                return (true, "FormIfspparticipant got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspparticipants
    }
}
