using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewCderCpService
    {
        // ViewCderCps Services
        Task<List<ViewCderCp>> GetViewCderCpListByValue(int offset, int limit, string val); // GET All ViewCderCpss
        Task<ViewCderCp> GetViewCderCp(string ViewCderCp_name); // GET Single ViewCderCps        
        Task<List<ViewCderCp>> GetViewCderCpList(string ViewCderCp_name); // GET List ViewCderCps        
        Task<ViewCderCp> AddViewCderCp(ViewCderCp ViewCderCp); // POST New ViewCderCps
        Task<ViewCderCp> UpdateViewCderCp(ViewCderCp ViewCderCp); // PUT ViewCderCps
        Task<(bool, string)> DeleteViewCderCp(ViewCderCp ViewCderCp); // DELETE ViewCderCps
    }

    public class ViewCderCpService : IViewCderCpService
    {
        private readonly XixsrvContext _db;

        public ViewCderCpService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewCderCps

        public async Task<List<ViewCderCp>> GetViewCderCpListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewCderCps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewCderCp> GetViewCderCp(string ViewCderCp_id)
        {
            try
            {
                return await _db.ViewCderCps.FindAsync(ViewCderCp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewCderCp>> GetViewCderCpList(string ViewCderCp_id)
        {
            try
            {
                return await _db.ViewCderCps
                    .Where(i => i.ViewCderCpId == ViewCderCp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewCderCp> AddViewCderCp(ViewCderCp ViewCderCp)
        {
            try
            {
                await _db.ViewCderCps.AddAsync(ViewCderCp);
                await _db.SaveChangesAsync();
                return await _db.ViewCderCps.FindAsync(ViewCderCp.ViewCderCpId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewCderCp> UpdateViewCderCp(ViewCderCp ViewCderCp)
        {
            try
            {
                _db.Entry(ViewCderCp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewCderCp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewCderCp(ViewCderCp ViewCderCp)
        {
            try
            {
                var dbViewCderCp = await _db.ViewCderCps.FindAsync(ViewCderCp.ViewCderCpId);

                if (dbViewCderCp == null)
                {
                    return (false, "ViewCderCp could not be found");
                }

                _db.ViewCderCps.Remove(ViewCderCp);
                await _db.SaveChangesAsync();

                return (true, "ViewCderCp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewCderCps
    }
}
