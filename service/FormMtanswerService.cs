using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtanswerService
    {
        // FormMtanswers Services
        Task<List<FormMtanswer>> GetFormMtanswerListByValue(int offset, int limit, string val); // GET All FormMtanswerss
        Task<FormMtanswer> GetFormMtanswer(string FormMtanswer_name); // GET Single FormMtanswers        
        Task<List<FormMtanswer>> GetFormMtanswerList(string FormMtanswer_name); // GET List FormMtanswers        
        Task<FormMtanswer> AddFormMtanswer(FormMtanswer FormMtanswer); // POST New FormMtanswers
        Task<FormMtanswer> UpdateFormMtanswer(FormMtanswer FormMtanswer); // PUT FormMtanswers
        Task<(bool, string)> DeleteFormMtanswer(FormMtanswer FormMtanswer); // DELETE FormMtanswers
    }

    public class FormMtanswerService : IFormMtanswerService
    {
        private readonly XixsrvContext _db;

        public FormMtanswerService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtanswers

        public async Task<List<FormMtanswer>> GetFormMtanswerListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtanswers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtanswer> GetFormMtanswer(string FormMtanswer_id)
        {
            try
            {
                return await _db.FormMtanswers.FindAsync(FormMtanswer_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtanswer>> GetFormMtanswerList(string FormMtanswer_id)
        {
            try
            {
                return await _db.FormMtanswers
                    .Where(i => i.FormMtanswerId == FormMtanswer_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtanswer> AddFormMtanswer(FormMtanswer FormMtanswer)
        {
            try
            {
                await _db.FormMtanswers.AddAsync(FormMtanswer);
                await _db.SaveChangesAsync();
                return await _db.FormMtanswers.FindAsync(FormMtanswer.FormMtanswerId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtanswer> UpdateFormMtanswer(FormMtanswer FormMtanswer)
        {
            try
            {
                _db.Entry(FormMtanswer).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtanswer;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtanswer(FormMtanswer FormMtanswer)
        {
            try
            {
                var dbFormMtanswer = await _db.FormMtanswers.FindAsync(FormMtanswer.FormMtanswerId);

                if (dbFormMtanswer == null)
                {
                    return (false, "FormMtanswer could not be found");
                }

                _db.FormMtanswers.Remove(FormMtanswer);
                await _db.SaveChangesAsync();

                return (true, "FormMtanswer got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtanswers
    }
}
