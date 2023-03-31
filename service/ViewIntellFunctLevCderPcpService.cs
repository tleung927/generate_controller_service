using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewIntellFunctLevCderPcpService
    {
        // ViewIntellFunctLevCderPcps Services
        Task<List<ViewIntellFunctLevCderPcp>> GetViewIntellFunctLevCderPcpListByValue(int offset, int limit, string val); // GET All ViewIntellFunctLevCderPcpss
        Task<ViewIntellFunctLevCderPcp> GetViewIntellFunctLevCderPcp(string ViewIntellFunctLevCderPcp_name); // GET Single ViewIntellFunctLevCderPcps        
        Task<List<ViewIntellFunctLevCderPcp>> GetViewIntellFunctLevCderPcpList(string ViewIntellFunctLevCderPcp_name); // GET List ViewIntellFunctLevCderPcps        
        Task<ViewIntellFunctLevCderPcp> AddViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp); // POST New ViewIntellFunctLevCderPcps
        Task<ViewIntellFunctLevCderPcp> UpdateViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp); // PUT ViewIntellFunctLevCderPcps
        Task<(bool, string)> DeleteViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp); // DELETE ViewIntellFunctLevCderPcps
    }

    public class ViewIntellFunctLevCderPcpService : IViewIntellFunctLevCderPcpService
    {
        private readonly XixsrvContext _db;

        public ViewIntellFunctLevCderPcpService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewIntellFunctLevCderPcps

        public async Task<List<ViewIntellFunctLevCderPcp>> GetViewIntellFunctLevCderPcpListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewIntellFunctLevCderPcps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewIntellFunctLevCderPcp> GetViewIntellFunctLevCderPcp(string ViewIntellFunctLevCderPcp_id)
        {
            try
            {
                return await _db.ViewIntellFunctLevCderPcps.FindAsync(ViewIntellFunctLevCderPcp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewIntellFunctLevCderPcp>> GetViewIntellFunctLevCderPcpList(string ViewIntellFunctLevCderPcp_id)
        {
            try
            {
                return await _db.ViewIntellFunctLevCderPcps
                    .Where(i => i.ViewIntellFunctLevCderPcpId == ViewIntellFunctLevCderPcp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewIntellFunctLevCderPcp> AddViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp)
        {
            try
            {
                await _db.ViewIntellFunctLevCderPcps.AddAsync(ViewIntellFunctLevCderPcp);
                await _db.SaveChangesAsync();
                return await _db.ViewIntellFunctLevCderPcps.FindAsync(ViewIntellFunctLevCderPcp.ViewIntellFunctLevCderPcpId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewIntellFunctLevCderPcp> UpdateViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp)
        {
            try
            {
                _db.Entry(ViewIntellFunctLevCderPcp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewIntellFunctLevCderPcp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewIntellFunctLevCderPcp(ViewIntellFunctLevCderPcp ViewIntellFunctLevCderPcp)
        {
            try
            {
                var dbViewIntellFunctLevCderPcp = await _db.ViewIntellFunctLevCderPcps.FindAsync(ViewIntellFunctLevCderPcp.ViewIntellFunctLevCderPcpId);

                if (dbViewIntellFunctLevCderPcp == null)
                {
                    return (false, "ViewIntellFunctLevCderPcp could not be found");
                }

                _db.ViewIntellFunctLevCderPcps.Remove(ViewIntellFunctLevCderPcp);
                await _db.SaveChangesAsync();

                return (true, "ViewIntellFunctLevCderPcp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewIntellFunctLevCderPcps
    }
}
