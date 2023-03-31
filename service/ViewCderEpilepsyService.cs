using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewCderEpilepsyService
    {
        // ViewCderEpilepsys Services
        Task<List<ViewCderEpilepsy>> GetViewCderEpilepsyListByValue(int offset, int limit, string val); // GET All ViewCderEpilepsyss
        Task<ViewCderEpilepsy> GetViewCderEpilepsy(string ViewCderEpilepsy_name); // GET Single ViewCderEpilepsys        
        Task<List<ViewCderEpilepsy>> GetViewCderEpilepsyList(string ViewCderEpilepsy_name); // GET List ViewCderEpilepsys        
        Task<ViewCderEpilepsy> AddViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy); // POST New ViewCderEpilepsys
        Task<ViewCderEpilepsy> UpdateViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy); // PUT ViewCderEpilepsys
        Task<(bool, string)> DeleteViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy); // DELETE ViewCderEpilepsys
    }

    public class ViewCderEpilepsyService : IViewCderEpilepsyService
    {
        private readonly XixsrvContext _db;

        public ViewCderEpilepsyService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewCderEpilepsys

        public async Task<List<ViewCderEpilepsy>> GetViewCderEpilepsyListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewCderEpilepsys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewCderEpilepsy> GetViewCderEpilepsy(string ViewCderEpilepsy_id)
        {
            try
            {
                return await _db.ViewCderEpilepsys.FindAsync(ViewCderEpilepsy_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewCderEpilepsy>> GetViewCderEpilepsyList(string ViewCderEpilepsy_id)
        {
            try
            {
                return await _db.ViewCderEpilepsys
                    .Where(i => i.ViewCderEpilepsyId == ViewCderEpilepsy_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewCderEpilepsy> AddViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy)
        {
            try
            {
                await _db.ViewCderEpilepsys.AddAsync(ViewCderEpilepsy);
                await _db.SaveChangesAsync();
                return await _db.ViewCderEpilepsys.FindAsync(ViewCderEpilepsy.ViewCderEpilepsyId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewCderEpilepsy> UpdateViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy)
        {
            try
            {
                _db.Entry(ViewCderEpilepsy).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewCderEpilepsy;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy)
        {
            try
            {
                var dbViewCderEpilepsy = await _db.ViewCderEpilepsys.FindAsync(ViewCderEpilepsy.ViewCderEpilepsyId);

                if (dbViewCderEpilepsy == null)
                {
                    return (false, "ViewCderEpilepsy could not be found");
                }

                _db.ViewCderEpilepsys.Remove(ViewCderEpilepsy);
                await _db.SaveChangesAsync();

                return (true, "ViewCderEpilepsy got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewCderEpilepsys
    }
}
