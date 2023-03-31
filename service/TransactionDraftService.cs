using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionDraftService
    {
        // TransactionDrafts Services
        Task<List<TransactionDraft>> GetTransactionDraftListByValue(int offset, int limit, string val); // GET All TransactionDraftss
        Task<TransactionDraft> GetTransactionDraft(string TransactionDraft_name); // GET Single TransactionDrafts        
        Task<List<TransactionDraft>> GetTransactionDraftList(string TransactionDraft_name); // GET List TransactionDrafts        
        Task<TransactionDraft> AddTransactionDraft(TransactionDraft TransactionDraft); // POST New TransactionDrafts
        Task<TransactionDraft> UpdateTransactionDraft(TransactionDraft TransactionDraft); // PUT TransactionDrafts
        Task<(bool, string)> DeleteTransactionDraft(TransactionDraft TransactionDraft); // DELETE TransactionDrafts
    }

    public class TransactionDraftService : ITransactionDraftService
    {
        private readonly XixsrvContext _db;

        public TransactionDraftService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionDrafts

        public async Task<List<TransactionDraft>> GetTransactionDraftListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionDrafts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionDraft> GetTransactionDraft(string TransactionDraft_id)
        {
            try
            {
                return await _db.TransactionDrafts.FindAsync(TransactionDraft_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionDraft>> GetTransactionDraftList(string TransactionDraft_id)
        {
            try
            {
                return await _db.TransactionDrafts
                    .Where(i => i.TransactionDraftId == TransactionDraft_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionDraft> AddTransactionDraft(TransactionDraft TransactionDraft)
        {
            try
            {
                await _db.TransactionDrafts.AddAsync(TransactionDraft);
                await _db.SaveChangesAsync();
                return await _db.TransactionDrafts.FindAsync(TransactionDraft.TransactionDraftId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionDraft> UpdateTransactionDraft(TransactionDraft TransactionDraft)
        {
            try
            {
                _db.Entry(TransactionDraft).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionDraft;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionDraft(TransactionDraft TransactionDraft)
        {
            try
            {
                var dbTransactionDraft = await _db.TransactionDrafts.FindAsync(TransactionDraft.TransactionDraftId);

                if (dbTransactionDraft == null)
                {
                    return (false, "TransactionDraft could not be found");
                }

                _db.TransactionDrafts.Remove(TransactionDraft);
                await _db.SaveChangesAsync();

                return (true, "TransactionDraft got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionDrafts
    }
}
