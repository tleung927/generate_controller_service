using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventColumns2Service
    {
        // EventColumns2s Services
        Task<List<EventColumns2>> GetEventColumns2ListByValue(int offset, int limit, string val); // GET All EventColumns2ss
        Task<EventColumns2> GetEventColumns2(string EventColumns2_name); // GET Single EventColumns2s        
        Task<List<EventColumns2>> GetEventColumns2List(string EventColumns2_name); // GET List EventColumns2s        
        Task<EventColumns2> AddEventColumns2(EventColumns2 EventColumns2); // POST New EventColumns2s
        Task<EventColumns2> UpdateEventColumns2(EventColumns2 EventColumns2); // PUT EventColumns2s
        Task<(bool, string)> DeleteEventColumns2(EventColumns2 EventColumns2); // DELETE EventColumns2s
    }

    public class EventColumns2Service : IEventColumns2Service
    {
        private readonly XixsrvContext _db;

        public EventColumns2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region EventColumns2s

        public async Task<List<EventColumns2>> GetEventColumns2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventColumns2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventColumns2> GetEventColumns2(string EventColumns2_id)
        {
            try
            {
                return await _db.EventColumns2s.FindAsync(EventColumns2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventColumns2>> GetEventColumns2List(string EventColumns2_id)
        {
            try
            {
                return await _db.EventColumns2s
                    .Where(i => i.EventColumns2Id == EventColumns2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventColumns2> AddEventColumns2(EventColumns2 EventColumns2)
        {
            try
            {
                await _db.EventColumns2s.AddAsync(EventColumns2);
                await _db.SaveChangesAsync();
                return await _db.EventColumns2s.FindAsync(EventColumns2.EventColumns2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventColumns2> UpdateEventColumns2(EventColumns2 EventColumns2)
        {
            try
            {
                _db.Entry(EventColumns2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventColumns2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventColumns2(EventColumns2 EventColumns2)
        {
            try
            {
                var dbEventColumns2 = await _db.EventColumns2s.FindAsync(EventColumns2.EventColumns2Id);

                if (dbEventColumns2 == null)
                {
                    return (false, "EventColumns2 could not be found");
                }

                _db.EventColumns2s.Remove(EventColumns2);
                await _db.SaveChangesAsync();

                return (true, "EventColumns2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventColumns2s
    }
}
