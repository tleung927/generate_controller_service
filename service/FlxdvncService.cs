using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdvncService
    {
        // Flxdvncs Services
        Task<List<Flxdvnc>> GetFlxdvncListByValue(int offset, int limit, string val); // GET All Flxdvncss
        Task<Flxdvnc> GetFlxdvnc(string Flxdvnc_name); // GET Single Flxdvncs        
        Task<List<Flxdvnc>> GetFlxdvncList(string Flxdvnc_name); // GET List Flxdvncs        
        Task<Flxdvnc> AddFlxdvnc(Flxdvnc Flxdvnc); // POST New Flxdvncs
        Task<Flxdvnc> UpdateFlxdvnc(Flxdvnc Flxdvnc); // PUT Flxdvncs
        Task<(bool, string)> DeleteFlxdvnc(Flxdvnc Flxdvnc); // DELETE Flxdvncs
    }

    public class FlxdvncService : IFlxdvncService
    {
        private readonly XixsrvContext _db;

        public FlxdvncService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdvncs

        public async Task<List<Flxdvnc>> GetFlxdvncListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdvncs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdvnc> GetFlxdvnc(string Flxdvnc_id)
        {
            try
            {
                return await _db.Flxdvncs.FindAsync(Flxdvnc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdvnc>> GetFlxdvncList(string Flxdvnc_id)
        {
            try
            {
                return await _db.Flxdvncs
                    .Where(i => i.FlxdvncId == Flxdvnc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdvnc> AddFlxdvnc(Flxdvnc Flxdvnc)
        {
            try
            {
                await _db.Flxdvncs.AddAsync(Flxdvnc);
                await _db.SaveChangesAsync();
                return await _db.Flxdvncs.FindAsync(Flxdvnc.FlxdvncId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdvnc> UpdateFlxdvnc(Flxdvnc Flxdvnc)
        {
            try
            {
                _db.Entry(Flxdvnc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdvnc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdvnc(Flxdvnc Flxdvnc)
        {
            try
            {
                var dbFlxdvnc = await _db.Flxdvncs.FindAsync(Flxdvnc.FlxdvncId);

                if (dbFlxdvnc == null)
                {
                    return (false, "Flxdvnc could not be found");
                }

                _db.Flxdvncs.Remove(Flxdvnc);
                await _db.SaveChangesAsync();

                return (true, "Flxdvnc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdvncs
    }
}
