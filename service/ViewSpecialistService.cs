using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewSpecialistService
    {
        // ViewSpecialists Services
        Task<List<ViewSpecialist>> GetViewSpecialistListByValue(int offset, int limit, string val); // GET All ViewSpecialistss
        Task<ViewSpecialist> GetViewSpecialist(string ViewSpecialist_name); // GET Single ViewSpecialists        
        Task<List<ViewSpecialist>> GetViewSpecialistList(string ViewSpecialist_name); // GET List ViewSpecialists        
        Task<ViewSpecialist> AddViewSpecialist(ViewSpecialist ViewSpecialist); // POST New ViewSpecialists
        Task<ViewSpecialist> UpdateViewSpecialist(ViewSpecialist ViewSpecialist); // PUT ViewSpecialists
        Task<(bool, string)> DeleteViewSpecialist(ViewSpecialist ViewSpecialist); // DELETE ViewSpecialists
    }

    public class ViewSpecialistService : IViewSpecialistService
    {
        private readonly XixsrvContext _db;

        public ViewSpecialistService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewSpecialists

        public async Task<List<ViewSpecialist>> GetViewSpecialistListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewSpecialists.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewSpecialist> GetViewSpecialist(string ViewSpecialist_id)
        {
            try
            {
                return await _db.ViewSpecialists.FindAsync(ViewSpecialist_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewSpecialist>> GetViewSpecialistList(string ViewSpecialist_id)
        {
            try
            {
                return await _db.ViewSpecialists
                    .Where(i => i.ViewSpecialistId == ViewSpecialist_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewSpecialist> AddViewSpecialist(ViewSpecialist ViewSpecialist)
        {
            try
            {
                await _db.ViewSpecialists.AddAsync(ViewSpecialist);
                await _db.SaveChangesAsync();
                return await _db.ViewSpecialists.FindAsync(ViewSpecialist.ViewSpecialistId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewSpecialist> UpdateViewSpecialist(ViewSpecialist ViewSpecialist)
        {
            try
            {
                _db.Entry(ViewSpecialist).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewSpecialist;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewSpecialist(ViewSpecialist ViewSpecialist)
        {
            try
            {
                var dbViewSpecialist = await _db.ViewSpecialists.FindAsync(ViewSpecialist.ViewSpecialistId);

                if (dbViewSpecialist == null)
                {
                    return (false, "ViewSpecialist could not be found");
                }

                _db.ViewSpecialists.Remove(ViewSpecialist);
                await _db.SaveChangesAsync();

                return (true, "ViewSpecialist got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewSpecialists
    }
}
