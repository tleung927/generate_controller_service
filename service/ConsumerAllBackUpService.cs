using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerAllBackUpService
    {
        // ConsumerAllBackUps Services
        Task<List<ConsumerAllBackUp>> GetConsumerAllBackUpListByValue(int offset, int limit, string val); // GET All ConsumerAllBackUpss
        Task<ConsumerAllBackUp> GetConsumerAllBackUp(string ConsumerAllBackUp_name); // GET Single ConsumerAllBackUps        
        Task<List<ConsumerAllBackUp>> GetConsumerAllBackUpList(string ConsumerAllBackUp_name); // GET List ConsumerAllBackUps        
        Task<ConsumerAllBackUp> AddConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp); // POST New ConsumerAllBackUps
        Task<ConsumerAllBackUp> UpdateConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp); // PUT ConsumerAllBackUps
        Task<(bool, string)> DeleteConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp); // DELETE ConsumerAllBackUps
    }

    public class ConsumerAllBackUpService : IConsumerAllBackUpService
    {
        private readonly XixsrvContext _db;

        public ConsumerAllBackUpService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerAllBackUps

        public async Task<List<ConsumerAllBackUp>> GetConsumerAllBackUpListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerAllBackUps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerAllBackUp> GetConsumerAllBackUp(string ConsumerAllBackUp_id)
        {
            try
            {
                return await _db.ConsumerAllBackUps.FindAsync(ConsumerAllBackUp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerAllBackUp>> GetConsumerAllBackUpList(string ConsumerAllBackUp_id)
        {
            try
            {
                return await _db.ConsumerAllBackUps
                    .Where(i => i.ConsumerAllBackUpId == ConsumerAllBackUp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerAllBackUp> AddConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp)
        {
            try
            {
                await _db.ConsumerAllBackUps.AddAsync(ConsumerAllBackUp);
                await _db.SaveChangesAsync();
                return await _db.ConsumerAllBackUps.FindAsync(ConsumerAllBackUp.ConsumerAllBackUpId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerAllBackUp> UpdateConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp)
        {
            try
            {
                _db.Entry(ConsumerAllBackUp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerAllBackUp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp)
        {
            try
            {
                var dbConsumerAllBackUp = await _db.ConsumerAllBackUps.FindAsync(ConsumerAllBackUp.ConsumerAllBackUpId);

                if (dbConsumerAllBackUp == null)
                {
                    return (false, "ConsumerAllBackUp could not be found");
                }

                _db.ConsumerAllBackUps.Remove(ConsumerAllBackUp);
                await _db.SaveChangesAsync();

                return (true, "ConsumerAllBackUp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerAllBackUps
    }
}
