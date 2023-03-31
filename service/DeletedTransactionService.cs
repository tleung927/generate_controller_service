using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedTransactionService
    {
        // DeletedTransactions Services
        Task<List<DeletedTransaction>> GetDeletedTransactionListByValue(int offset, int limit, string val); // GET All DeletedTransactionss
        Task<DeletedTransaction> GetDeletedTransaction(string DeletedTransaction_name); // GET Single DeletedTransactions        
        Task<List<DeletedTransaction>> GetDeletedTransactionList(string DeletedTransaction_name); // GET List DeletedTransactions        
        Task<DeletedTransaction> AddDeletedTransaction(DeletedTransaction DeletedTransaction); // POST New DeletedTransactions
        Task<DeletedTransaction> UpdateDeletedTransaction(DeletedTransaction DeletedTransaction); // PUT DeletedTransactions
        Task<(bool, string)> DeleteDeletedTransaction(DeletedTransaction DeletedTransaction); // DELETE DeletedTransactions
    }

    public class DeletedTransactionService : IDeletedTransactionService
    {
        private readonly XixsrvContext _db;

        public DeletedTransactionService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedTransactions

        public async Task<List<DeletedTransaction>> GetDeletedTransactionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedTransactions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedTransaction> GetDeletedTransaction(string DeletedTransaction_id)
        {
            try
            {
                return await _db.DeletedTransactions.FindAsync(DeletedTransaction_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedTransaction>> GetDeletedTransactionList(string DeletedTransaction_id)
        {
            try
            {
                return await _db.DeletedTransactions
                    .Where(i => i.DeletedTransactionId == DeletedTransaction_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedTransaction> AddDeletedTransaction(DeletedTransaction DeletedTransaction)
        {
            try
            {
                await _db.DeletedTransactions.AddAsync(DeletedTransaction);
                await _db.SaveChangesAsync();
                return await _db.DeletedTransactions.FindAsync(DeletedTransaction.DeletedTransactionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedTransaction> UpdateDeletedTransaction(DeletedTransaction DeletedTransaction)
        {
            try
            {
                _db.Entry(DeletedTransaction).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedTransaction;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedTransaction(DeletedTransaction DeletedTransaction)
        {
            try
            {
                var dbDeletedTransaction = await _db.DeletedTransactions.FindAsync(DeletedTransaction.DeletedTransactionId);

                if (dbDeletedTransaction == null)
                {
                    return (false, "DeletedTransaction could not be found");
                }

                _db.DeletedTransactions.Remove(DeletedTransaction);
                await _db.SaveChangesAsync();

                return (true, "DeletedTransaction got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedTransactions
    }
}
