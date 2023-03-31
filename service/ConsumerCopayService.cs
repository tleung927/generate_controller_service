using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerCopayService
    {
        // ConsumerCopays Services
        Task<List<ConsumerCopay>> GetConsumerCopayListByValue(int offset, int limit, string val); // GET All ConsumerCopayss
        Task<ConsumerCopay> GetConsumerCopay(string ConsumerCopay_name); // GET Single ConsumerCopays        
        Task<List<ConsumerCopay>> GetConsumerCopayList(string ConsumerCopay_name); // GET List ConsumerCopays        
        Task<ConsumerCopay> AddConsumerCopay(ConsumerCopay ConsumerCopay); // POST New ConsumerCopays
        Task<ConsumerCopay> UpdateConsumerCopay(ConsumerCopay ConsumerCopay); // PUT ConsumerCopays
        Task<(bool, string)> DeleteConsumerCopay(ConsumerCopay ConsumerCopay); // DELETE ConsumerCopays
    }

    public class ConsumerCopayService : IConsumerCopayService
    {
        private readonly XixsrvContext _db;

        public ConsumerCopayService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerCopays

        public async Task<List<ConsumerCopay>> GetConsumerCopayListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerCopays.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerCopay> GetConsumerCopay(string ConsumerCopay_id)
        {
            try
            {
                return await _db.ConsumerCopays.FindAsync(ConsumerCopay_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerCopay>> GetConsumerCopayList(string ConsumerCopay_id)
        {
            try
            {
                return await _db.ConsumerCopays
                    .Where(i => i.ConsumerCopayId == ConsumerCopay_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerCopay> AddConsumerCopay(ConsumerCopay ConsumerCopay)
        {
            try
            {
                await _db.ConsumerCopays.AddAsync(ConsumerCopay);
                await _db.SaveChangesAsync();
                return await _db.ConsumerCopays.FindAsync(ConsumerCopay.ConsumerCopayId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerCopay> UpdateConsumerCopay(ConsumerCopay ConsumerCopay)
        {
            try
            {
                _db.Entry(ConsumerCopay).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerCopay;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerCopay(ConsumerCopay ConsumerCopay)
        {
            try
            {
                var dbConsumerCopay = await _db.ConsumerCopays.FindAsync(ConsumerCopay.ConsumerCopayId);

                if (dbConsumerCopay == null)
                {
                    return (false, "ConsumerCopay could not be found");
                }

                _db.ConsumerCopays.Remove(ConsumerCopay);
                await _db.SaveChangesAsync();

                return (true, "ConsumerCopay got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerCopays
    }
}
