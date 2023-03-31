using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIrctService
    {
        // FormIrcts Services
        Task<List<FormIrct>> GetFormIrctListByValue(int offset, int limit, string val); // GET All FormIrctss
        Task<FormIrct> GetFormIrct(string FormIrct_name); // GET Single FormIrcts        
        Task<List<FormIrct>> GetFormIrctList(string FormIrct_name); // GET List FormIrcts        
        Task<FormIrct> AddFormIrct(FormIrct FormIrct); // POST New FormIrcts
        Task<FormIrct> UpdateFormIrct(FormIrct FormIrct); // PUT FormIrcts
        Task<(bool, string)> DeleteFormIrct(FormIrct FormIrct); // DELETE FormIrcts
    }

    public class FormIrctService : IFormIrctService
    {
        private readonly XixsrvContext _db;

        public FormIrctService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIrcts

        public async Task<List<FormIrct>> GetFormIrctListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIrcts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIrct> GetFormIrct(string FormIrct_id)
        {
            try
            {
                return await _db.FormIrcts.FindAsync(FormIrct_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIrct>> GetFormIrctList(string FormIrct_id)
        {
            try
            {
                return await _db.FormIrcts
                    .Where(i => i.FormIrctId == FormIrct_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIrct> AddFormIrct(FormIrct FormIrct)
        {
            try
            {
                await _db.FormIrcts.AddAsync(FormIrct);
                await _db.SaveChangesAsync();
                return await _db.FormIrcts.FindAsync(FormIrct.FormIrctId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIrct> UpdateFormIrct(FormIrct FormIrct)
        {
            try
            {
                _db.Entry(FormIrct).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIrct;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIrct(FormIrct FormIrct)
        {
            try
            {
                var dbFormIrct = await _db.FormIrcts.FindAsync(FormIrct.FormIrctId);

                if (dbFormIrct == null)
                {
                    return (false, "FormIrct could not be found");
                }

                _db.FormIrcts.Remove(FormIrct);
                await _db.SaveChangesAsync();

                return (true, "FormIrct got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIrcts
    }
}
