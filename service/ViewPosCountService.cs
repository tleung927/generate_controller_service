using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewPosCountService
    {
        // ViewPosCounts Services
        Task<List<ViewPosCount>> GetViewPosCountListByValue(int offset, int limit, string val); // GET All ViewPosCountss
        Task<ViewPosCount> GetViewPosCount(string ViewPosCount_name); // GET Single ViewPosCounts        
        Task<List<ViewPosCount>> GetViewPosCountList(string ViewPosCount_name); // GET List ViewPosCounts        
        Task<ViewPosCount> AddViewPosCount(ViewPosCount ViewPosCount); // POST New ViewPosCounts
        Task<ViewPosCount> UpdateViewPosCount(ViewPosCount ViewPosCount); // PUT ViewPosCounts
        Task<(bool, string)> DeleteViewPosCount(ViewPosCount ViewPosCount); // DELETE ViewPosCounts
    }

    public class ViewPosCountService : IViewPosCountService
    {
        private readonly XixsrvContext _db;

        public ViewPosCountService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewPosCounts

        public async Task<List<ViewPosCount>> GetViewPosCountListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewPosCounts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewPosCount> GetViewPosCount(string ViewPosCount_id)
        {
            try
            {
                return await _db.ViewPosCounts.FindAsync(ViewPosCount_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewPosCount>> GetViewPosCountList(string ViewPosCount_id)
        {
            try
            {
                return await _db.ViewPosCounts
                    .Where(i => i.ViewPosCountId == ViewPosCount_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewPosCount> AddViewPosCount(ViewPosCount ViewPosCount)
        {
            try
            {
                await _db.ViewPosCounts.AddAsync(ViewPosCount);
                await _db.SaveChangesAsync();
                return await _db.ViewPosCounts.FindAsync(ViewPosCount.ViewPosCountId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewPosCount> UpdateViewPosCount(ViewPosCount ViewPosCount)
        {
            try
            {
                _db.Entry(ViewPosCount).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewPosCount;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewPosCount(ViewPosCount ViewPosCount)
        {
            try
            {
                var dbViewPosCount = await _db.ViewPosCounts.FindAsync(ViewPosCount.ViewPosCountId);

                if (dbViewPosCount == null)
                {
                    return (false, "ViewPosCount could not be found");
                }

                _db.ViewPosCounts.Remove(ViewPosCount);
                await _db.SaveChangesAsync();

                return (true, "ViewPosCount got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewPosCounts
    }
}
