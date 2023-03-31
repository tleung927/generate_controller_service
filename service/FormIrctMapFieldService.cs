using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIrctMapFieldService
    {
        // FormIrctMapFields Services
        Task<List<FormIrctMapField>> GetFormIrctMapFieldListByValue(int offset, int limit, string val); // GET All FormIrctMapFieldss
        Task<FormIrctMapField> GetFormIrctMapField(string FormIrctMapField_name); // GET Single FormIrctMapFields        
        Task<List<FormIrctMapField>> GetFormIrctMapFieldList(string FormIrctMapField_name); // GET List FormIrctMapFields        
        Task<FormIrctMapField> AddFormIrctMapField(FormIrctMapField FormIrctMapField); // POST New FormIrctMapFields
        Task<FormIrctMapField> UpdateFormIrctMapField(FormIrctMapField FormIrctMapField); // PUT FormIrctMapFields
        Task<(bool, string)> DeleteFormIrctMapField(FormIrctMapField FormIrctMapField); // DELETE FormIrctMapFields
    }

    public class FormIrctMapFieldService : IFormIrctMapFieldService
    {
        private readonly XixsrvContext _db;

        public FormIrctMapFieldService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIrctMapFields

        public async Task<List<FormIrctMapField>> GetFormIrctMapFieldListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIrctMapFields.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIrctMapField> GetFormIrctMapField(string FormIrctMapField_id)
        {
            try
            {
                return await _db.FormIrctMapFields.FindAsync(FormIrctMapField_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIrctMapField>> GetFormIrctMapFieldList(string FormIrctMapField_id)
        {
            try
            {
                return await _db.FormIrctMapFields
                    .Where(i => i.FormIrctMapFieldId == FormIrctMapField_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIrctMapField> AddFormIrctMapField(FormIrctMapField FormIrctMapField)
        {
            try
            {
                await _db.FormIrctMapFields.AddAsync(FormIrctMapField);
                await _db.SaveChangesAsync();
                return await _db.FormIrctMapFields.FindAsync(FormIrctMapField.FormIrctMapFieldId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIrctMapField> UpdateFormIrctMapField(FormIrctMapField FormIrctMapField)
        {
            try
            {
                _db.Entry(FormIrctMapField).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIrctMapField;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIrctMapField(FormIrctMapField FormIrctMapField)
        {
            try
            {
                var dbFormIrctMapField = await _db.FormIrctMapFields.FindAsync(FormIrctMapField.FormIrctMapFieldId);

                if (dbFormIrctMapField == null)
                {
                    return (false, "FormIrctMapField could not be found");
                }

                _db.FormIrctMapFields.Remove(FormIrctMapField);
                await _db.SaveChangesAsync();

                return (true, "FormIrctMapField got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIrctMapFields
    }
}
