using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleEsEligibleConditionService
    {
        // ViewScheduleEsEligibleConditions Services
        Task<List<ViewScheduleEsEligibleCondition>> GetViewScheduleEsEligibleConditionListByValue(int offset, int limit, string val); // GET All ViewScheduleEsEligibleConditionss
        Task<ViewScheduleEsEligibleCondition> GetViewScheduleEsEligibleCondition(string ViewScheduleEsEligibleCondition_name); // GET Single ViewScheduleEsEligibleConditions        
        Task<List<ViewScheduleEsEligibleCondition>> GetViewScheduleEsEligibleConditionList(string ViewScheduleEsEligibleCondition_name); // GET List ViewScheduleEsEligibleConditions        
        Task<ViewScheduleEsEligibleCondition> AddViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition); // POST New ViewScheduleEsEligibleConditions
        Task<ViewScheduleEsEligibleCondition> UpdateViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition); // PUT ViewScheduleEsEligibleConditions
        Task<(bool, string)> DeleteViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition); // DELETE ViewScheduleEsEligibleConditions
    }

    public class ViewScheduleEsEligibleConditionService : IViewScheduleEsEligibleConditionService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleEsEligibleConditionService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleEsEligibleConditions

        public async Task<List<ViewScheduleEsEligibleCondition>> GetViewScheduleEsEligibleConditionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleEsEligibleConditions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleEsEligibleCondition> GetViewScheduleEsEligibleCondition(string ViewScheduleEsEligibleCondition_id)
        {
            try
            {
                return await _db.ViewScheduleEsEligibleConditions.FindAsync(ViewScheduleEsEligibleCondition_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleEsEligibleCondition>> GetViewScheduleEsEligibleConditionList(string ViewScheduleEsEligibleCondition_id)
        {
            try
            {
                return await _db.ViewScheduleEsEligibleConditions
                    .Where(i => i.ViewScheduleEsEligibleConditionId == ViewScheduleEsEligibleCondition_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleEsEligibleCondition> AddViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition)
        {
            try
            {
                await _db.ViewScheduleEsEligibleConditions.AddAsync(ViewScheduleEsEligibleCondition);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleEsEligibleConditions.FindAsync(ViewScheduleEsEligibleCondition.ViewScheduleEsEligibleConditionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleEsEligibleCondition> UpdateViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition)
        {
            try
            {
                _db.Entry(ViewScheduleEsEligibleCondition).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleEsEligibleCondition;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleEsEligibleCondition(ViewScheduleEsEligibleCondition ViewScheduleEsEligibleCondition)
        {
            try
            {
                var dbViewScheduleEsEligibleCondition = await _db.ViewScheduleEsEligibleConditions.FindAsync(ViewScheduleEsEligibleCondition.ViewScheduleEsEligibleConditionId);

                if (dbViewScheduleEsEligibleCondition == null)
                {
                    return (false, "ViewScheduleEsEligibleCondition could not be found");
                }

                _db.ViewScheduleEsEligibleConditions.Remove(ViewScheduleEsEligibleCondition);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleEsEligibleCondition got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleEsEligibleConditions
    }
}
