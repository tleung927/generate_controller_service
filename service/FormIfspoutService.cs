using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspoutService
    {
        // FormIfspouts Services
        Task<List<FormIfspout>> GetFormIfspoutListByValue(int offset, int limit, string val); // GET All FormIfspoutss
        Task<FormIfspout> GetFormIfspout(string FormIfspout_name); // GET Single FormIfspouts        
        Task<List<FormIfspout>> GetFormIfspoutList(string FormIfspout_name); // GET List FormIfspouts        
        Task<FormIfspout> AddFormIfspout(FormIfspout FormIfspout); // POST New FormIfspouts
        Task<FormIfspout> UpdateFormIfspout(FormIfspout FormIfspout); // PUT FormIfspouts
        Task<(bool, string)> DeleteFormIfspout(FormIfspout FormIfspout); // DELETE FormIfspouts
    }

    public class FormIfspoutService : IFormIfspoutService
    {
        private readonly XixsrvContext _db;

        public FormIfspoutService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspouts

        public async Task<List<FormIfspout>> GetFormIfspoutListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspouts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfspout> GetFormIfspout(string FormIfspout_id)
        {
            try
            {
                return await _db.FormIfspouts.FindAsync(FormIfspout_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfspout>> GetFormIfspoutList(string FormIfspout_id)
        {
            try
            {
                return await _db.FormIfspouts
                    .Where(i => i.FormIfspoutId == FormIfspout_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfspout> AddFormIfspout(FormIfspout FormIfspout)
        {
            try
            {
                await _db.FormIfspouts.AddAsync(FormIfspout);
                await _db.SaveChangesAsync();
                return await _db.FormIfspouts.FindAsync(FormIfspout.FormIfspoutId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfspout> UpdateFormIfspout(FormIfspout FormIfspout)
        {
            try
            {
                _db.Entry(FormIfspout).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfspout;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfspout(FormIfspout FormIfspout)
        {
            try
            {
                var dbFormIfspout = await _db.FormIfspouts.FindAsync(FormIfspout.FormIfspoutId);

                if (dbFormIfspout == null)
                {
                    return (false, "FormIfspout could not be found");
                }

                _db.FormIfspouts.Remove(FormIfspout);
                await _db.SaveChangesAsync();

                return (true, "FormIfspout got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspouts
    }
}
