using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerAll02212020Service
    {
        // ConsumerAll02212020s Services
        Task<List<ConsumerAll02212020>> GetConsumerAll02212020ListByValue(int offset, int limit, string val); // GET All ConsumerAll02212020ss
        Task<ConsumerAll02212020> GetConsumerAll02212020(string ConsumerAll02212020_name); // GET Single ConsumerAll02212020s        
        Task<List<ConsumerAll02212020>> GetConsumerAll02212020List(string ConsumerAll02212020_name); // GET List ConsumerAll02212020s        
        Task<ConsumerAll02212020> AddConsumerAll02212020(ConsumerAll02212020 ConsumerAll02212020); // POST New ConsumerAll02212020s
        Task<ConsumerAll02212020> UpdateConsumerAll02212020(ConsumerAll02212020 ConsumerAll02212020); // PUT ConsumerAll02212020s
        Task<(bool, string)> DeleteConsumerAll02212020(ConsumerAll02212020 ConsumerAll02212020); // DELETE ConsumerAll02212020s
    }

    public class ConsumerAll02212020Service : IConsumerAll02212020Service
    {
        private readonly XixsrvContext _db;

        public ConsumerAll02212020Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerAll02212020s

        public async Task<List<ConsumerAll02212020>> GetConsumerAll02212020ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerAll02212020s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerAll02212020> GetConsumerAll02212020(string ConsumerAll02212020_id)
        {
            try
            {
                return await _db.ConsumerAll02212020s.FindAsync(ConsumerAll02212020_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerAll02212020>> GetConsumerAll02212020List(string ConsumerAll02212020_id)
        {
            try
            {
                return await _db.ConsumerAll02212020s
                    .Where(i => i.ConsumerAll02212020Id == ConsumerAll02212020_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerAll02212020> AddConsumerAll02212020(ConsumerAll02212020 ConsumerAll02212020)
        {
            try
            {
                await _db.ConsumerAll02212020s.AddAsync(ConsumerAll02212020);
                await _db.SaveChangesAsync();
                return await _db.ConsumerAll02212020s.FindAsync(ConsumerAll02212020.ConsumerAll02212020Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerAll02212020> UpdateConsumerAll02212020(ConsumerAll02212020 ConsumerAll02212020)
        {
            try
            {
                _db.Entry(ConsumerAll02212020).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerAll02212020;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerAll02212020(ConsumerAll02212020 ConsumerAll02212020)
        {
            try
            {
                var dbConsumerAll02212020 = await _db.ConsumerAll02212020s.FindAsync(ConsumerAll02212020.ConsumerAll02212020Id);

                if (dbConsumerAll02212020 == null)
                {
                    return (false, "ConsumerAll02212020 could not be found");
                }

                _db.ConsumerAll02212020s.Remove(ConsumerAll02212020);
                await _db.SaveChangesAsync();

                return (true, "ConsumerAll02212020 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerAll02212020s
    }
}
