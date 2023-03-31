using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspMedicationService
    {
        // FormIfspMedications Services
        Task<List<FormIfspMedication>> GetFormIfspMedicationListByValue(int offset, int limit, string val); // GET All FormIfspMedicationss
        Task<FormIfspMedication> GetFormIfspMedication(string FormIfspMedication_name); // GET Single FormIfspMedications        
        Task<List<FormIfspMedication>> GetFormIfspMedicationList(string FormIfspMedication_name); // GET List FormIfspMedications        
        Task<FormIfspMedication> AddFormIfspMedication(FormIfspMedication FormIfspMedication); // POST New FormIfspMedications
        Task<FormIfspMedication> UpdateFormIfspMedication(FormIfspMedication FormIfspMedication); // PUT FormIfspMedications
        Task<(bool, string)> DeleteFormIfspMedication(FormIfspMedication FormIfspMedication); // DELETE FormIfspMedications
    }

    public class FormIfspMedicationService : IFormIfspMedicationService
    {
        private readonly XixsrvContext _db;

        public FormIfspMedicationService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspMedications

        public async Task<List<FormIfspMedication>> GetFormIfspMedicationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspMedications.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfspMedication> GetFormIfspMedication(string FormIfspMedication_id)
        {
            try
            {
                return await _db.FormIfspMedications.FindAsync(FormIfspMedication_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfspMedication>> GetFormIfspMedicationList(string FormIfspMedication_id)
        {
            try
            {
                return await _db.FormIfspMedications
                    .Where(i => i.FormIfspMedicationId == FormIfspMedication_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfspMedication> AddFormIfspMedication(FormIfspMedication FormIfspMedication)
        {
            try
            {
                await _db.FormIfspMedications.AddAsync(FormIfspMedication);
                await _db.SaveChangesAsync();
                return await _db.FormIfspMedications.FindAsync(FormIfspMedication.FormIfspMedicationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfspMedication> UpdateFormIfspMedication(FormIfspMedication FormIfspMedication)
        {
            try
            {
                _db.Entry(FormIfspMedication).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfspMedication;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfspMedication(FormIfspMedication FormIfspMedication)
        {
            try
            {
                var dbFormIfspMedication = await _db.FormIfspMedications.FindAsync(FormIfspMedication.FormIfspMedicationId);

                if (dbFormIfspMedication == null)
                {
                    return (false, "FormIfspMedication could not be found");
                }

                _db.FormIfspMedications.Remove(FormIfspMedication);
                await _db.SaveChangesAsync();

                return (true, "FormIfspMedication got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspMedications
    }
}
