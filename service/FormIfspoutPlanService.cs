using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspoutPlanService
    {
        // FormIfspoutPlans Services
        Task<List<FormIfspoutPlan>> GetFormIfspoutPlanListByValue(int offset, int limit, string val); // GET All FormIfspoutPlanss
        Task<FormIfspoutPlan> GetFormIfspoutPlan(string FormIfspoutPlan_name); // GET Single FormIfspoutPlans        
        Task<List<FormIfspoutPlan>> GetFormIfspoutPlanList(string FormIfspoutPlan_name); // GET List FormIfspoutPlans        
        Task<FormIfspoutPlan> AddFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan); // POST New FormIfspoutPlans
        Task<FormIfspoutPlan> UpdateFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan); // PUT FormIfspoutPlans
        Task<(bool, string)> DeleteFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan); // DELETE FormIfspoutPlans
    }

    public class FormIfspoutPlanService : IFormIfspoutPlanService
    {
        private readonly XixsrvContext _db;

        public FormIfspoutPlanService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspoutPlans

        public async Task<List<FormIfspoutPlan>> GetFormIfspoutPlanListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspoutPlans.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfspoutPlan> GetFormIfspoutPlan(string FormIfspoutPlan_id)
        {
            try
            {
                return await _db.FormIfspoutPlans.FindAsync(FormIfspoutPlan_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfspoutPlan>> GetFormIfspoutPlanList(string FormIfspoutPlan_id)
        {
            try
            {
                return await _db.FormIfspoutPlans
                    .Where(i => i.FormIfspoutPlanId == FormIfspoutPlan_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfspoutPlan> AddFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan)
        {
            try
            {
                await _db.FormIfspoutPlans.AddAsync(FormIfspoutPlan);
                await _db.SaveChangesAsync();
                return await _db.FormIfspoutPlans.FindAsync(FormIfspoutPlan.FormIfspoutPlanId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfspoutPlan> UpdateFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan)
        {
            try
            {
                _db.Entry(FormIfspoutPlan).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfspoutPlan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfspoutPlan(FormIfspoutPlan FormIfspoutPlan)
        {
            try
            {
                var dbFormIfspoutPlan = await _db.FormIfspoutPlans.FindAsync(FormIfspoutPlan.FormIfspoutPlanId);

                if (dbFormIfspoutPlan == null)
                {
                    return (false, "FormIfspoutPlan could not be found");
                }

                _db.FormIfspoutPlans.Remove(FormIfspoutPlan);
                await _db.SaveChangesAsync();

                return (true, "FormIfspoutPlan got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspoutPlans
    }
}
