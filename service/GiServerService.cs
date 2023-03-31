using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGiServerService
    {
        // GiServers Services
        Task<List<GiServer>> GetGiServerListByValue(int offset, int limit, string val); // GET All GiServerss
        Task<GiServer> GetGiServer(string GiServer_name); // GET Single GiServers        
        Task<List<GiServer>> GetGiServerList(string GiServer_name); // GET List GiServers        
        Task<GiServer> AddGiServer(GiServer GiServer); // POST New GiServers
        Task<GiServer> UpdateGiServer(GiServer GiServer); // PUT GiServers
        Task<(bool, string)> DeleteGiServer(GiServer GiServer); // DELETE GiServers
    }

    public class GiServerService : IGiServerService
    {
        private readonly XixsrvContext _db;

        public GiServerService(XixsrvContext db)
        {
            _db = db;
        }

        #region GiServers

        public async Task<List<GiServer>> GetGiServerListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.GiServers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GiServer> GetGiServer(string GiServer_id)
        {
            try
            {
                return await _db.GiServers.FindAsync(GiServer_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GiServer>> GetGiServerList(string GiServer_id)
        {
            try
            {
                return await _db.GiServers
                    .Where(i => i.GiServerId == GiServer_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<GiServer> AddGiServer(GiServer GiServer)
        {
            try
            {
                await _db.GiServers.AddAsync(GiServer);
                await _db.SaveChangesAsync();
                return await _db.GiServers.FindAsync(GiServer.GiServerId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<GiServer> UpdateGiServer(GiServer GiServer)
        {
            try
            {
                _db.Entry(GiServer).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return GiServer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGiServer(GiServer GiServer)
        {
            try
            {
                var dbGiServer = await _db.GiServers.FindAsync(GiServer.GiServerId);

                if (dbGiServer == null)
                {
                    return (false, "GiServer could not be found");
                }

                _db.GiServers.Remove(GiServer);
                await _db.SaveChangesAsync();

                return (true, "GiServer got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion GiServers
    }
}
