using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventCommentLabelService
    {
        // EventCommentLabels Services
        Task<List<EventCommentLabel>> GetEventCommentLabelListByValue(int offset, int limit, string val); // GET All EventCommentLabelss
        Task<EventCommentLabel> GetEventCommentLabel(string EventCommentLabel_name); // GET Single EventCommentLabels        
        Task<List<EventCommentLabel>> GetEventCommentLabelList(string EventCommentLabel_name); // GET List EventCommentLabels        
        Task<EventCommentLabel> AddEventCommentLabel(EventCommentLabel EventCommentLabel); // POST New EventCommentLabels
        Task<EventCommentLabel> UpdateEventCommentLabel(EventCommentLabel EventCommentLabel); // PUT EventCommentLabels
        Task<(bool, string)> DeleteEventCommentLabel(EventCommentLabel EventCommentLabel); // DELETE EventCommentLabels
    }

    public class EventCommentLabelService : IEventCommentLabelService
    {
        private readonly XixsrvContext _db;

        public EventCommentLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region EventCommentLabels

        public async Task<List<EventCommentLabel>> GetEventCommentLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventCommentLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventCommentLabel> GetEventCommentLabel(string EventCommentLabel_id)
        {
            try
            {
                return await _db.EventCommentLabels.FindAsync(EventCommentLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventCommentLabel>> GetEventCommentLabelList(string EventCommentLabel_id)
        {
            try
            {
                return await _db.EventCommentLabels
                    .Where(i => i.EventCommentLabelId == EventCommentLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventCommentLabel> AddEventCommentLabel(EventCommentLabel EventCommentLabel)
        {
            try
            {
                await _db.EventCommentLabels.AddAsync(EventCommentLabel);
                await _db.SaveChangesAsync();
                return await _db.EventCommentLabels.FindAsync(EventCommentLabel.EventCommentLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventCommentLabel> UpdateEventCommentLabel(EventCommentLabel EventCommentLabel)
        {
            try
            {
                _db.Entry(EventCommentLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventCommentLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventCommentLabel(EventCommentLabel EventCommentLabel)
        {
            try
            {
                var dbEventCommentLabel = await _db.EventCommentLabels.FindAsync(EventCommentLabel.EventCommentLabelId);

                if (dbEventCommentLabel == null)
                {
                    return (false, "EventCommentLabel could not be found");
                }

                _db.EventCommentLabels.Remove(EventCommentLabel);
                await _db.SaveChangesAsync();

                return (true, "EventCommentLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventCommentLabels
    }
}
