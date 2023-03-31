using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IAbbreviationService
    {
        // Abbreviations Services
        Task<List<Abbreviation>> GetAbbreviationListByValue(int offset, int limit, string val); // GET All Abbreviationss
        Task<Abbreviation> GetAbbreviation(string Abbreviation_name); // GET Single Abbreviations        
        Task<List<Abbreviation>> GetAbbreviationList(string Abbreviation_name); // GET List Abbreviations        
        Task<Abbreviation> AddAbbreviation(Abbreviation Abbreviation); // POST New Abbreviations
        Task<Abbreviation> UpdateAbbreviation(Abbreviation Abbreviation); // PUT Abbreviations
        Task<(bool, string)> DeleteAbbreviation(Abbreviation Abbreviation); // DELETE Abbreviations
    }

    public class AbbreviationService : IAbbreviationService
    {
        private readonly XixsrvContext _db;

        public AbbreviationService(XixsrvContext db)
        {
            _db = db;
        }

        #region Abbreviations

        public async Task<List<Abbreviation>> GetAbbreviationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Abbreviations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Abbreviation> GetAbbreviation(string Abbreviation_id)
        {
            try
            {
                return await _db.Abbreviations.FindAsync(Abbreviation_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Abbreviation>> GetAbbreviationList(string Abbreviation_id)
        {
            try
            {
                return await _db.Abbreviations
                    .Where(i => i.AbbreviationId == Abbreviation_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Abbreviation> AddAbbreviation(Abbreviation Abbreviation)
        {
            try
            {
                await _db.Abbreviations.AddAsync(Abbreviation);
                await _db.SaveChangesAsync();
                return await _db.Abbreviations.FindAsync(Abbreviation.AbbreviationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Abbreviation> UpdateAbbreviation(Abbreviation Abbreviation)
        {
            try
            {
                _db.Entry(Abbreviation).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Abbreviation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAbbreviation(Abbreviation Abbreviation)
        {
            try
            {
                var dbAbbreviation = await _db.Abbreviations.FindAsync(Abbreviation.AbbreviationId);

                if (dbAbbreviation == null)
                {
                    return (false, "Abbreviation could not be found");
                }

                _db.Abbreviations.Remove(Abbreviation);
                await _db.SaveChangesAsync();

                return (true, "Abbreviation got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Abbreviations
    }
}
