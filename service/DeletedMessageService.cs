using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedMessageService
    {
        // DeletedMessages Services
        Task<List<DeletedMessage>> GetDeletedMessageListByValue(int offset, int limit, string val); // GET All DeletedMessagess
        Task<DeletedMessage> GetDeletedMessage(string DeletedMessage_name); // GET Single DeletedMessages        
        Task<List<DeletedMessage>> GetDeletedMessageList(string DeletedMessage_name); // GET List DeletedMessages        
        Task<DeletedMessage> AddDeletedMessage(DeletedMessage DeletedMessage); // POST New DeletedMessages
        Task<DeletedMessage> UpdateDeletedMessage(DeletedMessage DeletedMessage); // PUT DeletedMessages
        Task<(bool, string)> DeleteDeletedMessage(DeletedMessage DeletedMessage); // DELETE DeletedMessages
    }

    public class DeletedMessageService : IDeletedMessageService
    {
        private readonly XixsrvContext _db;

        public DeletedMessageService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedMessages

        public async Task<List<DeletedMessage>> GetDeletedMessageListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedMessages.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedMessage> GetDeletedMessage(string DeletedMessage_id)
        {
            try
            {
                return await _db.DeletedMessages.FindAsync(DeletedMessage_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedMessage>> GetDeletedMessageList(string DeletedMessage_id)
        {
            try
            {
                return await _db.DeletedMessages
                    .Where(i => i.DeletedMessageId == DeletedMessage_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedMessage> AddDeletedMessage(DeletedMessage DeletedMessage)
        {
            try
            {
                await _db.DeletedMessages.AddAsync(DeletedMessage);
                await _db.SaveChangesAsync();
                return await _db.DeletedMessages.FindAsync(DeletedMessage.DeletedMessageId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedMessage> UpdateDeletedMessage(DeletedMessage DeletedMessage)
        {
            try
            {
                _db.Entry(DeletedMessage).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedMessage;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedMessage(DeletedMessage DeletedMessage)
        {
            try
            {
                var dbDeletedMessage = await _db.DeletedMessages.FindAsync(DeletedMessage.DeletedMessageId);

                if (dbDeletedMessage == null)
                {
                    return (false, "DeletedMessage could not be found");
                }

                _db.DeletedMessages.Remove(DeletedMessage);
                await _db.SaveChangesAsync();

                return (true, "DeletedMessage got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedMessages
    }
}
