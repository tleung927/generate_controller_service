using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleEsDiagnosisService
    {
        // ViewScheduleEsDiagnosiss Services
        Task<List<ViewScheduleEsDiagnosis>> GetViewScheduleEsDiagnosisListByValue(int offset, int limit, string val); // GET All ViewScheduleEsDiagnosisss
        Task<ViewScheduleEsDiagnosis> GetViewScheduleEsDiagnosis(string ViewScheduleEsDiagnosis_name); // GET Single ViewScheduleEsDiagnosiss        
        Task<List<ViewScheduleEsDiagnosis>> GetViewScheduleEsDiagnosisList(string ViewScheduleEsDiagnosis_name); // GET List ViewScheduleEsDiagnosiss        
        Task<ViewScheduleEsDiagnosis> AddViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis); // POST New ViewScheduleEsDiagnosiss
        Task<ViewScheduleEsDiagnosis> UpdateViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis); // PUT ViewScheduleEsDiagnosiss
        Task<(bool, string)> DeleteViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis); // DELETE ViewScheduleEsDiagnosiss
    }

    public class ViewScheduleEsDiagnosisService : IViewScheduleEsDiagnosisService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleEsDiagnosisService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleEsDiagnosiss

        public async Task<List<ViewScheduleEsDiagnosis>> GetViewScheduleEsDiagnosisListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleEsDiagnosiss.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleEsDiagnosis> GetViewScheduleEsDiagnosis(string ViewScheduleEsDiagnosis_id)
        {
            try
            {
                return await _db.ViewScheduleEsDiagnosiss.FindAsync(ViewScheduleEsDiagnosis_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleEsDiagnosis>> GetViewScheduleEsDiagnosisList(string ViewScheduleEsDiagnosis_id)
        {
            try
            {
                return await _db.ViewScheduleEsDiagnosiss
                    .Where(i => i.ViewScheduleEsDiagnosisId == ViewScheduleEsDiagnosis_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleEsDiagnosis> AddViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis)
        {
            try
            {
                await _db.ViewScheduleEsDiagnosiss.AddAsync(ViewScheduleEsDiagnosis);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleEsDiagnosiss.FindAsync(ViewScheduleEsDiagnosis.ViewScheduleEsDiagnosisId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleEsDiagnosis> UpdateViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis)
        {
            try
            {
                _db.Entry(ViewScheduleEsDiagnosis).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleEsDiagnosis;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleEsDiagnosis(ViewScheduleEsDiagnosis ViewScheduleEsDiagnosis)
        {
            try
            {
                var dbViewScheduleEsDiagnosis = await _db.ViewScheduleEsDiagnosiss.FindAsync(ViewScheduleEsDiagnosis.ViewScheduleEsDiagnosisId);

                if (dbViewScheduleEsDiagnosis == null)
                {
                    return (false, "ViewScheduleEsDiagnosis could not be found");
                }

                _db.ViewScheduleEsDiagnosiss.Remove(ViewScheduleEsDiagnosis);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleEsDiagnosis got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleEsDiagnosiss
    }
}
