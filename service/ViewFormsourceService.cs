using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewFormsourceService
    {
        // ViewFormsources Services
        Task<List<ViewFormsource>> GetViewFormsourceListByValue(int offset, int limit, string val); // GET All ViewFormsourcess
        Task<ViewFormsource> GetViewFormsource(string ViewFormsource_name); // GET Single ViewFormsources        
        Task<List<ViewFormsource>> GetViewFormsourceList(string ViewFormsource_name); // GET List ViewFormsources        
        Task<ViewFormsource> AddViewFormsource(ViewFormsource ViewFormsource); // POST New ViewFormsources
        Task<ViewFormsource> UpdateViewFormsource(ViewFormsource ViewFormsource); // PUT ViewFormsources
        Task<(bool, string)> DeleteViewFormsource(ViewFormsource ViewFormsource); // DELETE ViewFormsources
    }

    public class ViewFormsourceService : IViewFormsourceService
    {
        private readonly XixsrvContext _db;

        public ViewFormsourceService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewFormsources

        public async Task<List<ViewFormsource>> GetViewFormsourceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewFormsources.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewFormsource> GetViewFormsource(string ViewFormsource_id)
        {
            try
            {
                return await _db.ViewFormsources.FindAsync(ViewFormsource_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewFormsource>> GetViewFormsourceList(string ViewFormsource_id)
        {
            try
            {
                return await _db.ViewFormsources
                    .Where(i => i.ViewFormsourceId == ViewFormsource_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewFormsource> AddViewFormsource(ViewFormsource ViewFormsource)
        {
            try
            {
                await _db.ViewFormsources.AddAsync(ViewFormsource);
                await _db.SaveChangesAsync();
                return await _db.ViewFormsources.FindAsync(ViewFormsource.ViewFormsourceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewFormsource> UpdateViewFormsource(ViewFormsource ViewFormsource)
        {
            try
            {
                _db.Entry(ViewFormsource).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewFormsource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewFormsource(ViewFormsource ViewFormsource)
        {
            try
            {
                var dbViewFormsource = await _db.ViewFormsources.FindAsync(ViewFormsource.ViewFormsourceId);

                if (dbViewFormsource == null)
                {
                    return (false, "ViewFormsource could not be found");
                }

                _db.ViewFormsources.Remove(ViewFormsource);
                await _db.SaveChangesAsync();

                return (true, "ViewFormsource got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewFormsources
    }
}
