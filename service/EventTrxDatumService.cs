using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventTrxDatumService
    {
        // EventTrxDatums Services
        Task<List<EventTrxDatum>> GetEventTrxDatumListByValue(int offset, int limit, string val); // GET All EventTrxDatumss
        Task<EventTrxDatum> GetEventTrxDatum(string EventTrxDatum_name); // GET Single EventTrxDatums        
        Task<List<EventTrxDatum>> GetEventTrxDatumList(string EventTrxDatum_name); // GET List EventTrxDatums        
        Task<EventTrxDatum> AddEventTrxDatum(EventTrxDatum EventTrxDatum); // POST New EventTrxDatums
        Task<EventTrxDatum> UpdateEventTrxDatum(EventTrxDatum EventTrxDatum); // PUT EventTrxDatums
        Task<(bool, string)> DeleteEventTrxDatum(EventTrxDatum EventTrxDatum); // DELETE EventTrxDatums
    }

    public class EventTrxDatumService : IEventTrxDatumService
    {
        private readonly XixsrvContext _db;

        public EventTrxDatumService(XixsrvContext db)
        {
            _db = db;
        }

        #region EventTrxDatums

        public async Task<List<EventTrxDatum>> GetEventTrxDatumListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventTrxDatums.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventTrxDatum> GetEventTrxDatum(string EventTrxDatum_id)
        {
            try
            {
                return await _db.EventTrxDatums.FindAsync(EventTrxDatum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventTrxDatum>> GetEventTrxDatumList(string EventTrxDatum_id)
        {
            try
            {
                return await _db.EventTrxDatums
                    .Where(i => i.EventTrxDatumId == EventTrxDatum_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventTrxDatum> AddEventTrxDatum(EventTrxDatum EventTrxDatum)
        {
            try
            {
                await _db.EventTrxDatums.AddAsync(EventTrxDatum);
                await _db.SaveChangesAsync();
                return await _db.EventTrxDatums.FindAsync(EventTrxDatum.EventTrxDatumId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventTrxDatum> UpdateEventTrxDatum(EventTrxDatum EventTrxDatum)
        {
            try
            {
                _db.Entry(EventTrxDatum).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventTrxDatum;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventTrxDatum(EventTrxDatum EventTrxDatum)
        {
            try
            {
                var dbEventTrxDatum = await _db.EventTrxDatums.FindAsync(EventTrxDatum.EventTrxDatumId);

                if (dbEventTrxDatum == null)
                {
                    return (false, "EventTrxDatum could not be found");
                }

                _db.EventTrxDatums.Remove(EventTrxDatum);
                await _db.SaveChangesAsync();

                return (true, "EventTrxDatum got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventTrxDatums
    }
}
