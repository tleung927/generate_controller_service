using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPayrollTimecardService
    {
        // PayrollTimecards Services
        Task<List<PayrollTimecard>> GetPayrollTimecardListByValue(int offset, int limit, string val); // GET All PayrollTimecardss
        Task<PayrollTimecard> GetPayrollTimecard(string PayrollTimecard_name); // GET Single PayrollTimecards        
        Task<List<PayrollTimecard>> GetPayrollTimecardList(string PayrollTimecard_name); // GET List PayrollTimecards        
        Task<PayrollTimecard> AddPayrollTimecard(PayrollTimecard PayrollTimecard); // POST New PayrollTimecards
        Task<PayrollTimecard> UpdatePayrollTimecard(PayrollTimecard PayrollTimecard); // PUT PayrollTimecards
        Task<(bool, string)> DeletePayrollTimecard(PayrollTimecard PayrollTimecard); // DELETE PayrollTimecards
    }

    public class PayrollTimecardService : IPayrollTimecardService
    {
        private readonly XixsrvContext _db;

        public PayrollTimecardService(XixsrvContext db)
        {
            _db = db;
        }

        #region PayrollTimecards

        public async Task<List<PayrollTimecard>> GetPayrollTimecardListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PayrollTimecards.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PayrollTimecard> GetPayrollTimecard(string PayrollTimecard_id)
        {
            try
            {
                return await _db.PayrollTimecards.FindAsync(PayrollTimecard_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PayrollTimecard>> GetPayrollTimecardList(string PayrollTimecard_id)
        {
            try
            {
                return await _db.PayrollTimecards
                    .Where(i => i.PayrollTimecardId == PayrollTimecard_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PayrollTimecard> AddPayrollTimecard(PayrollTimecard PayrollTimecard)
        {
            try
            {
                await _db.PayrollTimecards.AddAsync(PayrollTimecard);
                await _db.SaveChangesAsync();
                return await _db.PayrollTimecards.FindAsync(PayrollTimecard.PayrollTimecardId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PayrollTimecard> UpdatePayrollTimecard(PayrollTimecard PayrollTimecard)
        {
            try
            {
                _db.Entry(PayrollTimecard).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PayrollTimecard;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePayrollTimecard(PayrollTimecard PayrollTimecard)
        {
            try
            {
                var dbPayrollTimecard = await _db.PayrollTimecards.FindAsync(PayrollTimecard.PayrollTimecardId);

                if (dbPayrollTimecard == null)
                {
                    return (false, "PayrollTimecard could not be found");
                }

                _db.PayrollTimecards.Remove(PayrollTimecard);
                await _db.SaveChangesAsync();

                return (true, "PayrollTimecard got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PayrollTimecards
    }
}
