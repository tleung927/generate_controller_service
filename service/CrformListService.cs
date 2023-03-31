using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICrformListService
    {
        // CrformLists Services
        Task<List<CrformList>> GetCrformListListByValue(int offset, int limit, string val); // GET All CrformListss
        Task<CrformList> GetCrformList(string CrformList_name); // GET Single CrformLists        
        Task<List<CrformList>> GetCrformListList(string CrformList_name); // GET List CrformLists        
        Task<CrformList> AddCrformList(CrformList CrformList); // POST New CrformLists
        Task<CrformList> UpdateCrformList(CrformList CrformList); // PUT CrformLists
        Task<(bool, string)> DeleteCrformList(CrformList CrformList); // DELETE CrformLists
    }

    public class CrformListService : ICrformListService
    {
        private readonly XixsrvContext _db;

        public CrformListService(XixsrvContext db)
        {
            _db = db;
        }

        #region CrformLists

        public async Task<List<CrformList>> GetCrformListListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.CrformLists.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<CrformList> GetCrformList(string CrformList_id)
        {
            try
            {
                return await _db.CrformLists.FindAsync(CrformList_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<CrformList>> GetCrformListList(string CrformList_id)
        {
            try
            {
                return await _db.CrformLists
                    .Where(i => i.CrformListId == CrformList_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<CrformList> AddCrformList(CrformList CrformList)
        {
            try
            {
                await _db.CrformLists.AddAsync(CrformList);
                await _db.SaveChangesAsync();
                return await _db.CrformLists.FindAsync(CrformList.CrformListId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<CrformList> UpdateCrformList(CrformList CrformList)
        {
            try
            {
                _db.Entry(CrformList).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return CrformList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCrformList(CrformList CrformList)
        {
            try
            {
                var dbCrformList = await _db.CrformLists.FindAsync(CrformList.CrformListId);

                if (dbCrformList == null)
                {
                    return (false, "CrformList could not be found");
                }

                _db.CrformLists.Remove(CrformList);
                await _db.SaveChangesAsync();

                return (true, "CrformList got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion CrformLists
    }
}
