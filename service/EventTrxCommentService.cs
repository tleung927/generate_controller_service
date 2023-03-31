using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEventTrxCommentService
    {
        // EventTrxComments Services
        Task<List<EventTrxComment>> GetEventTrxCommentListByValue(int offset, int limit, string val); // GET All EventTrxCommentss
        Task<EventTrxComment> GetEventTrxComment(string EventTrxComment_name); // GET Single EventTrxComments        
        Task<List<EventTrxComment>> GetEventTrxCommentList(string EventTrxComment_name); // GET List EventTrxComments        
        Task<EventTrxComment> AddEventTrxComment(EventTrxComment EventTrxComment); // POST New EventTrxComments
        Task<EventTrxComment> UpdateEventTrxComment(EventTrxComment EventTrxComment); // PUT EventTrxComments
        Task<(bool, string)> DeleteEventTrxComment(EventTrxComment EventTrxComment); // DELETE EventTrxComments
    }

    public class EventTrxCommentService : IEventTrxCommentService
    {
        private readonly XixsrvContext _db;

        public EventTrxCommentService(XixsrvContext db)
        {
            _db = db;
        }

        #region EventTrxComments

        public async Task<List<EventTrxComment>> GetEventTrxCommentListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EventTrxComments.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EventTrxComment> GetEventTrxComment(string EventTrxComment_id)
        {
            try
            {
                return await _db.EventTrxComments.FindAsync(EventTrxComment_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EventTrxComment>> GetEventTrxCommentList(string EventTrxComment_id)
        {
            try
            {
                return await _db.EventTrxComments
                    .Where(i => i.EventTrxCommentId == EventTrxComment_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EventTrxComment> AddEventTrxComment(EventTrxComment EventTrxComment)
        {
            try
            {
                await _db.EventTrxComments.AddAsync(EventTrxComment);
                await _db.SaveChangesAsync();
                return await _db.EventTrxComments.FindAsync(EventTrxComment.EventTrxCommentId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EventTrxComment> UpdateEventTrxComment(EventTrxComment EventTrxComment)
        {
            try
            {
                _db.Entry(EventTrxComment).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EventTrxComment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEventTrxComment(EventTrxComment EventTrxComment)
        {
            try
            {
                var dbEventTrxComment = await _db.EventTrxComments.FindAsync(EventTrxComment.EventTrxCommentId);

                if (dbEventTrxComment == null)
                {
                    return (false, "EventTrxComment could not be found");
                }

                _db.EventTrxComments.Remove(EventTrxComment);
                await _db.SaveChangesAsync();

                return (true, "EventTrxComment got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EventTrxComments
    }
}
