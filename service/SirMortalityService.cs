using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISirMortalityService
    {
        // SirMortalitys Services
        Task<List<SirMortality>> GetSirMortalityListByValue(int offset, int limit, string val); // GET All SirMortalityss
        Task<SirMortality> GetSirMortality(string SirMortality_name); // GET Single SirMortalitys        
        Task<List<SirMortality>> GetSirMortalityList(string SirMortality_name); // GET List SirMortalitys        
        Task<SirMortality> AddSirMortality(SirMortality SirMortality); // POST New SirMortalitys
        Task<SirMortality> UpdateSirMortality(SirMortality SirMortality); // PUT SirMortalitys
        Task<(bool, string)> DeleteSirMortality(SirMortality SirMortality); // DELETE SirMortalitys
    }

    public class SirMortalityService : ISirMortalityService
    {
        private readonly XixsrvContext _db;

        public SirMortalityService(XixsrvContext db)
        {
            _db = db;
        }

        #region SirMortalitys

        public async Task<List<SirMortality>> GetSirMortalityListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SirMortalitys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SirMortality> GetSirMortality(string SirMortality_id)
        {
            try
            {
                return await _db.SirMortalitys.FindAsync(SirMortality_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SirMortality>> GetSirMortalityList(string SirMortality_id)
        {
            try
            {
                return await _db.SirMortalitys
                    .Where(i => i.SirMortalityId == SirMortality_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SirMortality> AddSirMortality(SirMortality SirMortality)
        {
            try
            {
                await _db.SirMortalitys.AddAsync(SirMortality);
                await _db.SaveChangesAsync();
                return await _db.SirMortalitys.FindAsync(SirMortality.SirMortalityId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SirMortality> UpdateSirMortality(SirMortality SirMortality)
        {
            try
            {
                _db.Entry(SirMortality).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SirMortality;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSirMortality(SirMortality SirMortality)
        {
            try
            {
                var dbSirMortality = await _db.SirMortalitys.FindAsync(SirMortality.SirMortalityId);

                if (dbSirMortality == null)
                {
                    return (false, "SirMortality could not be found");
                }

                _db.SirMortalitys.Remove(SirMortality);
                await _db.SaveChangesAsync();

                return (true, "SirMortality got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SirMortalitys
    }
}
