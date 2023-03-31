using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormLawEnforceService
    {
        // FormLawEnforces Services
        Task<List<FormLawEnforce>> GetFormLawEnforceListByValue(int offset, int limit, string val); // GET All FormLawEnforcess
        Task<FormLawEnforce> GetFormLawEnforce(string FormLawEnforce_name); // GET Single FormLawEnforces        
        Task<List<FormLawEnforce>> GetFormLawEnforceList(string FormLawEnforce_name); // GET List FormLawEnforces        
        Task<FormLawEnforce> AddFormLawEnforce(FormLawEnforce FormLawEnforce); // POST New FormLawEnforces
        Task<FormLawEnforce> UpdateFormLawEnforce(FormLawEnforce FormLawEnforce); // PUT FormLawEnforces
        Task<(bool, string)> DeleteFormLawEnforce(FormLawEnforce FormLawEnforce); // DELETE FormLawEnforces
    }

    public class FormLawEnforceService : IFormLawEnforceService
    {
        private readonly XixsrvContext _db;

        public FormLawEnforceService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormLawEnforces

        public async Task<List<FormLawEnforce>> GetFormLawEnforceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormLawEnforces.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormLawEnforce> GetFormLawEnforce(string FormLawEnforce_id)
        {
            try
            {
                return await _db.FormLawEnforces.FindAsync(FormLawEnforce_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormLawEnforce>> GetFormLawEnforceList(string FormLawEnforce_id)
        {
            try
            {
                return await _db.FormLawEnforces
                    .Where(i => i.FormLawEnforceId == FormLawEnforce_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormLawEnforce> AddFormLawEnforce(FormLawEnforce FormLawEnforce)
        {
            try
            {
                await _db.FormLawEnforces.AddAsync(FormLawEnforce);
                await _db.SaveChangesAsync();
                return await _db.FormLawEnforces.FindAsync(FormLawEnforce.FormLawEnforceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormLawEnforce> UpdateFormLawEnforce(FormLawEnforce FormLawEnforce)
        {
            try
            {
                _db.Entry(FormLawEnforce).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormLawEnforce;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormLawEnforce(FormLawEnforce FormLawEnforce)
        {
            try
            {
                var dbFormLawEnforce = await _db.FormLawEnforces.FindAsync(FormLawEnforce.FormLawEnforceId);

                if (dbFormLawEnforce == null)
                {
                    return (false, "FormLawEnforce could not be found");
                }

                _db.FormLawEnforces.Remove(FormLawEnforce);
                await _db.SaveChangesAsync();

                return (true, "FormLawEnforce got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormLawEnforces
    }
}
