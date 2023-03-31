using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewMaritalStatusService
    {
        // ViewMaritalStatuss Services
        Task<List<ViewMaritalStatus>> GetViewMaritalStatusListByValue(int offset, int limit, string val); // GET All ViewMaritalStatusss
        Task<ViewMaritalStatus> GetViewMaritalStatus(string ViewMaritalStatus_name); // GET Single ViewMaritalStatuss        
        Task<List<ViewMaritalStatus>> GetViewMaritalStatusList(string ViewMaritalStatus_name); // GET List ViewMaritalStatuss        
        Task<ViewMaritalStatus> AddViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus); // POST New ViewMaritalStatuss
        Task<ViewMaritalStatus> UpdateViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus); // PUT ViewMaritalStatuss
        Task<(bool, string)> DeleteViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus); // DELETE ViewMaritalStatuss
    }

    public class ViewMaritalStatusService : IViewMaritalStatusService
    {
        private readonly XixsrvContext _db;

        public ViewMaritalStatusService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewMaritalStatuss

        public async Task<List<ViewMaritalStatus>> GetViewMaritalStatusListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewMaritalStatuss.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewMaritalStatus> GetViewMaritalStatus(string ViewMaritalStatus_id)
        {
            try
            {
                return await _db.ViewMaritalStatuss.FindAsync(ViewMaritalStatus_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewMaritalStatus>> GetViewMaritalStatusList(string ViewMaritalStatus_id)
        {
            try
            {
                return await _db.ViewMaritalStatuss
                    .Where(i => i.ViewMaritalStatusId == ViewMaritalStatus_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewMaritalStatus> AddViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus)
        {
            try
            {
                await _db.ViewMaritalStatuss.AddAsync(ViewMaritalStatus);
                await _db.SaveChangesAsync();
                return await _db.ViewMaritalStatuss.FindAsync(ViewMaritalStatus.ViewMaritalStatusId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewMaritalStatus> UpdateViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus)
        {
            try
            {
                _db.Entry(ViewMaritalStatus).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewMaritalStatus;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus)
        {
            try
            {
                var dbViewMaritalStatus = await _db.ViewMaritalStatuss.FindAsync(ViewMaritalStatus.ViewMaritalStatusId);

                if (dbViewMaritalStatus == null)
                {
                    return (false, "ViewMaritalStatus could not be found");
                }

                _db.ViewMaritalStatuss.Remove(ViewMaritalStatus);
                await _db.SaveChangesAsync();

                return (true, "ViewMaritalStatus got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewMaritalStatuss
    }
}
