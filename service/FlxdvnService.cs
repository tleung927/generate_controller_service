using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdvnService
    {
        // Flxdvns Services
        Task<List<Flxdvn>> GetFlxdvnListByValue(int offset, int limit, string val); // GET All Flxdvnss
        Task<Flxdvn> GetFlxdvn(string Flxdvn_name); // GET Single Flxdvns        
        Task<List<Flxdvn>> GetFlxdvnList(string Flxdvn_name); // GET List Flxdvns        
        Task<Flxdvn> AddFlxdvn(Flxdvn Flxdvn); // POST New Flxdvns
        Task<Flxdvn> UpdateFlxdvn(Flxdvn Flxdvn); // PUT Flxdvns
        Task<(bool, string)> DeleteFlxdvn(Flxdvn Flxdvn); // DELETE Flxdvns
    }

    public class FlxdvnService : IFlxdvnService
    {
        private readonly XixsrvContext _db;

        public FlxdvnService(XixsrvContext db)
        {
            _db = db;
        }

        #region Flxdvns

        public async Task<List<Flxdvn>> GetFlxdvnListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Flxdvns.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Flxdvn> GetFlxdvn(string Flxdvn_id)
        {
            try
            {
                return await _db.Flxdvns.FindAsync(Flxdvn_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Flxdvn>> GetFlxdvnList(string Flxdvn_id)
        {
            try
            {
                return await _db.Flxdvns
                    .Where(i => i.FlxdvnId == Flxdvn_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Flxdvn> AddFlxdvn(Flxdvn Flxdvn)
        {
            try
            {
                await _db.Flxdvns.AddAsync(Flxdvn);
                await _db.SaveChangesAsync();
                return await _db.Flxdvns.FindAsync(Flxdvn.FlxdvnId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Flxdvn> UpdateFlxdvn(Flxdvn Flxdvn)
        {
            try
            {
                _db.Entry(Flxdvn).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Flxdvn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdvn(Flxdvn Flxdvn)
        {
            try
            {
                var dbFlxdvn = await _db.Flxdvns.FindAsync(Flxdvn.FlxdvnId);

                if (dbFlxdvn == null)
                {
                    return (false, "Flxdvn could not be found");
                }

                _db.Flxdvns.Remove(Flxdvn);
                await _db.SaveChangesAsync();

                return (true, "Flxdvn got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Flxdvns
    }
}
