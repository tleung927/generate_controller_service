using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IConsumerAddService
    {
        // ConsumerAdds Services
        Task<List<ConsumerAdd>> GetConsumerAddListByValue(int offset, int limit, string val); // GET All ConsumerAddss
        Task<ConsumerAdd> GetConsumerAdd(string ConsumerAdd_name); // GET Single ConsumerAdds        
        Task<List<ConsumerAdd>> GetConsumerAddList(string ConsumerAdd_name); // GET List ConsumerAdds        
        Task<ConsumerAdd> AddConsumerAdd(ConsumerAdd ConsumerAdd); // POST New ConsumerAdds
        Task<ConsumerAdd> UpdateConsumerAdd(ConsumerAdd ConsumerAdd); // PUT ConsumerAdds
        Task<(bool, string)> DeleteConsumerAdd(ConsumerAdd ConsumerAdd); // DELETE ConsumerAdds
    }

    public class ConsumerAddService : IConsumerAddService
    {
        private readonly XixsrvContext _db;

        public ConsumerAddService(XixsrvContext db)
        {
            _db = db;
        }

        #region ConsumerAdds

        public async Task<List<ConsumerAdd>> GetConsumerAddListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ConsumerAdds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ConsumerAdd> GetConsumerAdd(string ConsumerAdd_id)
        {
            try
            {
                return await _db.ConsumerAdds.FindAsync(ConsumerAdd_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ConsumerAdd>> GetConsumerAddList(string ConsumerAdd_id)
        {
            try
            {
                return await _db.ConsumerAdds
                    .Where(i => i.ConsumerAddId == ConsumerAdd_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ConsumerAdd> AddConsumerAdd(ConsumerAdd ConsumerAdd)
        {
            try
            {
                await _db.ConsumerAdds.AddAsync(ConsumerAdd);
                await _db.SaveChangesAsync();
                return await _db.ConsumerAdds.FindAsync(ConsumerAdd.ConsumerAddId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ConsumerAdd> UpdateConsumerAdd(ConsumerAdd ConsumerAdd)
        {
            try
            {
                _db.Entry(ConsumerAdd).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ConsumerAdd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteConsumerAdd(ConsumerAdd ConsumerAdd)
        {
            try
            {
                var dbConsumerAdd = await _db.ConsumerAdds.FindAsync(ConsumerAdd.ConsumerAddId);

                if (dbConsumerAdd == null)
                {
                    return (false, "ConsumerAdd could not be found");
                }

                _db.ConsumerAdds.Remove(ConsumerAdd);
                await _db.SaveChangesAsync();

                return (true, "ConsumerAdd got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ConsumerAdds
    }
}
