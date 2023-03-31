using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewTablesGService
    {
        // ViewTablesGs Services
        Task<List<ViewTablesG>> GetViewTablesGListByValue(int offset, int limit, string val); // GET All ViewTablesGss
        Task<ViewTablesG> GetViewTablesG(string ViewTablesG_name); // GET Single ViewTablesGs        
        Task<List<ViewTablesG>> GetViewTablesGList(string ViewTablesG_name); // GET List ViewTablesGs        
        Task<ViewTablesG> AddViewTablesG(ViewTablesG ViewTablesG); // POST New ViewTablesGs
        Task<ViewTablesG> UpdateViewTablesG(ViewTablesG ViewTablesG); // PUT ViewTablesGs
        Task<(bool, string)> DeleteViewTablesG(ViewTablesG ViewTablesG); // DELETE ViewTablesGs
    }

    public class ViewTablesGService : IViewTablesGService
    {
        private readonly XixsrvContext _db;

        public ViewTablesGService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewTablesGs

        public async Task<List<ViewTablesG>> GetViewTablesGListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewTablesGs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewTablesG> GetViewTablesG(string ViewTablesG_id)
        {
            try
            {
                return await _db.ViewTablesGs.FindAsync(ViewTablesG_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewTablesG>> GetViewTablesGList(string ViewTablesG_id)
        {
            try
            {
                return await _db.ViewTablesGs
                    .Where(i => i.ViewTablesGId == ViewTablesG_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewTablesG> AddViewTablesG(ViewTablesG ViewTablesG)
        {
            try
            {
                await _db.ViewTablesGs.AddAsync(ViewTablesG);
                await _db.SaveChangesAsync();
                return await _db.ViewTablesGs.FindAsync(ViewTablesG.ViewTablesGId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewTablesG> UpdateViewTablesG(ViewTablesG ViewTablesG)
        {
            try
            {
                _db.Entry(ViewTablesG).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewTablesG;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewTablesG(ViewTablesG ViewTablesG)
        {
            try
            {
                var dbViewTablesG = await _db.ViewTablesGs.FindAsync(ViewTablesG.ViewTablesGId);

                if (dbViewTablesG == null)
                {
                    return (false, "ViewTablesG could not be found");
                }

                _db.ViewTablesGs.Remove(ViewTablesG);
                await _db.SaveChangesAsync();

                return (true, "ViewTablesG got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewTablesGs
    }
}
