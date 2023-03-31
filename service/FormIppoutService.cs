using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppoutService
    {
        // FormIppouts Services
        Task<List<FormIppout>> GetFormIppoutListByValue(int offset, int limit, string val); // GET All FormIppoutss
        Task<FormIppout> GetFormIppout(string FormIppout_name); // GET Single FormIppouts        
        Task<List<FormIppout>> GetFormIppoutList(string FormIppout_name); // GET List FormIppouts        
        Task<FormIppout> AddFormIppout(FormIppout FormIppout); // POST New FormIppouts
        Task<FormIppout> UpdateFormIppout(FormIppout FormIppout); // PUT FormIppouts
        Task<(bool, string)> DeleteFormIppout(FormIppout FormIppout); // DELETE FormIppouts
    }

    public class FormIppoutService : IFormIppoutService
    {
        private readonly XixsrvContext _db;

        public FormIppoutService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppouts

        public async Task<List<FormIppout>> GetFormIppoutListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppouts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppout> GetFormIppout(string FormIppout_id)
        {
            try
            {
                return await _db.FormIppouts.FindAsync(FormIppout_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppout>> GetFormIppoutList(string FormIppout_id)
        {
            try
            {
                return await _db.FormIppouts
                    .Where(i => i.FormIppoutId == FormIppout_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppout> AddFormIppout(FormIppout FormIppout)
        {
            try
            {
                await _db.FormIppouts.AddAsync(FormIppout);
                await _db.SaveChangesAsync();
                return await _db.FormIppouts.FindAsync(FormIppout.FormIppoutId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppout> UpdateFormIppout(FormIppout FormIppout)
        {
            try
            {
                _db.Entry(FormIppout).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppout;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppout(FormIppout FormIppout)
        {
            try
            {
                var dbFormIppout = await _db.FormIppouts.FindAsync(FormIppout.FormIppoutId);

                if (dbFormIppout == null)
                {
                    return (false, "FormIppout could not be found");
                }

                _db.FormIppouts.Remove(FormIppout);
                await _db.SaveChangesAsync();

                return (true, "FormIppout got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppouts
    }
}
