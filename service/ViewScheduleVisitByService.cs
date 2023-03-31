using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleVisitByService
    {
        // ViewScheduleVisitBys Services
        Task<List<ViewScheduleVisitBy>> GetViewScheduleVisitByListByValue(int offset, int limit, string val); // GET All ViewScheduleVisitByss
        Task<ViewScheduleVisitBy> GetViewScheduleVisitBy(string ViewScheduleVisitBy_name); // GET Single ViewScheduleVisitBys        
        Task<List<ViewScheduleVisitBy>> GetViewScheduleVisitByList(string ViewScheduleVisitBy_name); // GET List ViewScheduleVisitBys        
        Task<ViewScheduleVisitBy> AddViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy); // POST New ViewScheduleVisitBys
        Task<ViewScheduleVisitBy> UpdateViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy); // PUT ViewScheduleVisitBys
        Task<(bool, string)> DeleteViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy); // DELETE ViewScheduleVisitBys
    }

    public class ViewScheduleVisitByService : IViewScheduleVisitByService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleVisitByService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleVisitBys

        public async Task<List<ViewScheduleVisitBy>> GetViewScheduleVisitByListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleVisitBys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleVisitBy> GetViewScheduleVisitBy(string ViewScheduleVisitBy_id)
        {
            try
            {
                return await _db.ViewScheduleVisitBys.FindAsync(ViewScheduleVisitBy_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleVisitBy>> GetViewScheduleVisitByList(string ViewScheduleVisitBy_id)
        {
            try
            {
                return await _db.ViewScheduleVisitBys
                    .Where(i => i.ViewScheduleVisitById == ViewScheduleVisitBy_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleVisitBy> AddViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy)
        {
            try
            {
                await _db.ViewScheduleVisitBys.AddAsync(ViewScheduleVisitBy);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleVisitBys.FindAsync(ViewScheduleVisitBy.ViewScheduleVisitById); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleVisitBy> UpdateViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy)
        {
            try
            {
                _db.Entry(ViewScheduleVisitBy).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleVisitBy;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy)
        {
            try
            {
                var dbViewScheduleVisitBy = await _db.ViewScheduleVisitBys.FindAsync(ViewScheduleVisitBy.ViewScheduleVisitById);

                if (dbViewScheduleVisitBy == null)
                {
                    return (false, "ViewScheduleVisitBy could not be found");
                }

                _db.ViewScheduleVisitBys.Remove(ViewScheduleVisitBy);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleVisitBy got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleVisitBys
    }
}
