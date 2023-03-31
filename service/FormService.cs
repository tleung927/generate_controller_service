using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormService
    {
        // Forms Services
        Task<List<Form>> GetFormListByValue(int offset, int limit, string val); // GET All Formss
        Task<Form> GetForm(string Form_name); // GET Single Forms        
        Task<List<Form>> GetFormList(string Form_name); // GET List Forms        
        Task<Form> AddForm(Form Form); // POST New Forms
        Task<Form> UpdateForm(Form Form); // PUT Forms
        Task<(bool, string)> DeleteForm(Form Form); // DELETE Forms
    }

    public class FormService : IFormService
    {
        private readonly XixsrvContext _db;

        public FormService(XixsrvContext db)
        {
            _db = db;
        }

        #region Forms

        public async Task<List<Form>> GetFormListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Forms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Form> GetForm(string Form_id)
        {
            try
            {
                return await _db.Forms.FindAsync(Form_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Form>> GetFormList(string Form_id)
        {
            try
            {
                return await _db.Forms
                    .Where(i => i.FormId == Form_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Form> AddForm(Form Form)
        {
            try
            {
                await _db.Forms.AddAsync(Form);
                await _db.SaveChangesAsync();
                return await _db.Forms.FindAsync(Form.FormId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Form> UpdateForm(Form Form)
        {
            try
            {
                _db.Entry(Form).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Form;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteForm(Form Form)
        {
            try
            {
                var dbForm = await _db.Forms.FindAsync(Form.FormId);

                if (dbForm == null)
                {
                    return (false, "Form could not be found");
                }

                _db.Forms.Remove(Form);
                await _db.SaveChangesAsync();

                return (true, "Form got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Forms
    }
}
