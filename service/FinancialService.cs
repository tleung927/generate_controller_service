using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFinancialService
    {
        // Financials Services
        Task<List<Financial>> GetFinancialListByValue(int offset, int limit, string val); // GET All Financialss
        Task<Financial> GetFinancial(string Financial_name); // GET Single Financials        
        Task<List<Financial>> GetFinancialList(string Financial_name); // GET List Financials        
        Task<Financial> AddFinancial(Financial Financial); // POST New Financials
        Task<Financial> UpdateFinancial(Financial Financial); // PUT Financials
        Task<(bool, string)> DeleteFinancial(Financial Financial); // DELETE Financials
    }

    public class FinancialService : IFinancialService
    {
        private readonly XixsrvContext _db;

        public FinancialService(XixsrvContext db)
        {
            _db = db;
        }

        #region Financials

        public async Task<List<Financial>> GetFinancialListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Financials.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Financial> GetFinancial(string Financial_id)
        {
            try
            {
                return await _db.Financials.FindAsync(Financial_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Financial>> GetFinancialList(string Financial_id)
        {
            try
            {
                return await _db.Financials
                    .Where(i => i.FinancialId == Financial_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Financial> AddFinancial(Financial Financial)
        {
            try
            {
                await _db.Financials.AddAsync(Financial);
                await _db.SaveChangesAsync();
                return await _db.Financials.FindAsync(Financial.FinancialId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Financial> UpdateFinancial(Financial Financial)
        {
            try
            {
                _db.Entry(Financial).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Financial;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFinancial(Financial Financial)
        {
            try
            {
                var dbFinancial = await _db.Financials.FindAsync(Financial.FinancialId);

                if (dbFinancial == null)
                {
                    return (false, "Financial could not be found");
                }

                _db.Financials.Remove(Financial);
                await _db.SaveChangesAsync();

                return (true, "Financial got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Financials
    }
}
