using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormVaService
    {
        // FormVas Services
        Task<List<FormVa>> GetFormVaListByValue(int offset, int limit, string val); // GET All FormVass
        Task<FormVa> GetFormVa(string FormVa_name); // GET Single FormVas        
        Task<List<FormVa>> GetFormVaList(string FormVa_name); // GET List FormVas        
        Task<FormVa> AddFormVa(FormVa FormVa); // POST New FormVas
        Task<FormVa> UpdateFormVa(FormVa FormVa); // PUT FormVas
        Task<(bool, string)> DeleteFormVa(FormVa FormVa); // DELETE FormVas
    }

    public class FormVaService : IFormVaService
    {
        private readonly XixsrvContext _db;

        public FormVaService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormVas

        public async Task<List<FormVa>> GetFormVaListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormVas.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormVa> GetFormVa(string FormVa_id)
        {
            try
            {
                return await _db.FormVas.FindAsync(FormVa_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormVa>> GetFormVaList(string FormVa_id)
        {
            try
            {
                return await _db.FormVas
                    .Where(i => i.FormVaId == FormVa_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormVa> AddFormVa(FormVa FormVa)
        {
            try
            {
                await _db.FormVas.AddAsync(FormVa);
                await _db.SaveChangesAsync();
                return await _db.FormVas.FindAsync(FormVa.FormVaId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormVa> UpdateFormVa(FormVa FormVa)
        {
            try
            {
                _db.Entry(FormVa).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormVa;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormVa(FormVa FormVa)
        {
            try
            {
                var dbFormVa = await _db.FormVas.FindAsync(FormVa.FormVaId);

                if (dbFormVa == null)
                {
                    return (false, "FormVa could not be found");
                }

                _db.FormVas.Remove(FormVa);
                await _db.SaveChangesAsync();

                return (true, "FormVa got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormVas
    }
}
