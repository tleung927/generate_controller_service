using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormAnnualContactService
    {
        // FormAnnualContacts Services
        Task<List<FormAnnualContact>> GetFormAnnualContactListByValue(int offset, int limit, string val); // GET All FormAnnualContactss
        Task<FormAnnualContact> GetFormAnnualContact(string FormAnnualContact_name); // GET Single FormAnnualContacts        
        Task<List<FormAnnualContact>> GetFormAnnualContactList(string FormAnnualContact_name); // GET List FormAnnualContacts        
        Task<FormAnnualContact> AddFormAnnualContact(FormAnnualContact FormAnnualContact); // POST New FormAnnualContacts
        Task<FormAnnualContact> UpdateFormAnnualContact(FormAnnualContact FormAnnualContact); // PUT FormAnnualContacts
        Task<(bool, string)> DeleteFormAnnualContact(FormAnnualContact FormAnnualContact); // DELETE FormAnnualContacts
    }

    public class FormAnnualContactService : IFormAnnualContactService
    {
        private readonly XixsrvContext _db;

        public FormAnnualContactService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormAnnualContacts

        public async Task<List<FormAnnualContact>> GetFormAnnualContactListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormAnnualContacts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormAnnualContact> GetFormAnnualContact(string FormAnnualContact_id)
        {
            try
            {
                return await _db.FormAnnualContacts.FindAsync(FormAnnualContact_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormAnnualContact>> GetFormAnnualContactList(string FormAnnualContact_id)
        {
            try
            {
                return await _db.FormAnnualContacts
                    .Where(i => i.FormAnnualContactId == FormAnnualContact_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormAnnualContact> AddFormAnnualContact(FormAnnualContact FormAnnualContact)
        {
            try
            {
                await _db.FormAnnualContacts.AddAsync(FormAnnualContact);
                await _db.SaveChangesAsync();
                return await _db.FormAnnualContacts.FindAsync(FormAnnualContact.FormAnnualContactId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormAnnualContact> UpdateFormAnnualContact(FormAnnualContact FormAnnualContact)
        {
            try
            {
                _db.Entry(FormAnnualContact).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormAnnualContact;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormAnnualContact(FormAnnualContact FormAnnualContact)
        {
            try
            {
                var dbFormAnnualContact = await _db.FormAnnualContacts.FindAsync(FormAnnualContact.FormAnnualContactId);

                if (dbFormAnnualContact == null)
                {
                    return (false, "FormAnnualContact could not be found");
                }

                _db.FormAnnualContacts.Remove(FormAnnualContact);
                await _db.SaveChangesAsync();

                return (true, "FormAnnualContact got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormAnnualContacts
    }
}
