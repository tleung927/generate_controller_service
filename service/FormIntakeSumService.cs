using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIntakeSumService
    {
        // FormIntakeSums Services
        Task<List<FormIntakeSum>> GetFormIntakeSumListByValue(int offset, int limit, string val); // GET All FormIntakeSumss
        Task<FormIntakeSum> GetFormIntakeSum(string FormIntakeSum_name); // GET Single FormIntakeSums        
        Task<List<FormIntakeSum>> GetFormIntakeSumList(string FormIntakeSum_name); // GET List FormIntakeSums        
        Task<FormIntakeSum> AddFormIntakeSum(FormIntakeSum FormIntakeSum); // POST New FormIntakeSums
        Task<FormIntakeSum> UpdateFormIntakeSum(FormIntakeSum FormIntakeSum); // PUT FormIntakeSums
        Task<(bool, string)> DeleteFormIntakeSum(FormIntakeSum FormIntakeSum); // DELETE FormIntakeSums
    }

    public class FormIntakeSumService : IFormIntakeSumService
    {
        private readonly XixsrvContext _db;

        public FormIntakeSumService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIntakeSums

        public async Task<List<FormIntakeSum>> GetFormIntakeSumListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIntakeSums.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIntakeSum> GetFormIntakeSum(string FormIntakeSum_id)
        {
            try
            {
                return await _db.FormIntakeSums.FindAsync(FormIntakeSum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIntakeSum>> GetFormIntakeSumList(string FormIntakeSum_id)
        {
            try
            {
                return await _db.FormIntakeSums
                    .Where(i => i.FormIntakeSumId == FormIntakeSum_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIntakeSum> AddFormIntakeSum(FormIntakeSum FormIntakeSum)
        {
            try
            {
                await _db.FormIntakeSums.AddAsync(FormIntakeSum);
                await _db.SaveChangesAsync();
                return await _db.FormIntakeSums.FindAsync(FormIntakeSum.FormIntakeSumId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIntakeSum> UpdateFormIntakeSum(FormIntakeSum FormIntakeSum)
        {
            try
            {
                _db.Entry(FormIntakeSum).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIntakeSum;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIntakeSum(FormIntakeSum FormIntakeSum)
        {
            try
            {
                var dbFormIntakeSum = await _db.FormIntakeSums.FindAsync(FormIntakeSum.FormIntakeSumId);

                if (dbFormIntakeSum == null)
                {
                    return (false, "FormIntakeSum could not be found");
                }

                _db.FormIntakeSums.Remove(FormIntakeSum);
                await _db.SaveChangesAsync();

                return (true, "FormIntakeSum got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIntakeSums
    }
}
