using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISecuritySetService
    {
        // SecuritySets Services
        Task<List<SecuritySet>> GetSecuritySetListByValue(int offset, int limit, string val); // GET All SecuritySetss
        Task<SecuritySet> GetSecuritySet(string SecuritySet_name); // GET Single SecuritySets        
        Task<List<SecuritySet>> GetSecuritySetList(string SecuritySet_name); // GET List SecuritySets        
        Task<SecuritySet> AddSecuritySet(SecuritySet SecuritySet); // POST New SecuritySets
        Task<SecuritySet> UpdateSecuritySet(SecuritySet SecuritySet); // PUT SecuritySets
        Task<(bool, string)> DeleteSecuritySet(SecuritySet SecuritySet); // DELETE SecuritySets
    }

    public class SecuritySetService : ISecuritySetService
    {
        private readonly XixsrvContext _db;

        public SecuritySetService(XixsrvContext db)
        {
            _db = db;
        }

        #region SecuritySets

        public async Task<List<SecuritySet>> GetSecuritySetListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SecuritySets.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SecuritySet> GetSecuritySet(string SecuritySet_id)
        {
            try
            {
                return await _db.SecuritySets.FindAsync(SecuritySet_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SecuritySet>> GetSecuritySetList(string SecuritySet_id)
        {
            try
            {
                return await _db.SecuritySets
                    .Where(i => i.SecuritySetId == SecuritySet_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SecuritySet> AddSecuritySet(SecuritySet SecuritySet)
        {
            try
            {
                await _db.SecuritySets.AddAsync(SecuritySet);
                await _db.SaveChangesAsync();
                return await _db.SecuritySets.FindAsync(SecuritySet.SecuritySetId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SecuritySet> UpdateSecuritySet(SecuritySet SecuritySet)
        {
            try
            {
                _db.Entry(SecuritySet).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SecuritySet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSecuritySet(SecuritySet SecuritySet)
        {
            try
            {
                var dbSecuritySet = await _db.SecuritySets.FindAsync(SecuritySet.SecuritySetId);

                if (dbSecuritySet == null)
                {
                    return (false, "SecuritySet could not be found");
                }

                _db.SecuritySets.Remove(SecuritySet);
                await _db.SaveChangesAsync();

                return (true, "SecuritySet got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SecuritySets
    }
}
