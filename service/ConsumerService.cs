using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerService
    {
        // Consumers Services
        Task<List<Consumer>> GetConsumerListByValue(int offset, int limit, string val); // GET All Consumerss
        Task<Consumer> GetConsumer(string Consumer_name); // GET Single Consumers        
        Task<List<Consumer>> GetConsumerList(string Consumer_name); // GET List Consumers        
        Task<Consumer> AddConsumer(Consumer Consumer); // POST New Consumers
        Task<Consumer> UpdateConsumer(Consumer Consumer); // PUT Consumers
        Task<(bool, string)> DeleteConsumer(Consumer Consumer); // DELETE Consumers
    }

    public class ConsumerService : IConsumerService
    {
        private readonly XixsrvContext _db;

        public ConsumerService(XixsrvContext db)
        {
            _db = db;
        }

        #region Consumers

        public async Task<List<Consumer>> GetConsumerListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Consumers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Consumer> GetConsumer(string Consumer_id)
        {
            try
            {
                return await _db.Consumers.FindAsync(Consumer_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Consumer>> GetConsumerList(string Consumer_id)
        {
            try
            {
                return await _db.Consumers
                    .Where(i => i.ConsumerId == Consumer_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Consumer> AddConsumer(Consumer Consumer)
        {
            try
            {
                await _db.Consumers.AddAsync(Consumer);
                await _db.SaveChangesAsync();
                return await _db.Consumers.FindAsync(Consumer.ConsumerId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Consumer> UpdateConsumer(Consumer Consumer)
        {
            try
            {
                _db.Entry(Consumer).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Consumer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumer(Consumer Consumer)
        {
            try
            {
                var dbConsumer = await _db.Consumers.FindAsync(Consumer.ConsumerId);

                if (dbConsumer == null)
                {
                    return (false, "Consumer could not be found");
                }

                _db.Consumers.Remove(Consumer);
                await _db.SaveChangesAsync();

                return (true, "Consumer got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Consumers
    }
}
