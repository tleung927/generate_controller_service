using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormTransCaseLogService
    {
        // FormTransCaseLogs Services
        Task<List<FormTransCaseLog>> GetFormTransCaseLogListByValue(int offset, int limit, string val); // GET All FormTransCaseLogss
        Task<FormTransCaseLog> GetFormTransCaseLog(string FormTransCaseLog_name); // GET Single FormTransCaseLogs        
        Task<List<FormTransCaseLog>> GetFormTransCaseLogList(string FormTransCaseLog_name); // GET List FormTransCaseLogs        
        Task<FormTransCaseLog> AddFormTransCaseLog(FormTransCaseLog FormTransCaseLog); // POST New FormTransCaseLogs
        Task<FormTransCaseLog> UpdateFormTransCaseLog(FormTransCaseLog FormTransCaseLog); // PUT FormTransCaseLogs
        Task<(bool, string)> DeleteFormTransCaseLog(FormTransCaseLog FormTransCaseLog); // DELETE FormTransCaseLogs
    }

    public class FormTransCaseLogService : IFormTransCaseLogService
    {
        private readonly XixsrvContext _db;

        public FormTransCaseLogService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormTransCaseLogs

        public async Task<List<FormTransCaseLog>> GetFormTransCaseLogListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormTransCaseLogs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormTransCaseLog> GetFormTransCaseLog(string FormTransCaseLog_id)
        {
            try
            {
                return await _db.FormTransCaseLogs.FindAsync(FormTransCaseLog_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormTransCaseLog>> GetFormTransCaseLogList(string FormTransCaseLog_id)
        {
            try
            {
                return await _db.FormTransCaseLogs
                    .Where(i => i.FormTransCaseLogId == FormTransCaseLog_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormTransCaseLog> AddFormTransCaseLog(FormTransCaseLog FormTransCaseLog)
        {
            try
            {
                await _db.FormTransCaseLogs.AddAsync(FormTransCaseLog);
                await _db.SaveChangesAsync();
                return await _db.FormTransCaseLogs.FindAsync(FormTransCaseLog.FormTransCaseLogId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormTransCaseLog> UpdateFormTransCaseLog(FormTransCaseLog FormTransCaseLog)
        {
            try
            {
                _db.Entry(FormTransCaseLog).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormTransCaseLog;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormTransCaseLog(FormTransCaseLog FormTransCaseLog)
        {
            try
            {
                var dbFormTransCaseLog = await _db.FormTransCaseLogs.FindAsync(FormTransCaseLog.FormTransCaseLogId);

                if (dbFormTransCaseLog == null)
                {
                    return (false, "FormTransCaseLog could not be found");
                }

                _db.FormTransCaseLogs.Remove(FormTransCaseLog);
                await _db.SaveChangesAsync();

                return (true, "FormTransCaseLog got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormTransCaseLogs
    }
}
