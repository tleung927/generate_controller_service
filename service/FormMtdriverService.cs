using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtdriverService
    {
        // FormMtdrivers Services
        Task<List<FormMtdriver>> GetFormMtdriverListByValue(int offset, int limit, string val); // GET All FormMtdriverss
        Task<FormMtdriver> GetFormMtdriver(string FormMtdriver_name); // GET Single FormMtdrivers        
        Task<List<FormMtdriver>> GetFormMtdriverList(string FormMtdriver_name); // GET List FormMtdrivers        
        Task<FormMtdriver> AddFormMtdriver(FormMtdriver FormMtdriver); // POST New FormMtdrivers
        Task<FormMtdriver> UpdateFormMtdriver(FormMtdriver FormMtdriver); // PUT FormMtdrivers
        Task<(bool, string)> DeleteFormMtdriver(FormMtdriver FormMtdriver); // DELETE FormMtdrivers
    }

    public class FormMtdriverService : IFormMtdriverService
    {
        private readonly XixsrvContext _db;

        public FormMtdriverService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtdrivers

        public async Task<List<FormMtdriver>> GetFormMtdriverListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtdrivers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtdriver> GetFormMtdriver(string FormMtdriver_id)
        {
            try
            {
                return await _db.FormMtdrivers.FindAsync(FormMtdriver_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtdriver>> GetFormMtdriverList(string FormMtdriver_id)
        {
            try
            {
                return await _db.FormMtdrivers
                    .Where(i => i.FormMtdriverId == FormMtdriver_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtdriver> AddFormMtdriver(FormMtdriver FormMtdriver)
        {
            try
            {
                await _db.FormMtdrivers.AddAsync(FormMtdriver);
                await _db.SaveChangesAsync();
                return await _db.FormMtdrivers.FindAsync(FormMtdriver.FormMtdriverId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtdriver> UpdateFormMtdriver(FormMtdriver FormMtdriver)
        {
            try
            {
                _db.Entry(FormMtdriver).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtdriver;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtdriver(FormMtdriver FormMtdriver)
        {
            try
            {
                var dbFormMtdriver = await _db.FormMtdrivers.FindAsync(FormMtdriver.FormMtdriverId);

                if (dbFormMtdriver == null)
                {
                    return (false, "FormMtdriver could not be found");
                }

                _db.FormMtdrivers.Remove(FormMtdriver);
                await _db.SaveChangesAsync();

                return (true, "FormMtdriver got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtdrivers
    }
}
