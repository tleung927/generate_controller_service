using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspproutcomeService
    {
        // FormIfspproutcomes Services
        Task<List<FormIfspproutcome>> GetFormIfspproutcomeListByValue(int offset, int limit, string val); // GET All FormIfspproutcomess
        Task<FormIfspproutcome> GetFormIfspproutcome(string FormIfspproutcome_name); // GET Single FormIfspproutcomes        
        Task<List<FormIfspproutcome>> GetFormIfspproutcomeList(string FormIfspproutcome_name); // GET List FormIfspproutcomes        
        Task<FormIfspproutcome> AddFormIfspproutcome(FormIfspproutcome FormIfspproutcome); // POST New FormIfspproutcomes
        Task<FormIfspproutcome> UpdateFormIfspproutcome(FormIfspproutcome FormIfspproutcome); // PUT FormIfspproutcomes
        Task<(bool, string)> DeleteFormIfspproutcome(FormIfspproutcome FormIfspproutcome); // DELETE FormIfspproutcomes
    }

    public class FormIfspproutcomeService : IFormIfspproutcomeService
    {
        private readonly XixsrvContext _db;

        public FormIfspproutcomeService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspproutcomes

        public async Task<List<FormIfspproutcome>> GetFormIfspproutcomeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspproutcomes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfspproutcome> GetFormIfspproutcome(string FormIfspproutcome_id)
        {
            try
            {
                return await _db.FormIfspproutcomes.FindAsync(FormIfspproutcome_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfspproutcome>> GetFormIfspproutcomeList(string FormIfspproutcome_id)
        {
            try
            {
                return await _db.FormIfspproutcomes
                    .Where(i => i.FormIfspproutcomeId == FormIfspproutcome_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfspproutcome> AddFormIfspproutcome(FormIfspproutcome FormIfspproutcome)
        {
            try
            {
                await _db.FormIfspproutcomes.AddAsync(FormIfspproutcome);
                await _db.SaveChangesAsync();
                return await _db.FormIfspproutcomes.FindAsync(FormIfspproutcome.FormIfspproutcomeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfspproutcome> UpdateFormIfspproutcome(FormIfspproutcome FormIfspproutcome)
        {
            try
            {
                _db.Entry(FormIfspproutcome).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfspproutcome;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfspproutcome(FormIfspproutcome FormIfspproutcome)
        {
            try
            {
                var dbFormIfspproutcome = await _db.FormIfspproutcomes.FindAsync(FormIfspproutcome.FormIfspproutcomeId);

                if (dbFormIfspproutcome == null)
                {
                    return (false, "FormIfspproutcome could not be found");
                }

                _db.FormIfspproutcomes.Remove(FormIfspproutcome);
                await _db.SaveChangesAsync();

                return (true, "FormIfspproutcome got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspproutcomes
    }
}
