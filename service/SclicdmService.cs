using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclicdmService
    {
        // Sclicdms Services
        Task<List<Sclicdm>> GetSclicdmListByValue(int offset, int limit, string val); // GET All Sclicdmss
        Task<Sclicdm> GetSclicdm(string Sclicdm_name); // GET Single Sclicdms        
        Task<List<Sclicdm>> GetSclicdmList(string Sclicdm_name); // GET List Sclicdms        
        Task<Sclicdm> AddSclicdm(Sclicdm Sclicdm); // POST New Sclicdms
        Task<Sclicdm> UpdateSclicdm(Sclicdm Sclicdm); // PUT Sclicdms
        Task<(bool, string)> DeleteSclicdm(Sclicdm Sclicdm); // DELETE Sclicdms
    }

    public class SclicdmService : ISclicdmService
    {
        private readonly XixsrvContext _db;

        public SclicdmService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclicdms

        public async Task<List<Sclicdm>> GetSclicdmListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclicdms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclicdm> GetSclicdm(string Sclicdm_id)
        {
            try
            {
                return await _db.Sclicdms.FindAsync(Sclicdm_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclicdm>> GetSclicdmList(string Sclicdm_id)
        {
            try
            {
                return await _db.Sclicdms
                    .Where(i => i.SclicdmId == Sclicdm_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclicdm> AddSclicdm(Sclicdm Sclicdm)
        {
            try
            {
                await _db.Sclicdms.AddAsync(Sclicdm);
                await _db.SaveChangesAsync();
                return await _db.Sclicdms.FindAsync(Sclicdm.SclicdmId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclicdm> UpdateSclicdm(Sclicdm Sclicdm)
        {
            try
            {
                _db.Entry(Sclicdm).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclicdm;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclicdm(Sclicdm Sclicdm)
        {
            try
            {
                var dbSclicdm = await _db.Sclicdms.FindAsync(Sclicdm.SclicdmId);

                if (dbSclicdm == null)
                {
                    return (false, "Sclicdm could not be found");
                }

                _db.Sclicdms.Remove(Sclicdm);
                await _db.SaveChangesAsync();

                return (true, "Sclicdm got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclicdms
    }
}
