using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleDispositionService
    {
        // ViewScheduleDispositions Services
        Task<List<ViewScheduleDisposition>> GetViewScheduleDispositionListByValue(int offset, int limit, string val); // GET All ViewScheduleDispositionss
        Task<ViewScheduleDisposition> GetViewScheduleDisposition(string ViewScheduleDisposition_name); // GET Single ViewScheduleDispositions        
        Task<List<ViewScheduleDisposition>> GetViewScheduleDispositionList(string ViewScheduleDisposition_name); // GET List ViewScheduleDispositions        
        Task<ViewScheduleDisposition> AddViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition); // POST New ViewScheduleDispositions
        Task<ViewScheduleDisposition> UpdateViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition); // PUT ViewScheduleDispositions
        Task<(bool, string)> DeleteViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition); // DELETE ViewScheduleDispositions
    }

    public class ViewScheduleDispositionService : IViewScheduleDispositionService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleDispositionService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleDispositions

        public async Task<List<ViewScheduleDisposition>> GetViewScheduleDispositionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleDispositions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleDisposition> GetViewScheduleDisposition(string ViewScheduleDisposition_id)
        {
            try
            {
                return await _db.ViewScheduleDispositions.FindAsync(ViewScheduleDisposition_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleDisposition>> GetViewScheduleDispositionList(string ViewScheduleDisposition_id)
        {
            try
            {
                return await _db.ViewScheduleDispositions
                    .Where(i => i.ViewScheduleDispositionId == ViewScheduleDisposition_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleDisposition> AddViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition)
        {
            try
            {
                await _db.ViewScheduleDispositions.AddAsync(ViewScheduleDisposition);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleDispositions.FindAsync(ViewScheduleDisposition.ViewScheduleDispositionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleDisposition> UpdateViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition)
        {
            try
            {
                _db.Entry(ViewScheduleDisposition).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleDisposition;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition)
        {
            try
            {
                var dbViewScheduleDisposition = await _db.ViewScheduleDispositions.FindAsync(ViewScheduleDisposition.ViewScheduleDispositionId);

                if (dbViewScheduleDisposition == null)
                {
                    return (false, "ViewScheduleDisposition could not be found");
                }

                _db.ViewScheduleDispositions.Remove(ViewScheduleDisposition);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleDisposition got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleDispositions
    }
}
