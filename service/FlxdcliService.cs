using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdcliService
    {
        // Flxdclis Services
        Task<List<Flxdcli>> GetFlxdcliListByValue(int offset, int limit, string val); // GET All Flxdcliss
        Task<Flxdcli> GetFlxdcli(string Flxdcli_name); // GET Single Flxdclis        
        Task<List<Flxdcli>> GetFlxdcliList(string Flxdcli_name); // GET List Flxdclis        
        Task<Flxdcli> AddFlxdcli(Flxdcli Flxdcli); // POST New Flxdclis
        Task<Flxdcli> UpdateFlxdcli(Flxdcli Flxdcli); // PUT Flxdclis
        Task<(bool, string)> DeleteFlxdcli(Flxdcli Flxdcli); // DELETE Flxdclis
    }

    public class FlxdcliService : IFlxdcliService
    {
        private readonly XixsrvContext _db;

        public FlxdcliService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdclis

        public async Task<List<Flxdcli>> GetFlxdcliListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdclis.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdcli> GetFlxdcli(string Flxdcli_id)
        {
            try
            {
                return await _db.Flxdclis.FindAsync(Flxdcli_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdcli>> GetFlxdcliList(string Flxdcli_id)
        {
            try
            {
                return await _db.Flxdclis
                    .Where(i => i.FlxdcliId == Flxdcli_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdcli> AddFlxdcli(Flxdcli Flxdcli)
        {
            try
            {
                await _db.Flxdclis.AddAsync(Flxdcli);
                await _db.SaveChangesAsync();
                return await _db.Flxdclis.FindAsync(Flxdcli.FlxdcliId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdcli> UpdateFlxdcli(Flxdcli Flxdcli)
        {
            try
            {
                _db.Entry(Flxdcli).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdcli;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdcli(Flxdcli Flxdcli)
        {
            try
            {
                var dbFlxdcli = await _db.Flxdclis.FindAsync(Flxdcli.FlxdcliId);

                if (dbFlxdcli == null)
                {
                    return (false, "Flxdcli could not be found");
                }

                _db.Flxdclis.Remove(Flxdcli);
                await _db.SaveChangesAsync();

                return (true, "Flxdcli got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdclis
    }
}
