using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerAllService
    {
        // ConsumerAlls Services
        Task<List<ConsumerAll>> GetConsumerAllListByValue(int offset, int limit, string val); // GET All ConsumerAllss
        Task<ConsumerAll> GetConsumerAll(string ConsumerAll_name); // GET Single ConsumerAlls        
        Task<List<ConsumerAll>> GetConsumerAllList(string ConsumerAll_name); // GET List ConsumerAlls        
        Task<ConsumerAll> AddConsumerAll(ConsumerAll ConsumerAll); // POST New ConsumerAlls
        Task<ConsumerAll> UpdateConsumerAll(ConsumerAll ConsumerAll); // PUT ConsumerAlls
        Task<(bool, string)> DeleteConsumerAll(ConsumerAll ConsumerAll); // DELETE ConsumerAlls
    }

    public class ConsumerAllService : IConsumerAllService
    {
        private readonly XixsrvContext _db;

        public ConsumerAllService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerAlls

        public async Task<List<ConsumerAll>> GetConsumerAllListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerAlls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerAll> GetConsumerAll(string ConsumerAll_id)
        {
            try
            {
                return await _db.ConsumerAlls.FindAsync(ConsumerAll_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerAll>> GetConsumerAllList(string ConsumerAll_id)
        {
            try
            {
                return await _db.ConsumerAlls
                    .Where(i => i.ConsumerAllId == ConsumerAll_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerAll> AddConsumerAll(ConsumerAll ConsumerAll)
        {
            try
            {
                await _db.ConsumerAlls.AddAsync(ConsumerAll);
                await _db.SaveChangesAsync();
                return await _db.ConsumerAlls.FindAsync(ConsumerAll.ConsumerAllId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerAll> UpdateConsumerAll(ConsumerAll ConsumerAll)
        {
            try
            {
                _db.Entry(ConsumerAll).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerAll;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerAll(ConsumerAll ConsumerAll)
        {
            try
            {
                var dbConsumerAll = await _db.ConsumerAlls.FindAsync(ConsumerAll.ConsumerAllId);

                if (dbConsumerAll == null)
                {
                    return (false, "ConsumerAll could not be found");
                }

                _db.ConsumerAlls.Remove(ConsumerAll);
                await _db.SaveChangesAsync();

                return (true, "ConsumerAll got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerAlls
    }
}
