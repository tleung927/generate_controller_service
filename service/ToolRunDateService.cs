using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IToolRunDateService
    {
        // ToolRunDates Services
        Task<List<ToolRunDate>> GetToolRunDateListByValue(int offset, int limit, string val); // GET All ToolRunDatess
        Task<ToolRunDate> GetToolRunDate(string ToolRunDate_name); // GET Single ToolRunDates        
        Task<List<ToolRunDate>> GetToolRunDateList(string ToolRunDate_name); // GET List ToolRunDates        
        Task<ToolRunDate> AddToolRunDate(ToolRunDate ToolRunDate); // POST New ToolRunDates
        Task<ToolRunDate> UpdateToolRunDate(ToolRunDate ToolRunDate); // PUT ToolRunDates
        Task<(bool, string)> DeleteToolRunDate(ToolRunDate ToolRunDate); // DELETE ToolRunDates
    }

    public class ToolRunDateService : IToolRunDateService
    {
        private readonly XixsrvContext _db;

        public ToolRunDateService(XixsrvContext db)
        {
            _db = db;
        }

        #region ToolRunDates

        public async Task<List<ToolRunDate>> GetToolRunDateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ToolRunDates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ToolRunDate> GetToolRunDate(string ToolRunDate_id)
        {
            try
            {
                return await _db.ToolRunDates.FindAsync(ToolRunDate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ToolRunDate>> GetToolRunDateList(string ToolRunDate_id)
        {
            try
            {
                return await _db.ToolRunDates
                    .Where(i => i.ToolRunDateId == ToolRunDate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ToolRunDate> AddToolRunDate(ToolRunDate ToolRunDate)
        {
            try
            {
                await _db.ToolRunDates.AddAsync(ToolRunDate);
                await _db.SaveChangesAsync();
                return await _db.ToolRunDates.FindAsync(ToolRunDate.ToolRunDateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ToolRunDate> UpdateToolRunDate(ToolRunDate ToolRunDate)
        {
            try
            {
                _db.Entry(ToolRunDate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ToolRunDate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteToolRunDate(ToolRunDate ToolRunDate)
        {
            try
            {
                var dbToolRunDate = await _db.ToolRunDates.FindAsync(ToolRunDate.ToolRunDateId);

                if (dbToolRunDate == null)
                {
                    return (false, "ToolRunDate could not be found");
                }

                _db.ToolRunDates.Remove(ToolRunDate);
                await _db.SaveChangesAsync();

                return (true, "ToolRunDate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ToolRunDates
    }
}
