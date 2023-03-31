using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPayrollDetailService
    {
        // PayrollDetails Services
        Task<List<PayrollDetail>> GetPayrollDetailListByValue(int offset, int limit, string val); // GET All PayrollDetailss
        Task<PayrollDetail> GetPayrollDetail(string PayrollDetail_name); // GET Single PayrollDetails        
        Task<List<PayrollDetail>> GetPayrollDetailList(string PayrollDetail_name); // GET List PayrollDetails        
        Task<PayrollDetail> AddPayrollDetail(PayrollDetail PayrollDetail); // POST New PayrollDetails
        Task<PayrollDetail> UpdatePayrollDetail(PayrollDetail PayrollDetail); // PUT PayrollDetails
        Task<(bool, string)> DeletePayrollDetail(PayrollDetail PayrollDetail); // DELETE PayrollDetails
    }

    public class PayrollDetailService : IPayrollDetailService
    {
        private readonly XixsrvContext _db;

        public PayrollDetailService(XixsrvContext db)
        {
            _db = db;
        }

        #region PayrollDetails

        public async Task<List<PayrollDetail>> GetPayrollDetailListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PayrollDetails.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PayrollDetail> GetPayrollDetail(string PayrollDetail_id)
        {
            try
            {
                return await _db.PayrollDetails.FindAsync(PayrollDetail_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PayrollDetail>> GetPayrollDetailList(string PayrollDetail_id)
        {
            try
            {
                return await _db.PayrollDetails
                    .Where(i => i.PayrollDetailId == PayrollDetail_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PayrollDetail> AddPayrollDetail(PayrollDetail PayrollDetail)
        {
            try
            {
                await _db.PayrollDetails.AddAsync(PayrollDetail);
                await _db.SaveChangesAsync();
                return await _db.PayrollDetails.FindAsync(PayrollDetail.PayrollDetailId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PayrollDetail> UpdatePayrollDetail(PayrollDetail PayrollDetail)
        {
            try
            {
                _db.Entry(PayrollDetail).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PayrollDetail;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePayrollDetail(PayrollDetail PayrollDetail)
        {
            try
            {
                var dbPayrollDetail = await _db.PayrollDetails.FindAsync(PayrollDetail.PayrollDetailId);

                if (dbPayrollDetail == null)
                {
                    return (false, "PayrollDetail could not be found");
                }

                _db.PayrollDetails.Remove(PayrollDetail);
                await _db.SaveChangesAsync();

                return (true, "PayrollDetail got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PayrollDetails
    }
}
