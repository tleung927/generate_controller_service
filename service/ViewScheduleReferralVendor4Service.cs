using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleReferralVendor4Service
    {
        // ViewScheduleReferralVendor4s Services
        Task<List<ViewScheduleReferralVendor4>> GetViewScheduleReferralVendor4ListByValue(int offset, int limit, string val); // GET All ViewScheduleReferralVendor4ss
        Task<ViewScheduleReferralVendor4> GetViewScheduleReferralVendor4(string ViewScheduleReferralVendor4_name); // GET Single ViewScheduleReferralVendor4s        
        Task<List<ViewScheduleReferralVendor4>> GetViewScheduleReferralVendor4List(string ViewScheduleReferralVendor4_name); // GET List ViewScheduleReferralVendor4s        
        Task<ViewScheduleReferralVendor4> AddViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4); // POST New ViewScheduleReferralVendor4s
        Task<ViewScheduleReferralVendor4> UpdateViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4); // PUT ViewScheduleReferralVendor4s
        Task<(bool, string)> DeleteViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4); // DELETE ViewScheduleReferralVendor4s
    }

    public class ViewScheduleReferralVendor4Service : IViewScheduleReferralVendor4Service
    {
        private readonly XixsrvContext _db;

        public ViewScheduleReferralVendor4Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleReferralVendor4s

        public async Task<List<ViewScheduleReferralVendor4>> GetViewScheduleReferralVendor4ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleReferralVendor4s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleReferralVendor4> GetViewScheduleReferralVendor4(string ViewScheduleReferralVendor4_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor4s.FindAsync(ViewScheduleReferralVendor4_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleReferralVendor4>> GetViewScheduleReferralVendor4List(string ViewScheduleReferralVendor4_id)
        {
            try
            {
                return await _db.ViewScheduleReferralVendor4s
                    .Where(i => i.ViewScheduleReferralVendor4Id == ViewScheduleReferralVendor4_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleReferralVendor4> AddViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4)
        {
            try
            {
                await _db.ViewScheduleReferralVendor4s.AddAsync(ViewScheduleReferralVendor4);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleReferralVendor4s.FindAsync(ViewScheduleReferralVendor4.ViewScheduleReferralVendor4Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleReferralVendor4> UpdateViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4)
        {
            try
            {
                _db.Entry(ViewScheduleReferralVendor4).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleReferralVendor4;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleReferralVendor4(ViewScheduleReferralVendor4 ViewScheduleReferralVendor4)
        {
            try
            {
                var dbViewScheduleReferralVendor4 = await _db.ViewScheduleReferralVendor4s.FindAsync(ViewScheduleReferralVendor4.ViewScheduleReferralVendor4Id);

                if (dbViewScheduleReferralVendor4 == null)
                {
                    return (false, "ViewScheduleReferralVendor4 could not be found");
                }

                _db.ViewScheduleReferralVendor4s.Remove(ViewScheduleReferralVendor4);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleReferralVendor4 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleReferralVendor4s
    }
}
