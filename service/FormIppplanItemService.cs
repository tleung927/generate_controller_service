using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppplanItemService
    {
        // FormIppplanItems Services
        Task<List<FormIppplanItem>> GetFormIppplanItemListByValue(int offset, int limit, string val); // GET All FormIppplanItemss
        Task<FormIppplanItem> GetFormIppplanItem(string FormIppplanItem_name); // GET Single FormIppplanItems        
        Task<List<FormIppplanItem>> GetFormIppplanItemList(string FormIppplanItem_name); // GET List FormIppplanItems        
        Task<FormIppplanItem> AddFormIppplanItem(FormIppplanItem FormIppplanItem); // POST New FormIppplanItems
        Task<FormIppplanItem> UpdateFormIppplanItem(FormIppplanItem FormIppplanItem); // PUT FormIppplanItems
        Task<(bool, string)> DeleteFormIppplanItem(FormIppplanItem FormIppplanItem); // DELETE FormIppplanItems
    }

    public class FormIppplanItemService : IFormIppplanItemService
    {
        private readonly XixsrvContext _db;

        public FormIppplanItemService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppplanItems

        public async Task<List<FormIppplanItem>> GetFormIppplanItemListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppplanItems.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppplanItem> GetFormIppplanItem(string FormIppplanItem_id)
        {
            try
            {
                return await _db.FormIppplanItems.FindAsync(FormIppplanItem_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppplanItem>> GetFormIppplanItemList(string FormIppplanItem_id)
        {
            try
            {
                return await _db.FormIppplanItems
                    .Where(i => i.FormIppplanItemId == FormIppplanItem_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppplanItem> AddFormIppplanItem(FormIppplanItem FormIppplanItem)
        {
            try
            {
                await _db.FormIppplanItems.AddAsync(FormIppplanItem);
                await _db.SaveChangesAsync();
                return await _db.FormIppplanItems.FindAsync(FormIppplanItem.FormIppplanItemId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppplanItem> UpdateFormIppplanItem(FormIppplanItem FormIppplanItem)
        {
            try
            {
                _db.Entry(FormIppplanItem).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppplanItem;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppplanItem(FormIppplanItem FormIppplanItem)
        {
            try
            {
                var dbFormIppplanItem = await _db.FormIppplanItems.FindAsync(FormIppplanItem.FormIppplanItemId);

                if (dbFormIppplanItem == null)
                {
                    return (false, "FormIppplanItem could not be found");
                }

                _db.FormIppplanItems.Remove(FormIppplanItem);
                await _db.SaveChangesAsync();

                return (true, "FormIppplanItem got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppplanItems
    }
}
