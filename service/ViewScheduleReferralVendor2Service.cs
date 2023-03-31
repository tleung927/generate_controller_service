using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleReferralVendor2Service
    {
        // ViewScheduleReferralVendor2s Services
        Task<List<ViewScheduleReferralVendor2>> GetViewScheduleReferralVendor2ListByValue(int offset, int limit, string val); // GET All ViewScheduleReferralVendor2ss
        Task<ViewScheduleReferralVendor2> GetViewScheduleReferralVendor2(string ViewScheduleReferralVendor2_name); // GET Single ViewScheduleReferralVendor2s        
        Task<List<ViewScheduleReferralVendor2>> GetViewScheduleReferralVendor2List(string ViewScheduleReferralVendor2_name); // GET List ViewScheduleReferralVendor2s        
        Task<ViewScheduleReferralVendor2> AddViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2); // POST New ViewScheduleReferralVendor2s
        Task<ViewScheduleReferralVendor2> UpdateViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2); // PUT ViewScheduleReferralVendor2s
        Task<(bool, string)> DeleteViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2); // DELETE ViewScheduleReferralVendor2s
    }

    public class ViewScheduleReferralVendor2Service : IViewScheduleReferralVendor2Service
    {
        private readonly XixsrvContext _db;

        public ViewScheduleReferralVendor2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleReferralVendor2s

        public async Task<List<ViewScheduleReferralVendor2>> GetViewScheduleReferralVendor2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleReferralVendor2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleReferralVendor2> GetViewScheduleReferralVendor2(string ViewScheduleReferralVendor2_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor2s.FindAsync(ViewScheduleReferralVendor2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleReferralVendor2>> GetViewScheduleReferralVendor2List(string ViewScheduleReferralVendor2_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor2s
                    .Where(i => i.ViewScheduleReferralVendor2Id == ViewScheduleReferralVendor2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleReferralVendor2> AddViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2)
        {
            try
            {
                await _db.ViewScheduleReferralVendor2s.AddAsync(ViewScheduleReferralVendor2);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleReferralVendor2s.FindAsync(ViewScheduleReferralVendor2.ViewScheduleReferralVendor2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleReferralVendor2> UpdateViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2)
        {
            try
            {
                _db.Entry(ViewScheduleReferralVendor2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleReferralVendor2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleReferralVendor2(ViewScheduleReferralVendor2 ViewScheduleReferralVendor2)
        {
            try
            {
                var dbViewScheduleReferralVendor2 = await _db.ViewScheduleReferralVendor2s.FindAsync(ViewScheduleReferralVendor2.ViewScheduleReferralVendor2Id);

                if (dbViewScheduleReferralVendor2 == null)
                {
                    return (false, "ViewScheduleReferralVendor2 could not be found");
                }

                _db.ViewScheduleReferralVendor2s.Remove(ViewScheduleReferralVendor2);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleReferralVendor2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleReferralVendor2s
    }
}
