using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerHistoryService
    {
        // ConsumerHistorys Services
        Task<List<ConsumerHistory>> GetConsumerHistoryListByValue(int offset, int limit, string val); // GET All ConsumerHistoryss
        Task<ConsumerHistory> GetConsumerHistory(string ConsumerHistory_name); // GET Single ConsumerHistorys        
        Task<List<ConsumerHistory>> GetConsumerHistoryList(string ConsumerHistory_name); // GET List ConsumerHistorys        
        Task<ConsumerHistory> AddConsumerHistory(ConsumerHistory ConsumerHistory); // POST New ConsumerHistorys
        Task<ConsumerHistory> UpdateConsumerHistory(ConsumerHistory ConsumerHistory); // PUT ConsumerHistorys
        Task<(bool, string)> DeleteConsumerHistory(ConsumerHistory ConsumerHistory); // DELETE ConsumerHistorys
    }

    public class ConsumerHistoryService : IConsumerHistoryService
    {
        private readonly XixsrvContext _db;

        public ConsumerHistoryService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerHistorys

        public async Task<List<ConsumerHistory>> GetConsumerHistoryListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerHistorys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerHistory> GetConsumerHistory(string ConsumerHistory_id)
        {
            try
            {
                return await _db.ConsumerHistorys.FindAsync(ConsumerHistory_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerHistory>> GetConsumerHistoryList(string ConsumerHistory_id)
        {
            try
            {
                return await _db.ConsumerHistorys
                    .Where(i => i.ConsumerHistoryId == ConsumerHistory_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerHistory> AddConsumerHistory(ConsumerHistory ConsumerHistory)
        {
            try
            {
                await _db.ConsumerHistorys.AddAsync(ConsumerHistory);
                await _db.SaveChangesAsync();
                return await _db.ConsumerHistorys.FindAsync(ConsumerHistory.ConsumerHistoryId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerHistory> UpdateConsumerHistory(ConsumerHistory ConsumerHistory)
        {
            try
            {
                _db.Entry(ConsumerHistory).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerHistory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerHistory(ConsumerHistory ConsumerHistory)
        {
            try
            {
                var dbConsumerHistory = await _db.ConsumerHistorys.FindAsync(ConsumerHistory.ConsumerHistoryId);

                if (dbConsumerHistory == null)
                {
                    return (false, "ConsumerHistory could not be found");
                }

                _db.ConsumerHistorys.Remove(ConsumerHistory);
                await _db.SaveChangesAsync();

                return (true, "ConsumerHistory got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerHistorys
    }
}
