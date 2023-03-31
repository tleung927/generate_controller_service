using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleDispositionForService
    {
        // ViewScheduleDispositionFors Services
        Task<List<ViewScheduleDispositionFor>> GetViewScheduleDispositionForListByValue(int offset, int limit, string val); // GET All ViewScheduleDispositionForss
        Task<ViewScheduleDispositionFor> GetViewScheduleDispositionFor(string ViewScheduleDispositionFor_name); // GET Single ViewScheduleDispositionFors        
        Task<List<ViewScheduleDispositionFor>> GetViewScheduleDispositionForList(string ViewScheduleDispositionFor_name); // GET List ViewScheduleDispositionFors        
        Task<ViewScheduleDispositionFor> AddViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor); // POST New ViewScheduleDispositionFors
        Task<ViewScheduleDispositionFor> UpdateViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor); // PUT ViewScheduleDispositionFors
        Task<(bool, string)> DeleteViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor); // DELETE ViewScheduleDispositionFors
    }

    public class ViewScheduleDispositionForService : IViewScheduleDispositionForService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleDispositionForService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleDispositionFors

        public async Task<List<ViewScheduleDispositionFor>> GetViewScheduleDispositionForListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleDispositionFors.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleDispositionFor> GetViewScheduleDispositionFor(string ViewScheduleDispositionFor_id)
        {
            try
            {
                return await _db.ViewScheduleDispositionFors.FindAsync(ViewScheduleDispositionFor_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleDispositionFor>> GetViewScheduleDispositionForList(string ViewScheduleDispositionFor_id)
        {
            try
            {
                return await _db.ViewScheduleDispositionFors
                    .Where(i => i.ViewScheduleDispositionForId == ViewScheduleDispositionFor_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleDispositionFor> AddViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor)
        {
            try
            {
                await _db.ViewScheduleDispositionFors.AddAsync(ViewScheduleDispositionFor);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleDispositionFors.FindAsync(ViewScheduleDispositionFor.ViewScheduleDispositionForId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleDispositionFor> UpdateViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor)
        {
            try
            {
                _db.Entry(ViewScheduleDispositionFor).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleDispositionFor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor)
        {
            try
            {
                var dbViewScheduleDispositionFor = await _db.ViewScheduleDispositionFors.FindAsync(ViewScheduleDispositionFor.ViewScheduleDispositionForId);

                if (dbViewScheduleDispositionFor == null)
                {
                    return (false, "ViewScheduleDispositionFor could not be found");
                }

                _db.ViewScheduleDispositionFors.Remove(ViewScheduleDispositionFor);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleDispositionFor got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleDispositionFors
    }
}
