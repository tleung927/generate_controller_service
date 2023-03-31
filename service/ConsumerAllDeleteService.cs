using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerAllDeleteService
    {
        // ConsumerAllDeletes Services
        Task<List<ConsumerAllDelete>> GetConsumerAllDeleteListByValue(int offset, int limit, string val); // GET All ConsumerAllDeletess
        Task<ConsumerAllDelete> GetConsumerAllDelete(string ConsumerAllDelete_name); // GET Single ConsumerAllDeletes        
        Task<List<ConsumerAllDelete>> GetConsumerAllDeleteList(string ConsumerAllDelete_name); // GET List ConsumerAllDeletes        
        Task<ConsumerAllDelete> AddConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete); // POST New ConsumerAllDeletes
        Task<ConsumerAllDelete> UpdateConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete); // PUT ConsumerAllDeletes
        Task<(bool, string)> DeleteConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete); // DELETE ConsumerAllDeletes
    }

    public class ConsumerAllDeleteService : IConsumerAllDeleteService
    {
        private readonly XixsrvContext _db;

        public ConsumerAllDeleteService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerAllDeletes

        public async Task<List<ConsumerAllDelete>> GetConsumerAllDeleteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerAllDeletes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerAllDelete> GetConsumerAllDelete(string ConsumerAllDelete_id)
        {
            try
            {
                return await _db.ConsumerAllDeletes.FindAsync(ConsumerAllDelete_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerAllDelete>> GetConsumerAllDeleteList(string ConsumerAllDelete_id)
        {
            try
            {
                return await _db.ConsumerAllDeletes
                    .Where(i => i.ConsumerAllDeleteId == ConsumerAllDelete_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerAllDelete> AddConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete)
        {
            try
            {
                await _db.ConsumerAllDeletes.AddAsync(ConsumerAllDelete);
                await _db.SaveChangesAsync();
                return await _db.ConsumerAllDeletes.FindAsync(ConsumerAllDelete.ConsumerAllDeleteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerAllDelete> UpdateConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete)
        {
            try
            {
                _db.Entry(ConsumerAllDelete).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerAllDelete;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete)
        {
            try
            {
                var dbConsumerAllDelete = await _db.ConsumerAllDeletes.FindAsync(ConsumerAllDelete.ConsumerAllDeleteId);

                if (dbConsumerAllDelete == null)
                {
                    return (false, "ConsumerAllDelete could not be found");
                }

                _db.ConsumerAllDeletes.Remove(ConsumerAllDelete);
                await _db.SaveChangesAsync();

                return (true, "ConsumerAllDelete got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerAllDeletes
    }
}
