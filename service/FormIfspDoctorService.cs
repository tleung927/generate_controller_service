using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIfspDoctorService
    {
        // FormIfspDoctors Services
        Task<List<FormIfspDoctor>> GetFormIfspDoctorListByValue(int offset, int limit, string val); // GET All FormIfspDoctorss
        Task<FormIfspDoctor> GetFormIfspDoctor(string FormIfspDoctor_name); // GET Single FormIfspDoctors        
        Task<List<FormIfspDoctor>> GetFormIfspDoctorList(string FormIfspDoctor_name); // GET List FormIfspDoctors        
        Task<FormIfspDoctor> AddFormIfspDoctor(FormIfspDoctor FormIfspDoctor); // POST New FormIfspDoctors
        Task<FormIfspDoctor> UpdateFormIfspDoctor(FormIfspDoctor FormIfspDoctor); // PUT FormIfspDoctors
        Task<(bool, string)> DeleteFormIfspDoctor(FormIfspDoctor FormIfspDoctor); // DELETE FormIfspDoctors
    }

    public class FormIfspDoctorService : IFormIfspDoctorService
    {
        private readonly XixsrvContext _db;

        public FormIfspDoctorService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIfspDoctors

        public async Task<List<FormIfspDoctor>> GetFormIfspDoctorListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIfspDoctors.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIfspDoctor> GetFormIfspDoctor(string FormIfspDoctor_id)
        {
            try
            {
                return await _db.FormIfspDoctors.FindAsync(FormIfspDoctor_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIfspDoctor>> GetFormIfspDoctorList(string FormIfspDoctor_id)
        {
            try
            {
                return await _db.FormIfspDoctors
                    .Where(i => i.FormIfspDoctorId == FormIfspDoctor_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIfspDoctor> AddFormIfspDoctor(FormIfspDoctor FormIfspDoctor)
        {
            try
            {
                await _db.FormIfspDoctors.AddAsync(FormIfspDoctor);
                await _db.SaveChangesAsync();
                return await _db.FormIfspDoctors.FindAsync(FormIfspDoctor.FormIfspDoctorId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIfspDoctor> UpdateFormIfspDoctor(FormIfspDoctor FormIfspDoctor)
        {
            try
            {
                _db.Entry(FormIfspDoctor).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIfspDoctor;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIfspDoctor(FormIfspDoctor FormIfspDoctor)
        {
            try
            {
                var dbFormIfspDoctor = await _db.FormIfspDoctors.FindAsync(FormIfspDoctor.FormIfspDoctorId);

                if (dbFormIfspDoctor == null)
                {
                    return (false, "FormIfspDoctor could not be found");
                }

                _db.FormIfspDoctors.Remove(FormIfspDoctor);
                await _db.SaveChangesAsync();

                return (true, "FormIfspDoctor got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIfspDoctors
    }
}
