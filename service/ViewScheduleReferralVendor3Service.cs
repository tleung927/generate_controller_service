using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleReferralVendor3Service
    {
        // ViewScheduleReferralVendor3s Services
        Task<List<ViewScheduleReferralVendor3>> GetViewScheduleReferralVendor3ListByValue(int offset, int limit, string val); // GET All ViewScheduleReferralVendor3ss
        Task<ViewScheduleReferralVendor3> GetViewScheduleReferralVendor3(string ViewScheduleReferralVendor3_name); // GET Single ViewScheduleReferralVendor3s        
        Task<List<ViewScheduleReferralVendor3>> GetViewScheduleReferralVendor3List(string ViewScheduleReferralVendor3_name); // GET List ViewScheduleReferralVendor3s        
        Task<ViewScheduleReferralVendor3> AddViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3); // POST New ViewScheduleReferralVendor3s
        Task<ViewScheduleReferralVendor3> UpdateViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3); // PUT ViewScheduleReferralVendor3s
        Task<(bool, string)> DeleteViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3); // DELETE ViewScheduleReferralVendor3s
    }

    public class ViewScheduleReferralVendor3Service : IViewScheduleReferralVendor3Service
    {
        private readonly XixsrvContext _db;

        public ViewScheduleReferralVendor3Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleReferralVendor3s

        public async Task<List<ViewScheduleReferralVendor3>> GetViewScheduleReferralVendor3ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleReferralVendor3s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleReferralVendor3> GetViewScheduleReferralVendor3(string ViewScheduleReferralVendor3_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor3s.FindAsync(ViewScheduleReferralVendor3_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleReferralVendor3>> GetViewScheduleReferralVendor3List(string ViewScheduleReferralVendor3_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor3s
                    .Where(i => i.ViewScheduleReferralVendor3Id == ViewScheduleReferralVendor3_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleReferralVendor3> AddViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3)
        {
            try
            {
                await _db.ViewScheduleReferralVendor3s.AddAsync(ViewScheduleReferralVendor3);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleReferralVendor3s.FindAsync(ViewScheduleReferralVendor3.ViewScheduleReferralVendor3Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleReferralVendor3> UpdateViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3)
        {
            try
            {
                _db.Entry(ViewScheduleReferralVendor3).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleReferralVendor3;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleReferralVendor3(ViewScheduleReferralVendor3 ViewScheduleReferralVendor3)
        {
            try
            {
                var dbViewScheduleReferralVendor3 = await _db.ViewScheduleReferralVendor3s.FindAsync(ViewScheduleReferralVendor3.ViewScheduleReferralVendor3Id);

                if (dbViewScheduleReferralVendor3 == null)
                {
                    return (false, "ViewScheduleReferralVendor3 could not be found");
                }

                _db.ViewScheduleReferralVendor3s.Remove(ViewScheduleReferralVendor3);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleReferralVendor3 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleReferralVendor3s
    }
}
