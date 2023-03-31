using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspprService
    {
        // FormIfspprs Services
        Task<List<FormIfsppr>> GetFormIfspprListByValue(int offset, int limit, string val); // GET All FormIfspprss
        Task<FormIfsppr> GetFormIfsppr(string FormIfsppr_name); // GET Single FormIfspprs        
        Task<List<FormIfsppr>> GetFormIfspprList(string FormIfsppr_name); // GET List FormIfspprs        
        Task<FormIfsppr> AddFormIfsppr(FormIfsppr FormIfsppr); // POST New FormIfspprs
        Task<FormIfsppr> UpdateFormIfsppr(FormIfsppr FormIfsppr); // PUT FormIfspprs
        Task<(bool, string)> DeleteFormIfsppr(FormIfsppr FormIfsppr); // DELETE FormIfspprs
    }

    public class FormIfspprService : IFormIfspprService
    {
        private readonly XixsrvContext _db;

        public FormIfspprService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspprs

        public async Task<List<FormIfsppr>> GetFormIfspprListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspprs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfsppr> GetFormIfsppr(string FormIfsppr_id)
        {
            try
            {
                return await _db.FormIfspprs.FindAsync(FormIfsppr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfsppr>> GetFormIfspprList(string FormIfsppr_id)
        {
            try
            {
                return await _db.FormIfspprs
                    .Where(i => i.FormIfspprId == FormIfsppr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfsppr> AddFormIfsppr(FormIfsppr FormIfsppr)
        {
            try
            {
                await _db.FormIfspprs.AddAsync(FormIfsppr);
                await _db.SaveChangesAsync();
                return await _db.FormIfspprs.FindAsync(FormIfsppr.FormIfspprId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfsppr> UpdateFormIfsppr(FormIfsppr FormIfsppr)
        {
            try
            {
                _db.Entry(FormIfsppr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfsppr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfsppr(FormIfsppr FormIfsppr)
        {
            try
            {
                var dbFormIfsppr = await _db.FormIfspprs.FindAsync(FormIfsppr.FormIfspprId);

                if (dbFormIfsppr == null)
                {
                    return (false, "FormIfsppr could not be found");
                }

                _db.FormIfspprs.Remove(FormIfsppr);
                await _db.SaveChangesAsync();

                return (true, "FormIfsppr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspprs
    }
}
