using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionsAllService
    {
        // TransactionsAlls Services
        Task<List<TransactionsAll>> GetTransactionsAllListByValue(int offset, int limit, string val); // GET All TransactionsAllss
        Task<TransactionsAll> GetTransactionsAll(string TransactionsAll_name); // GET Single TransactionsAlls        
        Task<List<TransactionsAll>> GetTransactionsAllList(string TransactionsAll_name); // GET List TransactionsAlls        
        Task<TransactionsAll> AddTransactionsAll(TransactionsAll TransactionsAll); // POST New TransactionsAlls
        Task<TransactionsAll> UpdateTransactionsAll(TransactionsAll TransactionsAll); // PUT TransactionsAlls
        Task<(bool, string)> DeleteTransactionsAll(TransactionsAll TransactionsAll); // DELETE TransactionsAlls
    }

    public class TransactionsAllService : ITransactionsAllService
    {
        private readonly XixsrvContext _db;

        public TransactionsAllService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionsAlls

        public async Task<List<TransactionsAll>> GetTransactionsAllListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionsAlls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionsAll> GetTransactionsAll(string TransactionsAll_id)
        {
            try
            {
                return await _db.TransactionsAlls.FindAsync(TransactionsAll_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionsAll>> GetTransactionsAllList(string TransactionsAll_id)
        {
            try
            {
                return await _db.TransactionsAlls
                    .Where(i => i.TransactionsAllId == TransactionsAll_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionsAll> AddTransactionsAll(TransactionsAll TransactionsAll)
        {
            try
            {
                await _db.TransactionsAlls.AddAsync(TransactionsAll);
                await _db.SaveChangesAsync();
                return await _db.TransactionsAlls.FindAsync(TransactionsAll.TransactionsAllId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionsAll> UpdateTransactionsAll(TransactionsAll TransactionsAll)
        {
            try
            {
                _db.Entry(TransactionsAll).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionsAll;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionsAll(TransactionsAll TransactionsAll)
        {
            try
            {
                var dbTransactionsAll = await _db.TransactionsAlls.FindAsync(TransactionsAll.TransactionsAllId);

                if (dbTransactionsAll == null)
                {
                    return (false, "TransactionsAll could not be found");
                }

                _db.TransactionsAlls.Remove(TransactionsAll);
                await _db.SaveChangesAsync();

                return (true, "TransactionsAll got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionsAlls
    }
}
