using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewLegalStatusService
    {
        // ViewLegalStatuss Services
        Task<List<ViewLegalStatus>> GetViewLegalStatusListByValue(int offset, int limit, string val); // GET All ViewLegalStatusss
        Task<ViewLegalStatus> GetViewLegalStatus(string ViewLegalStatus_name); // GET Single ViewLegalStatuss        
        Task<List<ViewLegalStatus>> GetViewLegalStatusList(string ViewLegalStatus_name); // GET List ViewLegalStatuss        
        Task<ViewLegalStatus> AddViewLegalStatus(ViewLegalStatus ViewLegalStatus); // POST New ViewLegalStatuss
        Task<ViewLegalStatus> UpdateViewLegalStatus(ViewLegalStatus ViewLegalStatus); // PUT ViewLegalStatuss
        Task<(bool, string)> DeleteViewLegalStatus(ViewLegalStatus ViewLegalStatus); // DELETE ViewLegalStatuss
    }

    public class ViewLegalStatusService : IViewLegalStatusService
    {
        private readonly XixsrvContext _db;

        public ViewLegalStatusService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewLegalStatuss

        public async Task<List<ViewLegalStatus>> GetViewLegalStatusListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewLegalStatuss.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewLegalStatus> GetViewLegalStatus(string ViewLegalStatus_id)
        {
            try
            {
                return await _db.ViewLegalStatuss.FindAsync(ViewLegalStatus_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewLegalStatus>> GetViewLegalStatusList(string ViewLegalStatus_id)
        {
            try
            {
                return await _db.ViewLegalStatuss
                    .Where(i => i.ViewLegalStatusId == ViewLegalStatus_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewLegalStatus> AddViewLegalStatus(ViewLegalStatus ViewLegalStatus)
        {
            try
            {
                await _db.ViewLegalStatuss.AddAsync(ViewLegalStatus);
                await _db.SaveChangesAsync();
                return await _db.ViewLegalStatuss.FindAsync(ViewLegalStatus.ViewLegalStatusId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewLegalStatus> UpdateViewLegalStatus(ViewLegalStatus ViewLegalStatus)
        {
            try
            {
                _db.Entry(ViewLegalStatus).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewLegalStatus;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewLegalStatus(ViewLegalStatus ViewLegalStatus)
        {
            try
            {
                var dbViewLegalStatus = await _db.ViewLegalStatuss.FindAsync(ViewLegalStatus.ViewLegalStatusId);

                if (dbViewLegalStatus == null)
                {
                    return (false, "ViewLegalStatus could not be found");
                }

                _db.ViewLegalStatuss.Remove(ViewLegalStatus);
                await _db.SaveChangesAsync();

                return (true, "ViewLegalStatus got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewLegalStatuss
    }
}
