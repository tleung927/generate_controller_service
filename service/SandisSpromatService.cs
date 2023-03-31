using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSpromatService
    {
        // SandisSpromats Services
        Task<List<SandisSpromat>> GetSandisSpromatListByValue(int offset, int limit, string val); // GET All SandisSpromatss
        Task<SandisSpromat> GetSandisSpromat(string SandisSpromat_name); // GET Single SandisSpromats        
        Task<List<SandisSpromat>> GetSandisSpromatList(string SandisSpromat_name); // GET List SandisSpromats        
        Task<SandisSpromat> AddSandisSpromat(SandisSpromat SandisSpromat); // POST New SandisSpromats
        Task<SandisSpromat> UpdateSandisSpromat(SandisSpromat SandisSpromat); // PUT SandisSpromats
        Task<(bool, string)> DeleteSandisSpromat(SandisSpromat SandisSpromat); // DELETE SandisSpromats
    }

    public class SandisSpromatService : ISandisSpromatService
    {
        private readonly XixsrvContext _db;

        public SandisSpromatService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSpromats

        public async Task<List<SandisSpromat>> GetSandisSpromatListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSpromats.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSpromat> GetSandisSpromat(string SandisSpromat_id)
        {
            try
            {
                return await _db.SandisSpromats.FindAsync(SandisSpromat_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSpromat>> GetSandisSpromatList(string SandisSpromat_id)
        {
            try
            {
                return await _db.SandisSpromats
                    .Where(i => i.SandisSpromatId == SandisSpromat_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSpromat> AddSandisSpromat(SandisSpromat SandisSpromat)
        {
            try
            {
                await _db.SandisSpromats.AddAsync(SandisSpromat);
                await _db.SaveChangesAsync();
                return await _db.SandisSpromats.FindAsync(SandisSpromat.SandisSpromatId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSpromat> UpdateSandisSpromat(SandisSpromat SandisSpromat)
        {
            try
            {
                _db.Entry(SandisSpromat).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSpromat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSpromat(SandisSpromat SandisSpromat)
        {
            try
            {
                var dbSandisSpromat = await _db.SandisSpromats.FindAsync(SandisSpromat.SandisSpromatId);

                if (dbSandisSpromat == null)
                {
                    return (false, "SandisSpromat could not be found");
                }

                _db.SandisSpromats.Remove(SandisSpromat);
                await _db.SaveChangesAsync();

                return (true, "SandisSpromat got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSpromats
    }
}
