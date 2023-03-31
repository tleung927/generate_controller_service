using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppaddendumService
    {
        // FormIppaddendums Services
        Task<List<FormIppaddendum>> GetFormIppaddendumListByValue(int offset, int limit, string val); // GET All FormIppaddendumss
        Task<FormIppaddendum> GetFormIppaddendum(string FormIppaddendum_name); // GET Single FormIppaddendums        
        Task<List<FormIppaddendum>> GetFormIppaddendumList(string FormIppaddendum_name); // GET List FormIppaddendums        
        Task<FormIppaddendum> AddFormIppaddendum(FormIppaddendum FormIppaddendum); // POST New FormIppaddendums
        Task<FormIppaddendum> UpdateFormIppaddendum(FormIppaddendum FormIppaddendum); // PUT FormIppaddendums
        Task<(bool, string)> DeleteFormIppaddendum(FormIppaddendum FormIppaddendum); // DELETE FormIppaddendums
    }

    public class FormIppaddendumService : IFormIppaddendumService
    {
        private readonly XixsrvContext _db;

        public FormIppaddendumService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppaddendums

        public async Task<List<FormIppaddendum>> GetFormIppaddendumListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppaddendums.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppaddendum> GetFormIppaddendum(string FormIppaddendum_id)
        {
            try
            {
                return await _db.FormIppaddendums.FindAsync(FormIppaddendum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppaddendum>> GetFormIppaddendumList(string FormIppaddendum_id)
        {
            try
            {
                return await _db.FormIppaddendums
                    .Where(i => i.FormIppaddendumId == FormIppaddendum_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppaddendum> AddFormIppaddendum(FormIppaddendum FormIppaddendum)
        {
            try
            {
                await _db.FormIppaddendums.AddAsync(FormIppaddendum);
                await _db.SaveChangesAsync();
                return await _db.FormIppaddendums.FindAsync(FormIppaddendum.FormIppaddendumId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppaddendum> UpdateFormIppaddendum(FormIppaddendum FormIppaddendum)
        {
            try
            {
                _db.Entry(FormIppaddendum).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppaddendum;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppaddendum(FormIppaddendum FormIppaddendum)
        {
            try
            {
                var dbFormIppaddendum = await _db.FormIppaddendums.FindAsync(FormIppaddendum.FormIppaddendumId);

                if (dbFormIppaddendum == null)
                {
                    return (false, "FormIppaddendum could not be found");
                }

                _db.FormIppaddendums.Remove(FormIppaddendum);
                await _db.SaveChangesAsync();

                return (true, "FormIppaddendum got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppaddendums
    }
}
