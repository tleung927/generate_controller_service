using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IOperLogService
    {
        // OperLogs Services
        Task<List<OperLog>> GetOperLogListByValue(int offset, int limit, string val); // GET All OperLogss
        Task<OperLog> GetOperLog(string OperLog_name); // GET Single OperLogs        
        Task<List<OperLog>> GetOperLogList(string OperLog_name); // GET List OperLogs        
        Task<OperLog> AddOperLog(OperLog OperLog); // POST New OperLogs
        Task<OperLog> UpdateOperLog(OperLog OperLog); // PUT OperLogs
        Task<(bool, string)> DeleteOperLog(OperLog OperLog); // DELETE OperLogs
    }

    public class OperLogService : IOperLogService
    {
        private readonly XixsrvContext _db;

        public OperLogService(XixsrvContext db)
        {
            _db = db;
        }

        #region OperLogs

        public async Task<List<OperLog>> GetOperLogListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.OperLogs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OperLog> GetOperLog(string OperLog_id)
        {
            try
            {
                return await _db.OperLogs.FindAsync(OperLog_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<OperLog>> GetOperLogList(string OperLog_id)
        {
            try
            {
                return await _db.OperLogs
                    .Where(i => i.OperLogId == OperLog_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<OperLog> AddOperLog(OperLog OperLog)
        {
            try
            {
                await _db.OperLogs.AddAsync(OperLog);
                await _db.SaveChangesAsync();
                return await _db.OperLogs.FindAsync(OperLog.OperLogId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<OperLog> UpdateOperLog(OperLog OperLog)
        {
            try
            {
                _db.Entry(OperLog).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return OperLog;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteOperLog(OperLog OperLog)
        {
            try
            {
                var dbOperLog = await _db.OperLogs.FindAsync(OperLog.OperLogId);

                if (dbOperLog == null)
                {
                    return (false, "OperLog could not be found");
                }

                _db.OperLogs.Remove(OperLog);
                await _db.SaveChangesAsync();

                return (true, "OperLog got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion OperLogs
    }
}
