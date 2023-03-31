using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEligibilityRecommendationService
    {
        // EligibilityRecommendations Services
        Task<List<EligibilityRecommendation>> GetEligibilityRecommendationListByValue(int offset, int limit, string val); // GET All EligibilityRecommendationss
        Task<EligibilityRecommendation> GetEligibilityRecommendation(string EligibilityRecommendation_name); // GET Single EligibilityRecommendations        
        Task<List<EligibilityRecommendation>> GetEligibilityRecommendationList(string EligibilityRecommendation_name); // GET List EligibilityRecommendations        
        Task<EligibilityRecommendation> AddEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation); // POST New EligibilityRecommendations
        Task<EligibilityRecommendation> UpdateEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation); // PUT EligibilityRecommendations
        Task<(bool, string)> DeleteEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation); // DELETE EligibilityRecommendations
    }

    public class EligibilityRecommendationService : IEligibilityRecommendationService
    {
        private readonly XixsrvContext _db;

        public EligibilityRecommendationService(XixsrvContext db)
        {
            _db = db;
        }

        #region EligibilityRecommendations

        public async Task<List<EligibilityRecommendation>> GetEligibilityRecommendationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EligibilityRecommendations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EligibilityRecommendation> GetEligibilityRecommendation(string EligibilityRecommendation_id)
        {
            try
            {
                return await _db.EligibilityRecommendations.FindAsync(EligibilityRecommendation_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EligibilityRecommendation>> GetEligibilityRecommendationList(string EligibilityRecommendation_id)
        {
            try
            {
                return await _db.EligibilityRecommendations
                    .Where(i => i.EligibilityRecommendationId == EligibilityRecommendation_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EligibilityRecommendation> AddEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation)
        {
            try
            {
                await _db.EligibilityRecommendations.AddAsync(EligibilityRecommendation);
                await _db.SaveChangesAsync();
                return await _db.EligibilityRecommendations.FindAsync(EligibilityRecommendation.EligibilityRecommendationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EligibilityRecommendation> UpdateEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation)
        {
            try
            {
                _db.Entry(EligibilityRecommendation).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EligibilityRecommendation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation)
        {
            try
            {
                var dbEligibilityRecommendation = await _db.EligibilityRecommendations.FindAsync(EligibilityRecommendation.EligibilityRecommendationId);

                if (dbEligibilityRecommendation == null)
                {
                    return (false, "EligibilityRecommendation could not be found");
                }

                _db.EligibilityRecommendations.Remove(EligibilityRecommendation);
                await _db.SaveChangesAsync();

                return (true, "EligibilityRecommendation got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EligibilityRecommendations
    }
}
