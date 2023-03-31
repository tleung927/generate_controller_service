using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerLabelService
    {
        // ConsumerLabels Services
        Task<List<ConsumerLabel>> GetConsumerLabelListByValue(int offset, int limit, string val); // GET All ConsumerLabelss
        Task<ConsumerLabel> GetConsumerLabel(string ConsumerLabel_name); // GET Single ConsumerLabels        
        Task<List<ConsumerLabel>> GetConsumerLabelList(string ConsumerLabel_name); // GET List ConsumerLabels        
        Task<ConsumerLabel> AddConsumerLabel(ConsumerLabel ConsumerLabel); // POST New ConsumerLabels
        Task<ConsumerLabel> UpdateConsumerLabel(ConsumerLabel ConsumerLabel); // PUT ConsumerLabels
        Task<(bool, string)> DeleteConsumerLabel(ConsumerLabel ConsumerLabel); // DELETE ConsumerLabels
    }

    public class ConsumerLabelService : IConsumerLabelService
    {
        private readonly XixsrvContext _db;

        public ConsumerLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerLabels

        public async Task<List<ConsumerLabel>> GetConsumerLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerLabel> GetConsumerLabel(string ConsumerLabel_id)
        {
            try
            {
                return await _db.ConsumerLabels.FindAsync(ConsumerLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerLabel>> GetConsumerLabelList(string ConsumerLabel_id)
        {
            try
            {
                return await _db.ConsumerLabels
                    .Where(i => i.ConsumerLabelId == ConsumerLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerLabel> AddConsumerLabel(ConsumerLabel ConsumerLabel)
        {
            try
            {
                await _db.ConsumerLabels.AddAsync(ConsumerLabel);
                await _db.SaveChangesAsync();
                return await _db.ConsumerLabels.FindAsync(ConsumerLabel.ConsumerLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerLabel> UpdateConsumerLabel(ConsumerLabel ConsumerLabel)
        {
            try
            {
                _db.Entry(ConsumerLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerLabel(ConsumerLabel ConsumerLabel)
        {
            try
            {
                var dbConsumerLabel = await _db.ConsumerLabels.FindAsync(ConsumerLabel.ConsumerLabelId);

                if (dbConsumerLabel == null)
                {
                    return (false, "ConsumerLabel could not be found");
                }

                _db.ConsumerLabels.Remove(ConsumerLabel);
                await _db.SaveChangesAsync();

                return (true, "ConsumerLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerLabels
    }
}
