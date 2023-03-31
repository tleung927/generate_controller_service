using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclisupService
    {
        // Sclisups Services
        Task<List<Sclisup>> GetSclisupListByValue(int offset, int limit, string val); // GET All Sclisupss
        Task<Sclisup> GetSclisup(string Sclisup_name); // GET Single Sclisups        
        Task<List<Sclisup>> GetSclisupList(string Sclisup_name); // GET List Sclisups        
        Task<Sclisup> AddSclisup(Sclisup Sclisup); // POST New Sclisups
        Task<Sclisup> UpdateSclisup(Sclisup Sclisup); // PUT Sclisups
        Task<(bool, string)> DeleteSclisup(Sclisup Sclisup); // DELETE Sclisups
    }

    public class SclisupService : ISclisupService
    {
        private readonly XixsrvContext _db;

        public SclisupService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclisups

        public async Task<List<Sclisup>> GetSclisupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclisups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclisup> GetSclisup(string Sclisup_id)
        {
            try
            {
                return await _db.Sclisups.FindAsync(Sclisup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclisup>> GetSclisupList(string Sclisup_id)
        {
            try
            {
                return await _db.Sclisups
                    .Where(i => i.SclisupId == Sclisup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclisup> AddSclisup(Sclisup Sclisup)
        {
            try
            {
                await _db.Sclisups.AddAsync(Sclisup);
                await _db.SaveChangesAsync();
                return await _db.Sclisups.FindAsync(Sclisup.SclisupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclisup> UpdateSclisup(Sclisup Sclisup)
        {
            try
            {
                _db.Entry(Sclisup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclisup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclisup(Sclisup Sclisup)
        {
            try
            {
                var dbSclisup = await _db.Sclisups.FindAsync(Sclisup.SclisupId);

                if (dbSclisup == null)
                {
                    return (false, "Sclisup could not be found");
                }

                _db.Sclisups.Remove(Sclisup);
                await _db.SaveChangesAsync();

                return (true, "Sclisup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclisups
    }
}
