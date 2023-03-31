using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewCderAutService
    {
        // ViewCderAuts Services
        Task<List<ViewCderAut>> GetViewCderAutListByValue(int offset, int limit, string val); // GET All ViewCderAutss
        Task<ViewCderAut> GetViewCderAut(string ViewCderAut_name); // GET Single ViewCderAuts        
        Task<List<ViewCderAut>> GetViewCderAutList(string ViewCderAut_name); // GET List ViewCderAuts        
        Task<ViewCderAut> AddViewCderAut(ViewCderAut ViewCderAut); // POST New ViewCderAuts
        Task<ViewCderAut> UpdateViewCderAut(ViewCderAut ViewCderAut); // PUT ViewCderAuts
        Task<(bool, string)> DeleteViewCderAut(ViewCderAut ViewCderAut); // DELETE ViewCderAuts
    }

    public class ViewCderAutService : IViewCderAutService
    {
        private readonly XixsrvContext _db;

        public ViewCderAutService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewCderAuts

        public async Task<List<ViewCderAut>> GetViewCderAutListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewCderAuts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewCderAut> GetViewCderAut(string ViewCderAut_id)
        {
            try
            {
                return await _db.ViewCderAuts.FindAsync(ViewCderAut_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewCderAut>> GetViewCderAutList(string ViewCderAut_id)
        {
            try
            {
                return await _db.ViewCderAuts
                    .Where(i => i.ViewCderAutId == ViewCderAut_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewCderAut> AddViewCderAut(ViewCderAut ViewCderAut)
        {
            try
            {
                await _db.ViewCderAuts.AddAsync(ViewCderAut);
                await _db.SaveChangesAsync();
                return await _db.ViewCderAuts.FindAsync(ViewCderAut.ViewCderAutId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewCderAut> UpdateViewCderAut(ViewCderAut ViewCderAut)
        {
            try
            {
                _db.Entry(ViewCderAut).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewCderAut;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewCderAut(ViewCderAut ViewCderAut)
        {
            try
            {
                var dbViewCderAut = await _db.ViewCderAuts.FindAsync(ViewCderAut.ViewCderAutId);

                if (dbViewCderAut == null)
                {
                    return (false, "ViewCderAut could not be found");
                }

                _db.ViewCderAuts.Remove(ViewCderAut);
                await _db.SaveChangesAsync();

                return (true, "ViewCderAut got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewCderAuts
    }
}
