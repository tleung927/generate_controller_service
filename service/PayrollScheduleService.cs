using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPayrollScheduleService
    {
        // PayrollSchedules Services
        Task<List<PayrollSchedule>> GetPayrollScheduleListByValue(int offset, int limit, string val); // GET All PayrollScheduless
        Task<PayrollSchedule> GetPayrollSchedule(string PayrollSchedule_name); // GET Single PayrollSchedules        
        Task<List<PayrollSchedule>> GetPayrollScheduleList(string PayrollSchedule_name); // GET List PayrollSchedules        
        Task<PayrollSchedule> AddPayrollSchedule(PayrollSchedule PayrollSchedule); // POST New PayrollSchedules
        Task<PayrollSchedule> UpdatePayrollSchedule(PayrollSchedule PayrollSchedule); // PUT PayrollSchedules
        Task<(bool, string)> DeletePayrollSchedule(PayrollSchedule PayrollSchedule); // DELETE PayrollSchedules
    }

    public class PayrollScheduleService : IPayrollScheduleService
    {
        private readonly XixsrvContext _db;

        public PayrollScheduleService(XixsrvContext db)
        {
            _db = db;
        }

        #region PayrollSchedules

        public async Task<List<PayrollSchedule>> GetPayrollScheduleListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PayrollSchedules.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PayrollSchedule> GetPayrollSchedule(string PayrollSchedule_id)
        {
            try
            {
                return await _db.PayrollSchedules.FindAsync(PayrollSchedule_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PayrollSchedule>> GetPayrollScheduleList(string PayrollSchedule_id)
        {
            try
            {
                return await _db.PayrollSchedules
                    .Where(i => i.PayrollScheduleId == PayrollSchedule_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PayrollSchedule> AddPayrollSchedule(PayrollSchedule PayrollSchedule)
        {
            try
            {
                await _db.PayrollSchedules.AddAsync(PayrollSchedule);
                await _db.SaveChangesAsync();
                return await _db.PayrollSchedules.FindAsync(PayrollSchedule.PayrollScheduleId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PayrollSchedule> UpdatePayrollSchedule(PayrollSchedule PayrollSchedule)
        {
            try
            {
                _db.Entry(PayrollSchedule).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PayrollSchedule;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePayrollSchedule(PayrollSchedule PayrollSchedule)
        {
            try
            {
                var dbPayrollSchedule = await _db.PayrollSchedules.FindAsync(PayrollSchedule.PayrollScheduleId);

                if (dbPayrollSchedule == null)
                {
                    return (false, "PayrollSchedule could not be found");
                }

                _db.PayrollSchedules.Remove(PayrollSchedule);
                await _db.SaveChangesAsync();

                return (true, "PayrollSchedule got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PayrollSchedules
    }
}
