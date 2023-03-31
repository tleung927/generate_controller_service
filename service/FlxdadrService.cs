using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdadrService
    {
        // Flxdadrs Services
        Task<List<Flxdadr>> GetFlxdadrListByValue(int offset, int limit, string val); // GET All Flxdadrss
        Task<Flxdadr> GetFlxdadr(string Flxdadr_name); // GET Single Flxdadrs        
        Task<List<Flxdadr>> GetFlxdadrList(string Flxdadr_name); // GET List Flxdadrs        
        Task<Flxdadr> AddFlxdadr(Flxdadr Flxdadr); // POST New Flxdadrs
        Task<Flxdadr> UpdateFlxdadr(Flxdadr Flxdadr); // PUT Flxdadrs
        Task<(bool, string)> DeleteFlxdadr(Flxdadr Flxdadr); // DELETE Flxdadrs
    }

    public class FlxdadrService : IFlxdadrService
    {
        private readonly XixsrvContext _db;

        public FlxdadrService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdadrs

        public async Task<List<Flxdadr>> GetFlxdadrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdadrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdadr> GetFlxdadr(string Flxdadr_id)
        {
            try
            {
                return await _db.Flxdadrs.FindAsync(Flxdadr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdadr>> GetFlxdadrList(string Flxdadr_id)
        {
            try
            {
                return await _db.Flxdadrs
                    .Where(i => i.FlxdadrId == Flxdadr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdadr> AddFlxdadr(Flxdadr Flxdadr)
        {
            try
            {
                await _db.Flxdadrs.AddAsync(Flxdadr);
                await _db.SaveChangesAsync();
                return await _db.Flxdadrs.FindAsync(Flxdadr.FlxdadrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdadr> UpdateFlxdadr(Flxdadr Flxdadr)
        {
            try
            {
                _db.Entry(Flxdadr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdadr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdadr(Flxdadr Flxdadr)
        {
            try
            {
                var dbFlxdadr = await _db.Flxdadrs.FindAsync(Flxdadr.FlxdadrId);

                if (dbFlxdadr == null)
                {
                    return (false, "Flxdadr could not be found");
                }

                _db.Flxdadrs.Remove(Flxdadr);
                await _db.SaveChangesAsync();

                return (true, "Flxdadr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdadrs
    }
}
