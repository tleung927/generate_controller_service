using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleReferralVendor1Service
    {
        // ViewScheduleReferralVendor1s Services
        Task<List<ViewScheduleReferralVendor1>> GetViewScheduleReferralVendor1ListByValue(int offset, int limit, string val); // GET All ViewScheduleReferralVendor1ss
        Task<ViewScheduleReferralVendor1> GetViewScheduleReferralVendor1(string ViewScheduleReferralVendor1_name); // GET Single ViewScheduleReferralVendor1s        
        Task<List<ViewScheduleReferralVendor1>> GetViewScheduleReferralVendor1List(string ViewScheduleReferralVendor1_name); // GET List ViewScheduleReferralVendor1s        
        Task<ViewScheduleReferralVendor1> AddViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1); // POST New ViewScheduleReferralVendor1s
        Task<ViewScheduleReferralVendor1> UpdateViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1); // PUT ViewScheduleReferralVendor1s
        Task<(bool, string)> DeleteViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1); // DELETE ViewScheduleReferralVendor1s
    }

    public class ViewScheduleReferralVendor1Service : IViewScheduleReferralVendor1Service
    {
        private readonly XixsrvContext _db;

        public ViewScheduleReferralVendor1Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleReferralVendor1s

        public async Task<List<ViewScheduleReferralVendor1>> GetViewScheduleReferralVendor1ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleReferralVendor1s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleReferralVendor1> GetViewScheduleReferralVendor1(string ViewScheduleReferralVendor1_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor1s.FindAsync(ViewScheduleReferralVendor1_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleReferralVendor1>> GetViewScheduleReferralVendor1List(string ViewScheduleReferralVendor1_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor1s
                    .Where(i => i.ViewScheduleReferralVendor1Id == ViewScheduleReferralVendor1_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleReferralVendor1> AddViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1)
        {
            try
            {
                await _db.ViewScheduleReferralVendor1s.AddAsync(ViewScheduleReferralVendor1);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleReferralVendor1s.FindAsync(ViewScheduleReferralVendor1.ViewScheduleReferralVendor1Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleReferralVendor1> UpdateViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1)
        {
            try
            {
                _db.Entry(ViewScheduleReferralVendor1).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleReferralVendor1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleReferralVendor1(ViewScheduleReferralVendor1 ViewScheduleReferralVendor1)
        {
            try
            {
                var dbViewScheduleReferralVendor1 = await _db.ViewScheduleReferralVendor1s.FindAsync(ViewScheduleReferralVendor1.ViewScheduleReferralVendor1Id);

                if (dbViewScheduleReferralVendor1 == null)
                {
                    return (false, "ViewScheduleReferralVendor1 could not be found");
                }

                _db.ViewScheduleReferralVendor1s.Remove(ViewScheduleReferralVendor1);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleReferralVendor1 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleReferralVendor1s
    }
}
