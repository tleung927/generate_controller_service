using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewRService
    {
        // ViewRs Services
        Task<List<ViewR>> GetViewRListByValue(int offset, int limit, string val); // GET All ViewRss
        Task<ViewR> GetViewR(string ViewR_name); // GET Single ViewRs        
        Task<List<ViewR>> GetViewRList(string ViewR_name); // GET List ViewRs        
        Task<ViewR> AddViewR(ViewR ViewR); // POST New ViewRs
        Task<ViewR> UpdateViewR(ViewR ViewR); // PUT ViewRs
        Task<(bool, string)> DeleteViewR(ViewR ViewR); // DELETE ViewRs
    }

    public class ViewRService : IViewRService
    {
        private readonly XixsrvContext _db;

        public ViewRService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewRs

        public async Task<List<ViewR>> GetViewRListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewRs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewR> GetViewR(string ViewR_id)
        {
            try
            {
                return await _db.ViewRs.FindAsync(ViewR_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewR>> GetViewRList(string ViewR_id)
        {
            try
            {
                return await _db.ViewRs
                    .Where(i => i.ViewRId == ViewR_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewR> AddViewR(ViewR ViewR)
        {
            try
            {
                await _db.ViewRs.AddAsync(ViewR);
                await _db.SaveChangesAsync();
                return await _db.ViewRs.FindAsync(ViewR.ViewRId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewR> UpdateViewR(ViewR ViewR)
        {
            try
            {
                _db.Entry(ViewR).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewR;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewR(ViewR ViewR)
        {
            try
            {
                var dbViewR = await _db.ViewRs.FindAsync(ViewR.ViewRId);

                if (dbViewR == null)
                {
                    return (false, "ViewR could not be found");
                }

                _db.ViewRs.Remove(ViewR);
                await _db.SaveChangesAsync();

                return (true, "ViewR got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewRs
    }
}
