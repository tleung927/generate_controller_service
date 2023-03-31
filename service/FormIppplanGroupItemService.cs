using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppplanGroupItemService
    {
        // FormIppplanGroupItems Services
        Task<List<FormIppplanGroupItem>> GetFormIppplanGroupItemListByValue(int offset, int limit, string val); // GET All FormIppplanGroupItemss
        Task<FormIppplanGroupItem> GetFormIppplanGroupItem(string FormIppplanGroupItem_name); // GET Single FormIppplanGroupItems        
        Task<List<FormIppplanGroupItem>> GetFormIppplanGroupItemList(string FormIppplanGroupItem_name); // GET List FormIppplanGroupItems        
        Task<FormIppplanGroupItem> AddFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem); // POST New FormIppplanGroupItems
        Task<FormIppplanGroupItem> UpdateFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem); // PUT FormIppplanGroupItems
        Task<(bool, string)> DeleteFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem); // DELETE FormIppplanGroupItems
    }

    public class FormIppplanGroupItemService : IFormIppplanGroupItemService
    {
        private readonly XixsrvContext _db;

        public FormIppplanGroupItemService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppplanGroupItems

        public async Task<List<FormIppplanGroupItem>> GetFormIppplanGroupItemListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppplanGroupItems.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppplanGroupItem> GetFormIppplanGroupItem(string FormIppplanGroupItem_id)
        {
            try
            {
                return await _db.FormIppplanGroupItems.FindAsync(FormIppplanGroupItem_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppplanGroupItem>> GetFormIppplanGroupItemList(string FormIppplanGroupItem_id)
        {
            try
            {
                return await _db.FormIppplanGroupItems
                    .Where(i => i.FormIppplanGroupItemId == FormIppplanGroupItem_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppplanGroupItem> AddFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem)
        {
            try
            {
                await _db.FormIppplanGroupItems.AddAsync(FormIppplanGroupItem);
                await _db.SaveChangesAsync();
                return await _db.FormIppplanGroupItems.FindAsync(FormIppplanGroupItem.FormIppplanGroupItemId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppplanGroupItem> UpdateFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem)
        {
            try
            {
                _db.Entry(FormIppplanGroupItem).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppplanGroupItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppplanGroupItem(FormIppplanGroupItem FormIppplanGroupItem)
        {
            try
            {
                var dbFormIppplanGroupItem = await _db.FormIppplanGroupItems.FindAsync(FormIppplanGroupItem.FormIppplanGroupItemId);

                if (dbFormIppplanGroupItem == null)
                {
                    return (false, "FormIppplanGroupItem could not be found");
                }

                _db.FormIppplanGroupItems.Remove(FormIppplanGroupItem);
                await _db.SaveChangesAsync();

                return (true, "FormIppplanGroupItem got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppplanGroupItems
    }
}
