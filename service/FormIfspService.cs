using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspService
    {
        // FormIfsps Services
        Task<List<FormIfsp>> GetFormIfspListByValue(int offset, int limit, string val); // GET All FormIfspss
        Task<FormIfsp> GetFormIfsp(string FormIfsp_name); // GET Single FormIfsps        
        Task<List<FormIfsp>> GetFormIfspList(string FormIfsp_name); // GET List FormIfsps        
        Task<FormIfsp> AddFormIfsp(FormIfsp FormIfsp); // POST New FormIfsps
        Task<FormIfsp> UpdateFormIfsp(FormIfsp FormIfsp); // PUT FormIfsps
        Task<(bool, string)> DeleteFormIfsp(FormIfsp FormIfsp); // DELETE FormIfsps
    }

    public class FormIfspService : IFormIfspService
    {
        private readonly XixsrvContext _db;

        public FormIfspService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfsps

        public async Task<List<FormIfsp>> GetFormIfspListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfsps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfsp> GetFormIfsp(string FormIfsp_id)
        {
            try
            {
                return await _db.FormIfsps.FindAsync(FormIfsp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfsp>> GetFormIfspList(string FormIfsp_id)
        {
            try
            {
                return await _db.FormIfsps
                    .Where(i => i.FormIfspId == FormIfsp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfsp> AddFormIfsp(FormIfsp FormIfsp)
        {
            try
            {
                await _db.FormIfsps.AddAsync(FormIfsp);
                await _db.SaveChangesAsync();
                return await _db.FormIfsps.FindAsync(FormIfsp.FormIfspId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfsp> UpdateFormIfsp(FormIfsp FormIfsp)
        {
            try
            {
                _db.Entry(FormIfsp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfsp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfsp(FormIfsp FormIfsp)
        {
            try
            {
                var dbFormIfsp = await _db.FormIfsps.FindAsync(FormIfsp.FormIfspId);

                if (dbFormIfsp == null)
                {
                    return (false, "FormIfsp could not be found");
                }

                _db.FormIfsps.Remove(FormIfsp);
                await _db.SaveChangesAsync();

                return (true, "FormIfsp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfsps
    }
}
