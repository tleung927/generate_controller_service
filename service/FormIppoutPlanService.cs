using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppoutPlanService
    {
        // FormIppoutPlans Services
        Task<List<FormIppoutPlan>> GetFormIppoutPlanListByValue(int offset, int limit, string val); // GET All FormIppoutPlanss
        Task<FormIppoutPlan> GetFormIppoutPlan(string FormIppoutPlan_name); // GET Single FormIppoutPlans        
        Task<List<FormIppoutPlan>> GetFormIppoutPlanList(string FormIppoutPlan_name); // GET List FormIppoutPlans        
        Task<FormIppoutPlan> AddFormIppoutPlan(FormIppoutPlan FormIppoutPlan); // POST New FormIppoutPlans
        Task<FormIppoutPlan> UpdateFormIppoutPlan(FormIppoutPlan FormIppoutPlan); // PUT FormIppoutPlans
        Task<(bool, string)> DeleteFormIppoutPlan(FormIppoutPlan FormIppoutPlan); // DELETE FormIppoutPlans
    }

    public class FormIppoutPlanService : IFormIppoutPlanService
    {
        private readonly XixsrvContext _db;

        public FormIppoutPlanService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppoutPlans

        public async Task<List<FormIppoutPlan>> GetFormIppoutPlanListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppoutPlans.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppoutPlan> GetFormIppoutPlan(string FormIppoutPlan_id)
        {
            try
            {
                return await _db.FormIppoutPlans.FindAsync(FormIppoutPlan_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppoutPlan>> GetFormIppoutPlanList(string FormIppoutPlan_id)
        {
            try
            {
                return await _db.FormIppoutPlans
                    .Where(i => i.FormIppoutPlanId == FormIppoutPlan_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppoutPlan> AddFormIppoutPlan(FormIppoutPlan FormIppoutPlan)
        {
            try
            {
                await _db.FormIppoutPlans.AddAsync(FormIppoutPlan);
                await _db.SaveChangesAsync();
                return await _db.FormIppoutPlans.FindAsync(FormIppoutPlan.FormIppoutPlanId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppoutPlan> UpdateFormIppoutPlan(FormIppoutPlan FormIppoutPlan)
        {
            try
            {
                _db.Entry(FormIppoutPlan).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppoutPlan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppoutPlan(FormIppoutPlan FormIppoutPlan)
        {
            try
            {
                var dbFormIppoutPlan = await _db.FormIppoutPlans.FindAsync(FormIppoutPlan.FormIppoutPlanId);

                if (dbFormIppoutPlan == null)
                {
                    return (false, "FormIppoutPlan could not be found");
                }

                _db.FormIppoutPlans.Remove(FormIppoutPlan);
                await _db.SaveChangesAsync();

                return (true, "FormIppoutPlan got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppoutPlans
    }
}
