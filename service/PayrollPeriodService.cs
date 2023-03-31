using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPayrollPeriodService
    {
        // PayrollPeriods Services
        Task<List<PayrollPeriod>> GetPayrollPeriodListByValue(int offset, int limit, string val); // GET All PayrollPeriodss
        Task<PayrollPeriod> GetPayrollPeriod(string PayrollPeriod_name); // GET Single PayrollPeriods        
        Task<List<PayrollPeriod>> GetPayrollPeriodList(string PayrollPeriod_name); // GET List PayrollPeriods        
        Task<PayrollPeriod> AddPayrollPeriod(PayrollPeriod PayrollPeriod); // POST New PayrollPeriods
        Task<PayrollPeriod> UpdatePayrollPeriod(PayrollPeriod PayrollPeriod); // PUT PayrollPeriods
        Task<(bool, string)> DeletePayrollPeriod(PayrollPeriod PayrollPeriod); // DELETE PayrollPeriods
    }

    public class PayrollPeriodService : IPayrollPeriodService
    {
        private readonly XixsrvContext _db;

        public PayrollPeriodService(XixsrvContext db)
        {
            _db = db;
        }

        #region PayrollPeriods

        public async Task<List<PayrollPeriod>> GetPayrollPeriodListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PayrollPeriods.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PayrollPeriod> GetPayrollPeriod(string PayrollPeriod_id)
        {
            try
            {
                return await _db.PayrollPeriods.FindAsync(PayrollPeriod_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PayrollPeriod>> GetPayrollPeriodList(string PayrollPeriod_id)
        {
            try
            {
                return await _db.PayrollPeriods
                    .Where(i => i.PayrollPeriodId == PayrollPeriod_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PayrollPeriod> AddPayrollPeriod(PayrollPeriod PayrollPeriod)
        {
            try
            {
                await _db.PayrollPeriods.AddAsync(PayrollPeriod);
                await _db.SaveChangesAsync();
                return await _db.PayrollPeriods.FindAsync(PayrollPeriod.PayrollPeriodId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PayrollPeriod> UpdatePayrollPeriod(PayrollPeriod PayrollPeriod)
        {
            try
            {
                _db.Entry(PayrollPeriod).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PayrollPeriod;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePayrollPeriod(PayrollPeriod PayrollPeriod)
        {
            try
            {
                var dbPayrollPeriod = await _db.PayrollPeriods.FindAsync(PayrollPeriod.PayrollPeriodId);

                if (dbPayrollPeriod == null)
                {
                    return (false, "PayrollPeriod could not be found");
                }

                _db.PayrollPeriods.Remove(PayrollPeriod);
                await _db.SaveChangesAsync();

                return (true, "PayrollPeriod got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PayrollPeriods
    }
}
