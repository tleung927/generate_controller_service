using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleSlIlBudgetService
    {
        // ViewScheduleSlIlBudgets Services
        Task<List<ViewScheduleSlIlBudget>> GetViewScheduleSlIlBudgetListByValue(int offset, int limit, string val); // GET All ViewScheduleSlIlBudgetss
        Task<ViewScheduleSlIlBudget> GetViewScheduleSlIlBudget(string ViewScheduleSlIlBudget_name); // GET Single ViewScheduleSlIlBudgets        
        Task<List<ViewScheduleSlIlBudget>> GetViewScheduleSlIlBudgetList(string ViewScheduleSlIlBudget_name); // GET List ViewScheduleSlIlBudgets        
        Task<ViewScheduleSlIlBudget> AddViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget); // POST New ViewScheduleSlIlBudgets
        Task<ViewScheduleSlIlBudget> UpdateViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget); // PUT ViewScheduleSlIlBudgets
        Task<(bool, string)> DeleteViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget); // DELETE ViewScheduleSlIlBudgets
    }

    public class ViewScheduleSlIlBudgetService : IViewScheduleSlIlBudgetService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleSlIlBudgetService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleSlIlBudgets

        public async Task<List<ViewScheduleSlIlBudget>> GetViewScheduleSlIlBudgetListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleSlIlBudgets.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleSlIlBudget> GetViewScheduleSlIlBudget(string ViewScheduleSlIlBudget_id)
        {
            try
            {
                return await _db.ViewScheduleSlIlBudgets.FindAsync(ViewScheduleSlIlBudget_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleSlIlBudget>> GetViewScheduleSlIlBudgetList(string ViewScheduleSlIlBudget_id)
        {
            try
            {
                return await _db.ViewScheduleSlIlBudgets
                    .Where(i => i.ViewScheduleSlIlBudgetId == ViewScheduleSlIlBudget_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleSlIlBudget> AddViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget)
        {
            try
            {
                await _db.ViewScheduleSlIlBudgets.AddAsync(ViewScheduleSlIlBudget);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleSlIlBudgets.FindAsync(ViewScheduleSlIlBudget.ViewScheduleSlIlBudgetId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleSlIlBudget> UpdateViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget)
        {
            try
            {
                _db.Entry(ViewScheduleSlIlBudget).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleSlIlBudget;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget)
        {
            try
            {
                var dbViewScheduleSlIlBudget = await _db.ViewScheduleSlIlBudgets.FindAsync(ViewScheduleSlIlBudget.ViewScheduleSlIlBudgetId);

                if (dbViewScheduleSlIlBudget == null)
                {
                    return (false, "ViewScheduleSlIlBudget could not be found");
                }

                _db.ViewScheduleSlIlBudgets.Remove(ViewScheduleSlIlBudget);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleSlIlBudget got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleSlIlBudgets
    }
}
