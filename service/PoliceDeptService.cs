using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPoliceDeptService
    {
        // PoliceDepts Services
        Task<List<PoliceDept>> GetPoliceDeptListByValue(int offset, int limit, string val); // GET All PoliceDeptss
        Task<PoliceDept> GetPoliceDept(string PoliceDept_name); // GET Single PoliceDepts        
        Task<List<PoliceDept>> GetPoliceDeptList(string PoliceDept_name); // GET List PoliceDepts        
        Task<PoliceDept> AddPoliceDept(PoliceDept PoliceDept); // POST New PoliceDepts
        Task<PoliceDept> UpdatePoliceDept(PoliceDept PoliceDept); // PUT PoliceDepts
        Task<(bool, string)> DeletePoliceDept(PoliceDept PoliceDept); // DELETE PoliceDepts
    }

    public class PoliceDeptService : IPoliceDeptService
    {
        private readonly XixsrvContext _db;

        public PoliceDeptService(XixsrvContext db)
        {
            _db = db;
        }

        #region PoliceDepts

        public async Task<List<PoliceDept>> GetPoliceDeptListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PoliceDepts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PoliceDept> GetPoliceDept(string PoliceDept_id)
        {
            try
            {
                return await _db.PoliceDepts.FindAsync(PoliceDept_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PoliceDept>> GetPoliceDeptList(string PoliceDept_id)
        {
            try
            {
                return await _db.PoliceDepts
                    .Where(i => i.PoliceDeptId == PoliceDept_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PoliceDept> AddPoliceDept(PoliceDept PoliceDept)
        {
            try
            {
                await _db.PoliceDepts.AddAsync(PoliceDept);
                await _db.SaveChangesAsync();
                return await _db.PoliceDepts.FindAsync(PoliceDept.PoliceDeptId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PoliceDept> UpdatePoliceDept(PoliceDept PoliceDept)
        {
            try
            {
                _db.Entry(PoliceDept).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PoliceDept;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePoliceDept(PoliceDept PoliceDept)
        {
            try
            {
                var dbPoliceDept = await _db.PoliceDepts.FindAsync(PoliceDept.PoliceDeptId);

                if (dbPoliceDept == null)
                {
                    return (false, "PoliceDept could not be found");
                }

                _db.PoliceDepts.Remove(PoliceDept);
                await _db.SaveChangesAsync();

                return (true, "PoliceDept got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PoliceDepts
    }
}
