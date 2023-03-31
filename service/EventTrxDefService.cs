using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventTrxDefService
    {
        // EventTrxDefs Services
        Task<List<EventTrxDef>> GetEventTrxDefListByValue(int offset, int limit, string val); // GET All EventTrxDefss
        Task<EventTrxDef> GetEventTrxDef(string EventTrxDef_name); // GET Single EventTrxDefs        
        Task<List<EventTrxDef>> GetEventTrxDefList(string EventTrxDef_name); // GET List EventTrxDefs        
        Task<EventTrxDef> AddEventTrxDef(EventTrxDef EventTrxDef); // POST New EventTrxDefs
        Task<EventTrxDef> UpdateEventTrxDef(EventTrxDef EventTrxDef); // PUT EventTrxDefs
        Task<(bool, string)> DeleteEventTrxDef(EventTrxDef EventTrxDef); // DELETE EventTrxDefs
    }

    public class EventTrxDefService : IEventTrxDefService
    {
        private readonly XixsrvContext _db;

        public EventTrxDefService(XixsrvContext db)
        {
            _db = db;
        }

        #region EventTrxDefs

        public async Task<List<EventTrxDef>> GetEventTrxDefListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventTrxDefs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventTrxDef> GetEventTrxDef(string EventTrxDef_id)
        {
            try
            {
                return await _db.EventTrxDefs.FindAsync(EventTrxDef_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventTrxDef>> GetEventTrxDefList(string EventTrxDef_id)
        {
            try
            {
                return await _db.EventTrxDefs
                    .Where(i => i.EventTrxDefId == EventTrxDef_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventTrxDef> AddEventTrxDef(EventTrxDef EventTrxDef)
        {
            try
            {
                await _db.EventTrxDefs.AddAsync(EventTrxDef);
                await _db.SaveChangesAsync();
                return await _db.EventTrxDefs.FindAsync(EventTrxDef.EventTrxDefId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventTrxDef> UpdateEventTrxDef(EventTrxDef EventTrxDef)
        {
            try
            {
                _db.Entry(EventTrxDef).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventTrxDef;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventTrxDef(EventTrxDef EventTrxDef)
        {
            try
            {
                var dbEventTrxDef = await _db.EventTrxDefs.FindAsync(EventTrxDef.EventTrxDefId);

                if (dbEventTrxDef == null)
                {
                    return (false, "EventTrxDef could not be found");
                }

                _db.EventTrxDefs.Remove(EventTrxDef);
                await _db.SaveChangesAsync();

                return (true, "EventTrxDef got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventTrxDefs
    }
}
