using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleReferralVendor5Service
    {
        // ViewScheduleReferralVendor5s Services
        Task<List<ViewScheduleReferralVendor5>> GetViewScheduleReferralVendor5ListByValue(int offset, int limit, string val); // GET All ViewScheduleReferralVendor5ss
        Task<ViewScheduleReferralVendor5> GetViewScheduleReferralVendor5(string ViewScheduleReferralVendor5_name); // GET Single ViewScheduleReferralVendor5s        
        Task<List<ViewScheduleReferralVendor5>> GetViewScheduleReferralVendor5List(string ViewScheduleReferralVendor5_name); // GET List ViewScheduleReferralVendor5s        
        Task<ViewScheduleReferralVendor5> AddViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5); // POST New ViewScheduleReferralVendor5s
        Task<ViewScheduleReferralVendor5> UpdateViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5); // PUT ViewScheduleReferralVendor5s
        Task<(bool, string)> DeleteViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5); // DELETE ViewScheduleReferralVendor5s
    }

    public class ViewScheduleReferralVendor5Service : IViewScheduleReferralVendor5Service
    {
        private readonly XixsrvContext _db;

        public ViewScheduleReferralVendor5Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleReferralVendor5s

        public async Task<List<ViewScheduleReferralVendor5>> GetViewScheduleReferralVendor5ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleReferralVendor5s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleReferralVendor5> GetViewScheduleReferralVendor5(string ViewScheduleReferralVendor5_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor5s.FindAsync(ViewScheduleReferralVendor5_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleReferralVendor5>> GetViewScheduleReferralVendor5List(string ViewScheduleReferralVendor5_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor5s
                    .Where(i => i.ViewScheduleReferralVendor5Id == ViewScheduleReferralVendor5_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleReferralVendor5> AddViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5)
        {
            try
            {
                await _db.ViewScheduleReferralVendor5s.AddAsync(ViewScheduleReferralVendor5);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleReferralVendor5s.FindAsync(ViewScheduleReferralVendor5.ViewScheduleReferralVendor5Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleReferralVendor5> UpdateViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5)
        {
            try
            {
                _db.Entry(ViewScheduleReferralVendor5).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleReferralVendor5;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleReferralVendor5(ViewScheduleReferralVendor5 ViewScheduleReferralVendor5)
        {
            try
            {
                var dbViewScheduleReferralVendor5 = await _db.ViewScheduleReferralVendor5s.FindAsync(ViewScheduleReferralVendor5.ViewScheduleReferralVendor5Id);

                if (dbViewScheduleReferralVendor5 == null)
                {
                    return (false, "ViewScheduleReferralVendor5 could not be found");
                }

                _db.ViewScheduleReferralVendor5s.Remove(ViewScheduleReferralVendor5);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleReferralVendor5 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleReferralVendor5s
    }
}
