using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedRtransactionService
    {
        // DeletedRtransactions Services
        Task<List<DeletedRtransaction>> GetDeletedRtransactionListByValue(int offset, int limit, string val); // GET All DeletedRtransactionss
        Task<DeletedRtransaction> GetDeletedRtransaction(string DeletedRtransaction_name); // GET Single DeletedRtransactions        
        Task<List<DeletedRtransaction>> GetDeletedRtransactionList(string DeletedRtransaction_name); // GET List DeletedRtransactions        
        Task<DeletedRtransaction> AddDeletedRtransaction(DeletedRtransaction DeletedRtransaction); // POST New DeletedRtransactions
        Task<DeletedRtransaction> UpdateDeletedRtransaction(DeletedRtransaction DeletedRtransaction); // PUT DeletedRtransactions
        Task<(bool, string)> DeleteDeletedRtransaction(DeletedRtransaction DeletedRtransaction); // DELETE DeletedRtransactions
    }

    public class DeletedRtransactionService : IDeletedRtransactionService
    {
        private readonly XixsrvContext _db;

        public DeletedRtransactionService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedRtransactions

        public async Task<List<DeletedRtransaction>> GetDeletedRtransactionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedRtransactions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedRtransaction> GetDeletedRtransaction(string DeletedRtransaction_id)
        {
            try
            {
                return await _db.DeletedRtransactions.FindAsync(DeletedRtransaction_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedRtransaction>> GetDeletedRtransactionList(string DeletedRtransaction_id)
        {
            try
            {
                return await _db.DeletedRtransactions
                    .Where(i => i.DeletedRtransactionId == DeletedRtransaction_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedRtransaction> AddDeletedRtransaction(DeletedRtransaction DeletedRtransaction)
        {
            try
            {
                await _db.DeletedRtransactions.AddAsync(DeletedRtransaction);
                await _db.SaveChangesAsync();
                return await _db.DeletedRtransactions.FindAsync(DeletedRtransaction.DeletedRtransactionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedRtransaction> UpdateDeletedRtransaction(DeletedRtransaction DeletedRtransaction)
        {
            try
            {
                _db.Entry(DeletedRtransaction).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedRtransaction;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedRtransaction(DeletedRtransaction DeletedRtransaction)
        {
            try
            {
                var dbDeletedRtransaction = await _db.DeletedRtransactions.FindAsync(DeletedRtransaction.DeletedRtransactionId);

                if (dbDeletedRtransaction == null)
                {
                    return (false, "DeletedRtransaction could not be found");
                }

                _db.DeletedRtransactions.Remove(DeletedRtransaction);
                await _db.SaveChangesAsync();

                return (true, "DeletedRtransaction got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedRtransactions
    }
}
