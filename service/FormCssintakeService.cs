using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormCssintakeService
    {
        // FormCssintakes Services
        Task<List<FormCssintake>> GetFormCssintakeListByValue(int offset, int limit, string val); // GET All FormCssintakess
        Task<FormCssintake> GetFormCssintake(string FormCssintake_name); // GET Single FormCssintakes        
        Task<List<FormCssintake>> GetFormCssintakeList(string FormCssintake_name); // GET List FormCssintakes        
        Task<FormCssintake> AddFormCssintake(FormCssintake FormCssintake); // POST New FormCssintakes
        Task<FormCssintake> UpdateFormCssintake(FormCssintake FormCssintake); // PUT FormCssintakes
        Task<(bool, string)> DeleteFormCssintake(FormCssintake FormCssintake); // DELETE FormCssintakes
    }

    public class FormCssintakeService : IFormCssintakeService
    {
        private readonly XixsrvContext _db;

        public FormCssintakeService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormCssintakes

        public async Task<List<FormCssintake>> GetFormCssintakeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormCssintakes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormCssintake> GetFormCssintake(string FormCssintake_id)
        {
            try
            {
                return await _db.FormCssintakes.FindAsync(FormCssintake_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormCssintake>> GetFormCssintakeList(string FormCssintake_id)
        {
            try
            {
                return await _db.FormCssintakes
                    .Where(i => i.FormCssintakeId == FormCssintake_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormCssintake> AddFormCssintake(FormCssintake FormCssintake)
        {
            try
            {
                await _db.FormCssintakes.AddAsync(FormCssintake);
                await _db.SaveChangesAsync();
                return await _db.FormCssintakes.FindAsync(FormCssintake.FormCssintakeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormCssintake> UpdateFormCssintake(FormCssintake FormCssintake)
        {
            try
            {
                _db.Entry(FormCssintake).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormCssintake;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormCssintake(FormCssintake FormCssintake)
        {
            try
            {
                var dbFormCssintake = await _db.FormCssintakes.FindAsync(FormCssintake.FormCssintakeId);

                if (dbFormCssintake == null)
                {
                    return (false, "FormCssintake could not be found");
                }

                _db.FormCssintakes.Remove(FormCssintake);
                await _db.SaveChangesAsync();

                return (true, "FormCssintake got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormCssintakes
    }
}
