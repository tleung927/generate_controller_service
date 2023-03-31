using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormCaseLoadDelegateService
    {
        // FormCaseLoadDelegates Services
        Task<List<FormCaseLoadDelegate>> GetFormCaseLoadDelegateListByValue(int offset, int limit, string val); // GET All FormCaseLoadDelegatess
        Task<FormCaseLoadDelegate> GetFormCaseLoadDelegate(string FormCaseLoadDelegate_name); // GET Single FormCaseLoadDelegates        
        Task<List<FormCaseLoadDelegate>> GetFormCaseLoadDelegateList(string FormCaseLoadDelegate_name); // GET List FormCaseLoadDelegates        
        Task<FormCaseLoadDelegate> AddFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate); // POST New FormCaseLoadDelegates
        Task<FormCaseLoadDelegate> UpdateFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate); // PUT FormCaseLoadDelegates
        Task<(bool, string)> DeleteFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate); // DELETE FormCaseLoadDelegates
    }

    public class FormCaseLoadDelegateService : IFormCaseLoadDelegateService
    {
        private readonly XixsrvContext _db;

        public FormCaseLoadDelegateService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormCaseLoadDelegates

        public async Task<List<FormCaseLoadDelegate>> GetFormCaseLoadDelegateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormCaseLoadDelegates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormCaseLoadDelegate> GetFormCaseLoadDelegate(string FormCaseLoadDelegate_id)
        {
            try
            {
                return await _db.FormCaseLoadDelegates.FindAsync(FormCaseLoadDelegate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormCaseLoadDelegate>> GetFormCaseLoadDelegateList(string FormCaseLoadDelegate_id)
        {
            try
            {
                return await _db.FormCaseLoadDelegates
                    .Where(i => i.FormCaseLoadDelegateId == FormCaseLoadDelegate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormCaseLoadDelegate> AddFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate)
        {
            try
            {
                await _db.FormCaseLoadDelegates.AddAsync(FormCaseLoadDelegate);
                await _db.SaveChangesAsync();
                return await _db.FormCaseLoadDelegates.FindAsync(FormCaseLoadDelegate.FormCaseLoadDelegateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormCaseLoadDelegate> UpdateFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate)
        {
            try
            {
                _db.Entry(FormCaseLoadDelegate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormCaseLoadDelegate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormCaseLoadDelegate(FormCaseLoadDelegate FormCaseLoadDelegate)
        {
            try
            {
                var dbFormCaseLoadDelegate = await _db.FormCaseLoadDelegates.FindAsync(FormCaseLoadDelegate.FormCaseLoadDelegateId);

                if (dbFormCaseLoadDelegate == null)
                {
                    return (false, "FormCaseLoadDelegate could not be found");
                }

                _db.FormCaseLoadDelegates.Remove(FormCaseLoadDelegate);
                await _db.SaveChangesAsync();

                return (true, "FormCaseLoadDelegate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormCaseLoadDelegates
    }
}
