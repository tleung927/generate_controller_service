using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionsAll2Service
    {
        // TransactionsAll2s Services
        Task<List<TransactionsAll2>> GetTransactionsAll2ListByValue(int offset, int limit, string val); // GET All TransactionsAll2ss
        Task<TransactionsAll2> GetTransactionsAll2(string TransactionsAll2_name); // GET Single TransactionsAll2s        
        Task<List<TransactionsAll2>> GetTransactionsAll2List(string TransactionsAll2_name); // GET List TransactionsAll2s        
        Task<TransactionsAll2> AddTransactionsAll2(TransactionsAll2 TransactionsAll2); // POST New TransactionsAll2s
        Task<TransactionsAll2> UpdateTransactionsAll2(TransactionsAll2 TransactionsAll2); // PUT TransactionsAll2s
        Task<(bool, string)> DeleteTransactionsAll2(TransactionsAll2 TransactionsAll2); // DELETE TransactionsAll2s
    }

    public class TransactionsAll2Service : ITransactionsAll2Service
    {
        private readonly XixsrvContext _db;

        public TransactionsAll2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionsAll2s

        public async Task<List<TransactionsAll2>> GetTransactionsAll2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionsAll2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionsAll2> GetTransactionsAll2(string TransactionsAll2_id)
        {
            try
            {
                return await _db.TransactionsAll2s.FindAsync(TransactionsAll2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionsAll2>> GetTransactionsAll2List(string TransactionsAll2_id)
        {
            try
            {
                return await _db.TransactionsAll2s
                    .Where(i => i.TransactionsAll2Id == TransactionsAll2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionsAll2> AddTransactionsAll2(TransactionsAll2 TransactionsAll2)
        {
            try
            {
                await _db.TransactionsAll2s.AddAsync(TransactionsAll2);
                await _db.SaveChangesAsync();
                return await _db.TransactionsAll2s.FindAsync(TransactionsAll2.TransactionsAll2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionsAll2> UpdateTransactionsAll2(TransactionsAll2 TransactionsAll2)
        {
            try
            {
                _db.Entry(TransactionsAll2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionsAll2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionsAll2(TransactionsAll2 TransactionsAll2)
        {
            try
            {
                var dbTransactionsAll2 = await _db.TransactionsAll2s.FindAsync(TransactionsAll2.TransactionsAll2Id);

                if (dbTransactionsAll2 == null)
                {
                    return (false, "TransactionsAll2 could not be found");
                }

                _db.TransactionsAll2s.Remove(TransactionsAll2);
                await _db.SaveChangesAsync();

                return (true, "TransactionsAll2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionsAll2s
    }
}
