using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILegalService
    {
        // Legals Services
        Task<List<Legal>> GetLegalListByValue(int offset, int limit, string val); // GET All Legalss
        Task<Legal> GetLegal(string Legal_name); // GET Single Legals        
        Task<List<Legal>> GetLegalList(string Legal_name); // GET List Legals        
        Task<Legal> AddLegal(Legal Legal); // POST New Legals
        Task<Legal> UpdateLegal(Legal Legal); // PUT Legals
        Task<(bool, string)> DeleteLegal(Legal Legal); // DELETE Legals
    }

    public class LegalService : ILegalService
    {
        private readonly XixsrvContext _db;

        public LegalService(XixsrvContext db)
        {
            _db = db;
        }

        #region Legals

        public async Task<List<Legal>> GetLegalListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Legals.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Legal> GetLegal(string Legal_id)
        {
            try
            {
                return await _db.Legals.FindAsync(Legal_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Legal>> GetLegalList(string Legal_id)
        {
            try
            {
                return await _db.Legals
                    .Where(i => i.LegalId == Legal_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Legal> AddLegal(Legal Legal)
        {
            try
            {
                await _db.Legals.AddAsync(Legal);
                await _db.SaveChangesAsync();
                return await _db.Legals.FindAsync(Legal.LegalId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Legal> UpdateLegal(Legal Legal)
        {
            try
            {
                _db.Entry(Legal).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Legal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLegal(Legal Legal)
        {
            try
            {
                var dbLegal = await _db.Legals.FindAsync(Legal.LegalId);

                if (dbLegal == null)
                {
                    return (false, "Legal could not be found");
                }

                _db.Legals.Remove(Legal);
                await _db.SaveChangesAsync();

                return (true, "Legal got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Legals
    }
}
