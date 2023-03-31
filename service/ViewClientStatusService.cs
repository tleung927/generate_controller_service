using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewClientStatusService
    {
        // ViewClientStatuss Services
        Task<List<ViewClientStatus>> GetViewClientStatusListByValue(int offset, int limit, string val); // GET All ViewClientStatusss
        Task<ViewClientStatus> GetViewClientStatus(string ViewClientStatus_name); // GET Single ViewClientStatuss        
        Task<List<ViewClientStatus>> GetViewClientStatusList(string ViewClientStatus_name); // GET List ViewClientStatuss        
        Task<ViewClientStatus> AddViewClientStatus(ViewClientStatus ViewClientStatus); // POST New ViewClientStatuss
        Task<ViewClientStatus> UpdateViewClientStatus(ViewClientStatus ViewClientStatus); // PUT ViewClientStatuss
        Task<(bool, string)> DeleteViewClientStatus(ViewClientStatus ViewClientStatus); // DELETE ViewClientStatuss
    }

    public class ViewClientStatusService : IViewClientStatusService
    {
        private readonly XixsrvContext _db;

        public ViewClientStatusService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewClientStatuss

        public async Task<List<ViewClientStatus>> GetViewClientStatusListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewClientStatuss.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewClientStatus> GetViewClientStatus(string ViewClientStatus_id)
        {
            try
            {
                return await _db.ViewClientStatuss.FindAsync(ViewClientStatus_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewClientStatus>> GetViewClientStatusList(string ViewClientStatus_id)
        {
            try
            {
                return await _db.ViewClientStatuss
                    .Where(i => i.ViewClientStatusId == ViewClientStatus_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewClientStatus> AddViewClientStatus(ViewClientStatus ViewClientStatus)
        {
            try
            {
                await _db.ViewClientStatuss.AddAsync(ViewClientStatus);
                await _db.SaveChangesAsync();
                return await _db.ViewClientStatuss.FindAsync(ViewClientStatus.ViewClientStatusId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewClientStatus> UpdateViewClientStatus(ViewClientStatus ViewClientStatus)
        {
            try
            {
                _db.Entry(ViewClientStatus).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewClientStatus;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewClientStatus(ViewClientStatus ViewClientStatus)
        {
            try
            {
                var dbViewClientStatus = await _db.ViewClientStatuss.FindAsync(ViewClientStatus.ViewClientStatusId);

                if (dbViewClientStatus == null)
                {
                    return (false, "ViewClientStatus could not be found");
                }

                _db.ViewClientStatuss.Remove(ViewClientStatus);
                await _db.SaveChangesAsync();

                return (true, "ViewClientStatus got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewClientStatuss
    }
}
