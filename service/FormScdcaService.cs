using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormScdcaService
    {
        // FormScdcas Services
        Task<List<FormScdca>> GetFormScdcaListByValue(int offset, int limit, string val); // GET All FormScdcass
        Task<FormScdca> GetFormScdca(string FormScdca_name); // GET Single FormScdcas        
        Task<List<FormScdca>> GetFormScdcaList(string FormScdca_name); // GET List FormScdcas        
        Task<FormScdca> AddFormScdca(FormScdca FormScdca); // POST New FormScdcas
        Task<FormScdca> UpdateFormScdca(FormScdca FormScdca); // PUT FormScdcas
        Task<(bool, string)> DeleteFormScdca(FormScdca FormScdca); // DELETE FormScdcas
    }

    public class FormScdcaService : IFormScdcaService
    {
        private readonly XixsrvContext _db;

        public FormScdcaService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormScdcas

        public async Task<List<FormScdca>> GetFormScdcaListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormScdcas.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormScdca> GetFormScdca(string FormScdca_id)
        {
            try
            {
                return await _db.FormScdcas.FindAsync(FormScdca_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormScdca>> GetFormScdcaList(string FormScdca_id)
        {
            try
            {
                return await _db.FormScdcas
                    .Where(i => i.FormScdcaId == FormScdca_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormScdca> AddFormScdca(FormScdca FormScdca)
        {
            try
            {
                await _db.FormScdcas.AddAsync(FormScdca);
                await _db.SaveChangesAsync();
                return await _db.FormScdcas.FindAsync(FormScdca.FormScdcaId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormScdca> UpdateFormScdca(FormScdca FormScdca)
        {
            try
            {
                _db.Entry(FormScdca).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormScdca;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormScdca(FormScdca FormScdca)
        {
            try
            {
                var dbFormScdca = await _db.FormScdcas.FindAsync(FormScdca.FormScdcaId);

                if (dbFormScdca == null)
                {
                    return (false, "FormScdca could not be found");
                }

                _db.FormScdcas.Remove(FormScdca);
                await _db.SaveChangesAsync();

                return (true, "FormScdca got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormScdcas
    }
}
