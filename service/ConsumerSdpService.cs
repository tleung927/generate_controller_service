using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerSdpService
    {
        // ConsumerSdps Services
        Task<List<ConsumerSdp>> GetConsumerSdpListByValue(int offset, int limit, string val); // GET All ConsumerSdpss
        Task<ConsumerSdp> GetConsumerSdp(string ConsumerSdp_name); // GET Single ConsumerSdps        
        Task<List<ConsumerSdp>> GetConsumerSdpList(string ConsumerSdp_name); // GET List ConsumerSdps        
        Task<ConsumerSdp> AddConsumerSdp(ConsumerSdp ConsumerSdp); // POST New ConsumerSdps
        Task<ConsumerSdp> UpdateConsumerSdp(ConsumerSdp ConsumerSdp); // PUT ConsumerSdps
        Task<(bool, string)> DeleteConsumerSdp(ConsumerSdp ConsumerSdp); // DELETE ConsumerSdps
    }

    public class ConsumerSdpService : IConsumerSdpService
    {
        private readonly XixsrvContext _db;

        public ConsumerSdpService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerSdps

        public async Task<List<ConsumerSdp>> GetConsumerSdpListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerSdps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerSdp> GetConsumerSdp(string ConsumerSdp_id)
        {
            try
            {
                return await _db.ConsumerSdps.FindAsync(ConsumerSdp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerSdp>> GetConsumerSdpList(string ConsumerSdp_id)
        {
            try
            {
                return await _db.ConsumerSdps
                    .Where(i => i.ConsumerSdpId == ConsumerSdp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerSdp> AddConsumerSdp(ConsumerSdp ConsumerSdp)
        {
            try
            {
                await _db.ConsumerSdps.AddAsync(ConsumerSdp);
                await _db.SaveChangesAsync();
                return await _db.ConsumerSdps.FindAsync(ConsumerSdp.ConsumerSdpId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerSdp> UpdateConsumerSdp(ConsumerSdp ConsumerSdp)
        {
            try
            {
                _db.Entry(ConsumerSdp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerSdp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerSdp(ConsumerSdp ConsumerSdp)
        {
            try
            {
                var dbConsumerSdp = await _db.ConsumerSdps.FindAsync(ConsumerSdp.ConsumerSdpId);

                if (dbConsumerSdp == null)
                {
                    return (false, "ConsumerSdp could not be found");
                }

                _db.ConsumerSdps.Remove(ConsumerSdp);
                await _db.SaveChangesAsync();

                return (true, "ConsumerSdp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerSdps
    }
}
