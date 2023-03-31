using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewTablesLService
    {
        // ViewTablesLs Services
        Task<List<ViewTablesL>> GetViewTablesLListByValue(int offset, int limit, string val); // GET All ViewTablesLss
        Task<ViewTablesL> GetViewTablesL(string ViewTablesL_name); // GET Single ViewTablesLs        
        Task<List<ViewTablesL>> GetViewTablesLList(string ViewTablesL_name); // GET List ViewTablesLs        
        Task<ViewTablesL> AddViewTablesL(ViewTablesL ViewTablesL); // POST New ViewTablesLs
        Task<ViewTablesL> UpdateViewTablesL(ViewTablesL ViewTablesL); // PUT ViewTablesLs
        Task<(bool, string)> DeleteViewTablesL(ViewTablesL ViewTablesL); // DELETE ViewTablesLs
    }

    public class ViewTablesLService : IViewTablesLService
    {
        private readonly XixsrvContext _db;

        public ViewTablesLService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewTablesLs

        public async Task<List<ViewTablesL>> GetViewTablesLListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewTablesLs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewTablesL> GetViewTablesL(string ViewTablesL_id)
        {
            try
            {
                return await _db.ViewTablesLs.FindAsync(ViewTablesL_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewTablesL>> GetViewTablesLList(string ViewTablesL_id)
        {
            try
            {
                return await _db.ViewTablesLs
                    .Where(i => i.ViewTablesLId == ViewTablesL_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewTablesL> AddViewTablesL(ViewTablesL ViewTablesL)
        {
            try
            {
                await _db.ViewTablesLs.AddAsync(ViewTablesL);
                await _db.SaveChangesAsync();
                return await _db.ViewTablesLs.FindAsync(ViewTablesL.ViewTablesLId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewTablesL> UpdateViewTablesL(ViewTablesL ViewTablesL)
        {
            try
            {
                _db.Entry(ViewTablesL).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewTablesL;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewTablesL(ViewTablesL ViewTablesL)
        {
            try
            {
                var dbViewTablesL = await _db.ViewTablesLs.FindAsync(ViewTablesL.ViewTablesLId);

                if (dbViewTablesL == null)
                {
                    return (false, "ViewTablesL could not be found");
                }

                _db.ViewTablesLs.Remove(ViewTablesL);
                await _db.SaveChangesAsync();

                return (true, "ViewTablesL got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewTablesLs
    }
}
