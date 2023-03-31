using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerStatService
    {
        // ConsumerStats Services
        Task<List<ConsumerStat>> GetConsumerStatListByValue(int offset, int limit, string val); // GET All ConsumerStatss
        Task<ConsumerStat> GetConsumerStat(string ConsumerStat_name); // GET Single ConsumerStats        
        Task<List<ConsumerStat>> GetConsumerStatList(string ConsumerStat_name); // GET List ConsumerStats        
        Task<ConsumerStat> AddConsumerStat(ConsumerStat ConsumerStat); // POST New ConsumerStats
        Task<ConsumerStat> UpdateConsumerStat(ConsumerStat ConsumerStat); // PUT ConsumerStats
        Task<(bool, string)> DeleteConsumerStat(ConsumerStat ConsumerStat); // DELETE ConsumerStats
    }

    public class ConsumerStatService : IConsumerStatService
    {
        private readonly XixsrvContext _db;

        public ConsumerStatService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerStats

        public async Task<List<ConsumerStat>> GetConsumerStatListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerStats.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerStat> GetConsumerStat(string ConsumerStat_id)
        {
            try
            {
                return await _db.ConsumerStats.FindAsync(ConsumerStat_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerStat>> GetConsumerStatList(string ConsumerStat_id)
        {
            try
            {
                return await _db.ConsumerStats
                    .Where(i => i.ConsumerStatId == ConsumerStat_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerStat> AddConsumerStat(ConsumerStat ConsumerStat)
        {
            try
            {
                await _db.ConsumerStats.AddAsync(ConsumerStat);
                await _db.SaveChangesAsync();
                return await _db.ConsumerStats.FindAsync(ConsumerStat.ConsumerStatId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerStat> UpdateConsumerStat(ConsumerStat ConsumerStat)
        {
            try
            {
                _db.Entry(ConsumerStat).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerStat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerStat(ConsumerStat ConsumerStat)
        {
            try
            {
                var dbConsumerStat = await _db.ConsumerStats.FindAsync(ConsumerStat.ConsumerStatId);

                if (dbConsumerStat == null)
                {
                    return (false, "ConsumerStat could not be found");
                }

                _db.ConsumerStats.Remove(ConsumerStat);
                await _db.SaveChangesAsync();

                return (true, "ConsumerStat got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerStats
    }
}
