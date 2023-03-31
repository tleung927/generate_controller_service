using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppannualFdlrcBakService
    {
        // FormIppannualFdlrcBaks Services
        Task<List<FormIppannualFdlrcBak>> GetFormIppannualFdlrcBakListByValue(int offset, int limit, string val); // GET All FormIppannualFdlrcBakss
        Task<FormIppannualFdlrcBak> GetFormIppannualFdlrcBak(string FormIppannualFdlrcBak_name); // GET Single FormIppannualFdlrcBaks        
        Task<List<FormIppannualFdlrcBak>> GetFormIppannualFdlrcBakList(string FormIppannualFdlrcBak_name); // GET List FormIppannualFdlrcBaks        
        Task<FormIppannualFdlrcBak> AddFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak); // POST New FormIppannualFdlrcBaks
        Task<FormIppannualFdlrcBak> UpdateFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak); // PUT FormIppannualFdlrcBaks
        Task<(bool, string)> DeleteFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak); // DELETE FormIppannualFdlrcBaks
    }

    public class FormIppannualFdlrcBakService : IFormIppannualFdlrcBakService
    {
        private readonly XixsrvContext _db;

        public FormIppannualFdlrcBakService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppannualFdlrcBaks

        public async Task<List<FormIppannualFdlrcBak>> GetFormIppannualFdlrcBakListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppannualFdlrcBaks.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppannualFdlrcBak> GetFormIppannualFdlrcBak(string FormIppannualFdlrcBak_id)
        {
            try
            {
                return await _db.FormIppannualFdlrcBaks.FindAsync(FormIppannualFdlrcBak_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppannualFdlrcBak>> GetFormIppannualFdlrcBakList(string FormIppannualFdlrcBak_id)
        {
            try
            {
                return await _db.FormIppannualFdlrcBaks
                    .Where(i => i.FormIppannualFdlrcBakId == FormIppannualFdlrcBak_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppannualFdlrcBak> AddFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak)
        {
            try
            {
                await _db.FormIppannualFdlrcBaks.AddAsync(FormIppannualFdlrcBak);
                await _db.SaveChangesAsync();
                return await _db.FormIppannualFdlrcBaks.FindAsync(FormIppannualFdlrcBak.FormIppannualFdlrcBakId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppannualFdlrcBak> UpdateFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak)
        {
            try
            {
                _db.Entry(FormIppannualFdlrcBak).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppannualFdlrcBak;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppannualFdlrcBak(FormIppannualFdlrcBak FormIppannualFdlrcBak)
        {
            try
            {
                var dbFormIppannualFdlrcBak = await _db.FormIppannualFdlrcBaks.FindAsync(FormIppannualFdlrcBak.FormIppannualFdlrcBakId);

                if (dbFormIppannualFdlrcBak == null)
                {
                    return (false, "FormIppannualFdlrcBak could not be found");
                }

                _db.FormIppannualFdlrcBaks.Remove(FormIppannualFdlrcBak);
                await _db.SaveChangesAsync();

                return (true, "FormIppannualFdlrcBak got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppannualFdlrcBaks
    }
}
