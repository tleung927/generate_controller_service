using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPcpService
    {
        // Pcps Services
        Task<List<Pcp>> GetPcpListByValue(int offset, int limit, string val); // GET All Pcpss
        Task<Pcp> GetPcp(string Pcp_name); // GET Single Pcps        
        Task<List<Pcp>> GetPcpList(string Pcp_name); // GET List Pcps        
        Task<Pcp> AddPcp(Pcp Pcp); // POST New Pcps
        Task<Pcp> UpdatePcp(Pcp Pcp); // PUT Pcps
        Task<(bool, string)> DeletePcp(Pcp Pcp); // DELETE Pcps
    }

    public class PcpService : IPcpService
    {
        private readonly XixsrvContext _db;

        public PcpService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pcps

        public async Task<List<Pcp>> GetPcpListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pcps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pcp> GetPcp(string Pcp_id)
        {
            try
            {
                return await _db.Pcps.FindAsync(Pcp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pcp>> GetPcpList(string Pcp_id)
        {
            try
            {
                return await _db.Pcps
                    .Where(i => i.PcpId == Pcp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pcp> AddPcp(Pcp Pcp)
        {
            try
            {
                await _db.Pcps.AddAsync(Pcp);
                await _db.SaveChangesAsync();
                return await _db.Pcps.FindAsync(Pcp.PcpId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pcp> UpdatePcp(Pcp Pcp)
        {
            try
            {
                _db.Entry(Pcp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pcp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePcp(Pcp Pcp)
        {
            try
            {
                var dbPcp = await _db.Pcps.FindAsync(Pcp.PcpId);

                if (dbPcp == null)
                {
                    return (false, "Pcp could not be found");
                }

                _db.Pcps.Remove(Pcp);
                await _db.SaveChangesAsync();

                return (true, "Pcp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pcps
    }
}
