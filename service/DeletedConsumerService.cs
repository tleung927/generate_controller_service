using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedConsumerService
    {
        // DeletedConsumers Services
        Task<List<DeletedConsumer>> GetDeletedConsumerListByValue(int offset, int limit, string val); // GET All DeletedConsumerss
        Task<DeletedConsumer> GetDeletedConsumer(string DeletedConsumer_name); // GET Single DeletedConsumers        
        Task<List<DeletedConsumer>> GetDeletedConsumerList(string DeletedConsumer_name); // GET List DeletedConsumers        
        Task<DeletedConsumer> AddDeletedConsumer(DeletedConsumer DeletedConsumer); // POST New DeletedConsumers
        Task<DeletedConsumer> UpdateDeletedConsumer(DeletedConsumer DeletedConsumer); // PUT DeletedConsumers
        Task<(bool, string)> DeleteDeletedConsumer(DeletedConsumer DeletedConsumer); // DELETE DeletedConsumers
    }

    public class DeletedConsumerService : IDeletedConsumerService
    {
        private readonly XixsrvContext _db;

        public DeletedConsumerService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedConsumers

        public async Task<List<DeletedConsumer>> GetDeletedConsumerListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedConsumers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedConsumer> GetDeletedConsumer(string DeletedConsumer_id)
        {
            try
            {
                return await _db.DeletedConsumers.FindAsync(DeletedConsumer_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedConsumer>> GetDeletedConsumerList(string DeletedConsumer_id)
        {
            try
            {
                return await _db.DeletedConsumers
                    .Where(i => i.DeletedConsumerId == DeletedConsumer_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedConsumer> AddDeletedConsumer(DeletedConsumer DeletedConsumer)
        {
            try
            {
                await _db.DeletedConsumers.AddAsync(DeletedConsumer);
                await _db.SaveChangesAsync();
                return await _db.DeletedConsumers.FindAsync(DeletedConsumer.DeletedConsumerId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedConsumer> UpdateDeletedConsumer(DeletedConsumer DeletedConsumer)
        {
            try
            {
                _db.Entry(DeletedConsumer).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedConsumer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedConsumer(DeletedConsumer DeletedConsumer)
        {
            try
            {
                var dbDeletedConsumer = await _db.DeletedConsumers.FindAsync(DeletedConsumer.DeletedConsumerId);

                if (dbDeletedConsumer == null)
                {
                    return (false, "DeletedConsumer could not be found");
                }

                _db.DeletedConsumers.Remove(DeletedConsumer);
                await _db.SaveChangesAsync();

                return (true, "DeletedConsumer got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedConsumers
    }
}
