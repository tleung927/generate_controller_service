using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppplanTitleService
    {
        // FormIppplanTitles Services
        Task<List<FormIppplanTitle>> GetFormIppplanTitleListByValue(int offset, int limit, string val); // GET All FormIppplanTitless
        Task<FormIppplanTitle> GetFormIppplanTitle(string FormIppplanTitle_name); // GET Single FormIppplanTitles        
        Task<List<FormIppplanTitle>> GetFormIppplanTitleList(string FormIppplanTitle_name); // GET List FormIppplanTitles        
        Task<FormIppplanTitle> AddFormIppplanTitle(FormIppplanTitle FormIppplanTitle); // POST New FormIppplanTitles
        Task<FormIppplanTitle> UpdateFormIppplanTitle(FormIppplanTitle FormIppplanTitle); // PUT FormIppplanTitles
        Task<(bool, string)> DeleteFormIppplanTitle(FormIppplanTitle FormIppplanTitle); // DELETE FormIppplanTitles
    }

    public class FormIppplanTitleService : IFormIppplanTitleService
    {
        private readonly XixsrvContext _db;

        public FormIppplanTitleService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppplanTitles

        public async Task<List<FormIppplanTitle>> GetFormIppplanTitleListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppplanTitles.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppplanTitle> GetFormIppplanTitle(string FormIppplanTitle_id)
        {
            try
            {
                return await _db.FormIppplanTitles.FindAsync(FormIppplanTitle_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppplanTitle>> GetFormIppplanTitleList(string FormIppplanTitle_id)
        {
            try
            {
                return await _db.FormIppplanTitles
                    .Where(i => i.FormIppplanTitleId == FormIppplanTitle_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppplanTitle> AddFormIppplanTitle(FormIppplanTitle FormIppplanTitle)
        {
            try
            {
                await _db.FormIppplanTitles.AddAsync(FormIppplanTitle);
                await _db.SaveChangesAsync();
                return await _db.FormIppplanTitles.FindAsync(FormIppplanTitle.FormIppplanTitleId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppplanTitle> UpdateFormIppplanTitle(FormIppplanTitle FormIppplanTitle)
        {
            try
            {
                _db.Entry(FormIppplanTitle).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppplanTitle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppplanTitle(FormIppplanTitle FormIppplanTitle)
        {
            try
            {
                var dbFormIppplanTitle = await _db.FormIppplanTitles.FindAsync(FormIppplanTitle.FormIppplanTitleId);

                if (dbFormIppplanTitle == null)
                {
                    return (false, "FormIppplanTitle could not be found");
                }

                _db.FormIppplanTitles.Remove(FormIppplanTitle);
                await _db.SaveChangesAsync();

                return (true, "FormIppplanTitle got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppplanTitles
    }
}
