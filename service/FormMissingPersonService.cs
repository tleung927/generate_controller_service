using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMissingPersonService
    {
        // FormMissingPersons Services
        Task<List<FormMissingPerson>> GetFormMissingPersonListByValue(int offset, int limit, string val); // GET All FormMissingPersonss
        Task<FormMissingPerson> GetFormMissingPerson(string FormMissingPerson_name); // GET Single FormMissingPersons        
        Task<List<FormMissingPerson>> GetFormMissingPersonList(string FormMissingPerson_name); // GET List FormMissingPersons        
        Task<FormMissingPerson> AddFormMissingPerson(FormMissingPerson FormMissingPerson); // POST New FormMissingPersons
        Task<FormMissingPerson> UpdateFormMissingPerson(FormMissingPerson FormMissingPerson); // PUT FormMissingPersons
        Task<(bool, string)> DeleteFormMissingPerson(FormMissingPerson FormMissingPerson); // DELETE FormMissingPersons
    }

    public class FormMissingPersonService : IFormMissingPersonService
    {
        private readonly XixsrvContext _db;

        public FormMissingPersonService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMissingPersons

        public async Task<List<FormMissingPerson>> GetFormMissingPersonListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMissingPersons.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMissingPerson> GetFormMissingPerson(string FormMissingPerson_id)
        {
            try
            {
                return await _db.FormMissingPersons.FindAsync(FormMissingPerson_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMissingPerson>> GetFormMissingPersonList(string FormMissingPerson_id)
        {
            try
            {
                return await _db.FormMissingPersons
                    .Where(i => i.FormMissingPersonId == FormMissingPerson_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMissingPerson> AddFormMissingPerson(FormMissingPerson FormMissingPerson)
        {
            try
            {
                await _db.FormMissingPersons.AddAsync(FormMissingPerson);
                await _db.SaveChangesAsync();
                return await _db.FormMissingPersons.FindAsync(FormMissingPerson.FormMissingPersonId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMissingPerson> UpdateFormMissingPerson(FormMissingPerson FormMissingPerson)
        {
            try
            {
                _db.Entry(FormMissingPerson).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMissingPerson;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMissingPerson(FormMissingPerson FormMissingPerson)
        {
            try
            {
                var dbFormMissingPerson = await _db.FormMissingPersons.FindAsync(FormMissingPerson.FormMissingPersonId);

                if (dbFormMissingPerson == null)
                {
                    return (false, "FormMissingPerson could not be found");
                }

                _db.FormMissingPersons.Remove(FormMissingPerson);
                await _db.SaveChangesAsync();

                return (true, "FormMissingPerson got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMissingPersons
    }
}
