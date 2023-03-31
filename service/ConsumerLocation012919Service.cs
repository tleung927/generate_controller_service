using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerLocation012919Service
    {
        // ConsumerLocation012919s Services
        Task<List<ConsumerLocation012919>> GetConsumerLocation012919ListByValue(int offset, int limit, string val); // GET All ConsumerLocation012919ss
        Task<ConsumerLocation012919> GetConsumerLocation012919(string ConsumerLocation012919_name); // GET Single ConsumerLocation012919s        
        Task<List<ConsumerLocation012919>> GetConsumerLocation012919List(string ConsumerLocation012919_name); // GET List ConsumerLocation012919s        
        Task<ConsumerLocation012919> AddConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919); // POST New ConsumerLocation012919s
        Task<ConsumerLocation012919> UpdateConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919); // PUT ConsumerLocation012919s
        Task<(bool, string)> DeleteConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919); // DELETE ConsumerLocation012919s
    }

    public class ConsumerLocation012919Service : IConsumerLocation012919Service
    {
        private readonly XixsrvContext _db;

        public ConsumerLocation012919Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerLocation012919s

        public async Task<List<ConsumerLocation012919>> GetConsumerLocation012919ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerLocation012919s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerLocation012919> GetConsumerLocation012919(string ConsumerLocation012919_id)
        {
            try
            {
                return await _db.ConsumerLocation012919s.FindAsync(ConsumerLocation012919_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerLocation012919>> GetConsumerLocation012919List(string ConsumerLocation012919_id)
        {
            try
            {
                return await _db.ConsumerLocation012919s
                    .Where(i => i.ConsumerLocation012919Id == ConsumerLocation012919_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerLocation012919> AddConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919)
        {
            try
            {
                await _db.ConsumerLocation012919s.AddAsync(ConsumerLocation012919);
                await _db.SaveChangesAsync();
                return await _db.ConsumerLocation012919s.FindAsync(ConsumerLocation012919.ConsumerLocation012919Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerLocation012919> UpdateConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919)
        {
            try
            {
                _db.Entry(ConsumerLocation012919).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerLocation012919;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919)
        {
            try
            {
                var dbConsumerLocation012919 = await _db.ConsumerLocation012919s.FindAsync(ConsumerLocation012919.ConsumerLocation012919Id);

                if (dbConsumerLocation012919 == null)
                {
                    return (false, "ConsumerLocation012919 could not be found");
                }

                _db.ConsumerLocation012919s.Remove(ConsumerLocation012919);
                await _db.SaveChangesAsync();

                return (true, "ConsumerLocation012919 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerLocation012919s
    }
}
