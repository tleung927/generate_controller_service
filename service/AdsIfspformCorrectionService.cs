using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IAdsIfspformCorrectionService
    {
        // AdsIfspformCorrections Services
        Task<List<AdsIfspformCorrection>> GetAdsIfspformCorrectionListByValue(int offset, int limit, string val); // GET All AdsIfspformCorrectionss
        Task<AdsIfspformCorrection> GetAdsIfspformCorrection(string AdsIfspformCorrection_name); // GET Single AdsIfspformCorrections        
        Task<List<AdsIfspformCorrection>> GetAdsIfspformCorrectionList(string AdsIfspformCorrection_name); // GET List AdsIfspformCorrections        
        Task<AdsIfspformCorrection> AddAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection); // POST New AdsIfspformCorrections
        Task<AdsIfspformCorrection> UpdateAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection); // PUT AdsIfspformCorrections
        Task<(bool, string)> DeleteAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection); // DELETE AdsIfspformCorrections
    }

    public class AdsIfspformCorrectionService : IAdsIfspformCorrectionService
    {
        private readonly XixsrvContext _db;

        public AdsIfspformCorrectionService(XixsrvContext db)
        {
            _db = db;
        }

        #region AdsIfspformCorrections

        public async Task<List<AdsIfspformCorrection>> GetAdsIfspformCorrectionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.AdsIfspformCorrections.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<AdsIfspformCorrection> GetAdsIfspformCorrection(string AdsIfspformCorrection_id)
        {
            try
            {
                return await _db.AdsIfspformCorrections.FindAsync(AdsIfspformCorrection_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<AdsIfspformCorrection>> GetAdsIfspformCorrectionList(string AdsIfspformCorrection_id)
        {
            try
            {
                return await _db.AdsIfspformCorrections
                    .Where(i => i.AdsIfspformCorrectionId == AdsIfspformCorrection_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<AdsIfspformCorrection> AddAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection)
        {
            try
            {
                await _db.AdsIfspformCorrections.AddAsync(AdsIfspformCorrection);
                await _db.SaveChangesAsync();
                return await _db.AdsIfspformCorrections.FindAsync(AdsIfspformCorrection.AdsIfspformCorrectionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<AdsIfspformCorrection> UpdateAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection)
        {
            try
            {
                _db.Entry(AdsIfspformCorrection).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return AdsIfspformCorrection;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection)
        {
            try
            {
                var dbAdsIfspformCorrection = await _db.AdsIfspformCorrections.FindAsync(AdsIfspformCorrection.AdsIfspformCorrectionId);

                if (dbAdsIfspformCorrection == null)
                {
                    return (false, "AdsIfspformCorrection could not be found");
                }

                _db.AdsIfspformCorrections.Remove(AdsIfspformCorrection);
                await _db.SaveChangesAsync();

                return (true, "AdsIfspformCorrection got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion AdsIfspformCorrections
    }
}
