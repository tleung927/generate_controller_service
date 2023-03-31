using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEligibilityTurning3Service
    {
        // EligibilityTurning3s Services
        Task<List<EligibilityTurning3>> GetEligibilityTurning3ListByValue(int offset, int limit, string val); // GET All EligibilityTurning3ss
        Task<EligibilityTurning3> GetEligibilityTurning3(string EligibilityTurning3_name); // GET Single EligibilityTurning3s        
        Task<List<EligibilityTurning3>> GetEligibilityTurning3List(string EligibilityTurning3_name); // GET List EligibilityTurning3s        
        Task<EligibilityTurning3> AddEligibilityTurning3(EligibilityTurning3 EligibilityTurning3); // POST New EligibilityTurning3s
        Task<EligibilityTurning3> UpdateEligibilityTurning3(EligibilityTurning3 EligibilityTurning3); // PUT EligibilityTurning3s
        Task<(bool, string)> DeleteEligibilityTurning3(EligibilityTurning3 EligibilityTurning3); // DELETE EligibilityTurning3s
    }

    public class EligibilityTurning3Service : IEligibilityTurning3Service
    {
        private readonly XixsrvContext _db;

        public EligibilityTurning3Service(XixsrvContext db)
        {
            _db = db;
        }

        #region EligibilityTurning3s

        public async Task<List<EligibilityTurning3>> GetEligibilityTurning3ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EligibilityTurning3s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EligibilityTurning3> GetEligibilityTurning3(string EligibilityTurning3_id)
        {
            try
            {
                return await _db.EligibilityTurning3s.FindAsync(EligibilityTurning3_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EligibilityTurning3>> GetEligibilityTurning3List(string EligibilityTurning3_id)
        {
            try
            {
                return await _db.EligibilityTurning3s
                    .Where(i => i.EligibilityTurning3Id == EligibilityTurning3_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EligibilityTurning3> AddEligibilityTurning3(EligibilityTurning3 EligibilityTurning3)
        {
            try
            {
                await _db.EligibilityTurning3s.AddAsync(EligibilityTurning3);
                await _db.SaveChangesAsync();
                return await _db.EligibilityTurning3s.FindAsync(EligibilityTurning3.EligibilityTurning3Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EligibilityTurning3> UpdateEligibilityTurning3(EligibilityTurning3 EligibilityTurning3)
        {
            try
            {
                _db.Entry(EligibilityTurning3).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EligibilityTurning3;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEligibilityTurning3(EligibilityTurning3 EligibilityTurning3)
        {
            try
            {
                var dbEligibilityTurning3 = await _db.EligibilityTurning3s.FindAsync(EligibilityTurning3.EligibilityTurning3Id);

                if (dbEligibilityTurning3 == null)
                {
                    return (false, "EligibilityTurning3 could not be found");
                }

                _db.EligibilityTurning3s.Remove(EligibilityTurning3);
                await _db.SaveChangesAsync();

                return (true, "EligibilityTurning3 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EligibilityTurning3s
    }
}
