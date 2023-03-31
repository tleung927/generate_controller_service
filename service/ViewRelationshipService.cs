using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewRelationshipService
    {
        // ViewRelationships Services
        Task<List<ViewRelationship>> GetViewRelationshipListByValue(int offset, int limit, string val); // GET All ViewRelationshipss
        Task<ViewRelationship> GetViewRelationship(string ViewRelationship_name); // GET Single ViewRelationships        
        Task<List<ViewRelationship>> GetViewRelationshipList(string ViewRelationship_name); // GET List ViewRelationships        
        Task<ViewRelationship> AddViewRelationship(ViewRelationship ViewRelationship); // POST New ViewRelationships
        Task<ViewRelationship> UpdateViewRelationship(ViewRelationship ViewRelationship); // PUT ViewRelationships
        Task<(bool, string)> DeleteViewRelationship(ViewRelationship ViewRelationship); // DELETE ViewRelationships
    }

    public class ViewRelationshipService : IViewRelationshipService
    {
        private readonly XixsrvContext _db;

        public ViewRelationshipService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewRelationships

        public async Task<List<ViewRelationship>> GetViewRelationshipListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewRelationships.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewRelationship> GetViewRelationship(string ViewRelationship_id)
        {
            try
            {
                return await _db.ViewRelationships.FindAsync(ViewRelationship_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewRelationship>> GetViewRelationshipList(string ViewRelationship_id)
        {
            try
            {
                return await _db.ViewRelationships
                    .Where(i => i.ViewRelationshipId == ViewRelationship_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewRelationship> AddViewRelationship(ViewRelationship ViewRelationship)
        {
            try
            {
                await _db.ViewRelationships.AddAsync(ViewRelationship);
                await _db.SaveChangesAsync();
                return await _db.ViewRelationships.FindAsync(ViewRelationship.ViewRelationshipId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewRelationship> UpdateViewRelationship(ViewRelationship ViewRelationship)
        {
            try
            {
                _db.Entry(ViewRelationship).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewRelationship;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewRelationship(ViewRelationship ViewRelationship)
        {
            try
            {
                var dbViewRelationship = await _db.ViewRelationships.FindAsync(ViewRelationship.ViewRelationshipId);

                if (dbViewRelationship == null)
                {
                    return (false, "ViewRelationship could not be found");
                }

                _db.ViewRelationships.Remove(ViewRelationship);
                await _db.SaveChangesAsync();

                return (true, "ViewRelationship got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewRelationships
    }
}
