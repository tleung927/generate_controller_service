using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGeneralLabelService
    {
        // GeneralLabels Services
        Task<List<GeneralLabel>> GetGeneralLabelListByValue(int offset, int limit, string val); // GET All GeneralLabelss
        Task<GeneralLabel> GetGeneralLabel(string GeneralLabel_name); // GET Single GeneralLabels        
        Task<List<GeneralLabel>> GetGeneralLabelList(string GeneralLabel_name); // GET List GeneralLabels        
        Task<GeneralLabel> AddGeneralLabel(GeneralLabel GeneralLabel); // POST New GeneralLabels
        Task<GeneralLabel> UpdateGeneralLabel(GeneralLabel GeneralLabel); // PUT GeneralLabels
        Task<(bool, string)> DeleteGeneralLabel(GeneralLabel GeneralLabel); // DELETE GeneralLabels
    }

    public class GeneralLabelService : IGeneralLabelService
    {
        private readonly XixsrvContext _db;

        public GeneralLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region GeneralLabels

        public async Task<List<GeneralLabel>> GetGeneralLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.GeneralLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GeneralLabel> GetGeneralLabel(string GeneralLabel_id)
        {
            try
            {
                return await _db.GeneralLabels.FindAsync(GeneralLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GeneralLabel>> GetGeneralLabelList(string GeneralLabel_id)
        {
            try
            {
                return await _db.GeneralLabels
                    .Where(i => i.GeneralLabelId == GeneralLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<GeneralLabel> AddGeneralLabel(GeneralLabel GeneralLabel)
        {
            try
            {
                await _db.GeneralLabels.AddAsync(GeneralLabel);
                await _db.SaveChangesAsync();
                return await _db.GeneralLabels.FindAsync(GeneralLabel.GeneralLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<GeneralLabel> UpdateGeneralLabel(GeneralLabel GeneralLabel)
        {
            try
            {
                _db.Entry(GeneralLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return GeneralLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGeneralLabel(GeneralLabel GeneralLabel)
        {
            try
            {
                var dbGeneralLabel = await _db.GeneralLabels.FindAsync(GeneralLabel.GeneralLabelId);

                if (dbGeneralLabel == null)
                {
                    return (false, "GeneralLabel could not be found");
                }

                _db.GeneralLabels.Remove(GeneralLabel);
                await _db.SaveChangesAsync();

                return (true, "GeneralLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion GeneralLabels
    }
}
