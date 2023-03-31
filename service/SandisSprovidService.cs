using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSprovidService
    {
        // SandisSprovids Services
        Task<List<SandisSprovid>> GetSandisSprovidListByValue(int offset, int limit, string val); // GET All SandisSprovidss
        Task<SandisSprovid> GetSandisSprovid(string SandisSprovid_name); // GET Single SandisSprovids        
        Task<List<SandisSprovid>> GetSandisSprovidList(string SandisSprovid_name); // GET List SandisSprovids        
        Task<SandisSprovid> AddSandisSprovid(SandisSprovid SandisSprovid); // POST New SandisSprovids
        Task<SandisSprovid> UpdateSandisSprovid(SandisSprovid SandisSprovid); // PUT SandisSprovids
        Task<(bool, string)> DeleteSandisSprovid(SandisSprovid SandisSprovid); // DELETE SandisSprovids
    }

    public class SandisSprovidService : ISandisSprovidService
    {
        private readonly XixsrvContext _db;

        public SandisSprovidService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSprovids

        public async Task<List<SandisSprovid>> GetSandisSprovidListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSprovids.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSprovid> GetSandisSprovid(string SandisSprovid_id)
        {
            try
            {
                return await _db.SandisSprovids.FindAsync(SandisSprovid_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSprovid>> GetSandisSprovidList(string SandisSprovid_id)
        {
            try
            {
                return await _db.SandisSprovids
                    .Where(i => i.SandisSprovidId == SandisSprovid_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSprovid> AddSandisSprovid(SandisSprovid SandisSprovid)
        {
            try
            {
                await _db.SandisSprovids.AddAsync(SandisSprovid);
                await _db.SaveChangesAsync();
                return await _db.SandisSprovids.FindAsync(SandisSprovid.SandisSprovidId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSprovid> UpdateSandisSprovid(SandisSprovid SandisSprovid)
        {
            try
            {
                _db.Entry(SandisSprovid).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSprovid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSprovid(SandisSprovid SandisSprovid)
        {
            try
            {
                var dbSandisSprovid = await _db.SandisSprovids.FindAsync(SandisSprovid.SandisSprovidId);

                if (dbSandisSprovid == null)
                {
                    return (false, "SandisSprovid could not be found");
                }

                _db.SandisSprovids.Remove(SandisSprovid);
                await _db.SaveChangesAsync();

                return (true, "SandisSprovid got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSprovids
    }
}
