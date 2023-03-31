using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMessagingService
    {
        // Messagings Services
        Task<List<Messaging>> GetMessagingListByValue(int offset, int limit, string val); // GET All Messagingss
        Task<Messaging> GetMessaging(string Messaging_name); // GET Single Messagings        
        Task<List<Messaging>> GetMessagingList(string Messaging_name); // GET List Messagings        
        Task<Messaging> AddMessaging(Messaging Messaging); // POST New Messagings
        Task<Messaging> UpdateMessaging(Messaging Messaging); // PUT Messagings
        Task<(bool, string)> DeleteMessaging(Messaging Messaging); // DELETE Messagings
    }

    public class MessagingService : IMessagingService
    {
        private readonly XixsrvContext _db;

        public MessagingService(XixsrvContext db)
        {
            _db = db;
        }

        #region Messagings

        public async Task<List<Messaging>> GetMessagingListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Messagings.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Messaging> GetMessaging(string Messaging_id)
        {
            try
            {
                return await _db.Messagings.FindAsync(Messaging_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Messaging>> GetMessagingList(string Messaging_id)
        {
            try
            {
                return await _db.Messagings
                    .Where(i => i.MessagingId == Messaging_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Messaging> AddMessaging(Messaging Messaging)
        {
            try
            {
                await _db.Messagings.AddAsync(Messaging);
                await _db.SaveChangesAsync();
                return await _db.Messagings.FindAsync(Messaging.MessagingId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Messaging> UpdateMessaging(Messaging Messaging)
        {
            try
            {
                _db.Entry(Messaging).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Messaging;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMessaging(Messaging Messaging)
        {
            try
            {
                var dbMessaging = await _db.Messagings.FindAsync(Messaging.MessagingId);

                if (dbMessaging == null)
                {
                    return (false, "Messaging could not be found");
                }

                _db.Messagings.Remove(Messaging);
                await _db.SaveChangesAsync();

                return (true, "Messaging got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Messagings
    }
}
