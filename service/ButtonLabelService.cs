using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IButtonLabelService
    {
        // ButtonLabels Services
        Task<List<ButtonLabel>> GetButtonLabelListByValue(int offset, int limit, string val); // GET All ButtonLabelss
        Task<ButtonLabel> GetButtonLabel(string ButtonLabel_name); // GET Single ButtonLabels        
        Task<List<ButtonLabel>> GetButtonLabelList(string ButtonLabel_name); // GET List ButtonLabels        
        Task<ButtonLabel> AddButtonLabel(ButtonLabel ButtonLabel); // POST New ButtonLabels
        Task<ButtonLabel> UpdateButtonLabel(ButtonLabel ButtonLabel); // PUT ButtonLabels
        Task<(bool, string)> DeleteButtonLabel(ButtonLabel ButtonLabel); // DELETE ButtonLabels
    }

    public class ButtonLabelService : IButtonLabelService
    {
        private readonly XixsrvContext _db;

        public ButtonLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region ButtonLabels

        public async Task<List<ButtonLabel>> GetButtonLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ButtonLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ButtonLabel> GetButtonLabel(string ButtonLabel_id)
        {
            try
            {
                return await _db.ButtonLabels.FindAsync(ButtonLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ButtonLabel>> GetButtonLabelList(string ButtonLabel_id)
        {
            try
            {
                return await _db.ButtonLabels
                    .Where(i => i.ButtonLabelId == ButtonLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ButtonLabel> AddButtonLabel(ButtonLabel ButtonLabel)
        {
            try
            {
                await _db.ButtonLabels.AddAsync(ButtonLabel);
                await _db.SaveChangesAsync();
                return await _db.ButtonLabels.FindAsync(ButtonLabel.ButtonLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ButtonLabel> UpdateButtonLabel(ButtonLabel ButtonLabel)
        {
            try
            {
                _db.Entry(ButtonLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ButtonLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteButtonLabel(ButtonLabel ButtonLabel)
        {
            try
            {
                var dbButtonLabel = await _db.ButtonLabels.FindAsync(ButtonLabel.ButtonLabelId);

                if (dbButtonLabel == null)
                {
                    return (false, "ButtonLabel could not be found");
                }

                _db.ButtonLabels.Remove(ButtonLabel);
                await _db.SaveChangesAsync();

                return (true, "ButtonLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ButtonLabels
    }
}
