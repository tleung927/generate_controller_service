using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventColumnService
    {
        // EventColumns Services
        Task<List<EventColumn>> GetEventColumnListByValue(int offset, int limit, string val); // GET All EventColumnss
        Task<EventColumn> GetEventColumn(string EventColumn_name); // GET Single EventColumns        
        Task<List<EventColumn>> GetEventColumnList(string EventColumn_name); // GET List EventColumns        
        Task<EventColumn> AddEventColumn(EventColumn EventColumn); // POST New EventColumns
        Task<EventColumn> UpdateEventColumn(EventColumn EventColumn); // PUT EventColumns
        Task<(bool, string)> DeleteEventColumn(EventColumn EventColumn); // DELETE EventColumns
    }

    public class EventColumnService : IEventColumnService
    {
        private readonly XixsrvContext _db;

        public EventColumnService(XixsrvContext db)
        {
            _db = db;
        }

        #region EventColumns

        public async Task<List<EventColumn>> GetEventColumnListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventColumns.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventColumn> GetEventColumn(string EventColumn_id)
        {
            try
            {
                return await _db.EventColumns.FindAsync(EventColumn_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventColumn>> GetEventColumnList(string EventColumn_id)
        {
            try
            {
                return await _db.EventColumns
                    .Where(i => i.EventColumnId == EventColumn_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventColumn> AddEventColumn(EventColumn EventColumn)
        {
            try
            {
                await _db.EventColumns.AddAsync(EventColumn);
                await _db.SaveChangesAsync();
                return await _db.EventColumns.FindAsync(EventColumn.EventColumnId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventColumn> UpdateEventColumn(EventColumn EventColumn)
        {
            try
            {
                _db.Entry(EventColumn).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventColumn;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventColumn(EventColumn EventColumn)
        {
            try
            {
                var dbEventColumn = await _db.EventColumns.FindAsync(EventColumn.EventColumnId);

                if (dbEventColumn == null)
                {
                    return (false, "EventColumn could not be found");
                }

                _db.EventColumns.Remove(EventColumn);
                await _db.SaveChangesAsync();

                return (true, "EventColumn got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventColumns
    }
}
