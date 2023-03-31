using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormFairHearingService
    {
        // FormFairHearings Services
        Task<List<FormFairHearing>> GetFormFairHearingListByValue(int offset, int limit, string val); // GET All FormFairHearingss
        Task<FormFairHearing> GetFormFairHearing(string FormFairHearing_name); // GET Single FormFairHearings        
        Task<List<FormFairHearing>> GetFormFairHearingList(string FormFairHearing_name); // GET List FormFairHearings        
        Task<FormFairHearing> AddFormFairHearing(FormFairHearing FormFairHearing); // POST New FormFairHearings
        Task<FormFairHearing> UpdateFormFairHearing(FormFairHearing FormFairHearing); // PUT FormFairHearings
        Task<(bool, string)> DeleteFormFairHearing(FormFairHearing FormFairHearing); // DELETE FormFairHearings
    }

    public class FormFairHearingService : IFormFairHearingService
    {
        private readonly XixsrvContext _db;

        public FormFairHearingService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormFairHearings

        public async Task<List<FormFairHearing>> GetFormFairHearingListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormFairHearings.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormFairHearing> GetFormFairHearing(string FormFairHearing_id)
        {
            try
            {
                return await _db.FormFairHearings.FindAsync(FormFairHearing_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormFairHearing>> GetFormFairHearingList(string FormFairHearing_id)
        {
            try
            {
                return await _db.FormFairHearings
                    .Where(i => i.FormFairHearingId == FormFairHearing_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormFairHearing> AddFormFairHearing(FormFairHearing FormFairHearing)
        {
            try
            {
                await _db.FormFairHearings.AddAsync(FormFairHearing);
                await _db.SaveChangesAsync();
                return await _db.FormFairHearings.FindAsync(FormFairHearing.FormFairHearingId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormFairHearing> UpdateFormFairHearing(FormFairHearing FormFairHearing)
        {
            try
            {
                _db.Entry(FormFairHearing).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormFairHearing;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormFairHearing(FormFairHearing FormFairHearing)
        {
            try
            {
                var dbFormFairHearing = await _db.FormFairHearings.FindAsync(FormFairHearing.FormFairHearingId);

                if (dbFormFairHearing == null)
                {
                    return (false, "FormFairHearing could not be found");
                }

                _db.FormFairHearings.Remove(FormFairHearing);
                await _db.SaveChangesAsync();

                return (true, "FormFairHearing got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormFairHearings
    }
}
