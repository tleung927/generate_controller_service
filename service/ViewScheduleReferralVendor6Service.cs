using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleReferralVendor6Service
    {
        // ViewScheduleReferralVendor6s Services
        Task<List<ViewScheduleReferralVendor6>> GetViewScheduleReferralVendor6ListByValue(int offset, int limit, string val); // GET All ViewScheduleReferralVendor6ss
        Task<ViewScheduleReferralVendor6> GetViewScheduleReferralVendor6(string ViewScheduleReferralVendor6_name); // GET Single ViewScheduleReferralVendor6s        
        Task<List<ViewScheduleReferralVendor6>> GetViewScheduleReferralVendor6List(string ViewScheduleReferralVendor6_name); // GET List ViewScheduleReferralVendor6s        
        Task<ViewScheduleReferralVendor6> AddViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6); // POST New ViewScheduleReferralVendor6s
        Task<ViewScheduleReferralVendor6> UpdateViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6); // PUT ViewScheduleReferralVendor6s
        Task<(bool, string)> DeleteViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6); // DELETE ViewScheduleReferralVendor6s
    }

    public class ViewScheduleReferralVendor6Service : IViewScheduleReferralVendor6Service
    {
        private readonly XixsrvContext _db;

        public ViewScheduleReferralVendor6Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleReferralVendor6s

        public async Task<List<ViewScheduleReferralVendor6>> GetViewScheduleReferralVendor6ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleReferralVendor6s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleReferralVendor6> GetViewScheduleReferralVendor6(string ViewScheduleReferralVendor6_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor6s.FindAsync(ViewScheduleReferralVendor6_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleReferralVendor6>> GetViewScheduleReferralVendor6List(string ViewScheduleReferralVendor6_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor6s
                    .Where(i => i.ViewScheduleReferralVendor6Id == ViewScheduleReferralVendor6_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleReferralVendor6> AddViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6)
        {
            try
            {
                await _db.ViewScheduleReferralVendor6s.AddAsync(ViewScheduleReferralVendor6);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleReferralVendor6s.FindAsync(ViewScheduleReferralVendor6.ViewScheduleReferralVendor6Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleReferralVendor6> UpdateViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6)
        {
            try
            {
                _db.Entry(ViewScheduleReferralVendor6).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleReferralVendor6;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleReferralVendor6(ViewScheduleReferralVendor6 ViewScheduleReferralVendor6)
        {
            try
            {
                var dbViewScheduleReferralVendor6 = await _db.ViewScheduleReferralVendor6s.FindAsync(ViewScheduleReferralVendor6.ViewScheduleReferralVendor6Id);

                if (dbViewScheduleReferralVendor6 == null)
                {
                    return (false, "ViewScheduleReferralVendor6 could not be found");
                }

                _db.ViewScheduleReferralVendor6s.Remove(ViewScheduleReferralVendor6);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleReferralVendor6 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleReferralVendor6s
    }
}
