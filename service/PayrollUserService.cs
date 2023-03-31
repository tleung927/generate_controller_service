using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPayrollUserService
    {
        // PayrollUsers Services
        Task<List<PayrollUser>> GetPayrollUserListByValue(int offset, int limit, string val); // GET All PayrollUserss
        Task<PayrollUser> GetPayrollUser(string PayrollUser_name); // GET Single PayrollUsers        
        Task<List<PayrollUser>> GetPayrollUserList(string PayrollUser_name); // GET List PayrollUsers        
        Task<PayrollUser> AddPayrollUser(PayrollUser PayrollUser); // POST New PayrollUsers
        Task<PayrollUser> UpdatePayrollUser(PayrollUser PayrollUser); // PUT PayrollUsers
        Task<(bool, string)> DeletePayrollUser(PayrollUser PayrollUser); // DELETE PayrollUsers
    }

    public class PayrollUserService : IPayrollUserService
    {
        private readonly XixsrvContext _db;

        public PayrollUserService(XixsrvContext db)
        {
            _db = db;
        }

        #region PayrollUsers

        public async Task<List<PayrollUser>> GetPayrollUserListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PayrollUsers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PayrollUser> GetPayrollUser(string PayrollUser_id)
        {
            try
            {
                return await _db.PayrollUsers.FindAsync(PayrollUser_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PayrollUser>> GetPayrollUserList(string PayrollUser_id)
        {
            try
            {
                return await _db.PayrollUsers
                    .Where(i => i.PayrollUserId == PayrollUser_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PayrollUser> AddPayrollUser(PayrollUser PayrollUser)
        {
            try
            {
                await _db.PayrollUsers.AddAsync(PayrollUser);
                await _db.SaveChangesAsync();
                return await _db.PayrollUsers.FindAsync(PayrollUser.PayrollUserId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PayrollUser> UpdatePayrollUser(PayrollUser PayrollUser)
        {
            try
            {
                _db.Entry(PayrollUser).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PayrollUser;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePayrollUser(PayrollUser PayrollUser)
        {
            try
            {
                var dbPayrollUser = await _db.PayrollUsers.FindAsync(PayrollUser.PayrollUserId);

                if (dbPayrollUser == null)
                {
                    return (false, "PayrollUser could not be found");
                }

                _db.PayrollUsers.Remove(PayrollUser);
                await _db.SaveChangesAsync();

                return (true, "PayrollUser got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PayrollUsers
    }
}
