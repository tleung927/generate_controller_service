using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormScmontholdService
    {
        // FormScmontholds Services
        Task<List<FormScmonthold>> GetFormScmontholdListByValue(int offset, int limit, string val); // GET All FormScmontholdss
        Task<FormScmonthold> GetFormScmonthold(string FormScmonthold_name); // GET Single FormScmontholds        
        Task<List<FormScmonthold>> GetFormScmontholdList(string FormScmonthold_name); // GET List FormScmontholds        
        Task<FormScmonthold> AddFormScmonthold(FormScmonthold FormScmonthold); // POST New FormScmontholds
        Task<FormScmonthold> UpdateFormScmonthold(FormScmonthold FormScmonthold); // PUT FormScmontholds
        Task<(bool, string)> DeleteFormScmonthold(FormScmonthold FormScmonthold); // DELETE FormScmontholds
    }

    public class FormScmontholdService : IFormScmontholdService
    {
        private readonly XixsrvContext _db;

        public FormScmontholdService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormScmontholds

        public async Task<List<FormScmonthold>> GetFormScmontholdListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormScmontholds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormScmonthold> GetFormScmonthold(string FormScmonthold_id)
        {
            try
            {
                return await _db.FormScmontholds.FindAsync(FormScmonthold_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormScmonthold>> GetFormScmontholdList(string FormScmonthold_id)
        {
            try
            {
                return await _db.FormScmontholds
                    .Where(i => i.FormScmontholdId == FormScmonthold_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormScmonthold> AddFormScmonthold(FormScmonthold FormScmonthold)
        {
            try
            {
                await _db.FormScmontholds.AddAsync(FormScmonthold);
                await _db.SaveChangesAsync();
                return await _db.FormScmontholds.FindAsync(FormScmonthold.FormScmontholdId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormScmonthold> UpdateFormScmonthold(FormScmonthold FormScmonthold)
        {
            try
            {
                _db.Entry(FormScmonthold).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormScmonthold;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormScmonthold(FormScmonthold FormScmonthold)
        {
            try
            {
                var dbFormScmonthold = await _db.FormScmontholds.FindAsync(FormScmonthold.FormScmontholdId);

                if (dbFormScmonthold == null)
                {
                    return (false, "FormScmonthold could not be found");
                }

                _db.FormScmontholds.Remove(FormScmonthold);
                await _db.SaveChangesAsync();

                return (true, "FormScmonthold got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormScmontholds
    }
}
