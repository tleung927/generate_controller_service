using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerLocationService
    {
        // ConsumerLocations Services
        Task<List<ConsumerLocation>> GetConsumerLocationListByValue(int offset, int limit, string val); // GET All ConsumerLocationss
        Task<ConsumerLocation> GetConsumerLocation(string ConsumerLocation_name); // GET Single ConsumerLocations        
        Task<List<ConsumerLocation>> GetConsumerLocationList(string ConsumerLocation_name); // GET List ConsumerLocations        
        Task<ConsumerLocation> AddConsumerLocation(ConsumerLocation ConsumerLocation); // POST New ConsumerLocations
        Task<ConsumerLocation> UpdateConsumerLocation(ConsumerLocation ConsumerLocation); // PUT ConsumerLocations
        Task<(bool, string)> DeleteConsumerLocation(ConsumerLocation ConsumerLocation); // DELETE ConsumerLocations
    }

    public class ConsumerLocationService : IConsumerLocationService
    {
        private readonly XixsrvContext _db;

        public ConsumerLocationService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerLocations

        public async Task<List<ConsumerLocation>> GetConsumerLocationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerLocations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerLocation> GetConsumerLocation(string ConsumerLocation_id)
        {
            try
            {
                return await _db.ConsumerLocations.FindAsync(ConsumerLocation_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerLocation>> GetConsumerLocationList(string ConsumerLocation_id)
        {
            try
            {
                return await _db.ConsumerLocations
                    .Where(i => i.ConsumerLocationId == ConsumerLocation_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerLocation> AddConsumerLocation(ConsumerLocation ConsumerLocation)
        {
            try
            {
                await _db.ConsumerLocations.AddAsync(ConsumerLocation);
                await _db.SaveChangesAsync();
                return await _db.ConsumerLocations.FindAsync(ConsumerLocation.ConsumerLocationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerLocation> UpdateConsumerLocation(ConsumerLocation ConsumerLocation)
        {
            try
            {
                _db.Entry(ConsumerLocation).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerLocation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerLocation(ConsumerLocation ConsumerLocation)
        {
            try
            {
                var dbConsumerLocation = await _db.ConsumerLocations.FindAsync(ConsumerLocation.ConsumerLocationId);

                if (dbConsumerLocation == null)
                {
                    return (false, "ConsumerLocation could not be found");
                }

                _db.ConsumerLocations.Remove(ConsumerLocation);
                await _db.SaveChangesAsync();

                return (true, "ConsumerLocation got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerLocations
    }
}
