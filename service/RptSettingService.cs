using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRptSettingService
    {
        // RptSettings Services
        Task<List<RptSetting>> GetRptSettingListByValue(int offset, int limit, string val); // GET All RptSettingss
        Task<RptSetting> GetRptSetting(string RptSetting_name); // GET Single RptSettings        
        Task<List<RptSetting>> GetRptSettingList(string RptSetting_name); // GET List RptSettings        
        Task<RptSetting> AddRptSetting(RptSetting RptSetting); // POST New RptSettings
        Task<RptSetting> UpdateRptSetting(RptSetting RptSetting); // PUT RptSettings
        Task<(bool, string)> DeleteRptSetting(RptSetting RptSetting); // DELETE RptSettings
    }

    public class RptSettingService : IRptSettingService
    {
        private readonly XixsrvContext _db;

        public RptSettingService(XixsrvContext db)
        {
            _db = db;
        }

        #region RptSettings

        public async Task<List<RptSetting>> GetRptSettingListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.RptSettings.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RptSetting> GetRptSetting(string RptSetting_id)
        {
            try
            {
                return await _db.RptSettings.FindAsync(RptSetting_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RptSetting>> GetRptSettingList(string RptSetting_id)
        {
            try
            {
                return await _db.RptSettings
                    .Where(i => i.RptSettingId == RptSetting_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<RptSetting> AddRptSetting(RptSetting RptSetting)
        {
            try
            {
                await _db.RptSettings.AddAsync(RptSetting);
                await _db.SaveChangesAsync();
                return await _db.RptSettings.FindAsync(RptSetting.RptSettingId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<RptSetting> UpdateRptSetting(RptSetting RptSetting)
        {
            try
            {
                _db.Entry(RptSetting).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RptSetting;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRptSetting(RptSetting RptSetting)
        {
            try
            {
                var dbRptSetting = await _db.RptSettings.FindAsync(RptSetting.RptSettingId);

                if (dbRptSetting == null)
                {
                    return (false, "RptSetting could not be found");
                }

                _db.RptSettings.Remove(RptSetting);
                await _db.SaveChangesAsync();

                return (true, "RptSetting got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion RptSettings
    }
}
