using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEligibilityEsrService
    {
        // EligibilityEsrs Services
        Task<List<EligibilityEsr>> GetEligibilityEsrListByValue(int offset, int limit, string val); // GET All EligibilityEsrss
        Task<EligibilityEsr> GetEligibilityEsr(string EligibilityEsr_name); // GET Single EligibilityEsrs        
        Task<List<EligibilityEsr>> GetEligibilityEsrList(string EligibilityEsr_name); // GET List EligibilityEsrs        
        Task<EligibilityEsr> AddEligibilityEsr(EligibilityEsr EligibilityEsr); // POST New EligibilityEsrs
        Task<EligibilityEsr> UpdateEligibilityEsr(EligibilityEsr EligibilityEsr); // PUT EligibilityEsrs
        Task<(bool, string)> DeleteEligibilityEsr(EligibilityEsr EligibilityEsr); // DELETE EligibilityEsrs
    }

    public class EligibilityEsrService : IEligibilityEsrService
    {
        private readonly XixsrvContext _db;

        public EligibilityEsrService(XixsrvContext db)
        {
            _db = db;
        }

        #region EligibilityEsrs

        public async Task<List<EligibilityEsr>> GetEligibilityEsrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EligibilityEsrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EligibilityEsr> GetEligibilityEsr(string EligibilityEsr_id)
        {
            try
            {
                return await _db.EligibilityEsrs.FindAsync(EligibilityEsr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EligibilityEsr>> GetEligibilityEsrList(string EligibilityEsr_id)
        {
            try
            {
                return await _db.EligibilityEsrs
                    .Where(i => i.EligibilityEsrId == EligibilityEsr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EligibilityEsr> AddEligibilityEsr(EligibilityEsr EligibilityEsr)
        {
            try
            {
                await _db.EligibilityEsrs.AddAsync(EligibilityEsr);
                await _db.SaveChangesAsync();
                return await _db.EligibilityEsrs.FindAsync(EligibilityEsr.EligibilityEsrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EligibilityEsr> UpdateEligibilityEsr(EligibilityEsr EligibilityEsr)
        {
            try
            {
                _db.Entry(EligibilityEsr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EligibilityEsr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEligibilityEsr(EligibilityEsr EligibilityEsr)
        {
            try
            {
                var dbEligibilityEsr = await _db.EligibilityEsrs.FindAsync(EligibilityEsr.EligibilityEsrId);

                if (dbEligibilityEsr == null)
                {
                    return (false, "EligibilityEsr could not be found");
                }

                _db.EligibilityEsrs.Remove(EligibilityEsr);
                await _db.SaveChangesAsync();

                return (true, "EligibilityEsr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EligibilityEsrs
    }
}
