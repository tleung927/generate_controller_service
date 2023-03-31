using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtquestionService
    {
        // FormMtquestions Services
        Task<List<FormMtquestion>> GetFormMtquestionListByValue(int offset, int limit, string val); // GET All FormMtquestionss
        Task<FormMtquestion> GetFormMtquestion(string FormMtquestion_name); // GET Single FormMtquestions        
        Task<List<FormMtquestion>> GetFormMtquestionList(string FormMtquestion_name); // GET List FormMtquestions        
        Task<FormMtquestion> AddFormMtquestion(FormMtquestion FormMtquestion); // POST New FormMtquestions
        Task<FormMtquestion> UpdateFormMtquestion(FormMtquestion FormMtquestion); // PUT FormMtquestions
        Task<(bool, string)> DeleteFormMtquestion(FormMtquestion FormMtquestion); // DELETE FormMtquestions
    }

    public class FormMtquestionService : IFormMtquestionService
    {
        private readonly XixsrvContext _db;

        public FormMtquestionService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtquestions

        public async Task<List<FormMtquestion>> GetFormMtquestionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtquestions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtquestion> GetFormMtquestion(string FormMtquestion_id)
        {
            try
            {
                return await _db.FormMtquestions.FindAsync(FormMtquestion_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtquestion>> GetFormMtquestionList(string FormMtquestion_id)
        {
            try
            {
                return await _db.FormMtquestions
                    .Where(i => i.FormMtquestionId == FormMtquestion_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtquestion> AddFormMtquestion(FormMtquestion FormMtquestion)
        {
            try
            {
                await _db.FormMtquestions.AddAsync(FormMtquestion);
                await _db.SaveChangesAsync();
                return await _db.FormMtquestions.FindAsync(FormMtquestion.FormMtquestionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtquestion> UpdateFormMtquestion(FormMtquestion FormMtquestion)
        {
            try
            {
                _db.Entry(FormMtquestion).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtquestion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtquestion(FormMtquestion FormMtquestion)
        {
            try
            {
                var dbFormMtquestion = await _db.FormMtquestions.FindAsync(FormMtquestion.FormMtquestionId);

                if (dbFormMtquestion == null)
                {
                    return (false, "FormMtquestion could not be found");
                }

                _db.FormMtquestions.Remove(FormMtquestion);
                await _db.SaveChangesAsync();

                return (true, "FormMtquestion got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtquestions
    }
}
