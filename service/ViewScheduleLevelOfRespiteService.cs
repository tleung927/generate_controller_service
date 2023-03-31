using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleLevelOfRespiteService
    {
        // ViewScheduleLevelOfRespites Services
        Task<List<ViewScheduleLevelOfRespite>> GetViewScheduleLevelOfRespiteListByValue(int offset, int limit, string val); // GET All ViewScheduleLevelOfRespitess
        Task<ViewScheduleLevelOfRespite> GetViewScheduleLevelOfRespite(string ViewScheduleLevelOfRespite_name); // GET Single ViewScheduleLevelOfRespites        
        Task<List<ViewScheduleLevelOfRespite>> GetViewScheduleLevelOfRespiteList(string ViewScheduleLevelOfRespite_name); // GET List ViewScheduleLevelOfRespites        
        Task<ViewScheduleLevelOfRespite> AddViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite); // POST New ViewScheduleLevelOfRespites
        Task<ViewScheduleLevelOfRespite> UpdateViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite); // PUT ViewScheduleLevelOfRespites
        Task<(bool, string)> DeleteViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite); // DELETE ViewScheduleLevelOfRespites
    }

    public class ViewScheduleLevelOfRespiteService : IViewScheduleLevelOfRespiteService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleLevelOfRespiteService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleLevelOfRespites

        public async Task<List<ViewScheduleLevelOfRespite>> GetViewScheduleLevelOfRespiteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleLevelOfRespites.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleLevelOfRespite> GetViewScheduleLevelOfRespite(string ViewScheduleLevelOfRespite_id)
        {
            try
            {
                return await _db.ViewScheduleLevelOfRespites.FindAsync(ViewScheduleLevelOfRespite_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleLevelOfRespite>> GetViewScheduleLevelOfRespiteList(string ViewScheduleLevelOfRespite_id)
        {
            try
            {
                return await _db.ViewScheduleLevelOfRespites
                    .Where(i => i.ViewScheduleLevelOfRespiteId == ViewScheduleLevelOfRespite_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleLevelOfRespite> AddViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite)
        {
            try
            {
                await _db.ViewScheduleLevelOfRespites.AddAsync(ViewScheduleLevelOfRespite);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleLevelOfRespites.FindAsync(ViewScheduleLevelOfRespite.ViewScheduleLevelOfRespiteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleLevelOfRespite> UpdateViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite)
        {
            try
            {
                _db.Entry(ViewScheduleLevelOfRespite).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleLevelOfRespite;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite)
        {
            try
            {
                var dbViewScheduleLevelOfRespite = await _db.ViewScheduleLevelOfRespites.FindAsync(ViewScheduleLevelOfRespite.ViewScheduleLevelOfRespiteId);

                if (dbViewScheduleLevelOfRespite == null)
                {
                    return (false, "ViewScheduleLevelOfRespite could not be found");
                }

                _db.ViewScheduleLevelOfRespites.Remove(ViewScheduleLevelOfRespite);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleLevelOfRespite got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleLevelOfRespites
    }
}
