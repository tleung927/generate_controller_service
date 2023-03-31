using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclifcpService
    {
        // Sclifcps Services
        Task<List<Sclifcp>> GetSclifcpListByValue(int offset, int limit, string val); // GET All Sclifcpss
        Task<Sclifcp> GetSclifcp(string Sclifcp_name); // GET Single Sclifcps        
        Task<List<Sclifcp>> GetSclifcpList(string Sclifcp_name); // GET List Sclifcps        
        Task<Sclifcp> AddSclifcp(Sclifcp Sclifcp); // POST New Sclifcps
        Task<Sclifcp> UpdateSclifcp(Sclifcp Sclifcp); // PUT Sclifcps
        Task<(bool, string)> DeleteSclifcp(Sclifcp Sclifcp); // DELETE Sclifcps
    }

    public class SclifcpService : ISclifcpService
    {
        private readonly XixsrvContext _db;

        public SclifcpService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclifcps

        public async Task<List<Sclifcp>> GetSclifcpListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclifcps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclifcp> GetSclifcp(string Sclifcp_id)
        {
            try
            {
                return await _db.Sclifcps.FindAsync(Sclifcp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclifcp>> GetSclifcpList(string Sclifcp_id)
        {
            try
            {
                return await _db.Sclifcps
                    .Where(i => i.SclifcpId == Sclifcp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclifcp> AddSclifcp(Sclifcp Sclifcp)
        {
            try
            {
                await _db.Sclifcps.AddAsync(Sclifcp);
                await _db.SaveChangesAsync();
                return await _db.Sclifcps.FindAsync(Sclifcp.SclifcpId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclifcp> UpdateSclifcp(Sclifcp Sclifcp)
        {
            try
            {
                _db.Entry(Sclifcp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclifcp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclifcp(Sclifcp Sclifcp)
        {
            try
            {
                var dbSclifcp = await _db.Sclifcps.FindAsync(Sclifcp.SclifcpId);

                if (dbSclifcp == null)
                {
                    return (false, "Sclifcp could not be found");
                }

                _db.Sclifcps.Remove(Sclifcp);
                await _db.SaveChangesAsync();

                return (true, "Sclifcp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclifcps
    }
}
