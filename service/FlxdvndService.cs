using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdvndService
    {
        // Flxdvnds Services
        Task<List<Flxdvnd>> GetFlxdvndListByValue(int offset, int limit, string val); // GET All Flxdvndss
        Task<Flxdvnd> GetFlxdvnd(string Flxdvnd_name); // GET Single Flxdvnds        
        Task<List<Flxdvnd>> GetFlxdvndList(string Flxdvnd_name); // GET List Flxdvnds        
        Task<Flxdvnd> AddFlxdvnd(Flxdvnd Flxdvnd); // POST New Flxdvnds
        Task<Flxdvnd> UpdateFlxdvnd(Flxdvnd Flxdvnd); // PUT Flxdvnds
        Task<(bool, string)> DeleteFlxdvnd(Flxdvnd Flxdvnd); // DELETE Flxdvnds
    }

    public class FlxdvndService : IFlxdvndService
    {
        private readonly XixsrvContext _db;

        public FlxdvndService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdvnds

        public async Task<List<Flxdvnd>> GetFlxdvndListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdvnds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdvnd> GetFlxdvnd(string Flxdvnd_id)
        {
            try
            {
                return await _db.Flxdvnds.FindAsync(Flxdvnd_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdvnd>> GetFlxdvndList(string Flxdvnd_id)
        {
            try
            {
                return await _db.Flxdvnds
                    .Where(i => i.FlxdvndId == Flxdvnd_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdvnd> AddFlxdvnd(Flxdvnd Flxdvnd)
        {
            try
            {
                await _db.Flxdvnds.AddAsync(Flxdvnd);
                await _db.SaveChangesAsync();
                return await _db.Flxdvnds.FindAsync(Flxdvnd.FlxdvndId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdvnd> UpdateFlxdvnd(Flxdvnd Flxdvnd)
        {
            try
            {
                _db.Entry(Flxdvnd).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdvnd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdvnd(Flxdvnd Flxdvnd)
        {
            try
            {
                var dbFlxdvnd = await _db.Flxdvnds.FindAsync(Flxdvnd.FlxdvndId);

                if (dbFlxdvnd == null)
                {
                    return (false, "Flxdvnd could not be found");
                }

                _db.Flxdvnds.Remove(Flxdvnd);
                await _db.SaveChangesAsync();

                return (true, "Flxdvnd got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdvnds
    }
}
