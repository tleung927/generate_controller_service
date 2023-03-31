using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleTypePresentService
    {
        // ViewScheduleTypePresents Services
        Task<List<ViewScheduleTypePresent>> GetViewScheduleTypePresentListByValue(int offset, int limit, string val); // GET All ViewScheduleTypePresentss
        Task<ViewScheduleTypePresent> GetViewScheduleTypePresent(string ViewScheduleTypePresent_name); // GET Single ViewScheduleTypePresents        
        Task<List<ViewScheduleTypePresent>> GetViewScheduleTypePresentList(string ViewScheduleTypePresent_name); // GET List ViewScheduleTypePresents        
        Task<ViewScheduleTypePresent> AddViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent); // POST New ViewScheduleTypePresents
        Task<ViewScheduleTypePresent> UpdateViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent); // PUT ViewScheduleTypePresents
        Task<(bool, string)> DeleteViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent); // DELETE ViewScheduleTypePresents
    }

    public class ViewScheduleTypePresentService : IViewScheduleTypePresentService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleTypePresentService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleTypePresents

        public async Task<List<ViewScheduleTypePresent>> GetViewScheduleTypePresentListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleTypePresents.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleTypePresent> GetViewScheduleTypePresent(string ViewScheduleTypePresent_id)
        {
            try
            {
                return await _db.ViewScheduleTypePresents.FindAsync(ViewScheduleTypePresent_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleTypePresent>> GetViewScheduleTypePresentList(string ViewScheduleTypePresent_id)
        {
            try
            {
                return await _db.ViewScheduleTypePresents
                    .Where(i => i.ViewScheduleTypePresentId == ViewScheduleTypePresent_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleTypePresent> AddViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent)
        {
            try
            {
                await _db.ViewScheduleTypePresents.AddAsync(ViewScheduleTypePresent);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleTypePresents.FindAsync(ViewScheduleTypePresent.ViewScheduleTypePresentId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleTypePresent> UpdateViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent)
        {
            try
            {
                _db.Entry(ViewScheduleTypePresent).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleTypePresent;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent)
        {
            try
            {
                var dbViewScheduleTypePresent = await _db.ViewScheduleTypePresents.FindAsync(ViewScheduleTypePresent.ViewScheduleTypePresentId);

                if (dbViewScheduleTypePresent == null)
                {
                    return (false, "ViewScheduleTypePresent could not be found");
                }

                _db.ViewScheduleTypePresents.Remove(ViewScheduleTypePresent);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleTypePresent got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleTypePresents
    }
}
