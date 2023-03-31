using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppfdlrcService
    {
        // FormIppfdlrcs Services
        Task<List<FormIppfdlrc>> GetFormIppfdlrcListByValue(int offset, int limit, string val); // GET All FormIppfdlrcss
        Task<FormIppfdlrc> GetFormIppfdlrc(string FormIppfdlrc_name); // GET Single FormIppfdlrcs        
        Task<List<FormIppfdlrc>> GetFormIppfdlrcList(string FormIppfdlrc_name); // GET List FormIppfdlrcs        
        Task<FormIppfdlrc> AddFormIppfdlrc(FormIppfdlrc FormIppfdlrc); // POST New FormIppfdlrcs
        Task<FormIppfdlrc> UpdateFormIppfdlrc(FormIppfdlrc FormIppfdlrc); // PUT FormIppfdlrcs
        Task<(bool, string)> DeleteFormIppfdlrc(FormIppfdlrc FormIppfdlrc); // DELETE FormIppfdlrcs
    }

    public class FormIppfdlrcService : IFormIppfdlrcService
    {
        private readonly XixsrvContext _db;

        public FormIppfdlrcService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppfdlrcs

        public async Task<List<FormIppfdlrc>> GetFormIppfdlrcListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppfdlrcs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppfdlrc> GetFormIppfdlrc(string FormIppfdlrc_id)
        {
            try
            {
                return await _db.FormIppfdlrcs.FindAsync(FormIppfdlrc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppfdlrc>> GetFormIppfdlrcList(string FormIppfdlrc_id)
        {
            try
            {
                return await _db.FormIppfdlrcs
                    .Where(i => i.FormIppfdlrcId == FormIppfdlrc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppfdlrc> AddFormIppfdlrc(FormIppfdlrc FormIppfdlrc)
        {
            try
            {
                await _db.FormIppfdlrcs.AddAsync(FormIppfdlrc);
                await _db.SaveChangesAsync();
                return await _db.FormIppfdlrcs.FindAsync(FormIppfdlrc.FormIppfdlrcId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppfdlrc> UpdateFormIppfdlrc(FormIppfdlrc FormIppfdlrc)
        {
            try
            {
                _db.Entry(FormIppfdlrc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppfdlrc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppfdlrc(FormIppfdlrc FormIppfdlrc)
        {
            try
            {
                var dbFormIppfdlrc = await _db.FormIppfdlrcs.FindAsync(FormIppfdlrc.FormIppfdlrcId);

                if (dbFormIppfdlrc == null)
                {
                    return (false, "FormIppfdlrc could not be found");
                }

                _db.FormIppfdlrcs.Remove(FormIppfdlrc);
                await _db.SaveChangesAsync();

                return (true, "FormIppfdlrc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppfdlrcs
    }
}
