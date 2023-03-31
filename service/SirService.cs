using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISirService
    {
        // Sirs Services
        Task<List<Sir>> GetSirListByValue(int offset, int limit, string val); // GET All Sirss
        Task<Sir> GetSir(string Sir_name); // GET Single Sirs        
        Task<List<Sir>> GetSirList(string Sir_name); // GET List Sirs        
        Task<Sir> AddSir(Sir Sir); // POST New Sirs
        Task<Sir> UpdateSir(Sir Sir); // PUT Sirs
        Task<(bool, string)> DeleteSir(Sir Sir); // DELETE Sirs
    }

    public class SirService : ISirService
    {
        private readonly XixsrvContext _db;

        public SirService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sirs

        public async Task<List<Sir>> GetSirListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sirs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sir> GetSir(string Sir_id)
        {
            try
            {
                return await _db.Sirs.FindAsync(Sir_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sir>> GetSirList(string Sir_id)
        {
            try
            {
                return await _db.Sirs
                    .Where(i => i.SirId == Sir_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sir> AddSir(Sir Sir)
        {
            try
            {
                await _db.Sirs.AddAsync(Sir);
                await _db.SaveChangesAsync();
                return await _db.Sirs.FindAsync(Sir.SirId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sir> UpdateSir(Sir Sir)
        {
            try
            {
                _db.Entry(Sir).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sir;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSir(Sir Sir)
        {
            try
            {
                var dbSir = await _db.Sirs.FindAsync(Sir.SirId);

                if (dbSir == null)
                {
                    return (false, "Sir could not be found");
                }

                _db.Sirs.Remove(Sir);
                await _db.SaveChangesAsync();

                return (true, "Sir got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sirs
    }
}
