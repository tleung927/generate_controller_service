using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormEsrTrfService
    {
        // FormEsrTrfs Services
        Task<List<FormEsrTrf>> GetFormEsrTrfListByValue(int offset, int limit, string val); // GET All FormEsrTrfss
        Task<FormEsrTrf> GetFormEsrTrf(string FormEsrTrf_name); // GET Single FormEsrTrfs        
        Task<List<FormEsrTrf>> GetFormEsrTrfList(string FormEsrTrf_name); // GET List FormEsrTrfs        
        Task<FormEsrTrf> AddFormEsrTrf(FormEsrTrf FormEsrTrf); // POST New FormEsrTrfs
        Task<FormEsrTrf> UpdateFormEsrTrf(FormEsrTrf FormEsrTrf); // PUT FormEsrTrfs
        Task<(bool, string)> DeleteFormEsrTrf(FormEsrTrf FormEsrTrf); // DELETE FormEsrTrfs
    }

    public class FormEsrTrfService : IFormEsrTrfService
    {
        private readonly XixsrvContext _db;

        public FormEsrTrfService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormEsrTrfs

        public async Task<List<FormEsrTrf>> GetFormEsrTrfListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormEsrTrfs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormEsrTrf> GetFormEsrTrf(string FormEsrTrf_id)
        {
            try
            {
                return await _db.FormEsrTrfs.FindAsync(FormEsrTrf_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormEsrTrf>> GetFormEsrTrfList(string FormEsrTrf_id)
        {
            try
            {
                return await _db.FormEsrTrfs
                    .Where(i => i.FormEsrTrfId == FormEsrTrf_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormEsrTrf> AddFormEsrTrf(FormEsrTrf FormEsrTrf)
        {
            try
            {
                await _db.FormEsrTrfs.AddAsync(FormEsrTrf);
                await _db.SaveChangesAsync();
                return await _db.FormEsrTrfs.FindAsync(FormEsrTrf.FormEsrTrfId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormEsrTrf> UpdateFormEsrTrf(FormEsrTrf FormEsrTrf)
        {
            try
            {
                _db.Entry(FormEsrTrf).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormEsrTrf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormEsrTrf(FormEsrTrf FormEsrTrf)
        {
            try
            {
                var dbFormEsrTrf = await _db.FormEsrTrfs.FindAsync(FormEsrTrf.FormEsrTrfId);

                if (dbFormEsrTrf == null)
                {
                    return (false, "FormEsrTrf could not be found");
                }

                _db.FormEsrTrfs.Remove(FormEsrTrf);
                await _db.SaveChangesAsync();

                return (true, "FormEsrTrf got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormEsrTrfs
    }
}
