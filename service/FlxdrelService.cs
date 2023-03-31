using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdrelService
    {
        // Flxdrels Services
        Task<List<Flxdrel>> GetFlxdrelListByValue(int offset, int limit, string val); // GET All Flxdrelss
        Task<Flxdrel> GetFlxdrel(string Flxdrel_name); // GET Single Flxdrels        
        Task<List<Flxdrel>> GetFlxdrelList(string Flxdrel_name); // GET List Flxdrels        
        Task<Flxdrel> AddFlxdrel(Flxdrel Flxdrel); // POST New Flxdrels
        Task<Flxdrel> UpdateFlxdrel(Flxdrel Flxdrel); // PUT Flxdrels
        Task<(bool, string)> DeleteFlxdrel(Flxdrel Flxdrel); // DELETE Flxdrels
    }

    public class FlxdrelService : IFlxdrelService
    {
        private readonly XixsrvContext _db;

        public FlxdrelService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdrels

        public async Task<List<Flxdrel>> GetFlxdrelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdrels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdrel> GetFlxdrel(string Flxdrel_id)
        {
            try
            {
                return await _db.Flxdrels.FindAsync(Flxdrel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdrel>> GetFlxdrelList(string Flxdrel_id)
        {
            try
            {
                return await _db.Flxdrels
                    .Where(i => i.FlxdrelId == Flxdrel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdrel> AddFlxdrel(Flxdrel Flxdrel)
        {
            try
            {
                await _db.Flxdrels.AddAsync(Flxdrel);
                await _db.SaveChangesAsync();
                return await _db.Flxdrels.FindAsync(Flxdrel.FlxdrelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdrel> UpdateFlxdrel(Flxdrel Flxdrel)
        {
            try
            {
                _db.Entry(Flxdrel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdrel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdrel(Flxdrel Flxdrel)
        {
            try
            {
                var dbFlxdrel = await _db.Flxdrels.FindAsync(Flxdrel.FlxdrelId);

                if (dbFlxdrel == null)
                {
                    return (false, "Flxdrel could not be found");
                }

                _db.Flxdrels.Remove(Flxdrel);
                await _db.SaveChangesAsync();

                return (true, "Flxdrel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdrels
    }
}
