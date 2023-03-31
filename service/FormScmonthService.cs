using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormScmonthService
    {
        // FormScmonths Services
        Task<List<FormScmonth>> GetFormScmonthListByValue(int offset, int limit, string val); // GET All FormScmonthss
        Task<FormScmonth> GetFormScmonth(string FormScmonth_name); // GET Single FormScmonths        
        Task<List<FormScmonth>> GetFormScmonthList(string FormScmonth_name); // GET List FormScmonths        
        Task<FormScmonth> AddFormScmonth(FormScmonth FormScmonth); // POST New FormScmonths
        Task<FormScmonth> UpdateFormScmonth(FormScmonth FormScmonth); // PUT FormScmonths
        Task<(bool, string)> DeleteFormScmonth(FormScmonth FormScmonth); // DELETE FormScmonths
    }

    public class FormScmonthService : IFormScmonthService
    {
        private readonly XixsrvContext _db;

        public FormScmonthService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormScmonths

        public async Task<List<FormScmonth>> GetFormScmonthListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormScmonths.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormScmonth> GetFormScmonth(string FormScmonth_id)
        {
            try
            {
                return await _db.FormScmonths.FindAsync(FormScmonth_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormScmonth>> GetFormScmonthList(string FormScmonth_id)
        {
            try
            {
                return await _db.FormScmonths
                    .Where(i => i.FormScmonthId == FormScmonth_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormScmonth> AddFormScmonth(FormScmonth FormScmonth)
        {
            try
            {
                await _db.FormScmonths.AddAsync(FormScmonth);
                await _db.SaveChangesAsync();
                return await _db.FormScmonths.FindAsync(FormScmonth.FormScmonthId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormScmonth> UpdateFormScmonth(FormScmonth FormScmonth)
        {
            try
            {
                _db.Entry(FormScmonth).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormScmonth;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormScmonth(FormScmonth FormScmonth)
        {
            try
            {
                var dbFormScmonth = await _db.FormScmonths.FindAsync(FormScmonth.FormScmonthId);

                if (dbFormScmonth == null)
                {
                    return (false, "FormScmonth could not be found");
                }

                _db.FormScmonths.Remove(FormScmonth);
                await _db.SaveChangesAsync();

                return (true, "FormScmonth got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormScmonths
    }
}
