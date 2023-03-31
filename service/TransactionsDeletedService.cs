using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionsDeletedService
    {
        // TransactionsDeleteds Services
        Task<List<TransactionsDeleted>> GetTransactionsDeletedListByValue(int offset, int limit, string val); // GET All TransactionsDeletedss
        Task<TransactionsDeleted> GetTransactionsDeleted(string TransactionsDeleted_name); // GET Single TransactionsDeleteds        
        Task<List<TransactionsDeleted>> GetTransactionsDeletedList(string TransactionsDeleted_name); // GET List TransactionsDeleteds        
        Task<TransactionsDeleted> AddTransactionsDeleted(TransactionsDeleted TransactionsDeleted); // POST New TransactionsDeleteds
        Task<TransactionsDeleted> UpdateTransactionsDeleted(TransactionsDeleted TransactionsDeleted); // PUT TransactionsDeleteds
        Task<(bool, string)> DeleteTransactionsDeleted(TransactionsDeleted TransactionsDeleted); // DELETE TransactionsDeleteds
    }

    public class TransactionsDeletedService : ITransactionsDeletedService
    {
        private readonly XixsrvContext _db;

        public TransactionsDeletedService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionsDeleteds

        public async Task<List<TransactionsDeleted>> GetTransactionsDeletedListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionsDeleteds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionsDeleted> GetTransactionsDeleted(string TransactionsDeleted_id)
        {
            try
            {
                return await _db.TransactionsDeleteds.FindAsync(TransactionsDeleted_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionsDeleted>> GetTransactionsDeletedList(string TransactionsDeleted_id)
        {
            try
            {
                return await _db.TransactionsDeleteds
                    .Where(i => i.TransactionsDeletedId == TransactionsDeleted_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionsDeleted> AddTransactionsDeleted(TransactionsDeleted TransactionsDeleted)
        {
            try
            {
                await _db.TransactionsDeleteds.AddAsync(TransactionsDeleted);
                await _db.SaveChangesAsync();
                return await _db.TransactionsDeleteds.FindAsync(TransactionsDeleted.TransactionsDeletedId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionsDeleted> UpdateTransactionsDeleted(TransactionsDeleted TransactionsDeleted)
        {
            try
            {
                _db.Entry(TransactionsDeleted).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionsDeleted;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionsDeleted(TransactionsDeleted TransactionsDeleted)
        {
            try
            {
                var dbTransactionsDeleted = await _db.TransactionsDeleteds.FindAsync(TransactionsDeleted.TransactionsDeletedId);

                if (dbTransactionsDeleted == null)
                {
                    return (false, "TransactionsDeleted could not be found");
                }

                _db.TransactionsDeleteds.Remove(TransactionsDeleted);
                await _db.SaveChangesAsync();

                return (true, "TransactionsDeleted got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionsDeleteds
    }
}
