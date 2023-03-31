using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewStatusService
    {
        // ViewStatuss Services
        Task<List<ViewStatus>> GetViewStatusListByValue(int offset, int limit, string val); // GET All ViewStatusss
        Task<ViewStatus> GetViewStatus(string ViewStatus_name); // GET Single ViewStatuss        
        Task<List<ViewStatus>> GetViewStatusList(string ViewStatus_name); // GET List ViewStatuss        
        Task<ViewStatus> AddViewStatus(ViewStatus ViewStatus); // POST New ViewStatuss
        Task<ViewStatus> UpdateViewStatus(ViewStatus ViewStatus); // PUT ViewStatuss
        Task<(bool, string)> DeleteViewStatus(ViewStatus ViewStatus); // DELETE ViewStatuss
    }

    public class ViewStatusService : IViewStatusService
    {
        private readonly XixsrvContext _db;

        public ViewStatusService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewStatuss

        public async Task<List<ViewStatus>> GetViewStatusListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewStatuss.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewStatus> GetViewStatus(string ViewStatus_id)
        {
            try
            {
                return await _db.ViewStatuss.FindAsync(ViewStatus_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewStatus>> GetViewStatusList(string ViewStatus_id)
        {
            try
            {
                return await _db.ViewStatuss
                    .Where(i => i.ViewStatusId == ViewStatus_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewStatus> AddViewStatus(ViewStatus ViewStatus)
        {
            try
            {
                await _db.ViewStatuss.AddAsync(ViewStatus);
                await _db.SaveChangesAsync();
                return await _db.ViewStatuss.FindAsync(ViewStatus.ViewStatusId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewStatus> UpdateViewStatus(ViewStatus ViewStatus)
        {
            try
            {
                _db.Entry(ViewStatus).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewStatus;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewStatus(ViewStatus ViewStatus)
        {
            try
            {
                var dbViewStatus = await _db.ViewStatuss.FindAsync(ViewStatus.ViewStatusId);

                if (dbViewStatus == null)
                {
                    return (false, "ViewStatus could not be found");
                }

                _db.ViewStatuss.Remove(ViewStatus);
                await _db.SaveChangesAsync();

                return (true, "ViewStatus got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewStatuss
    }
}
