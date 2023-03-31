using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtcitationService
    {
        // FormMtcitations Services
        Task<List<FormMtcitation>> GetFormMtcitationListByValue(int offset, int limit, string val); // GET All FormMtcitationss
        Task<FormMtcitation> GetFormMtcitation(string FormMtcitation_name); // GET Single FormMtcitations        
        Task<List<FormMtcitation>> GetFormMtcitationList(string FormMtcitation_name); // GET List FormMtcitations        
        Task<FormMtcitation> AddFormMtcitation(FormMtcitation FormMtcitation); // POST New FormMtcitations
        Task<FormMtcitation> UpdateFormMtcitation(FormMtcitation FormMtcitation); // PUT FormMtcitations
        Task<(bool, string)> DeleteFormMtcitation(FormMtcitation FormMtcitation); // DELETE FormMtcitations
    }

    public class FormMtcitationService : IFormMtcitationService
    {
        private readonly XixsrvContext _db;

        public FormMtcitationService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtcitations

        public async Task<List<FormMtcitation>> GetFormMtcitationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtcitations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtcitation> GetFormMtcitation(string FormMtcitation_id)
        {
            try
            {
                return await _db.FormMtcitations.FindAsync(FormMtcitation_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtcitation>> GetFormMtcitationList(string FormMtcitation_id)
        {
            try
            {
                return await _db.FormMtcitations
                    .Where(i => i.FormMtcitationId == FormMtcitation_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtcitation> AddFormMtcitation(FormMtcitation FormMtcitation)
        {
            try
            {
                await _db.FormMtcitations.AddAsync(FormMtcitation);
                await _db.SaveChangesAsync();
                return await _db.FormMtcitations.FindAsync(FormMtcitation.FormMtcitationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtcitation> UpdateFormMtcitation(FormMtcitation FormMtcitation)
        {
            try
            {
                _db.Entry(FormMtcitation).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtcitation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtcitation(FormMtcitation FormMtcitation)
        {
            try
            {
                var dbFormMtcitation = await _db.FormMtcitations.FindAsync(FormMtcitation.FormMtcitationId);

                if (dbFormMtcitation == null)
                {
                    return (false, "FormMtcitation could not be found");
                }

                _db.FormMtcitations.Remove(FormMtcitation);
                await _db.SaveChangesAsync();

                return (true, "FormMtcitation got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtcitations
    }
}
