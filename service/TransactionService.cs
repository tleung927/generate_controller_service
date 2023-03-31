using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionService
    {
        // Transactions Services
        Task<List<Transaction>> GetTransactionListByValue(int offset, int limit, string val); // GET All Transactionss
        Task<Transaction> GetTransaction(string Transaction_name); // GET Single Transactions        
        Task<List<Transaction>> GetTransactionList(string Transaction_name); // GET List Transactions        
        Task<Transaction> AddTransaction(Transaction Transaction); // POST New Transactions
        Task<Transaction> UpdateTransaction(Transaction Transaction); // PUT Transactions
        Task<(bool, string)> DeleteTransaction(Transaction Transaction); // DELETE Transactions
    }

    public class TransactionService : ITransactionService
    {
        private readonly XixsrvContext _db;

        public TransactionService(XixsrvContext db)
        {
            _db = db;
        }

        #region Transactions

        public async Task<List<Transaction>> GetTransactionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Transactions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Transaction> GetTransaction(string Transaction_id)
        {
            try
            {
                return await _db.Transactions.FindAsync(Transaction_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Transaction>> GetTransactionList(string Transaction_id)
        {
            try
            {
                return await _db.Transactions
                    .Where(i => i.TransactionId == Transaction_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Transaction> AddTransaction(Transaction Transaction)
        {
            try
            {
                await _db.Transactions.AddAsync(Transaction);
                await _db.SaveChangesAsync();
                return await _db.Transactions.FindAsync(Transaction.TransactionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Transaction> UpdateTransaction(Transaction Transaction)
        {
            try
            {
                _db.Entry(Transaction).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Transaction;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransaction(Transaction Transaction)
        {
            try
            {
                var dbTransaction = await _db.Transactions.FindAsync(Transaction.TransactionId);

                if (dbTransaction == null)
                {
                    return (false, "Transaction could not be found");
                }

                _db.Transactions.Remove(Transaction);
                await _db.SaveChangesAsync();

                return (true, "Transaction got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Transactions
    }
}
