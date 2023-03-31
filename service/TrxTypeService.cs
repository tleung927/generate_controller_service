using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITrxTypeService
    {
        // TrxTypes Services
        Task<List<TrxType>> GetTrxTypeListByValue(int offset, int limit, string val); // GET All TrxTypess
        Task<TrxType> GetTrxType(string TrxType_name); // GET Single TrxTypes        
        Task<List<TrxType>> GetTrxTypeList(string TrxType_name); // GET List TrxTypes        
        Task<TrxType> AddTrxType(TrxType TrxType); // POST New TrxTypes
        Task<TrxType> UpdateTrxType(TrxType TrxType); // PUT TrxTypes
        Task<(bool, string)> DeleteTrxType(TrxType TrxType); // DELETE TrxTypes
    }

    public class TrxTypeService : ITrxTypeService
    {
        private readonly XixsrvContext _db;

        public TrxTypeService(XixsrvContext db)
        {
            _db = db;
        }

        #region TrxTypes

        public async Task<List<TrxType>> GetTrxTypeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TrxTypes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TrxType> GetTrxType(string TrxType_id)
        {
            try
            {
                return await _db.TrxTypes.FindAsync(TrxType_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TrxType>> GetTrxTypeList(string TrxType_id)
        {
            try
            {
                return await _db.TrxTypes
                    .Where(i => i.TrxTypeId == TrxType_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TrxType> AddTrxType(TrxType TrxType)
        {
            try
            {
                await _db.TrxTypes.AddAsync(TrxType);
                await _db.SaveChangesAsync();
                return await _db.TrxTypes.FindAsync(TrxType.TrxTypeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TrxType> UpdateTrxType(TrxType TrxType)
        {
            try
            {
                _db.Entry(TrxType).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TrxType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTrxType(TrxType TrxType)
        {
            try
            {
                var dbTrxType = await _db.TrxTypes.FindAsync(TrxType.TrxTypeId);

                if (dbTrxType == null)
                {
                    return (false, "TrxType could not be found");
                }

                _db.TrxTypes.Remove(TrxType);
                await _db.SaveChangesAsync();

                return (true, "TrxType got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TrxTypes
    }
}
