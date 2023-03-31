using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionArchiveService
    {
        // TransactionArchives Services
        Task<List<TransactionArchive>> GetTransactionArchiveListByValue(int offset, int limit, string val); // GET All TransactionArchivess
        Task<TransactionArchive> GetTransactionArchive(string TransactionArchive_name); // GET Single TransactionArchives        
        Task<List<TransactionArchive>> GetTransactionArchiveList(string TransactionArchive_name); // GET List TransactionArchives        
        Task<TransactionArchive> AddTransactionArchive(TransactionArchive TransactionArchive); // POST New TransactionArchives
        Task<TransactionArchive> UpdateTransactionArchive(TransactionArchive TransactionArchive); // PUT TransactionArchives
        Task<(bool, string)> DeleteTransactionArchive(TransactionArchive TransactionArchive); // DELETE TransactionArchives
    }

    public class TransactionArchiveService : ITransactionArchiveService
    {
        private readonly XixsrvContext _db;

        public TransactionArchiveService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionArchives

        public async Task<List<TransactionArchive>> GetTransactionArchiveListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionArchives.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionArchive> GetTransactionArchive(string TransactionArchive_id)
        {
            try
            {
                return await _db.TransactionArchives.FindAsync(TransactionArchive_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionArchive>> GetTransactionArchiveList(string TransactionArchive_id)
        {
            try
            {
                return await _db.TransactionArchives
                    .Where(i => i.TransactionArchiveId == TransactionArchive_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionArchive> AddTransactionArchive(TransactionArchive TransactionArchive)
        {
            try
            {
                await _db.TransactionArchives.AddAsync(TransactionArchive);
                await _db.SaveChangesAsync();
                return await _db.TransactionArchives.FindAsync(TransactionArchive.TransactionArchiveId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionArchive> UpdateTransactionArchive(TransactionArchive TransactionArchive)
        {
            try
            {
                _db.Entry(TransactionArchive).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionArchive;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionArchive(TransactionArchive TransactionArchive)
        {
            try
            {
                var dbTransactionArchive = await _db.TransactionArchives.FindAsync(TransactionArchive.TransactionArchiveId);

                if (dbTransactionArchive == null)
                {
                    return (false, "TransactionArchive could not be found");
                }

                _db.TransactionArchives.Remove(TransactionArchive);
                await _db.SaveChangesAsync();

                return (true, "TransactionArchive got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionArchives
    }
}
