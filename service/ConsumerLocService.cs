using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerLocService
    {
        // ConsumerLocs Services
        Task<List<ConsumerLoc>> GetConsumerLocListByValue(int offset, int limit, string val); // GET All ConsumerLocss
        Task<ConsumerLoc> GetConsumerLoc(string ConsumerLoc_name); // GET Single ConsumerLocs        
        Task<List<ConsumerLoc>> GetConsumerLocList(string ConsumerLoc_name); // GET List ConsumerLocs        
        Task<ConsumerLoc> AddConsumerLoc(ConsumerLoc ConsumerLoc); // POST New ConsumerLocs
        Task<ConsumerLoc> UpdateConsumerLoc(ConsumerLoc ConsumerLoc); // PUT ConsumerLocs
        Task<(bool, string)> DeleteConsumerLoc(ConsumerLoc ConsumerLoc); // DELETE ConsumerLocs
    }

    public class ConsumerLocService : IConsumerLocService
    {
        private readonly XixsrvContext _db;

        public ConsumerLocService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerLocs

        public async Task<List<ConsumerLoc>> GetConsumerLocListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerLocs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerLoc> GetConsumerLoc(string ConsumerLoc_id)
        {
            try
            {
                return await _db.ConsumerLocs.FindAsync(ConsumerLoc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerLoc>> GetConsumerLocList(string ConsumerLoc_id)
        {
            try
            {
                return await _db.ConsumerLocs
                    .Where(i => i.ConsumerLocId == ConsumerLoc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerLoc> AddConsumerLoc(ConsumerLoc ConsumerLoc)
        {
            try
            {
                await _db.ConsumerLocs.AddAsync(ConsumerLoc);
                await _db.SaveChangesAsync();
                return await _db.ConsumerLocs.FindAsync(ConsumerLoc.ConsumerLocId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerLoc> UpdateConsumerLoc(ConsumerLoc ConsumerLoc)
        {
            try
            {
                _db.Entry(ConsumerLoc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerLoc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerLoc(ConsumerLoc ConsumerLoc)
        {
            try
            {
                var dbConsumerLoc = await _db.ConsumerLocs.FindAsync(ConsumerLoc.ConsumerLocId);

                if (dbConsumerLoc == null)
                {
                    return (false, "ConsumerLoc could not be found");
                }

                _db.ConsumerLocs.Remove(ConsumerLoc);
                await _db.SaveChangesAsync();

                return (true, "ConsumerLoc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerLocs
    }
}
