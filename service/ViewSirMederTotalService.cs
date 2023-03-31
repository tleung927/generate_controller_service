using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewSirMederTotalService
    {
        // ViewSirMederTotals Services
        Task<List<ViewSirMederTotal>> GetViewSirMederTotalListByValue(int offset, int limit, string val); // GET All ViewSirMederTotalss
        Task<ViewSirMederTotal> GetViewSirMederTotal(string ViewSirMederTotal_name); // GET Single ViewSirMederTotals        
        Task<List<ViewSirMederTotal>> GetViewSirMederTotalList(string ViewSirMederTotal_name); // GET List ViewSirMederTotals        
        Task<ViewSirMederTotal> AddViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal); // POST New ViewSirMederTotals
        Task<ViewSirMederTotal> UpdateViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal); // PUT ViewSirMederTotals
        Task<(bool, string)> DeleteViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal); // DELETE ViewSirMederTotals
    }

    public class ViewSirMederTotalService : IViewSirMederTotalService
    {
        private readonly XixsrvContext _db;

        public ViewSirMederTotalService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewSirMederTotals

        public async Task<List<ViewSirMederTotal>> GetViewSirMederTotalListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewSirMederTotals.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewSirMederTotal> GetViewSirMederTotal(string ViewSirMederTotal_id)
        {
            try
            {
                return await _db.ViewSirMederTotals.FindAsync(ViewSirMederTotal_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewSirMederTotal>> GetViewSirMederTotalList(string ViewSirMederTotal_id)
        {
            try
            {
                return await _db.ViewSirMederTotals
                    .Where(i => i.ViewSirMederTotalId == ViewSirMederTotal_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewSirMederTotal> AddViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal)
        {
            try
            {
                await _db.ViewSirMederTotals.AddAsync(ViewSirMederTotal);
                await _db.SaveChangesAsync();
                return await _db.ViewSirMederTotals.FindAsync(ViewSirMederTotal.ViewSirMederTotalId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewSirMederTotal> UpdateViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal)
        {
            try
            {
                _db.Entry(ViewSirMederTotal).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewSirMederTotal;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal)
        {
            try
            {
                var dbViewSirMederTotal = await _db.ViewSirMederTotals.FindAsync(ViewSirMederTotal.ViewSirMederTotalId);

                if (dbViewSirMederTotal == null)
                {
                    return (false, "ViewSirMederTotal could not be found");
                }

                _db.ViewSirMederTotals.Remove(ViewSirMederTotal);
                await _db.SaveChangesAsync();

                return (true, "ViewSirMederTotal got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewSirMederTotals
    }
}
