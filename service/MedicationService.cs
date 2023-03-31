using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMedicationService
    {
        // Medications Services
        Task<List<Medication>> GetMedicationListByValue(int offset, int limit, string val); // GET All Medicationss
        Task<Medication> GetMedication(string Medication_name); // GET Single Medications        
        Task<List<Medication>> GetMedicationList(string Medication_name); // GET List Medications        
        Task<Medication> AddMedication(Medication Medication); // POST New Medications
        Task<Medication> UpdateMedication(Medication Medication); // PUT Medications
        Task<(bool, string)> DeleteMedication(Medication Medication); // DELETE Medications
    }

    public class MedicationService : IMedicationService
    {
        private readonly XixsrvContext _db;

        public MedicationService(XixsrvContext db)
        {
            _db = db;
        }

        #region Medications

        public async Task<List<Medication>> GetMedicationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Medications.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Medication> GetMedication(string Medication_id)
        {
            try
            {
                return await _db.Medications.FindAsync(Medication_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Medication>> GetMedicationList(string Medication_id)
        {
            try
            {
                return await _db.Medications
                    .Where(i => i.MedicationId == Medication_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Medication> AddMedication(Medication Medication)
        {
            try
            {
                await _db.Medications.AddAsync(Medication);
                await _db.SaveChangesAsync();
                return await _db.Medications.FindAsync(Medication.MedicationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Medication> UpdateMedication(Medication Medication)
        {
            try
            {
                _db.Entry(Medication).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Medication;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMedication(Medication Medication)
        {
            try
            {
                var dbMedication = await _db.Medications.FindAsync(Medication.MedicationId);

                if (dbMedication == null)
                {
                    return (false, "Medication could not be found");
                }

                _db.Medications.Remove(Medication);
                await _db.SaveChangesAsync();

                return (true, "Medication got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Medications
    }
}
