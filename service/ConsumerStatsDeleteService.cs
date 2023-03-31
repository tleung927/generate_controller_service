using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerStatsDeleteService
    {
        // ConsumerStatsDeletes Services
        Task<List<ConsumerStatsDelete>> GetConsumerStatsDeleteListByValue(int offset, int limit, string val); // GET All ConsumerStatsDeletess
        Task<ConsumerStatsDelete> GetConsumerStatsDelete(string ConsumerStatsDelete_name); // GET Single ConsumerStatsDeletes        
        Task<List<ConsumerStatsDelete>> GetConsumerStatsDeleteList(string ConsumerStatsDelete_name); // GET List ConsumerStatsDeletes        
        Task<ConsumerStatsDelete> AddConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete); // POST New ConsumerStatsDeletes
        Task<ConsumerStatsDelete> UpdateConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete); // PUT ConsumerStatsDeletes
        Task<(bool, string)> DeleteConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete); // DELETE ConsumerStatsDeletes
    }

    public class ConsumerStatsDeleteService : IConsumerStatsDeleteService
    {
        private readonly XixsrvContext _db;

        public ConsumerStatsDeleteService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerStatsDeletes

        public async Task<List<ConsumerStatsDelete>> GetConsumerStatsDeleteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerStatsDeletes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerStatsDelete> GetConsumerStatsDelete(string ConsumerStatsDelete_id)
        {
            try
            {
                return await _db.ConsumerStatsDeletes.FindAsync(ConsumerStatsDelete_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerStatsDelete>> GetConsumerStatsDeleteList(string ConsumerStatsDelete_id)
        {
            try
            {
                return await _db.ConsumerStatsDeletes
                    .Where(i => i.ConsumerStatsDeleteId == ConsumerStatsDelete_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerStatsDelete> AddConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete)
        {
            try
            {
                await _db.ConsumerStatsDeletes.AddAsync(ConsumerStatsDelete);
                await _db.SaveChangesAsync();
                return await _db.ConsumerStatsDeletes.FindAsync(ConsumerStatsDelete.ConsumerStatsDeleteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerStatsDelete> UpdateConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete)
        {
            try
            {
                _db.Entry(ConsumerStatsDelete).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerStatsDelete;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete)
        {
            try
            {
                var dbConsumerStatsDelete = await _db.ConsumerStatsDeletes.FindAsync(ConsumerStatsDelete.ConsumerStatsDeleteId);

                if (dbConsumerStatsDelete == null)
                {
                    return (false, "ConsumerStatsDelete could not be found");
                }

                _db.ConsumerStatsDeletes.Remove(ConsumerStatsDelete);
                await _db.SaveChangesAsync();

                return (true, "ConsumerStatsDelete got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerStatsDeletes
    }
}
