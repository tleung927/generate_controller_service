using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRptCaseMgnPlanService
    {
        // RptCaseMgnPlans Services
        Task<List<RptCaseMgnPlan>> GetRptCaseMgnPlanListByValue(int offset, int limit, string val); // GET All RptCaseMgnPlanss
        Task<RptCaseMgnPlan> GetRptCaseMgnPlan(string RptCaseMgnPlan_name); // GET Single RptCaseMgnPlans        
        Task<List<RptCaseMgnPlan>> GetRptCaseMgnPlanList(string RptCaseMgnPlan_name); // GET List RptCaseMgnPlans        
        Task<RptCaseMgnPlan> AddRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan); // POST New RptCaseMgnPlans
        Task<RptCaseMgnPlan> UpdateRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan); // PUT RptCaseMgnPlans
        Task<(bool, string)> DeleteRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan); // DELETE RptCaseMgnPlans
    }

    public class RptCaseMgnPlanService : IRptCaseMgnPlanService
    {
        private readonly XixsrvContext _db;

        public RptCaseMgnPlanService(XixsrvContext db)
        {
            _db = db;
        }

        #region RptCaseMgnPlans

        public async Task<List<RptCaseMgnPlan>> GetRptCaseMgnPlanListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.RptCaseMgnPlans.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RptCaseMgnPlan> GetRptCaseMgnPlan(string RptCaseMgnPlan_id)
        {
            try
            {
                return await _db.RptCaseMgnPlans.FindAsync(RptCaseMgnPlan_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RptCaseMgnPlan>> GetRptCaseMgnPlanList(string RptCaseMgnPlan_id)
        {
            try
            {
                return await _db.RptCaseMgnPlans
                    .Where(i => i.RptCaseMgnPlanId == RptCaseMgnPlan_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<RptCaseMgnPlan> AddRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan)
        {
            try
            {
                await _db.RptCaseMgnPlans.AddAsync(RptCaseMgnPlan);
                await _db.SaveChangesAsync();
                return await _db.RptCaseMgnPlans.FindAsync(RptCaseMgnPlan.RptCaseMgnPlanId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<RptCaseMgnPlan> UpdateRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan)
        {
            try
            {
                _db.Entry(RptCaseMgnPlan).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RptCaseMgnPlan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRptCaseMgnPlan(RptCaseMgnPlan RptCaseMgnPlan)
        {
            try
            {
                var dbRptCaseMgnPlan = await _db.RptCaseMgnPlans.FindAsync(RptCaseMgnPlan.RptCaseMgnPlanId);

                if (dbRptCaseMgnPlan == null)
                {
                    return (false, "RptCaseMgnPlan could not be found");
                }

                _db.RptCaseMgnPlans.Remove(RptCaseMgnPlan);
                await _db.SaveChangesAsync();

                return (true, "RptCaseMgnPlan got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion RptCaseMgnPlans
    }
}
