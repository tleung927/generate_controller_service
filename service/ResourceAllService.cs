using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IResourceAllService
    {
        // ResourceAlls Services
        Task<List<ResourceAll>> GetResourceAllListByValue(int offset, int limit, string val); // GET All ResourceAllss
        Task<ResourceAll> GetResourceAll(string ResourceAll_name); // GET Single ResourceAlls        
        Task<List<ResourceAll>> GetResourceAllList(string ResourceAll_name); // GET List ResourceAlls        
        Task<ResourceAll> AddResourceAll(ResourceAll ResourceAll); // POST New ResourceAlls
        Task<ResourceAll> UpdateResourceAll(ResourceAll ResourceAll); // PUT ResourceAlls
        Task<(bool, string)> DeleteResourceAll(ResourceAll ResourceAll); // DELETE ResourceAlls
    }

    public class ResourceAllService : IResourceAllService
    {
        private readonly XixsrvContext _db;

        public ResourceAllService(XixsrvContext db)
        {
            _db = db;
        }

        #region ResourceAlls

        public async Task<List<ResourceAll>> GetResourceAllListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ResourceAlls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResourceAll> GetResourceAll(string ResourceAll_id)
        {
            try
            {
                return await _db.ResourceAlls.FindAsync(ResourceAll_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ResourceAll>> GetResourceAllList(string ResourceAll_id)
        {
            try
            {
                return await _db.ResourceAlls
                    .Where(i => i.ResourceAllId == ResourceAll_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ResourceAll> AddResourceAll(ResourceAll ResourceAll)
        {
            try
            {
                await _db.ResourceAlls.AddAsync(ResourceAll);
                await _db.SaveChangesAsync();
                return await _db.ResourceAlls.FindAsync(ResourceAll.ResourceAllId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ResourceAll> UpdateResourceAll(ResourceAll ResourceAll)
        {
            try
            {
                _db.Entry(ResourceAll).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ResourceAll;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteResourceAll(ResourceAll ResourceAll)
        {
            try
            {
                var dbResourceAll = await _db.ResourceAlls.FindAsync(ResourceAll.ResourceAllId);

                if (dbResourceAll == null)
                {
                    return (false, "ResourceAll could not be found");
                }

                _db.ResourceAlls.Remove(ResourceAll);
                await _db.SaveChangesAsync();

                return (true, "ResourceAll got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ResourceAlls
    }
}
