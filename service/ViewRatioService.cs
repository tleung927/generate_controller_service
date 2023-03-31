using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewRatioService
    {
        // ViewRatios Services
        Task<List<ViewRatio>> GetViewRatioListByValue(int offset, int limit, string val); // GET All ViewRatioss
        Task<ViewRatio> GetViewRatio(string ViewRatio_name); // GET Single ViewRatios        
        Task<List<ViewRatio>> GetViewRatioList(string ViewRatio_name); // GET List ViewRatios        
        Task<ViewRatio> AddViewRatio(ViewRatio ViewRatio); // POST New ViewRatios
        Task<ViewRatio> UpdateViewRatio(ViewRatio ViewRatio); // PUT ViewRatios
        Task<(bool, string)> DeleteViewRatio(ViewRatio ViewRatio); // DELETE ViewRatios
    }

    public class ViewRatioService : IViewRatioService
    {
        private readonly XixsrvContext _db;

        public ViewRatioService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewRatios

        public async Task<List<ViewRatio>> GetViewRatioListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewRatios.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewRatio> GetViewRatio(string ViewRatio_id)
        {
            try
            {
                return await _db.ViewRatios.FindAsync(ViewRatio_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewRatio>> GetViewRatioList(string ViewRatio_id)
        {
            try
            {
                return await _db.ViewRatios
                    .Where(i => i.ViewRatioId == ViewRatio_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewRatio> AddViewRatio(ViewRatio ViewRatio)
        {
            try
            {
                await _db.ViewRatios.AddAsync(ViewRatio);
                await _db.SaveChangesAsync();
                return await _db.ViewRatios.FindAsync(ViewRatio.ViewRatioId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewRatio> UpdateViewRatio(ViewRatio ViewRatio)
        {
            try
            {
                _db.Entry(ViewRatio).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewRatio;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewRatio(ViewRatio ViewRatio)
        {
            try
            {
                var dbViewRatio = await _db.ViewRatios.FindAsync(ViewRatio.ViewRatioId);

                if (dbViewRatio == null)
                {
                    return (false, "ViewRatio could not be found");
                }

                _db.ViewRatios.Remove(ViewRatio);
                await _db.SaveChangesAsync();

                return (true, "ViewRatio got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewRatios
    }
}
