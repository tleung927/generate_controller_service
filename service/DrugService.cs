using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDrugService
    {
        // Drugs Services
        Task<List<Drug>> GetDrugListByValue(int offset, int limit, string val); // GET All Drugss
        Task<Drug> GetDrug(string Drug_name); // GET Single Drugs        
        Task<List<Drug>> GetDrugList(string Drug_name); // GET List Drugs        
        Task<Drug> AddDrug(Drug Drug); // POST New Drugs
        Task<Drug> UpdateDrug(Drug Drug); // PUT Drugs
        Task<(bool, string)> DeleteDrug(Drug Drug); // DELETE Drugs
    }

    public class DrugService : IDrugService
    {
        private readonly XixsrvContext _db;

        public DrugService(XixsrvContext db)
        {
            _db = db;
        }

        #region Drugs

        public async Task<List<Drug>> GetDrugListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Drugs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Drug> GetDrug(string Drug_id)
        {
            try
            {
                return await _db.Drugs.FindAsync(Drug_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Drug>> GetDrugList(string Drug_id)
        {
            try
            {
                return await _db.Drugs
                    .Where(i => i.DrugId == Drug_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Drug> AddDrug(Drug Drug)
        {
            try
            {
                await _db.Drugs.AddAsync(Drug);
                await _db.SaveChangesAsync();
                return await _db.Drugs.FindAsync(Drug.DrugId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Drug> UpdateDrug(Drug Drug)
        {
            try
            {
                _db.Entry(Drug).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Drug;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDrug(Drug Drug)
        {
            try
            {
                var dbDrug = await _db.Drugs.FindAsync(Drug.DrugId);

                if (dbDrug == null)
                {
                    return (false, "Drug could not be found");
                }

                _db.Drugs.Remove(Drug);
                await _db.SaveChangesAsync();

                return (true, "Drug got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Drugs
    }
}
