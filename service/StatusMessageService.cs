using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IStatusMessageService
    {
        // StatusMessages Services
        Task<List<StatusMessage>> GetStatusMessageListByValue(int offset, int limit, string val); // GET All StatusMessagess
        Task<StatusMessage> GetStatusMessage(string StatusMessage_name); // GET Single StatusMessages        
        Task<List<StatusMessage>> GetStatusMessageList(string StatusMessage_name); // GET List StatusMessages        
        Task<StatusMessage> AddStatusMessage(StatusMessage StatusMessage); // POST New StatusMessages
        Task<StatusMessage> UpdateStatusMessage(StatusMessage StatusMessage); // PUT StatusMessages
        Task<(bool, string)> DeleteStatusMessage(StatusMessage StatusMessage); // DELETE StatusMessages
    }

    public class StatusMessageService : IStatusMessageService
    {
        private readonly XixsrvContext _db;

        public StatusMessageService(XixsrvContext db)
        {
            _db = db;
        }

        #region StatusMessages

        public async Task<List<StatusMessage>> GetStatusMessageListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.StatusMessages.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<StatusMessage> GetStatusMessage(string StatusMessage_id)
        {
            try
            {
                return await _db.StatusMessages.FindAsync(StatusMessage_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<StatusMessage>> GetStatusMessageList(string StatusMessage_id)
        {
            try
            {
                return await _db.StatusMessages
                    .Where(i => i.StatusMessageId == StatusMessage_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<StatusMessage> AddStatusMessage(StatusMessage StatusMessage)
        {
            try
            {
                await _db.StatusMessages.AddAsync(StatusMessage);
                await _db.SaveChangesAsync();
                return await _db.StatusMessages.FindAsync(StatusMessage.StatusMessageId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<StatusMessage> UpdateStatusMessage(StatusMessage StatusMessage)
        {
            try
            {
                _db.Entry(StatusMessage).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return StatusMessage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteStatusMessage(StatusMessage StatusMessage)
        {
            try
            {
                var dbStatusMessage = await _db.StatusMessages.FindAsync(StatusMessage.StatusMessageId);

                if (dbStatusMessage == null)
                {
                    return (false, "StatusMessage could not be found");
                }

                _db.StatusMessages.Remove(StatusMessage);
                await _db.SaveChangesAsync();

                return (true, "StatusMessage got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion StatusMessages
    }
}
