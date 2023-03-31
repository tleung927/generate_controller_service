using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransactionHistory072318Service
    {
        // TransactionHistory072318s Services
        Task<List<TransactionHistory072318>> GetTransactionHistory072318ListByValue(int offset, int limit, string val); // GET All TransactionHistory072318ss
        Task<TransactionHistory072318> GetTransactionHistory072318(string TransactionHistory072318_name); // GET Single TransactionHistory072318s        
        Task<List<TransactionHistory072318>> GetTransactionHistory072318List(string TransactionHistory072318_name); // GET List TransactionHistory072318s        
        Task<TransactionHistory072318> AddTransactionHistory072318(TransactionHistory072318 TransactionHistory072318); // POST New TransactionHistory072318s
        Task<TransactionHistory072318> UpdateTransactionHistory072318(TransactionHistory072318 TransactionHistory072318); // PUT TransactionHistory072318s
        Task<(bool, string)> DeleteTransactionHistory072318(TransactionHistory072318 TransactionHistory072318); // DELETE TransactionHistory072318s
    }

    public class TransactionHistory072318Service : ITransactionHistory072318Service
    {
        private readonly XixsrvContext _db;

        public TransactionHistory072318Service(XixsrvContext db)
        {
            _db = db;
        }

        #region TransactionHistory072318s

        public async Task<List<TransactionHistory072318>> GetTransactionHistory072318ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransactionHistory072318s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransactionHistory072318> GetTransactionHistory072318(string TransactionHistory072318_id)
        {
            try
            {
                return await _db.TransactionHistory072318s.FindAsync(TransactionHistory072318_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransactionHistory072318>> GetTransactionHistory072318List(string TransactionHistory072318_id)
        {
            try
            {
                return await _db.TransactionHistory072318s
                    .Where(i => i.TransactionHistory072318Id == TransactionHistory072318_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransactionHistory072318> AddTransactionHistory072318(TransactionHistory072318 TransactionHistory072318)
        {
            try
            {
                await _db.TransactionHistory072318s.AddAsync(TransactionHistory072318);
                await _db.SaveChangesAsync();
                return await _db.TransactionHistory072318s.FindAsync(TransactionHistory072318.TransactionHistory072318Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransactionHistory072318> UpdateTransactionHistory072318(TransactionHistory072318 TransactionHistory072318)
        {
            try
            {
                _db.Entry(TransactionHistory072318).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransactionHistory072318;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransactionHistory072318(TransactionHistory072318 TransactionHistory072318)
        {
            try
            {
                var dbTransactionHistory072318 = await _db.TransactionHistory072318s.FindAsync(TransactionHistory072318.TransactionHistory072318Id);

                if (dbTransactionHistory072318 == null)
                {
                    return (false, "TransactionHistory072318 could not be found");
                }

                _db.TransactionHistory072318s.Remove(TransactionHistory072318);
                await _db.SaveChangesAsync();

                return (true, "TransactionHistory072318 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransactionHistory072318s
    }
}
