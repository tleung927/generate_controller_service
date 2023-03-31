using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppannualFdlrcService
    {
        // FormIppannualFdlrcs Services
        Task<List<FormIppannualFdlrc>> GetFormIppannualFdlrcListByValue(int offset, int limit, string val); // GET All FormIppannualFdlrcss
        Task<FormIppannualFdlrc> GetFormIppannualFdlrc(string FormIppannualFdlrc_name); // GET Single FormIppannualFdlrcs        
        Task<List<FormIppannualFdlrc>> GetFormIppannualFdlrcList(string FormIppannualFdlrc_name); // GET List FormIppannualFdlrcs        
        Task<FormIppannualFdlrc> AddFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc); // POST New FormIppannualFdlrcs
        Task<FormIppannualFdlrc> UpdateFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc); // PUT FormIppannualFdlrcs
        Task<(bool, string)> DeleteFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc); // DELETE FormIppannualFdlrcs
    }

    public class FormIppannualFdlrcService : IFormIppannualFdlrcService
    {
        private readonly XixsrvContext _db;

        public FormIppannualFdlrcService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppannualFdlrcs

        public async Task<List<FormIppannualFdlrc>> GetFormIppannualFdlrcListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppannualFdlrcs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppannualFdlrc> GetFormIppannualFdlrc(string FormIppannualFdlrc_id)
        {
            try
            {
                return await _db.FormIppannualFdlrcs.FindAsync(FormIppannualFdlrc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppannualFdlrc>> GetFormIppannualFdlrcList(string FormIppannualFdlrc_id)
        {
            try
            {
                return await _db.FormIppannualFdlrcs
                    .Where(i => i.FormIppannualFdlrcId == FormIppannualFdlrc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppannualFdlrc> AddFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc)
        {
            try
            {
                await _db.FormIppannualFdlrcs.AddAsync(FormIppannualFdlrc);
                await _db.SaveChangesAsync();
                return await _db.FormIppannualFdlrcs.FindAsync(FormIppannualFdlrc.FormIppannualFdlrcId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppannualFdlrc> UpdateFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc)
        {
            try
            {
                _db.Entry(FormIppannualFdlrc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppannualFdlrc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppannualFdlrc(FormIppannualFdlrc FormIppannualFdlrc)
        {
            try
            {
                var dbFormIppannualFdlrc = await _db.FormIppannualFdlrcs.FindAsync(FormIppannualFdlrc.FormIppannualFdlrcId);

                if (dbFormIppannualFdlrc == null)
                {
                    return (false, "FormIppannualFdlrc could not be found");
                }

                _db.FormIppannualFdlrcs.Remove(FormIppannualFdlrc);
                await _db.SaveChangesAsync();

                return (true, "FormIppannualFdlrc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppannualFdlrcs
    }
}
