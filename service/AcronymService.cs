using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IAcronymService
    {
        // Acronyms Services
        Task<List<Acronym>> GetAcronymListByValue(int offset, int limit, string val); // GET All Acronymss
        Task<Acronym> GetAcronym(string Acronym_name); // GET Single Acronyms        
        Task<List<Acronym>> GetAcronymList(string Acronym_name); // GET List Acronyms        
        Task<Acronym> AddAcronym(Acronym Acronym); // POST New Acronyms
        Task<Acronym> UpdateAcronym(Acronym Acronym); // PUT Acronyms
        Task<(bool, string)> DeleteAcronym(Acronym Acronym); // DELETE Acronyms
    }

    public class AcronymService : IAcronymService
    {
        private readonly XixsrvContext _db;

        public AcronymService(XixsrvContext db)
        {
            _db = db;
        }

        #region Acronyms

        public async Task<List<Acronym>> GetAcronymListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Acronyms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Acronym> GetAcronym(string Acronym_id)
        {
            try
            {
                return await _db.Acronyms.FindAsync(Acronym_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Acronym>> GetAcronymList(string Acronym_id)
        {
            try
            {
                return await _db.Acronyms
                    .Where(i => i.AcronymId == Acronym_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Acronym> AddAcronym(Acronym Acronym)
        {
            try
            {
                await _db.Acronyms.AddAsync(Acronym);
                await _db.SaveChangesAsync();
                return await _db.Acronyms.FindAsync(Acronym.AcronymId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Acronym> UpdateAcronym(Acronym Acronym)
        {
            try
            {
                _db.Entry(Acronym).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Acronym;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAcronym(Acronym Acronym)
        {
            try
            {
                var dbAcronym = await _db.Acronyms.FindAsync(Acronym.AcronymId);

                if (dbAcronym == null)
                {
                    return (false, "Acronym could not be found");
                }

                _db.Acronyms.Remove(Acronym);
                await _db.SaveChangesAsync();

                return (true, "Acronym got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Acronyms
    }
}
