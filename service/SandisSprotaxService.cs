using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSprotaxService
    {
        // SandisSprotaxs Services
        Task<List<SandisSprotax>> GetSandisSprotaxListByValue(int offset, int limit, string val); // GET All SandisSprotaxss
        Task<SandisSprotax> GetSandisSprotax(string SandisSprotax_name); // GET Single SandisSprotaxs        
        Task<List<SandisSprotax>> GetSandisSprotaxList(string SandisSprotax_name); // GET List SandisSprotaxs        
        Task<SandisSprotax> AddSandisSprotax(SandisSprotax SandisSprotax); // POST New SandisSprotaxs
        Task<SandisSprotax> UpdateSandisSprotax(SandisSprotax SandisSprotax); // PUT SandisSprotaxs
        Task<(bool, string)> DeleteSandisSprotax(SandisSprotax SandisSprotax); // DELETE SandisSprotaxs
    }

    public class SandisSprotaxService : ISandisSprotaxService
    {
        private readonly XixsrvContext _db;

        public SandisSprotaxService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSprotaxs

        public async Task<List<SandisSprotax>> GetSandisSprotaxListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSprotaxs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSprotax> GetSandisSprotax(string SandisSprotax_id)
        {
            try
            {
                return await _db.SandisSprotaxs.FindAsync(SandisSprotax_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSprotax>> GetSandisSprotaxList(string SandisSprotax_id)
        {
            try
            {
                return await _db.SandisSprotaxs
                    .Where(i => i.SandisSprotaxId == SandisSprotax_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSprotax> AddSandisSprotax(SandisSprotax SandisSprotax)
        {
            try
            {
                await _db.SandisSprotaxs.AddAsync(SandisSprotax);
                await _db.SaveChangesAsync();
                return await _db.SandisSprotaxs.FindAsync(SandisSprotax.SandisSprotaxId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSprotax> UpdateSandisSprotax(SandisSprotax SandisSprotax)
        {
            try
            {
                _db.Entry(SandisSprotax).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSprotax;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSprotax(SandisSprotax SandisSprotax)
        {
            try
            {
                var dbSandisSprotax = await _db.SandisSprotaxs.FindAsync(SandisSprotax.SandisSprotaxId);

                if (dbSandisSprotax == null)
                {
                    return (false, "SandisSprotax could not be found");
                }

                _db.SandisSprotaxs.Remove(SandisSprotax);
                await _db.SaveChangesAsync();

                return (true, "SandisSprotax got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSprotaxs
    }
}
