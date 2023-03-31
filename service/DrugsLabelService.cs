using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDrugsLabelService
    {
        // DrugsLabels Services
        Task<List<DrugsLabel>> GetDrugsLabelListByValue(int offset, int limit, string val); // GET All DrugsLabelss
        Task<DrugsLabel> GetDrugsLabel(string DrugsLabel_name); // GET Single DrugsLabels        
        Task<List<DrugsLabel>> GetDrugsLabelList(string DrugsLabel_name); // GET List DrugsLabels        
        Task<DrugsLabel> AddDrugsLabel(DrugsLabel DrugsLabel); // POST New DrugsLabels
        Task<DrugsLabel> UpdateDrugsLabel(DrugsLabel DrugsLabel); // PUT DrugsLabels
        Task<(bool, string)> DeleteDrugsLabel(DrugsLabel DrugsLabel); // DELETE DrugsLabels
    }

    public class DrugsLabelService : IDrugsLabelService
    {
        private readonly XixsrvContext _db;

        public DrugsLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region DrugsLabels

        public async Task<List<DrugsLabel>> GetDrugsLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DrugsLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DrugsLabel> GetDrugsLabel(string DrugsLabel_id)
        {
            try
            {
                return await _db.DrugsLabels.FindAsync(DrugsLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DrugsLabel>> GetDrugsLabelList(string DrugsLabel_id)
        {
            try
            {
                return await _db.DrugsLabels
                    .Where(i => i.DrugsLabelId == DrugsLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DrugsLabel> AddDrugsLabel(DrugsLabel DrugsLabel)
        {
            try
            {
                await _db.DrugsLabels.AddAsync(DrugsLabel);
                await _db.SaveChangesAsync();
                return await _db.DrugsLabels.FindAsync(DrugsLabel.DrugsLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DrugsLabel> UpdateDrugsLabel(DrugsLabel DrugsLabel)
        {
            try
            {
                _db.Entry(DrugsLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DrugsLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDrugsLabel(DrugsLabel DrugsLabel)
        {
            try
            {
                var dbDrugsLabel = await _db.DrugsLabels.FindAsync(DrugsLabel.DrugsLabelId);

                if (dbDrugsLabel == null)
                {
                    return (false, "DrugsLabel could not be found");
                }

                _db.DrugsLabels.Remove(DrugsLabel);
                await _db.SaveChangesAsync();

                return (true, "DrugsLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DrugsLabels
    }
}
