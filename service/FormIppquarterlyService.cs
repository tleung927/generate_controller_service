using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppquarterlyService
    {
        // FormIppquarterlys Services
        Task<List<FormIppquarterly>> GetFormIppquarterlyListByValue(int offset, int limit, string val); // GET All FormIppquarterlyss
        Task<FormIppquarterly> GetFormIppquarterly(string FormIppquarterly_name); // GET Single FormIppquarterlys        
        Task<List<FormIppquarterly>> GetFormIppquarterlyList(string FormIppquarterly_name); // GET List FormIppquarterlys        
        Task<FormIppquarterly> AddFormIppquarterly(FormIppquarterly FormIppquarterly); // POST New FormIppquarterlys
        Task<FormIppquarterly> UpdateFormIppquarterly(FormIppquarterly FormIppquarterly); // PUT FormIppquarterlys
        Task<(bool, string)> DeleteFormIppquarterly(FormIppquarterly FormIppquarterly); // DELETE FormIppquarterlys
    }

    public class FormIppquarterlyService : IFormIppquarterlyService
    {
        private readonly XixsrvContext _db;

        public FormIppquarterlyService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppquarterlys

        public async Task<List<FormIppquarterly>> GetFormIppquarterlyListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppquarterlys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppquarterly> GetFormIppquarterly(string FormIppquarterly_id)
        {
            try
            {
                return await _db.FormIppquarterlys.FindAsync(FormIppquarterly_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppquarterly>> GetFormIppquarterlyList(string FormIppquarterly_id)
        {
            try
            {
                return await _db.FormIppquarterlys
                    .Where(i => i.FormIppquarterlyId == FormIppquarterly_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppquarterly> AddFormIppquarterly(FormIppquarterly FormIppquarterly)
        {
            try
            {
                await _db.FormIppquarterlys.AddAsync(FormIppquarterly);
                await _db.SaveChangesAsync();
                return await _db.FormIppquarterlys.FindAsync(FormIppquarterly.FormIppquarterlyId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppquarterly> UpdateFormIppquarterly(FormIppquarterly FormIppquarterly)
        {
            try
            {
                _db.Entry(FormIppquarterly).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppquarterly;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppquarterly(FormIppquarterly FormIppquarterly)
        {
            try
            {
                var dbFormIppquarterly = await _db.FormIppquarterlys.FindAsync(FormIppquarterly.FormIppquarterlyId);

                if (dbFormIppquarterly == null)
                {
                    return (false, "FormIppquarterly could not be found");
                }

                _db.FormIppquarterlys.Remove(FormIppquarterly);
                await _db.SaveChangesAsync();

                return (true, "FormIppquarterly got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppquarterlys
    }
}
