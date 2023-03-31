using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICAllCaseLoadCountService
    {
        // CAllCaseLoadCounts Services
        Task<List<CAllCaseLoadCount>> GetCAllCaseLoadCountListByValue(int offset, int limit, string val); // GET All CAllCaseLoadCountss
        Task<CAllCaseLoadCount> GetCAllCaseLoadCount(string CAllCaseLoadCount_name); // GET Single CAllCaseLoadCounts        
        Task<List<CAllCaseLoadCount>> GetCAllCaseLoadCountList(string CAllCaseLoadCount_name); // GET List CAllCaseLoadCounts        
        Task<CAllCaseLoadCount> AddCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount); // POST New CAllCaseLoadCounts
        Task<CAllCaseLoadCount> UpdateCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount); // PUT CAllCaseLoadCounts
        Task<(bool, string)> DeleteCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount); // DELETE CAllCaseLoadCounts
    }

    public class CAllCaseLoadCountService : ICAllCaseLoadCountService
    {
        private readonly XixsrvContext _db;

        public CAllCaseLoadCountService(XixsrvContext db)
        {
            _db = db;
        }

        #region CAllCaseLoadCounts

        public async Task<List<CAllCaseLoadCount>> GetCAllCaseLoadCountListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CAllCaseLoadCounts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CAllCaseLoadCount> GetCAllCaseLoadCount(string CAllCaseLoadCount_id)
        {
            try
            {
                return await _db.CAllCaseLoadCounts.FindAsync(CAllCaseLoadCount_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CAllCaseLoadCount>> GetCAllCaseLoadCountList(string CAllCaseLoadCount_id)
        {
            try
            {
                return await _db.CAllCaseLoadCounts
                    .Where(i => i.CAllCaseLoadCountId == CAllCaseLoadCount_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CAllCaseLoadCount> AddCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount)
        {
            try
            {
                await _db.CAllCaseLoadCounts.AddAsync(CAllCaseLoadCount);
                await _db.SaveChangesAsync();
                return await _db.CAllCaseLoadCounts.FindAsync(CAllCaseLoadCount.CAllCaseLoadCountId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CAllCaseLoadCount> UpdateCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount)
        {
            try
            {
                _db.Entry(CAllCaseLoadCount).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CAllCaseLoadCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCAllCaseLoadCount(CAllCaseLoadCount CAllCaseLoadCount)
        {
            try
            {
                var dbCAllCaseLoadCount = await _db.CAllCaseLoadCounts.FindAsync(CAllCaseLoadCount.CAllCaseLoadCountId);

                if (dbCAllCaseLoadCount == null)
                {
                    return (false, "CAllCaseLoadCount could not be found");
                }

                _db.CAllCaseLoadCounts.Remove(CAllCaseLoadCount);
                await _db.SaveChangesAsync();

                return (true, "CAllCaseLoadCount got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CAllCaseLoadCounts
    }
}
