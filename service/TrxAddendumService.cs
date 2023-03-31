using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITrxAddendumService
    {
        // TrxAddendums Services
        Task<List<TrxAddendum>> GetTrxAddendumListByValue(int offset, int limit, string val); // GET All TrxAddendumss
        Task<TrxAddendum> GetTrxAddendum(string TrxAddendum_name); // GET Single TrxAddendums        
        Task<List<TrxAddendum>> GetTrxAddendumList(string TrxAddendum_name); // GET List TrxAddendums        
        Task<TrxAddendum> AddTrxAddendum(TrxAddendum TrxAddendum); // POST New TrxAddendums
        Task<TrxAddendum> UpdateTrxAddendum(TrxAddendum TrxAddendum); // PUT TrxAddendums
        Task<(bool, string)> DeleteTrxAddendum(TrxAddendum TrxAddendum); // DELETE TrxAddendums
    }

    public class TrxAddendumService : ITrxAddendumService
    {
        private readonly XixsrvContext _db;

        public TrxAddendumService(XixsrvContext db)
        {
            _db = db;
        }

        #region TrxAddendums

        public async Task<List<TrxAddendum>> GetTrxAddendumListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TrxAddendums.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TrxAddendum> GetTrxAddendum(string TrxAddendum_id)
        {
            try
            {
                return await _db.TrxAddendums.FindAsync(TrxAddendum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TrxAddendum>> GetTrxAddendumList(string TrxAddendum_id)
        {
            try
            {
                return await _db.TrxAddendums
                    .Where(i => i.TrxAddendumId == TrxAddendum_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TrxAddendum> AddTrxAddendum(TrxAddendum TrxAddendum)
        {
            try
            {
                await _db.TrxAddendums.AddAsync(TrxAddendum);
                await _db.SaveChangesAsync();
                return await _db.TrxAddendums.FindAsync(TrxAddendum.TrxAddendumId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TrxAddendum> UpdateTrxAddendum(TrxAddendum TrxAddendum)
        {
            try
            {
                _db.Entry(TrxAddendum).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TrxAddendum;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTrxAddendum(TrxAddendum TrxAddendum)
        {
            try
            {
                var dbTrxAddendum = await _db.TrxAddendums.FindAsync(TrxAddendum.TrxAddendumId);

                if (dbTrxAddendum == null)
                {
                    return (false, "TrxAddendum could not be found");
                }

                _db.TrxAddendums.Remove(TrxAddendum);
                await _db.SaveChangesAsync();

                return (true, "TrxAddendum got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TrxAddendums
    }
}
