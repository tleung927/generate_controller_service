using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerLabeltipService
    {
        // ConsumerLabeltips Services
        Task<List<ConsumerLabeltip>> GetConsumerLabeltipListByValue(int offset, int limit, string val); // GET All ConsumerLabeltipss
        Task<ConsumerLabeltip> GetConsumerLabeltip(string ConsumerLabeltip_name); // GET Single ConsumerLabeltips        
        Task<List<ConsumerLabeltip>> GetConsumerLabeltipList(string ConsumerLabeltip_name); // GET List ConsumerLabeltips        
        Task<ConsumerLabeltip> AddConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip); // POST New ConsumerLabeltips
        Task<ConsumerLabeltip> UpdateConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip); // PUT ConsumerLabeltips
        Task<(bool, string)> DeleteConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip); // DELETE ConsumerLabeltips
    }

    public class ConsumerLabeltipService : IConsumerLabeltipService
    {
        private readonly XixsrvContext _db;

        public ConsumerLabeltipService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerLabeltips

        public async Task<List<ConsumerLabeltip>> GetConsumerLabeltipListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerLabeltips.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerLabeltip> GetConsumerLabeltip(string ConsumerLabeltip_id)
        {
            try
            {
                return await _db.ConsumerLabeltips.FindAsync(ConsumerLabeltip_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerLabeltip>> GetConsumerLabeltipList(string ConsumerLabeltip_id)
        {
            try
            {
                return await _db.ConsumerLabeltips
                    .Where(i => i.ConsumerLabeltipId == ConsumerLabeltip_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerLabeltip> AddConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip)
        {
            try
            {
                await _db.ConsumerLabeltips.AddAsync(ConsumerLabeltip);
                await _db.SaveChangesAsync();
                return await _db.ConsumerLabeltips.FindAsync(ConsumerLabeltip.ConsumerLabeltipId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerLabeltip> UpdateConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip)
        {
            try
            {
                _db.Entry(ConsumerLabeltip).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerLabeltip;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip)
        {
            try
            {
                var dbConsumerLabeltip = await _db.ConsumerLabeltips.FindAsync(ConsumerLabeltip.ConsumerLabeltipId);

                if (dbConsumerLabeltip == null)
                {
                    return (false, "ConsumerLabeltip could not be found");
                }

                _db.ConsumerLabeltips.Remove(ConsumerLabeltip);
                await _db.SaveChangesAsync();

                return (true, "ConsumerLabeltip got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerLabeltips
    }
}
