using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDownloadListService
    {
        // DownloadLists Services
        Task<List<DownloadList>> GetDownloadListListByValue(int offset, int limit, string val); // GET All DownloadListss
        Task<DownloadList> GetDownloadList(string DownloadList_name); // GET Single DownloadLists        
        Task<List<DownloadList>> GetDownloadListList(string DownloadList_name); // GET List DownloadLists        
        Task<DownloadList> AddDownloadList(DownloadList DownloadList); // POST New DownloadLists
        Task<DownloadList> UpdateDownloadList(DownloadList DownloadList); // PUT DownloadLists
        Task<(bool, string)> DeleteDownloadList(DownloadList DownloadList); // DELETE DownloadLists
    }

    public class DownloadListService : IDownloadListService
    {
        private readonly XixsrvContext _db;

        public DownloadListService(XixsrvContext db)
        {
            _db = db;
        }

        #region DownloadLists

        public async Task<List<DownloadList>> GetDownloadListListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DownloadLists.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DownloadList> GetDownloadList(string DownloadList_id)
        {
            try
            {
                return await _db.DownloadLists.FindAsync(DownloadList_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DownloadList>> GetDownloadListList(string DownloadList_id)
        {
            try
            {
                return await _db.DownloadLists
                    .Where(i => i.DownloadListId == DownloadList_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DownloadList> AddDownloadList(DownloadList DownloadList)
        {
            try
            {
                await _db.DownloadLists.AddAsync(DownloadList);
                await _db.SaveChangesAsync();
                return await _db.DownloadLists.FindAsync(DownloadList.DownloadListId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DownloadList> UpdateDownloadList(DownloadList DownloadList)
        {
            try
            {
                _db.Entry(DownloadList).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DownloadList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDownloadList(DownloadList DownloadList)
        {
            try
            {
                var dbDownloadList = await _db.DownloadLists.FindAsync(DownloadList.DownloadListId);

                if (dbDownloadList == null)
                {
                    return (false, "DownloadList could not be found");
                }

                _db.DownloadLists.Remove(DownloadList);
                await _db.SaveChangesAsync();

                return (true, "DownloadList got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DownloadLists
    }
}
