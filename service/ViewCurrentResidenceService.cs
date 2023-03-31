using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewCurrentResidenceService
    {
        // ViewCurrentResidences Services
        Task<List<ViewCurrentResidence>> GetViewCurrentResidenceListByValue(int offset, int limit, string val); // GET All ViewCurrentResidencess
        Task<ViewCurrentResidence> GetViewCurrentResidence(string ViewCurrentResidence_name); // GET Single ViewCurrentResidences        
        Task<List<ViewCurrentResidence>> GetViewCurrentResidenceList(string ViewCurrentResidence_name); // GET List ViewCurrentResidences        
        Task<ViewCurrentResidence> AddViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence); // POST New ViewCurrentResidences
        Task<ViewCurrentResidence> UpdateViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence); // PUT ViewCurrentResidences
        Task<(bool, string)> DeleteViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence); // DELETE ViewCurrentResidences
    }

    public class ViewCurrentResidenceService : IViewCurrentResidenceService
    {
        private readonly XixsrvContext _db;

        public ViewCurrentResidenceService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewCurrentResidences

        public async Task<List<ViewCurrentResidence>> GetViewCurrentResidenceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewCurrentResidences.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewCurrentResidence> GetViewCurrentResidence(string ViewCurrentResidence_id)
        {
            try
            {
                return await _db.ViewCurrentResidences.FindAsync(ViewCurrentResidence_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewCurrentResidence>> GetViewCurrentResidenceList(string ViewCurrentResidence_id)
        {
            try
            {
                return await _db.ViewCurrentResidences
                    .Where(i => i.ViewCurrentResidenceId == ViewCurrentResidence_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewCurrentResidence> AddViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence)
        {
            try
            {
                await _db.ViewCurrentResidences.AddAsync(ViewCurrentResidence);
                await _db.SaveChangesAsync();
                return await _db.ViewCurrentResidences.FindAsync(ViewCurrentResidence.ViewCurrentResidenceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewCurrentResidence> UpdateViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence)
        {
            try
            {
                _db.Entry(ViewCurrentResidence).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewCurrentResidence;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewCurrentResidence(ViewCurrentResidence ViewCurrentResidence)
        {
            try
            {
                var dbViewCurrentResidence = await _db.ViewCurrentResidences.FindAsync(ViewCurrentResidence.ViewCurrentResidenceId);

                if (dbViewCurrentResidence == null)
                {
                    return (false, "ViewCurrentResidence could not be found");
                }

                _db.ViewCurrentResidences.Remove(ViewCurrentResidence);
                await _db.SaveChangesAsync();

                return (true, "ViewCurrentResidence got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewCurrentResidences
    }
}
