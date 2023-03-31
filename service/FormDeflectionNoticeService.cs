using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormDeflectionNoticeService
    {
        // FormDeflectionNotices Services
        Task<List<FormDeflectionNotice>> GetFormDeflectionNoticeListByValue(int offset, int limit, string val); // GET All FormDeflectionNoticess
        Task<FormDeflectionNotice> GetFormDeflectionNotice(string FormDeflectionNotice_name); // GET Single FormDeflectionNotices        
        Task<List<FormDeflectionNotice>> GetFormDeflectionNoticeList(string FormDeflectionNotice_name); // GET List FormDeflectionNotices        
        Task<FormDeflectionNotice> AddFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice); // POST New FormDeflectionNotices
        Task<FormDeflectionNotice> UpdateFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice); // PUT FormDeflectionNotices
        Task<(bool, string)> DeleteFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice); // DELETE FormDeflectionNotices
    }

    public class FormDeflectionNoticeService : IFormDeflectionNoticeService
    {
        private readonly XixsrvContext _db;

        public FormDeflectionNoticeService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormDeflectionNotices

        public async Task<List<FormDeflectionNotice>> GetFormDeflectionNoticeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormDeflectionNotices.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormDeflectionNotice> GetFormDeflectionNotice(string FormDeflectionNotice_id)
        {
            try
            {
                return await _db.FormDeflectionNotices.FindAsync(FormDeflectionNotice_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormDeflectionNotice>> GetFormDeflectionNoticeList(string FormDeflectionNotice_id)
        {
            try
            {
                return await _db.FormDeflectionNotices
                    .Where(i => i.FormDeflectionNoticeId == FormDeflectionNotice_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormDeflectionNotice> AddFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice)
        {
            try
            {
                await _db.FormDeflectionNotices.AddAsync(FormDeflectionNotice);
                await _db.SaveChangesAsync();
                return await _db.FormDeflectionNotices.FindAsync(FormDeflectionNotice.FormDeflectionNoticeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormDeflectionNotice> UpdateFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice)
        {
            try
            {
                _db.Entry(FormDeflectionNotice).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormDeflectionNotice;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormDeflectionNotice(FormDeflectionNotice FormDeflectionNotice)
        {
            try
            {
                var dbFormDeflectionNotice = await _db.FormDeflectionNotices.FindAsync(FormDeflectionNotice.FormDeflectionNoticeId);

                if (dbFormDeflectionNotice == null)
                {
                    return (false, "FormDeflectionNotice could not be found");
                }

                _db.FormDeflectionNotices.Remove(FormDeflectionNotice);
                await _db.SaveChangesAsync();

                return (true, "FormDeflectionNotice got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormDeflectionNotices
    }
}
