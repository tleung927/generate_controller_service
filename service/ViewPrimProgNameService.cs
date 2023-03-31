using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewPrimProgNameService
    {
        // ViewPrimProgNames Services
        Task<List<ViewPrimProgName>> GetViewPrimProgNameListByValue(int offset, int limit, string val); // GET All ViewPrimProgNamess
        Task<ViewPrimProgName> GetViewPrimProgName(string ViewPrimProgName_name); // GET Single ViewPrimProgNames        
        Task<List<ViewPrimProgName>> GetViewPrimProgNameList(string ViewPrimProgName_name); // GET List ViewPrimProgNames        
        Task<ViewPrimProgName> AddViewPrimProgName(ViewPrimProgName ViewPrimProgName); // POST New ViewPrimProgNames
        Task<ViewPrimProgName> UpdateViewPrimProgName(ViewPrimProgName ViewPrimProgName); // PUT ViewPrimProgNames
        Task<(bool, string)> DeleteViewPrimProgName(ViewPrimProgName ViewPrimProgName); // DELETE ViewPrimProgNames
    }

    public class ViewPrimProgNameService : IViewPrimProgNameService
    {
        private readonly XixsrvContext _db;

        public ViewPrimProgNameService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewPrimProgNames

        public async Task<List<ViewPrimProgName>> GetViewPrimProgNameListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewPrimProgNames.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewPrimProgName> GetViewPrimProgName(string ViewPrimProgName_id)
        {
            try
            {
                return await _db.ViewPrimProgNames.FindAsync(ViewPrimProgName_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewPrimProgName>> GetViewPrimProgNameList(string ViewPrimProgName_id)
        {
            try
            {
                return await _db.ViewPrimProgNames
                    .Where(i => i.ViewPrimProgNameId == ViewPrimProgName_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewPrimProgName> AddViewPrimProgName(ViewPrimProgName ViewPrimProgName)
        {
            try
            {
                await _db.ViewPrimProgNames.AddAsync(ViewPrimProgName);
                await _db.SaveChangesAsync();
                return await _db.ViewPrimProgNames.FindAsync(ViewPrimProgName.ViewPrimProgNameId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewPrimProgName> UpdateViewPrimProgName(ViewPrimProgName ViewPrimProgName)
        {
            try
            {
                _db.Entry(ViewPrimProgName).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewPrimProgName;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewPrimProgName(ViewPrimProgName ViewPrimProgName)
        {
            try
            {
                var dbViewPrimProgName = await _db.ViewPrimProgNames.FindAsync(ViewPrimProgName.ViewPrimProgNameId);

                if (dbViewPrimProgName == null)
                {
                    return (false, "ViewPrimProgName could not be found");
                }

                _db.ViewPrimProgNames.Remove(ViewPrimProgName);
                await _db.SaveChangesAsync();

                return (true, "ViewPrimProgName got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewPrimProgNames
    }
}
