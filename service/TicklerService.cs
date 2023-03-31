using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITicklerService
    {
        // Ticklers Services
        Task<List<Tickler>> GetTicklerListByValue(int offset, int limit, string val); // GET All Ticklerss
        Task<Tickler> GetTickler(string Tickler_name); // GET Single Ticklers        
        Task<List<Tickler>> GetTicklerList(string Tickler_name); // GET List Ticklers        
        Task<Tickler> AddTickler(Tickler Tickler); // POST New Ticklers
        Task<Tickler> UpdateTickler(Tickler Tickler); // PUT Ticklers
        Task<(bool, string)> DeleteTickler(Tickler Tickler); // DELETE Ticklers
    }

    public class TicklerService : ITicklerService
    {
        private readonly XixsrvContext _db;

        public TicklerService(XixsrvContext db)
        {
            _db = db;
        }

        #region Ticklers

        public async Task<List<Tickler>> GetTicklerListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Ticklers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Tickler> GetTickler(string Tickler_id)
        {
            try
            {
                return await _db.Ticklers.FindAsync(Tickler_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Tickler>> GetTicklerList(string Tickler_id)
        {
            try
            {
                return await _db.Ticklers
                    .Where(i => i.TicklerId == Tickler_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Tickler> AddTickler(Tickler Tickler)
        {
            try
            {
                await _db.Ticklers.AddAsync(Tickler);
                await _db.SaveChangesAsync();
                return await _db.Ticklers.FindAsync(Tickler.TicklerId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Tickler> UpdateTickler(Tickler Tickler)
        {
            try
            {
                _db.Entry(Tickler).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Tickler;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTickler(Tickler Tickler)
        {
            try
            {
                var dbTickler = await _db.Ticklers.FindAsync(Tickler.TicklerId);

                if (dbTickler == null)
                {
                    return (false, "Tickler could not be found");
                }

                _db.Ticklers.Remove(Tickler);
                await _db.SaveChangesAsync();

                return (true, "Tickler got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Ticklers
    }
}
