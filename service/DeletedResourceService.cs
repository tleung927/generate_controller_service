using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedResourceService
    {
        // DeletedResources Services
        Task<List<DeletedResource>> GetDeletedResourceListByValue(int offset, int limit, string val); // GET All DeletedResourcess
        Task<DeletedResource> GetDeletedResource(string DeletedResource_name); // GET Single DeletedResources        
        Task<List<DeletedResource>> GetDeletedResourceList(string DeletedResource_name); // GET List DeletedResources        
        Task<DeletedResource> AddDeletedResource(DeletedResource DeletedResource); // POST New DeletedResources
        Task<DeletedResource> UpdateDeletedResource(DeletedResource DeletedResource); // PUT DeletedResources
        Task<(bool, string)> DeleteDeletedResource(DeletedResource DeletedResource); // DELETE DeletedResources
    }

    public class DeletedResourceService : IDeletedResourceService
    {
        private readonly XixsrvContext _db;

        public DeletedResourceService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedResources

        public async Task<List<DeletedResource>> GetDeletedResourceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedResources.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedResource> GetDeletedResource(string DeletedResource_id)
        {
            try
            {
                return await _db.DeletedResources.FindAsync(DeletedResource_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedResource>> GetDeletedResourceList(string DeletedResource_id)
        {
            try
            {
                return await _db.DeletedResources
                    .Where(i => i.DeletedResourceId == DeletedResource_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedResource> AddDeletedResource(DeletedResource DeletedResource)
        {
            try
            {
                await _db.DeletedResources.AddAsync(DeletedResource);
                await _db.SaveChangesAsync();
                return await _db.DeletedResources.FindAsync(DeletedResource.DeletedResourceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedResource> UpdateDeletedResource(DeletedResource DeletedResource)
        {
            try
            {
                _db.Entry(DeletedResource).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedResource;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedResource(DeletedResource DeletedResource)
        {
            try
            {
                var dbDeletedResource = await _db.DeletedResources.FindAsync(DeletedResource.DeletedResourceId);

                if (dbDeletedResource == null)
                {
                    return (false, "DeletedResource could not be found");
                }

                _db.DeletedResources.Remove(DeletedResource);
                await _db.SaveChangesAsync();

                return (true, "DeletedResource got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedResources
    }
}
