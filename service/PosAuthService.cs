using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPosAuthService
    {
        // PosAuths Services
        Task<List<PosAuth>> GetPosAuthListByValue(int offset, int limit, string val); // GET All PosAuthss
        Task<PosAuth> GetPosAuth(string PosAuth_name); // GET Single PosAuths        
        Task<List<PosAuth>> GetPosAuthList(string PosAuth_name); // GET List PosAuths        
        Task<PosAuth> AddPosAuth(PosAuth PosAuth); // POST New PosAuths
        Task<PosAuth> UpdatePosAuth(PosAuth PosAuth); // PUT PosAuths
        Task<(bool, string)> DeletePosAuth(PosAuth PosAuth); // DELETE PosAuths
    }

    public class PosAuthService : IPosAuthService
    {
        private readonly XixsrvContext _db;

        public PosAuthService(XixsrvContext db)
        {
            _db = db;
        }

        #region PosAuths

        public async Task<List<PosAuth>> GetPosAuthListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PosAuths.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PosAuth> GetPosAuth(string PosAuth_id)
        {
            try
            {
                return await _db.PosAuths.FindAsync(PosAuth_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PosAuth>> GetPosAuthList(string PosAuth_id)
        {
            try
            {
                return await _db.PosAuths
                    .Where(i => i.PosAuthId == PosAuth_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PosAuth> AddPosAuth(PosAuth PosAuth)
        {
            try
            {
                await _db.PosAuths.AddAsync(PosAuth);
                await _db.SaveChangesAsync();
                return await _db.PosAuths.FindAsync(PosAuth.PosAuthId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PosAuth> UpdatePosAuth(PosAuth PosAuth)
        {
            try
            {
                _db.Entry(PosAuth).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PosAuth;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePosAuth(PosAuth PosAuth)
        {
            try
            {
                var dbPosAuth = await _db.PosAuths.FindAsync(PosAuth.PosAuthId);

                if (dbPosAuth == null)
                {
                    return (false, "PosAuth could not be found");
                }

                _db.PosAuths.Remove(PosAuth);
                await _db.SaveChangesAsync();

                return (true, "PosAuth got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PosAuths
    }
}
