using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IResourceService
    {
        // Resources Services
        Task<List<Resource>> GetResourceListByValue(int offset, int limit, string val); // GET All Resourcess
        Task<Resource> GetResource(string Resource_name); // GET Single Resources        
        Task<List<Resource>> GetResourceList(string Resource_name); // GET List Resources        
        Task<Resource> AddResource(Resource Resource); // POST New Resources
        Task<Resource> UpdateResource(Resource Resource); // PUT Resources
        Task<(bool, string)> DeleteResource(Resource Resource); // DELETE Resources
    }

    public class ResourceService : IResourceService
    {
        private readonly XixsrvContext _db;

        public ResourceService(XixsrvContext db)
        {
            _db = db;
        }

        #region Resources

        public async Task<List<Resource>> GetResourceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Resources.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Resource> GetResource(string Resource_id)
        {
            try
            {
                return await _db.Resources.FindAsync(Resource_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Resource>> GetResourceList(string Resource_id)
        {
            try
            {
                return await _db.Resources
                    .Where(i => i.ResourceId == Resource_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Resource> AddResource(Resource Resource)
        {
            try
            {
                await _db.Resources.AddAsync(Resource);
                await _db.SaveChangesAsync();
                return await _db.Resources.FindAsync(Resource.ResourceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Resource> UpdateResource(Resource Resource)
        {
            try
            {
                _db.Entry(Resource).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Resource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteResource(Resource Resource)
        {
            try
            {
                var dbResource = await _db.Resources.FindAsync(Resource.ResourceId);

                if (dbResource == null)
                {
                    return (false, "Resource could not be found");
                }

                _db.Resources.Remove(Resource);
                await _db.SaveChangesAsync();

                return (true, "Resource got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Resources
    }
}
