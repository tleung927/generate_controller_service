using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionsDeleteDevBackupNotHistoryService
    {
        // TransactionsDeleteDevBackupNotHistorys Services
        Task<List<TransactionsDeleteDevBackupNotHistory>> GetTransactionsDeleteDevBackupNotHistoryListByValue(int offset, int limit, string val); // GET All TransactionsDeleteDevBackupNotHistoryss
        Task<TransactionsDeleteDevBackupNotHistory> GetTransactionsDeleteDevBackupNotHistory(string TransactionsDeleteDevBackupNotHistory_name); // GET Single TransactionsDeleteDevBackupNotHistorys        
        Task<List<TransactionsDeleteDevBackupNotHistory>> GetTransactionsDeleteDevBackupNotHistoryList(string TransactionsDeleteDevBackupNotHistory_name); // GET List TransactionsDeleteDevBackupNotHistorys        
        Task<TransactionsDeleteDevBackupNotHistory> AddTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory); // POST New TransactionsDeleteDevBackupNotHistorys
        Task<TransactionsDeleteDevBackupNotHistory> UpdateTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory); // PUT TransactionsDeleteDevBackupNotHistorys
        Task<(bool, string)> DeleteTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory); // DELETE TransactionsDeleteDevBackupNotHistorys
    }

    public class TransactionsDeleteDevBackupNotHistoryService : ITransactionsDeleteDevBackupNotHistoryService
    {
        private readonly XixsrvContext _db;

        public TransactionsDeleteDevBackupNotHistoryService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionsDeleteDevBackupNotHistorys

        public async Task<List<TransactionsDeleteDevBackupNotHistory>> GetTransactionsDeleteDevBackupNotHistoryListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionsDeleteDevBackupNotHistorys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionsDeleteDevBackupNotHistory> GetTransactionsDeleteDevBackupNotHistory(string TransactionsDeleteDevBackupNotHistory_id)
        {
            try
            {
                return await _db.TransactionsDeleteDevBackupNotHistorys.FindAsync(TransactionsDeleteDevBackupNotHistory_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionsDeleteDevBackupNotHistory>> GetTransactionsDeleteDevBackupNotHistoryList(string TransactionsDeleteDevBackupNotHistory_id)
        {
            try
            {
                return await _db.TransactionsDeleteDevBackupNotHistorys
                    .Where(i => i.TransactionsDeleteDevBackupNotHistoryId == TransactionsDeleteDevBackupNotHistory_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionsDeleteDevBackupNotHistory> AddTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory)
        {
            try
            {
                await _db.TransactionsDeleteDevBackupNotHistorys.AddAsync(TransactionsDeleteDevBackupNotHistory);
                await _db.SaveChangesAsync();
                return await _db.TransactionsDeleteDevBackupNotHistorys.FindAsync(TransactionsDeleteDevBackupNotHistory.TransactionsDeleteDevBackupNotHistoryId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionsDeleteDevBackupNotHistory> UpdateTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory)
        {
            try
            {
                _db.Entry(TransactionsDeleteDevBackupNotHistory).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionsDeleteDevBackupNotHistory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionsDeleteDevBackupNotHistory(TransactionsDeleteDevBackupNotHistory TransactionsDeleteDevBackupNotHistory)
        {
            try
            {
                var dbTransactionsDeleteDevBackupNotHistory = await _db.TransactionsDeleteDevBackupNotHistorys.FindAsync(TransactionsDeleteDevBackupNotHistory.TransactionsDeleteDevBackupNotHistoryId);

                if (dbTransactionsDeleteDevBackupNotHistory == null)
                {
                    return (false, "TransactionsDeleteDevBackupNotHistory could not be found");
                }

                _db.TransactionsDeleteDevBackupNotHistorys.Remove(TransactionsDeleteDevBackupNotHistory);
                await _db.SaveChangesAsync();

                return (true, "TransactionsDeleteDevBackupNotHistory got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionsDeleteDevBackupNotHistorys
    }
}
