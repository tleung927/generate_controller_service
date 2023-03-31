using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdcdeService
    {
        // Flxdcdes Services
        Task<List<Flxdcde>> GetFlxdcdeListByValue(int offset, int limit, string val); // GET All Flxdcdess
        Task<Flxdcde> GetFlxdcde(string Flxdcde_name); // GET Single Flxdcdes        
        Task<List<Flxdcde>> GetFlxdcdeList(string Flxdcde_name); // GET List Flxdcdes        
        Task<Flxdcde> AddFlxdcde(Flxdcde Flxdcde); // POST New Flxdcdes
        Task<Flxdcde> UpdateFlxdcde(Flxdcde Flxdcde); // PUT Flxdcdes
        Task<(bool, string)> DeleteFlxdcde(Flxdcde Flxdcde); // DELETE Flxdcdes
    }

    public class FlxdcdeService : IFlxdcdeService
    {
        private readonly XixsrvContext _db;

        public FlxdcdeService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdcdes

        public async Task<List<Flxdcde>> GetFlxdcdeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdcdes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdcde> GetFlxdcde(string Flxdcde_id)
        {
            try
            {
                return await _db.Flxdcdes.FindAsync(Flxdcde_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdcde>> GetFlxdcdeList(string Flxdcde_id)
        {
            try
            {
                return await _db.Flxdcdes
                    .Where(i => i.FlxdcdeId == Flxdcde_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdcde> AddFlxdcde(Flxdcde Flxdcde)
        {
            try
            {
                await _db.Flxdcdes.AddAsync(Flxdcde);
                await _db.SaveChangesAsync();
                return await _db.Flxdcdes.FindAsync(Flxdcde.FlxdcdeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdcde> UpdateFlxdcde(Flxdcde Flxdcde)
        {
            try
            {
                _db.Entry(Flxdcde).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdcde;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdcde(Flxdcde Flxdcde)
        {
            try
            {
                var dbFlxdcde = await _db.Flxdcdes.FindAsync(Flxdcde.FlxdcdeId);

                if (dbFlxdcde == null)
                {
                    return (false, "Flxdcde could not be found");
                }

                _db.Flxdcdes.Remove(Flxdcde);
                await _db.SaveChangesAsync();

                return (true, "Flxdcde got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdcdes
    }
}
