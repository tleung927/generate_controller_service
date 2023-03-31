using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionsDeleteDevBackupService
    {
        // TransactionsDeleteDevBackups Services
        Task<List<TransactionsDeleteDevBackup>> GetTransactionsDeleteDevBackupListByValue(int offset, int limit, string val); // GET All TransactionsDeleteDevBackupss
        Task<TransactionsDeleteDevBackup> GetTransactionsDeleteDevBackup(string TransactionsDeleteDevBackup_name); // GET Single TransactionsDeleteDevBackups        
        Task<List<TransactionsDeleteDevBackup>> GetTransactionsDeleteDevBackupList(string TransactionsDeleteDevBackup_name); // GET List TransactionsDeleteDevBackups        
        Task<TransactionsDeleteDevBackup> AddTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup); // POST New TransactionsDeleteDevBackups
        Task<TransactionsDeleteDevBackup> UpdateTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup); // PUT TransactionsDeleteDevBackups
        Task<(bool, string)> DeleteTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup); // DELETE TransactionsDeleteDevBackups
    }

    public class TransactionsDeleteDevBackupService : ITransactionsDeleteDevBackupService
    {
        private readonly XixsrvContext _db;

        public TransactionsDeleteDevBackupService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionsDeleteDevBackups

        public async Task<List<TransactionsDeleteDevBackup>> GetTransactionsDeleteDevBackupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionsDeleteDevBackups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionsDeleteDevBackup> GetTransactionsDeleteDevBackup(string TransactionsDeleteDevBackup_id)
        {
            try
            {
                return await _db.TransactionsDeleteDevBackups.FindAsync(TransactionsDeleteDevBackup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionsDeleteDevBackup>> GetTransactionsDeleteDevBackupList(string TransactionsDeleteDevBackup_id)
        {
            try
            {
                return await _db.TransactionsDeleteDevBackups
                    .Where(i => i.TransactionsDeleteDevBackupId == TransactionsDeleteDevBackup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionsDeleteDevBackup> AddTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup)
        {
            try
            {
                await _db.TransactionsDeleteDevBackups.AddAsync(TransactionsDeleteDevBackup);
                await _db.SaveChangesAsync();
                return await _db.TransactionsDeleteDevBackups.FindAsync(TransactionsDeleteDevBackup.TransactionsDeleteDevBackupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionsDeleteDevBackup> UpdateTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup)
        {
            try
            {
                _db.Entry(TransactionsDeleteDevBackup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionsDeleteDevBackup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionsDeleteDevBackup(TransactionsDeleteDevBackup TransactionsDeleteDevBackup)
        {
            try
            {
                var dbTransactionsDeleteDevBackup = await _db.TransactionsDeleteDevBackups.FindAsync(TransactionsDeleteDevBackup.TransactionsDeleteDevBackupId);

                if (dbTransactionsDeleteDevBackup == null)
                {
                    return (false, "TransactionsDeleteDevBackup could not be found");
                }

                _db.TransactionsDeleteDevBackups.Remove(TransactionsDeleteDevBackup);
                await _db.SaveChangesAsync();

                return (true, "TransactionsDeleteDevBackup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionsDeleteDevBackups
    }
}
