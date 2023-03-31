using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDispOptionService
    {
        // DispOptions Services
        Task<List<DispOption>> GetDispOptionListByValue(int offset, int limit, string val); // GET All DispOptionss
        Task<DispOption> GetDispOption(string DispOption_name); // GET Single DispOptions        
        Task<List<DispOption>> GetDispOptionList(string DispOption_name); // GET List DispOptions        
        Task<DispOption> AddDispOption(DispOption DispOption); // POST New DispOptions
        Task<DispOption> UpdateDispOption(DispOption DispOption); // PUT DispOptions
        Task<(bool, string)> DeleteDispOption(DispOption DispOption); // DELETE DispOptions
    }

    public class DispOptionService : IDispOptionService
    {
        private readonly XixsrvContext _db;

        public DispOptionService(XixsrvContext db)
        {
            _db = db;
        }

        #region DispOptions

        public async Task<List<DispOption>> GetDispOptionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DispOptions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DispOption> GetDispOption(string DispOption_id)
        {
            try
            {
                return await _db.DispOptions.FindAsync(DispOption_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DispOption>> GetDispOptionList(string DispOption_id)
        {
            try
            {
                return await _db.DispOptions
                    .Where(i => i.DispOptionId == DispOption_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DispOption> AddDispOption(DispOption DispOption)
        {
            try
            {
                await _db.DispOptions.AddAsync(DispOption);
                await _db.SaveChangesAsync();
                return await _db.DispOptions.FindAsync(DispOption.DispOptionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DispOption> UpdateDispOption(DispOption DispOption)
        {
            try
            {
                _db.Entry(DispOption).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DispOption;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDispOption(DispOption DispOption)
        {
            try
            {
                var dbDispOption = await _db.DispOptions.FindAsync(DispOption.DispOptionId);

                if (dbDispOption == null)
                {
                    return (false, "DispOption could not be found");
                }

                _db.DispOptions.Remove(DispOption);
                await _db.SaveChangesAsync();

                return (true, "DispOption got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DispOptions
    }
}
