using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormSocialService
    {
        // FormSocials Services
        Task<List<FormSocial>> GetFormSocialListByValue(int offset, int limit, string val); // GET All FormSocialss
        Task<FormSocial> GetFormSocial(string FormSocial_name); // GET Single FormSocials        
        Task<List<FormSocial>> GetFormSocialList(string FormSocial_name); // GET List FormSocials        
        Task<FormSocial> AddFormSocial(FormSocial FormSocial); // POST New FormSocials
        Task<FormSocial> UpdateFormSocial(FormSocial FormSocial); // PUT FormSocials
        Task<(bool, string)> DeleteFormSocial(FormSocial FormSocial); // DELETE FormSocials
    }

    public class FormSocialService : IFormSocialService
    {
        private readonly XixsrvContext _db;

        public FormSocialService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormSocials

        public async Task<List<FormSocial>> GetFormSocialListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormSocials.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormSocial> GetFormSocial(string FormSocial_id)
        {
            try
            {
                return await _db.FormSocials.FindAsync(FormSocial_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormSocial>> GetFormSocialList(string FormSocial_id)
        {
            try
            {
                return await _db.FormSocials
                    .Where(i => i.FormSocialId == FormSocial_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormSocial> AddFormSocial(FormSocial FormSocial)
        {
            try
            {
                await _db.FormSocials.AddAsync(FormSocial);
                await _db.SaveChangesAsync();
                return await _db.FormSocials.FindAsync(FormSocial.FormSocialId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormSocial> UpdateFormSocial(FormSocial FormSocial)
        {
            try
            {
                _db.Entry(FormSocial).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormSocial;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormSocial(FormSocial FormSocial)
        {
            try
            {
                var dbFormSocial = await _db.FormSocials.FindAsync(FormSocial.FormSocialId);

                if (dbFormSocial == null)
                {
                    return (false, "FormSocial could not be found");
                }

                _db.FormSocials.Remove(FormSocial);
                await _db.SaveChangesAsync();

                return (true, "FormSocial got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormSocials
    }
}
