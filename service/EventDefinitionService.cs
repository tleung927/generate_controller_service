using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventDefinitionService
    {
        // EventDefinitions Services
        Task<List<EventDefinition>> GetEventDefinitionListByValue(int offset, int limit, string val); // GET All EventDefinitionss
        Task<EventDefinition> GetEventDefinition(string EventDefinition_name); // GET Single EventDefinitions        
        Task<List<EventDefinition>> GetEventDefinitionList(string EventDefinition_name); // GET List EventDefinitions        
        Task<EventDefinition> AddEventDefinition(EventDefinition EventDefinition); // POST New EventDefinitions
        Task<EventDefinition> UpdateEventDefinition(EventDefinition EventDefinition); // PUT EventDefinitions
        Task<(bool, string)> DeleteEventDefinition(EventDefinition EventDefinition); // DELETE EventDefinitions
    }

    public class EventDefinitionService : IEventDefinitionService
    {
        private readonly XixsrvContext _db;

        public EventDefinitionService(XixsrvContext db)
        {
            _db = db;
        }

        #region EventDefinitions

        public async Task<List<EventDefinition>> GetEventDefinitionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventDefinitions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventDefinition> GetEventDefinition(string EventDefinition_id)
        {
            try
            {
                return await _db.EventDefinitions.FindAsync(EventDefinition_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventDefinition>> GetEventDefinitionList(string EventDefinition_id)
        {
            try
            {
                return await _db.EventDefinitions
                    .Where(i => i.EventDefinitionId == EventDefinition_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventDefinition> AddEventDefinition(EventDefinition EventDefinition)
        {
            try
            {
                await _db.EventDefinitions.AddAsync(EventDefinition);
                await _db.SaveChangesAsync();
                return await _db.EventDefinitions.FindAsync(EventDefinition.EventDefinitionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventDefinition> UpdateEventDefinition(EventDefinition EventDefinition)
        {
            try
            {
                _db.Entry(EventDefinition).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventDefinition;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventDefinition(EventDefinition EventDefinition)
        {
            try
            {
                var dbEventDefinition = await _db.EventDefinitions.FindAsync(EventDefinition.EventDefinitionId);

                if (dbEventDefinition == null)
                {
                    return (false, "EventDefinition could not be found");
                }

                _db.EventDefinitions.Remove(EventDefinition);
                await _db.SaveChangesAsync();

                return (true, "EventDefinition got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventDefinitions
    }
}
