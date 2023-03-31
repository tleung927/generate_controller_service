using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerInfoService
    {
        // ConsumerInfos Services
        Task<List<ConsumerInfo>> GetConsumerInfoListByValue(int offset, int limit, string val); // GET All ConsumerInfoss
        Task<ConsumerInfo> GetConsumerInfo(string ConsumerInfo_name); // GET Single ConsumerInfos        
        Task<List<ConsumerInfo>> GetConsumerInfoList(string ConsumerInfo_name); // GET List ConsumerInfos        
        Task<ConsumerInfo> AddConsumerInfo(ConsumerInfo ConsumerInfo); // POST New ConsumerInfos
        Task<ConsumerInfo> UpdateConsumerInfo(ConsumerInfo ConsumerInfo); // PUT ConsumerInfos
        Task<(bool, string)> DeleteConsumerInfo(ConsumerInfo ConsumerInfo); // DELETE ConsumerInfos
    }

    public class ConsumerInfoService : IConsumerInfoService
    {
        private readonly XixsrvContext _db;

        public ConsumerInfoService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerInfos

        public async Task<List<ConsumerInfo>> GetConsumerInfoListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerInfos.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerInfo> GetConsumerInfo(string ConsumerInfo_id)
        {
            try
            {
                return await _db.ConsumerInfos.FindAsync(ConsumerInfo_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerInfo>> GetConsumerInfoList(string ConsumerInfo_id)
        {
            try
            {
                return await _db.ConsumerInfos
                    .Where(i => i.ConsumerInfoId == ConsumerInfo_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerInfo> AddConsumerInfo(ConsumerInfo ConsumerInfo)
        {
            try
            {
                await _db.ConsumerInfos.AddAsync(ConsumerInfo);
                await _db.SaveChangesAsync();
                return await _db.ConsumerInfos.FindAsync(ConsumerInfo.ConsumerInfoId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerInfo> UpdateConsumerInfo(ConsumerInfo ConsumerInfo)
        {
            try
            {
                _db.Entry(ConsumerInfo).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerInfo;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerInfo(ConsumerInfo ConsumerInfo)
        {
            try
            {
                var dbConsumerInfo = await _db.ConsumerInfos.FindAsync(ConsumerInfo.ConsumerInfoId);

                if (dbConsumerInfo == null)
                {
                    return (false, "ConsumerInfo could not be found");
                }

                _db.ConsumerInfos.Remove(ConsumerInfo);
                await _db.SaveChangesAsync();

                return (true, "ConsumerInfo got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerInfos
    }
}
