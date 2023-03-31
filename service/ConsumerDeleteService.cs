using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerDeleteService
    {
        // ConsumerDeletes Services
        Task<List<ConsumerDelete>> GetConsumerDeleteListByValue(int offset, int limit, string val); // GET All ConsumerDeletess
        Task<ConsumerDelete> GetConsumerDelete(string ConsumerDelete_name); // GET Single ConsumerDeletes        
        Task<List<ConsumerDelete>> GetConsumerDeleteList(string ConsumerDelete_name); // GET List ConsumerDeletes        
        Task<ConsumerDelete> AddConsumerDelete(ConsumerDelete ConsumerDelete); // POST New ConsumerDeletes
        Task<ConsumerDelete> UpdateConsumerDelete(ConsumerDelete ConsumerDelete); // PUT ConsumerDeletes
        Task<(bool, string)> DeleteConsumerDelete(ConsumerDelete ConsumerDelete); // DELETE ConsumerDeletes
    }

    public class ConsumerDeleteService : IConsumerDeleteService
    {
        private readonly XixsrvContext _db;

        public ConsumerDeleteService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerDeletes

        public async Task<List<ConsumerDelete>> GetConsumerDeleteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerDeletes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerDelete> GetConsumerDelete(string ConsumerDelete_id)
        {
            try
            {
                return await _db.ConsumerDeletes.FindAsync(ConsumerDelete_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerDelete>> GetConsumerDeleteList(string ConsumerDelete_id)
        {
            try
            {
                return await _db.ConsumerDeletes
                    .Where(i => i.ConsumerDeleteId == ConsumerDelete_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerDelete> AddConsumerDelete(ConsumerDelete ConsumerDelete)
        {
            try
            {
                await _db.ConsumerDeletes.AddAsync(ConsumerDelete);
                await _db.SaveChangesAsync();
                return await _db.ConsumerDeletes.FindAsync(ConsumerDelete.ConsumerDeleteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerDelete> UpdateConsumerDelete(ConsumerDelete ConsumerDelete)
        {
            try
            {
                _db.Entry(ConsumerDelete).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerDelete;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerDelete(ConsumerDelete ConsumerDelete)
        {
            try
            {
                var dbConsumerDelete = await _db.ConsumerDeletes.FindAsync(ConsumerDelete.ConsumerDeleteId);

                if (dbConsumerDelete == null)
                {
                    return (false, "ConsumerDelete could not be found");
                }

                _db.ConsumerDeletes.Remove(ConsumerDelete);
                await _db.SaveChangesAsync();

                return (true, "ConsumerDelete got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerDeletes
    }
}
