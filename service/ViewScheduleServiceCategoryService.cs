using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleServiceCategoryService
    {
        // ViewScheduleServiceCategorys Services
        Task<List<ViewScheduleServiceCategory>> GetViewScheduleServiceCategoryListByValue(int offset, int limit, string val); // GET All ViewScheduleServiceCategoryss
        Task<ViewScheduleServiceCategory> GetViewScheduleServiceCategory(string ViewScheduleServiceCategory_name); // GET Single ViewScheduleServiceCategorys        
        Task<List<ViewScheduleServiceCategory>> GetViewScheduleServiceCategoryList(string ViewScheduleServiceCategory_name); // GET List ViewScheduleServiceCategorys        
        Task<ViewScheduleServiceCategory> AddViewScheduleServiceCategory(ViewScheduleServiceCategory ViewScheduleServiceCategory); // POST New ViewScheduleServiceCategorys
        Task<ViewScheduleServiceCategory> UpdateViewScheduleServiceCategory(ViewScheduleServiceCategory ViewScheduleServiceCategory); // PUT ViewScheduleServiceCategorys
        Task<(bool, string)> DeleteViewScheduleServiceCategory(ViewScheduleServiceCategory ViewScheduleServiceCategory); // DELETE ViewScheduleServiceCategorys
    }

    public class ViewScheduleServiceCategoryService : IViewScheduleServiceCategoryService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleServiceCategoryService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleServiceCategorys

        public async Task<List<ViewScheduleServiceCategory>> GetViewScheduleServiceCategoryListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleServiceCategorys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleServiceCategory> GetViewScheduleServiceCategory(string ViewScheduleServiceCategory_id)
        {
            try
            {
                return await _db.ViewScheduleServiceCategorys.FindAsync(ViewScheduleServiceCategory_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleServiceCategory>> GetViewScheduleServiceCategoryList(string ViewScheduleServiceCategory_id)
        {
            try
            {
                return await _db.ViewScheduleServiceCategorys
                    .Where(i => i.ViewScheduleServiceCategoryId == ViewScheduleServiceCategory_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleServiceCategory> AddViewScheduleServiceCategory(ViewScheduleServiceCategory ViewScheduleServiceCategory)
        {
            try
            {
                await _db.ViewScheduleServiceCategorys.AddAsync(ViewScheduleServiceCategory);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleServiceCategorys.FindAsync(ViewScheduleServiceCategory.ViewScheduleServiceCategoryId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleServiceCategory> UpdateViewScheduleServiceCategory(ViewScheduleServiceCategory ViewScheduleServiceCategory)
        {
            try
            {
                _db.Entry(ViewScheduleServiceCategory).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleServiceCategory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleServiceCategory(ViewScheduleServiceCategory ViewScheduleServiceCategory)
        {
            try
            {
                var dbViewScheduleServiceCategory = await _db.ViewScheduleServiceCategorys.FindAsync(ViewScheduleServiceCategory.ViewScheduleServiceCategoryId);

                if (dbViewScheduleServiceCategory == null)
                {
                    return (false, "ViewScheduleServiceCategory could not be found");
                }

                _db.ViewScheduleServiceCategorys.Remove(ViewScheduleServiceCategory);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleServiceCategory got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleServiceCategorys
    }
}
