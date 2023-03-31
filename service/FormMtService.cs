using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtService
    {
        // FormMts Services
        Task<List<FormMt>> GetFormMtListByValue(int offset, int limit, string val); // GET All FormMtss
        Task<FormMt> GetFormMt(string FormMt_name); // GET Single FormMts        
        Task<List<FormMt>> GetFormMtList(string FormMt_name); // GET List FormMts        
        Task<FormMt> AddFormMt(FormMt FormMt); // POST New FormMts
        Task<FormMt> UpdateFormMt(FormMt FormMt); // PUT FormMts
        Task<(bool, string)> DeleteFormMt(FormMt FormMt); // DELETE FormMts
    }

    public class FormMtService : IFormMtService
    {
        private readonly XixsrvContext _db;

        public FormMtService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMts

        public async Task<List<FormMt>> GetFormMtListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMt> GetFormMt(string FormMt_id)
        {
            try
            {
                return await _db.FormMts.FindAsync(FormMt_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMt>> GetFormMtList(string FormMt_id)
        {
            try
            {
                return await _db.FormMts
                    .Where(i => i.FormMtId == FormMt_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMt> AddFormMt(FormMt FormMt)
        {
            try
            {
                await _db.FormMts.AddAsync(FormMt);
                await _db.SaveChangesAsync();
                return await _db.FormMts.FindAsync(FormMt.FormMtId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMt> UpdateFormMt(FormMt FormMt)
        {
            try
            {
                _db.Entry(FormMt).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMt(FormMt FormMt)
        {
            try
            {
                var dbFormMt = await _db.FormMts.FindAsync(FormMt.FormMtId);

                if (dbFormMt == null)
                {
                    return (false, "FormMt could not be found");
                }

                _db.FormMts.Remove(FormMt);
                await _db.SaveChangesAsync();

                return (true, "FormMt got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMts
    }
}
