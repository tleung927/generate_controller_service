using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITrxAddendumBService
    {
        // TrxAddendumBs Services
        Task<List<TrxAddendumB>> GetTrxAddendumBListByValue(int offset, int limit, string val); // GET All TrxAddendumBss
        Task<TrxAddendumB> GetTrxAddendumB(string TrxAddendumB_name); // GET Single TrxAddendumBs        
        Task<List<TrxAddendumB>> GetTrxAddendumBList(string TrxAddendumB_name); // GET List TrxAddendumBs        
        Task<TrxAddendumB> AddTrxAddendumB(TrxAddendumB TrxAddendumB); // POST New TrxAddendumBs
        Task<TrxAddendumB> UpdateTrxAddendumB(TrxAddendumB TrxAddendumB); // PUT TrxAddendumBs
        Task<(bool, string)> DeleteTrxAddendumB(TrxAddendumB TrxAddendumB); // DELETE TrxAddendumBs
    }

    public class TrxAddendumBService : ITrxAddendumBService
    {
        private readonly XixsrvContext _db;

        public TrxAddendumBService(XixsrvContext db)
        {
            _db = db;
        }

        #region TrxAddendumBs

        public async Task<List<TrxAddendumB>> GetTrxAddendumBListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TrxAddendumBs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TrxAddendumB> GetTrxAddendumB(string TrxAddendumB_id)
        {
            try
            {
                return await _db.TrxAddendumBs.FindAsync(TrxAddendumB_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TrxAddendumB>> GetTrxAddendumBList(string TrxAddendumB_id)
        {
            try
            {
                return await _db.TrxAddendumBs
                    .Where(i => i.TrxAddendumBId == TrxAddendumB_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TrxAddendumB> AddTrxAddendumB(TrxAddendumB TrxAddendumB)
        {
            try
            {
                await _db.TrxAddendumBs.AddAsync(TrxAddendumB);
                await _db.SaveChangesAsync();
                return await _db.TrxAddendumBs.FindAsync(TrxAddendumB.TrxAddendumBId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TrxAddendumB> UpdateTrxAddendumB(TrxAddendumB TrxAddendumB)
        {
            try
            {
                _db.Entry(TrxAddendumB).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TrxAddendumB;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTrxAddendumB(TrxAddendumB TrxAddendumB)
        {
            try
            {
                var dbTrxAddendumB = await _db.TrxAddendumBs.FindAsync(TrxAddendumB.TrxAddendumBId);

                if (dbTrxAddendumB == null)
                {
                    return (false, "TrxAddendumB could not be found");
                }

                _db.TrxAddendumBs.Remove(TrxAddendumB);
                await _db.SaveChangesAsync();

                return (true, "TrxAddendumB got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TrxAddendumBs
    }
}
