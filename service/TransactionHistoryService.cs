using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionHistoryService
    {
        // TransactionHistorys Services
        Task<List<TransactionHistory>> GetTransactionHistoryListByValue(int offset, int limit, string val); // GET All TransactionHistoryss
        Task<TransactionHistory> GetTransactionHistory(string TransactionHistory_name); // GET Single TransactionHistorys        
        Task<List<TransactionHistory>> GetTransactionHistoryList(string TransactionHistory_name); // GET List TransactionHistorys        
        Task<TransactionHistory> AddTransactionHistory(TransactionHistory TransactionHistory); // POST New TransactionHistorys
        Task<TransactionHistory> UpdateTransactionHistory(TransactionHistory TransactionHistory); // PUT TransactionHistorys
        Task<(bool, string)> DeleteTransactionHistory(TransactionHistory TransactionHistory); // DELETE TransactionHistorys
    }

    public class TransactionHistoryService : ITransactionHistoryService
    {
        private readonly XixsrvContext _db;

        public TransactionHistoryService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionHistorys

        public async Task<List<TransactionHistory>> GetTransactionHistoryListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionHistorys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionHistory> GetTransactionHistory(string TransactionHistory_id)
        {
            try
            {
                return await _db.TransactionHistorys.FindAsync(TransactionHistory_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionHistory>> GetTransactionHistoryList(string TransactionHistory_id)
        {
            try
            {
                return await _db.TransactionHistorys
                    .Where(i => i.TransactionHistoryId == TransactionHistory_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionHistory> AddTransactionHistory(TransactionHistory TransactionHistory)
        {
            try
            {
                await _db.TransactionHistorys.AddAsync(TransactionHistory);
                await _db.SaveChangesAsync();
                return await _db.TransactionHistorys.FindAsync(TransactionHistory.TransactionHistoryId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionHistory> UpdateTransactionHistory(TransactionHistory TransactionHistory)
        {
            try
            {
                _db.Entry(TransactionHistory).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionHistory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionHistory(TransactionHistory TransactionHistory)
        {
            try
            {
                var dbTransactionHistory = await _db.TransactionHistorys.FindAsync(TransactionHistory.TransactionHistoryId);

                if (dbTransactionHistory == null)
                {
                    return (false, "TransactionHistory could not be found");
                }

                _db.TransactionHistorys.Remove(TransactionHistory);
                await _db.SaveChangesAsync();

                return (true, "TransactionHistory got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionHistorys
    }
}
