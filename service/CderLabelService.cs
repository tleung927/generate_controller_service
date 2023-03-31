using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderLabelService
    {
        // CderLabels Services
        Task<List<CderLabel>> GetCderLabelListByValue(int offset, int limit, string val); // GET All CderLabelss
        Task<CderLabel> GetCderLabel(string CderLabel_name); // GET Single CderLabels        
        Task<List<CderLabel>> GetCderLabelList(string CderLabel_name); // GET List CderLabels        
        Task<CderLabel> AddCderLabel(CderLabel CderLabel); // POST New CderLabels
        Task<CderLabel> UpdateCderLabel(CderLabel CderLabel); // PUT CderLabels
        Task<(bool, string)> DeleteCderLabel(CderLabel CderLabel); // DELETE CderLabels
    }

    public class CderLabelService : ICderLabelService
    {
        private readonly XixsrvContext _db;

        public CderLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region CderLabels

        public async Task<List<CderLabel>> GetCderLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CderLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CderLabel> GetCderLabel(string CderLabel_id)
        {
            try
            {
                return await _db.CderLabels.FindAsync(CderLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CderLabel>> GetCderLabelList(string CderLabel_id)
        {
            try
            {
                return await _db.CderLabels
                    .Where(i => i.CderLabelId == CderLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CderLabel> AddCderLabel(CderLabel CderLabel)
        {
            try
            {
                await _db.CderLabels.AddAsync(CderLabel);
                await _db.SaveChangesAsync();
                return await _db.CderLabels.FindAsync(CderLabel.CderLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CderLabel> UpdateCderLabel(CderLabel CderLabel)
        {
            try
            {
                _db.Entry(CderLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CderLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCderLabel(CderLabel CderLabel)
        {
            try
            {
                var dbCderLabel = await _db.CderLabels.FindAsync(CderLabel.CderLabelId);

                if (dbCderLabel == null)
                {
                    return (false, "CderLabel could not be found");
                }

                _db.CderLabels.Remove(CderLabel);
                await _db.SaveChangesAsync();

                return (true, "CderLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CderLabels
    }
}
