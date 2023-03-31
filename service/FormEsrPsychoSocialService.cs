using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormEsrPsychoSocialService
    {
        // FormEsrPsychoSocials Services
        Task<List<FormEsrPsychoSocial>> GetFormEsrPsychoSocialListByValue(int offset, int limit, string val); // GET All FormEsrPsychoSocialss
        Task<FormEsrPsychoSocial> GetFormEsrPsychoSocial(string FormEsrPsychoSocial_name); // GET Single FormEsrPsychoSocials        
        Task<List<FormEsrPsychoSocial>> GetFormEsrPsychoSocialList(string FormEsrPsychoSocial_name); // GET List FormEsrPsychoSocials        
        Task<FormEsrPsychoSocial> AddFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial); // POST New FormEsrPsychoSocials
        Task<FormEsrPsychoSocial> UpdateFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial); // PUT FormEsrPsychoSocials
        Task<(bool, string)> DeleteFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial); // DELETE FormEsrPsychoSocials
    }

    public class FormEsrPsychoSocialService : IFormEsrPsychoSocialService
    {
        private readonly XixsrvContext _db;

        public FormEsrPsychoSocialService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormEsrPsychoSocials

        public async Task<List<FormEsrPsychoSocial>> GetFormEsrPsychoSocialListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormEsrPsychoSocials.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormEsrPsychoSocial> GetFormEsrPsychoSocial(string FormEsrPsychoSocial_id)
        {
            try
            {
                return await _db.FormEsrPsychoSocials.FindAsync(FormEsrPsychoSocial_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormEsrPsychoSocial>> GetFormEsrPsychoSocialList(string FormEsrPsychoSocial_id)
        {
            try
            {
                return await _db.FormEsrPsychoSocials
                    .Where(i => i.FormEsrPsychoSocialId == FormEsrPsychoSocial_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormEsrPsychoSocial> AddFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial)
        {
            try
            {
                await _db.FormEsrPsychoSocials.AddAsync(FormEsrPsychoSocial);
                await _db.SaveChangesAsync();
                return await _db.FormEsrPsychoSocials.FindAsync(FormEsrPsychoSocial.FormEsrPsychoSocialId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormEsrPsychoSocial> UpdateFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial)
        {
            try
            {
                _db.Entry(FormEsrPsychoSocial).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormEsrPsychoSocial;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormEsrPsychoSocial(FormEsrPsychoSocial FormEsrPsychoSocial)
        {
            try
            {
                var dbFormEsrPsychoSocial = await _db.FormEsrPsychoSocials.FindAsync(FormEsrPsychoSocial.FormEsrPsychoSocialId);

                if (dbFormEsrPsychoSocial == null)
                {
                    return (false, "FormEsrPsychoSocial could not be found");
                }

                _db.FormEsrPsychoSocials.Remove(FormEsrPsychoSocial);
                await _db.SaveChangesAsync();

                return (true, "FormEsrPsychoSocial got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormEsrPsychoSocials
    }
}
