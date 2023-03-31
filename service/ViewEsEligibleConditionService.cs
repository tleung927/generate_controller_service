using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewEsEligibleConditionService
    {
        // ViewEsEligibleConditions Services
        Task<List<ViewEsEligibleCondition>> GetViewEsEligibleConditionListByValue(int offset, int limit, string val); // GET All ViewEsEligibleConditionss
        Task<ViewEsEligibleCondition> GetViewEsEligibleCondition(string ViewEsEligibleCondition_name); // GET Single ViewEsEligibleConditions        
        Task<List<ViewEsEligibleCondition>> GetViewEsEligibleConditionList(string ViewEsEligibleCondition_name); // GET List ViewEsEligibleConditions        
        Task<ViewEsEligibleCondition> AddViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition); // POST New ViewEsEligibleConditions
        Task<ViewEsEligibleCondition> UpdateViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition); // PUT ViewEsEligibleConditions
        Task<(bool, string)> DeleteViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition); // DELETE ViewEsEligibleConditions
    }

    public class ViewEsEligibleConditionService : IViewEsEligibleConditionService
    {
        private readonly XixsrvContext _db;

        public ViewEsEligibleConditionService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewEsEligibleConditions

        public async Task<List<ViewEsEligibleCondition>> GetViewEsEligibleConditionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewEsEligibleConditions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewEsEligibleCondition> GetViewEsEligibleCondition(string ViewEsEligibleCondition_id)
        {
            try
            {
                return await _db.ViewEsEligibleConditions.FindAsync(ViewEsEligibleCondition_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewEsEligibleCondition>> GetViewEsEligibleConditionList(string ViewEsEligibleCondition_id)
        {
            try
            {
                return await _db.ViewEsEligibleConditions
                    .Where(i => i.ViewEsEligibleConditionId == ViewEsEligibleCondition_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewEsEligibleCondition> AddViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition)
        {
            try
            {
                await _db.ViewEsEligibleConditions.AddAsync(ViewEsEligibleCondition);
                await _db.SaveChangesAsync();
                return await _db.ViewEsEligibleConditions.FindAsync(ViewEsEligibleCondition.ViewEsEligibleConditionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewEsEligibleCondition> UpdateViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition)
        {
            try
            {
                _db.Entry(ViewEsEligibleCondition).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewEsEligibleCondition;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewEsEligibleCondition(ViewEsEligibleCondition ViewEsEligibleCondition)
        {
            try
            {
                var dbViewEsEligibleCondition = await _db.ViewEsEligibleConditions.FindAsync(ViewEsEligibleCondition.ViewEsEligibleConditionId);

                if (dbViewEsEligibleCondition == null)
                {
                    return (false, "ViewEsEligibleCondition could not be found");
                }

                _db.ViewEsEligibleConditions.Remove(ViewEsEligibleCondition);
                await _db.SaveChangesAsync();

                return (true, "ViewEsEligibleCondition got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewEsEligibleConditions
    }
}
