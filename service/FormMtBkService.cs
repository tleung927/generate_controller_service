using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtBkService
    {
        // FormMtBks Services
        Task<List<FormMtBk>> GetFormMtBkListByValue(int offset, int limit, string val); // GET All FormMtBkss
        Task<FormMtBk> GetFormMtBk(string FormMtBk_name); // GET Single FormMtBks        
        Task<List<FormMtBk>> GetFormMtBkList(string FormMtBk_name); // GET List FormMtBks        
        Task<FormMtBk> AddFormMtBk(FormMtBk FormMtBk); // POST New FormMtBks
        Task<FormMtBk> UpdateFormMtBk(FormMtBk FormMtBk); // PUT FormMtBks
        Task<(bool, string)> DeleteFormMtBk(FormMtBk FormMtBk); // DELETE FormMtBks
    }

    public class FormMtBkService : IFormMtBkService
    {
        private readonly XixsrvContext _db;

        public FormMtBkService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtBks

        public async Task<List<FormMtBk>> GetFormMtBkListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtBks.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtBk> GetFormMtBk(string FormMtBk_id)
        {
            try
            {
                return await _db.FormMtBks.FindAsync(FormMtBk_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtBk>> GetFormMtBkList(string FormMtBk_id)
        {
            try
            {
                return await _db.FormMtBks
                    .Where(i => i.FormMtBkId == FormMtBk_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtBk> AddFormMtBk(FormMtBk FormMtBk)
        {
            try
            {
                await _db.FormMtBks.AddAsync(FormMtBk);
                await _db.SaveChangesAsync();
                return await _db.FormMtBks.FindAsync(FormMtBk.FormMtBkId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtBk> UpdateFormMtBk(FormMtBk FormMtBk)
        {
            try
            {
                _db.Entry(FormMtBk).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtBk;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtBk(FormMtBk FormMtBk)
        {
            try
            {
                var dbFormMtBk = await _db.FormMtBks.FindAsync(FormMtBk.FormMtBkId);

                if (dbFormMtBk == null)
                {
                    return (false, "FormMtBk could not be found");
                }

                _db.FormMtBks.Remove(FormMtBk);
                await _db.SaveChangesAsync();

                return (true, "FormMtBk got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtBks
    }
}
