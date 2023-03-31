using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRAllCaseLoadCountService
    {
        // RAllCaseLoadCounts Services
        Task<List<RAllCaseLoadCount>> GetRAllCaseLoadCountListByValue(int offset, int limit, string val); // GET All RAllCaseLoadCountss
        Task<RAllCaseLoadCount> GetRAllCaseLoadCount(string RAllCaseLoadCount_name); // GET Single RAllCaseLoadCounts        
        Task<List<RAllCaseLoadCount>> GetRAllCaseLoadCountList(string RAllCaseLoadCount_name); // GET List RAllCaseLoadCounts        
        Task<RAllCaseLoadCount> AddRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount); // POST New RAllCaseLoadCounts
        Task<RAllCaseLoadCount> UpdateRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount); // PUT RAllCaseLoadCounts
        Task<(bool, string)> DeleteRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount); // DELETE RAllCaseLoadCounts
    }

    public class RAllCaseLoadCountService : IRAllCaseLoadCountService
    {
        private readonly XixsrvContext _db;

        public RAllCaseLoadCountService(XixsrvContext db)
        {
            _db = db;
        }

        #region RAllCaseLoadCounts

        public async Task<List<RAllCaseLoadCount>> GetRAllCaseLoadCountListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.RAllCaseLoadCounts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RAllCaseLoadCount> GetRAllCaseLoadCount(string RAllCaseLoadCount_id)
        {
            try
            {
                return await _db.RAllCaseLoadCounts.FindAsync(RAllCaseLoadCount_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RAllCaseLoadCount>> GetRAllCaseLoadCountList(string RAllCaseLoadCount_id)
        {
            try
            {
                return await _db.RAllCaseLoadCounts
                    .Where(i => i.RAllCaseLoadCountId == RAllCaseLoadCount_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<RAllCaseLoadCount> AddRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount)
        {
            try
            {
                await _db.RAllCaseLoadCounts.AddAsync(RAllCaseLoadCount);
                await _db.SaveChangesAsync();
                return await _db.RAllCaseLoadCounts.FindAsync(RAllCaseLoadCount.RAllCaseLoadCountId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<RAllCaseLoadCount> UpdateRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount)
        {
            try
            {
                _db.Entry(RAllCaseLoadCount).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RAllCaseLoadCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRAllCaseLoadCount(RAllCaseLoadCount RAllCaseLoadCount)
        {
            try
            {
                var dbRAllCaseLoadCount = await _db.RAllCaseLoadCounts.FindAsync(RAllCaseLoadCount.RAllCaseLoadCountId);

                if (dbRAllCaseLoadCount == null)
                {
                    return (false, "RAllCaseLoadCount could not be found");
                }

                _db.RAllCaseLoadCounts.Remove(RAllCaseLoadCount);
                await _db.SaveChangesAsync();

                return (true, "RAllCaseLoadCount got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion RAllCaseLoadCounts
    }
}
