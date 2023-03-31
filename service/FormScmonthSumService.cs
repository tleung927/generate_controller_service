using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormScmonthSumService
    {
        // FormScmonthSums Services
        Task<List<FormScmonthSum>> GetFormScmonthSumListByValue(int offset, int limit, string val); // GET All FormScmonthSumss
        Task<FormScmonthSum> GetFormScmonthSum(string FormScmonthSum_name); // GET Single FormScmonthSums        
        Task<List<FormScmonthSum>> GetFormScmonthSumList(string FormScmonthSum_name); // GET List FormScmonthSums        
        Task<FormScmonthSum> AddFormScmonthSum(FormScmonthSum FormScmonthSum); // POST New FormScmonthSums
        Task<FormScmonthSum> UpdateFormScmonthSum(FormScmonthSum FormScmonthSum); // PUT FormScmonthSums
        Task<(bool, string)> DeleteFormScmonthSum(FormScmonthSum FormScmonthSum); // DELETE FormScmonthSums
    }

    public class FormScmonthSumService : IFormScmonthSumService
    {
        private readonly XixsrvContext _db;

        public FormScmonthSumService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormScmonthSums

        public async Task<List<FormScmonthSum>> GetFormScmonthSumListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormScmonthSums.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormScmonthSum> GetFormScmonthSum(string FormScmonthSum_id)
        {
            try
            {
                return await _db.FormScmonthSums.FindAsync(FormScmonthSum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormScmonthSum>> GetFormScmonthSumList(string FormScmonthSum_id)
        {
            try
            {
                return await _db.FormScmonthSums
                    .Where(i => i.FormScmonthSumId == FormScmonthSum_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormScmonthSum> AddFormScmonthSum(FormScmonthSum FormScmonthSum)
        {
            try
            {
                await _db.FormScmonthSums.AddAsync(FormScmonthSum);
                await _db.SaveChangesAsync();
                return await _db.FormScmonthSums.FindAsync(FormScmonthSum.FormScmonthSumId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormScmonthSum> UpdateFormScmonthSum(FormScmonthSum FormScmonthSum)
        {
            try
            {
                _db.Entry(FormScmonthSum).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormScmonthSum;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormScmonthSum(FormScmonthSum FormScmonthSum)
        {
            try
            {
                var dbFormScmonthSum = await _db.FormScmonthSums.FindAsync(FormScmonthSum.FormScmonthSumId);

                if (dbFormScmonthSum == null)
                {
                    return (false, "FormScmonthSum could not be found");
                }

                _db.FormScmonthSums.Remove(FormScmonthSum);
                await _db.SaveChangesAsync();

                return (true, "FormScmonthSum got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormScmonthSums
    }
}
