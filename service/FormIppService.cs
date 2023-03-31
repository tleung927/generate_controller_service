using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppService
    {
        // FormIpps Services
        Task<List<FormIpp>> GetFormIppListByValue(int offset, int limit, string val); // GET All FormIppss
        Task<FormIpp> GetFormIpp(string FormIpp_name); // GET Single FormIpps        
        Task<List<FormIpp>> GetFormIppList(string FormIpp_name); // GET List FormIpps        
        Task<FormIpp> AddFormIpp(FormIpp FormIpp); // POST New FormIpps
        Task<FormIpp> UpdateFormIpp(FormIpp FormIpp); // PUT FormIpps
        Task<(bool, string)> DeleteFormIpp(FormIpp FormIpp); // DELETE FormIpps
    }

    public class FormIppService : IFormIppService
    {
        private readonly XixsrvContext _db;

        public FormIppService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIpps

        public async Task<List<FormIpp>> GetFormIppListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIpps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIpp> GetFormIpp(string FormIpp_id)
        {
            try
            {
                return await _db.FormIpps.FindAsync(FormIpp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIpp>> GetFormIppList(string FormIpp_id)
        {
            try
            {
                return await _db.FormIpps
                    .Where(i => i.FormIppId == FormIpp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIpp> AddFormIpp(FormIpp FormIpp)
        {
            try
            {
                await _db.FormIpps.AddAsync(FormIpp);
                await _db.SaveChangesAsync();
                return await _db.FormIpps.FindAsync(FormIpp.FormIppId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIpp> UpdateFormIpp(FormIpp FormIpp)
        {
            try
            {
                _db.Entry(FormIpp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIpp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIpp(FormIpp FormIpp)
        {
            try
            {
                var dbFormIpp = await _db.FormIpps.FindAsync(FormIpp.FormIppId);

                if (dbFormIpp == null)
                {
                    return (false, "FormIpp could not be found");
                }

                _db.FormIpps.Remove(FormIpp);
                await _db.SaveChangesAsync();

                return (true, "FormIpp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIpps
    }
}
