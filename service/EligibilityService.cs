using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEligibilityService
    {
        // Eligibilitys Services
        Task<List<Eligibility>> GetEligibilityListByValue(int offset, int limit, string val); // GET All Eligibilityss
        Task<Eligibility> GetEligibility(string Eligibility_name); // GET Single Eligibilitys        
        Task<List<Eligibility>> GetEligibilityList(string Eligibility_name); // GET List Eligibilitys        
        Task<Eligibility> AddEligibility(Eligibility Eligibility); // POST New Eligibilitys
        Task<Eligibility> UpdateEligibility(Eligibility Eligibility); // PUT Eligibilitys
        Task<(bool, string)> DeleteEligibility(Eligibility Eligibility); // DELETE Eligibilitys
    }

    public class EligibilityService : IEligibilityService
    {
        private readonly XixsrvContext _db;

        public EligibilityService(XixsrvContext db)
        {
            _db = db;
        }

        #region Eligibilitys

        public async Task<List<Eligibility>> GetEligibilityListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Eligibilitys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Eligibility> GetEligibility(string Eligibility_id)
        {
            try
            {
                return await _db.Eligibilitys.FindAsync(Eligibility_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Eligibility>> GetEligibilityList(string Eligibility_id)
        {
            try
            {
                return await _db.Eligibilitys
                    .Where(i => i.EligibilityId == Eligibility_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Eligibility> AddEligibility(Eligibility Eligibility)
        {
            try
            {
                await _db.Eligibilitys.AddAsync(Eligibility);
                await _db.SaveChangesAsync();
                return await _db.Eligibilitys.FindAsync(Eligibility.EligibilityId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Eligibility> UpdateEligibility(Eligibility Eligibility)
        {
            try
            {
                _db.Entry(Eligibility).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Eligibility;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEligibility(Eligibility Eligibility)
        {
            try
            {
                var dbEligibility = await _db.Eligibilitys.FindAsync(Eligibility.EligibilityId);

                if (dbEligibility == null)
                {
                    return (false, "Eligibility could not be found");
                }

                _db.Eligibilitys.Remove(Eligibility);
                await _db.SaveChangesAsync();

                return (true, "Eligibility got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Eligibilitys
    }
}
