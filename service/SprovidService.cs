using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISprovidService
    {
        // Sprovids Services
        Task<List<Sprovid>> GetSprovidListByValue(int offset, int limit, string val); // GET All Sprovidss
        Task<Sprovid> GetSprovid(string Sprovid_name); // GET Single Sprovids        
        Task<List<Sprovid>> GetSprovidList(string Sprovid_name); // GET List Sprovids        
        Task<Sprovid> AddSprovid(Sprovid Sprovid); // POST New Sprovids
        Task<Sprovid> UpdateSprovid(Sprovid Sprovid); // PUT Sprovids
        Task<(bool, string)> DeleteSprovid(Sprovid Sprovid); // DELETE Sprovids
    }

    public class SprovidService : ISprovidService
    {
        private readonly XixsrvContext _db;

        public SprovidService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sprovids

        public async Task<List<Sprovid>> GetSprovidListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sprovids.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sprovid> GetSprovid(string Sprovid_id)
        {
            try
            {
                return await _db.Sprovids.FindAsync(Sprovid_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sprovid>> GetSprovidList(string Sprovid_id)
        {
            try
            {
                return await _db.Sprovids
                    .Where(i => i.SprovidId == Sprovid_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sprovid> AddSprovid(Sprovid Sprovid)
        {
            try
            {
                await _db.Sprovids.AddAsync(Sprovid);
                await _db.SaveChangesAsync();
                return await _db.Sprovids.FindAsync(Sprovid.SprovidId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sprovid> UpdateSprovid(Sprovid Sprovid)
        {
            try
            {
                _db.Entry(Sprovid).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sprovid;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSprovid(Sprovid Sprovid)
        {
            try
            {
                var dbSprovid = await _db.Sprovids.FindAsync(Sprovid.SprovidId);

                if (dbSprovid == null)
                {
                    return (false, "Sprovid could not be found");
                }

                _db.Sprovids.Remove(Sprovid);
                await _db.SaveChangesAsync();

                return (true, "Sprovid got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sprovids
    }
}
