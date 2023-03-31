using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITicklersTaskService
    {
        // TicklersTasks Services
        Task<List<TicklersTask>> GetTicklersTaskListByValue(int offset, int limit, string val); // GET All TicklersTaskss
        Task<TicklersTask> GetTicklersTask(string TicklersTask_name); // GET Single TicklersTasks        
        Task<List<TicklersTask>> GetTicklersTaskList(string TicklersTask_name); // GET List TicklersTasks        
        Task<TicklersTask> AddTicklersTask(TicklersTask TicklersTask); // POST New TicklersTasks
        Task<TicklersTask> UpdateTicklersTask(TicklersTask TicklersTask); // PUT TicklersTasks
        Task<(bool, string)> DeleteTicklersTask(TicklersTask TicklersTask); // DELETE TicklersTasks
    }

    public class TicklersTaskService : ITicklersTaskService
    {
        private readonly XixsrvContext _db;

        public TicklersTaskService(XixsrvContext db)
        {
            _db = db;
        }

        #region TicklersTasks

        public async Task<List<TicklersTask>> GetTicklersTaskListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TicklersTasks.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TicklersTask> GetTicklersTask(string TicklersTask_id)
        {
            try
            {
                return await _db.TicklersTasks.FindAsync(TicklersTask_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TicklersTask>> GetTicklersTaskList(string TicklersTask_id)
        {
            try
            {
                return await _db.TicklersTasks
                    .Where(i => i.TicklersTaskId == TicklersTask_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TicklersTask> AddTicklersTask(TicklersTask TicklersTask)
        {
            try
            {
                await _db.TicklersTasks.AddAsync(TicklersTask);
                await _db.SaveChangesAsync();
                return await _db.TicklersTasks.FindAsync(TicklersTask.TicklersTaskId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TicklersTask> UpdateTicklersTask(TicklersTask TicklersTask)
        {
            try
            {
                _db.Entry(TicklersTask).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TicklersTask;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTicklersTask(TicklersTask TicklersTask)
        {
            try
            {
                var dbTicklersTask = await _db.TicklersTasks.FindAsync(TicklersTask.TicklersTaskId);

                if (dbTicklersTask == null)
                {
                    return (false, "TicklersTask could not be found");
                }

                _db.TicklersTasks.Remove(TicklersTask);
                await _db.SaveChangesAsync();

                return (true, "TicklersTask got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TicklersTasks
    }
}
