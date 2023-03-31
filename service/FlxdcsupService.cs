using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdcsupService
    {
        // Flxdcsups Services
        Task<List<Flxdcsup>> GetFlxdcsupListByValue(int offset, int limit, string val); // GET All Flxdcsupss
        Task<Flxdcsup> GetFlxdcsup(string Flxdcsup_name); // GET Single Flxdcsups        
        Task<List<Flxdcsup>> GetFlxdcsupList(string Flxdcsup_name); // GET List Flxdcsups        
        Task<Flxdcsup> AddFlxdcsup(Flxdcsup Flxdcsup); // POST New Flxdcsups
        Task<Flxdcsup> UpdateFlxdcsup(Flxdcsup Flxdcsup); // PUT Flxdcsups
        Task<(bool, string)> DeleteFlxdcsup(Flxdcsup Flxdcsup); // DELETE Flxdcsups
    }

    public class FlxdcsupService : IFlxdcsupService
    {
        private readonly XixsrvContext _db;

        public FlxdcsupService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdcsups

        public async Task<List<Flxdcsup>> GetFlxdcsupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdcsups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdcsup> GetFlxdcsup(string Flxdcsup_id)
        {
            try
            {
                return await _db.Flxdcsups.FindAsync(Flxdcsup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdcsup>> GetFlxdcsupList(string Flxdcsup_id)
        {
            try
            {
                return await _db.Flxdcsups
                    .Where(i => i.FlxdcsupId == Flxdcsup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdcsup> AddFlxdcsup(Flxdcsup Flxdcsup)
        {
            try
            {
                await _db.Flxdcsups.AddAsync(Flxdcsup);
                await _db.SaveChangesAsync();
                return await _db.Flxdcsups.FindAsync(Flxdcsup.FlxdcsupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdcsup> UpdateFlxdcsup(Flxdcsup Flxdcsup)
        {
            try
            {
                _db.Entry(Flxdcsup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdcsup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdcsup(Flxdcsup Flxdcsup)
        {
            try
            {
                var dbFlxdcsup = await _db.Flxdcsups.FindAsync(Flxdcsup.FlxdcsupId);

                if (dbFlxdcsup == null)
                {
                    return (false, "Flxdcsup could not be found");
                }

                _db.Flxdcsups.Remove(Flxdcsup);
                await _db.SaveChangesAsync();

                return (true, "Flxdcsup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdcsups
    }
}
