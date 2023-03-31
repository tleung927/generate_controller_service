using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRtrxTypeService
    {
        // RtrxTypes Services
        Task<List<RtrxType>> GetRtrxTypeListByValue(int offset, int limit, string val); // GET All RtrxTypess
        Task<RtrxType> GetRtrxType(string RtrxType_name); // GET Single RtrxTypes        
        Task<List<RtrxType>> GetRtrxTypeList(string RtrxType_name); // GET List RtrxTypes        
        Task<RtrxType> AddRtrxType(RtrxType RtrxType); // POST New RtrxTypes
        Task<RtrxType> UpdateRtrxType(RtrxType RtrxType); // PUT RtrxTypes
        Task<(bool, string)> DeleteRtrxType(RtrxType RtrxType); // DELETE RtrxTypes
    }

    public class RtrxTypeService : IRtrxTypeService
    {
        private readonly XixsrvContext _db;

        public RtrxTypeService(XixsrvContext db)
        {
            _db = db;
        }

        #region RtrxTypes

        public async Task<List<RtrxType>> GetRtrxTypeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.RtrxTypes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<RtrxType> GetRtrxType(string RtrxType_id)
        {
            try
            {
                return await _db.RtrxTypes.FindAsync(RtrxType_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<RtrxType>> GetRtrxTypeList(string RtrxType_id)
        {
            try
            {
                return await _db.RtrxTypes
                    .Where(i => i.RtrxTypeId == RtrxType_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<RtrxType> AddRtrxType(RtrxType RtrxType)
        {
            try
            {
                await _db.RtrxTypes.AddAsync(RtrxType);
                await _db.SaveChangesAsync();
                return await _db.RtrxTypes.FindAsync(RtrxType.RtrxTypeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<RtrxType> UpdateRtrxType(RtrxType RtrxType)
        {
            try
            {
                _db.Entry(RtrxType).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return RtrxType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRtrxType(RtrxType RtrxType)
        {
            try
            {
                var dbRtrxType = await _db.RtrxTypes.FindAsync(RtrxType.RtrxTypeId);

                if (dbRtrxType == null)
                {
                    return (false, "RtrxType could not be found");
                }

                _db.RtrxTypes.Remove(RtrxType);
                await _db.SaveChangesAsync();

                return (true, "RtrxType got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion RtrxTypes
    }
}
