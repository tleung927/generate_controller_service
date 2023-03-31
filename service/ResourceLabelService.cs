using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IResourceLabelService
    {
        // ResourceLabels Services
        Task<List<ResourceLabel>> GetResourceLabelListByValue(int offset, int limit, string val); // GET All ResourceLabelss
        Task<ResourceLabel> GetResourceLabel(string ResourceLabel_name); // GET Single ResourceLabels        
        Task<List<ResourceLabel>> GetResourceLabelList(string ResourceLabel_name); // GET List ResourceLabels        
        Task<ResourceLabel> AddResourceLabel(ResourceLabel ResourceLabel); // POST New ResourceLabels
        Task<ResourceLabel> UpdateResourceLabel(ResourceLabel ResourceLabel); // PUT ResourceLabels
        Task<(bool, string)> DeleteResourceLabel(ResourceLabel ResourceLabel); // DELETE ResourceLabels
    }

    public class ResourceLabelService : IResourceLabelService
    {
        private readonly XixsrvContext _db;

        public ResourceLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region ResourceLabels

        public async Task<List<ResourceLabel>> GetResourceLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ResourceLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ResourceLabel> GetResourceLabel(string ResourceLabel_id)
        {
            try
            {
                return await _db.ResourceLabels.FindAsync(ResourceLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ResourceLabel>> GetResourceLabelList(string ResourceLabel_id)
        {
            try
            {
                return await _db.ResourceLabels
                    .Where(i => i.ResourceLabelId == ResourceLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ResourceLabel> AddResourceLabel(ResourceLabel ResourceLabel)
        {
            try
            {
                await _db.ResourceLabels.AddAsync(ResourceLabel);
                await _db.SaveChangesAsync();
                return await _db.ResourceLabels.FindAsync(ResourceLabel.ResourceLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ResourceLabel> UpdateResourceLabel(ResourceLabel ResourceLabel)
        {
            try
            {
                _db.Entry(ResourceLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ResourceLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteResourceLabel(ResourceLabel ResourceLabel)
        {
            try
            {
                var dbResourceLabel = await _db.ResourceLabels.FindAsync(ResourceLabel.ResourceLabelId);

                if (dbResourceLabel == null)
                {
                    return (false, "ResourceLabel could not be found");
                }

                _db.ResourceLabels.Remove(ResourceLabel);
                await _db.SaveChangesAsync();

                return (true, "ResourceLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ResourceLabels
    }
}
