using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPcpAddedPlanService
    {
        // PcpAddedPlans Services
        Task<List<PcpAddedPlan>> GetPcpAddedPlanListByValue(int offset, int limit, string val); // GET All PcpAddedPlanss
        Task<PcpAddedPlan> GetPcpAddedPlan(string PcpAddedPlan_name); // GET Single PcpAddedPlans        
        Task<List<PcpAddedPlan>> GetPcpAddedPlanList(string PcpAddedPlan_name); // GET List PcpAddedPlans        
        Task<PcpAddedPlan> AddPcpAddedPlan(PcpAddedPlan PcpAddedPlan); // POST New PcpAddedPlans
        Task<PcpAddedPlan> UpdatePcpAddedPlan(PcpAddedPlan PcpAddedPlan); // PUT PcpAddedPlans
        Task<(bool, string)> DeletePcpAddedPlan(PcpAddedPlan PcpAddedPlan); // DELETE PcpAddedPlans
    }

    public class PcpAddedPlanService : IPcpAddedPlanService
    {
        private readonly XixsrvContext _db;

        public PcpAddedPlanService(XixsrvContext db)
        {
            _db = db;
        }

        #region PcpAddedPlans

        public async Task<List<PcpAddedPlan>> GetPcpAddedPlanListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PcpAddedPlans.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PcpAddedPlan> GetPcpAddedPlan(string PcpAddedPlan_id)
        {
            try
            {
                return await _db.PcpAddedPlans.FindAsync(PcpAddedPlan_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PcpAddedPlan>> GetPcpAddedPlanList(string PcpAddedPlan_id)
        {
            try
            {
                return await _db.PcpAddedPlans
                    .Where(i => i.PcpAddedPlanId == PcpAddedPlan_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PcpAddedPlan> AddPcpAddedPlan(PcpAddedPlan PcpAddedPlan)
        {
            try
            {
                await _db.PcpAddedPlans.AddAsync(PcpAddedPlan);
                await _db.SaveChangesAsync();
                return await _db.PcpAddedPlans.FindAsync(PcpAddedPlan.PcpAddedPlanId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PcpAddedPlan> UpdatePcpAddedPlan(PcpAddedPlan PcpAddedPlan)
        {
            try
            {
                _db.Entry(PcpAddedPlan).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PcpAddedPlan;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePcpAddedPlan(PcpAddedPlan PcpAddedPlan)
        {
            try
            {
                var dbPcpAddedPlan = await _db.PcpAddedPlans.FindAsync(PcpAddedPlan.PcpAddedPlanId);

                if (dbPcpAddedPlan == null)
                {
                    return (false, "PcpAddedPlan could not be found");
                }

                _db.PcpAddedPlans.Remove(PcpAddedPlan);
                await _db.SaveChangesAsync();

                return (true, "PcpAddedPlan got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PcpAddedPlans
    }
}
