using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdcdrService
    {
        // Flxdcdrs Services
        Task<List<Flxdcdr>> GetFlxdcdrListByValue(int offset, int limit, string val); // GET All Flxdcdrss
        Task<Flxdcdr> GetFlxdcdr(string Flxdcdr_name); // GET Single Flxdcdrs        
        Task<List<Flxdcdr>> GetFlxdcdrList(string Flxdcdr_name); // GET List Flxdcdrs        
        Task<Flxdcdr> AddFlxdcdr(Flxdcdr Flxdcdr); // POST New Flxdcdrs
        Task<Flxdcdr> UpdateFlxdcdr(Flxdcdr Flxdcdr); // PUT Flxdcdrs
        Task<(bool, string)> DeleteFlxdcdr(Flxdcdr Flxdcdr); // DELETE Flxdcdrs
    }

    public class FlxdcdrService : IFlxdcdrService
    {
        private readonly XixsrvContext _db;

        public FlxdcdrService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdcdrs

        public async Task<List<Flxdcdr>> GetFlxdcdrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdcdrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdcdr> GetFlxdcdr(string Flxdcdr_id)
        {
            try
            {
                return await _db.Flxdcdrs.FindAsync(Flxdcdr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdcdr>> GetFlxdcdrList(string Flxdcdr_id)
        {
            try
            {
                return await _db.Flxdcdrs
                    .Where(i => i.FlxdcdrId == Flxdcdr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdcdr> AddFlxdcdr(Flxdcdr Flxdcdr)
        {
            try
            {
                await _db.Flxdcdrs.AddAsync(Flxdcdr);
                await _db.SaveChangesAsync();
                return await _db.Flxdcdrs.FindAsync(Flxdcdr.FlxdcdrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdcdr> UpdateFlxdcdr(Flxdcdr Flxdcdr)
        {
            try
            {
                _db.Entry(Flxdcdr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdcdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdcdr(Flxdcdr Flxdcdr)
        {
            try
            {
                var dbFlxdcdr = await _db.Flxdcdrs.FindAsync(Flxdcdr.FlxdcdrId);

                if (dbFlxdcdr == null)
                {
                    return (false, "Flxdcdr could not be found");
                }

                _db.Flxdcdrs.Remove(Flxdcdr);
                await _db.SaveChangesAsync();

                return (true, "Flxdcdr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdcdrs
    }
}
