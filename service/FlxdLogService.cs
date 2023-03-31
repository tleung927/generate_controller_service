using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFlxdLogService
    {
        // FlxdLogs Services
        Task<List<FlxdLog>> GetFlxdLogListByValue(int offset, int limit, string val); // GET All FlxdLogss
        Task<FlxdLog> GetFlxdLog(string FlxdLog_name); // GET Single FlxdLogs        
        Task<List<FlxdLog>> GetFlxdLogList(string FlxdLog_name); // GET List FlxdLogs        
        Task<FlxdLog> AddFlxdLog(FlxdLog FlxdLog); // POST New FlxdLogs
        Task<FlxdLog> UpdateFlxdLog(FlxdLog FlxdLog); // PUT FlxdLogs
        Task<(bool, string)> DeleteFlxdLog(FlxdLog FlxdLog); // DELETE FlxdLogs
    }

    public class FlxdLogService : IFlxdLogService
    {
        private readonly XixsrvContext _db;

        public FlxdLogService(XixsrvContext db)
        {
            _db = db;
        }

        #region FlxdLogs

        public async Task<List<FlxdLog>> GetFlxdLogListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FlxdLogs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FlxdLog> GetFlxdLog(string FlxdLog_id)
        {
            try
            {
                return await _db.FlxdLogs.FindAsync(FlxdLog_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FlxdLog>> GetFlxdLogList(string FlxdLog_id)
        {
            try
            {
                return await _db.FlxdLogs
                    .Where(i => i.FlxdLogId == FlxdLog_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FlxdLog> AddFlxdLog(FlxdLog FlxdLog)
        {
            try
            {
                await _db.FlxdLogs.AddAsync(FlxdLog);
                await _db.SaveChangesAsync();
                return await _db.FlxdLogs.FindAsync(FlxdLog.FlxdLogId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FlxdLog> UpdateFlxdLog(FlxdLog FlxdLog)
        {
            try
            {
                _db.Entry(FlxdLog).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FlxdLog;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFlxdLog(FlxdLog FlxdLog)
        {
            try
            {
                var dbFlxdLog = await _db.FlxdLogs.FindAsync(FlxdLog.FlxdLogId);

                if (dbFlxdLog == null)
                {
                    return (false, "FlxdLog could not be found");
                }

                _db.FlxdLogs.Remove(FlxdLog);
                await _db.SaveChangesAsync();

                return (true, "FlxdLog got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FlxdLogs
    }
}
