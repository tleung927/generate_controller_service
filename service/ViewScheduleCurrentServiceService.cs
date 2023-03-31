using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleCurrentServiceService
    {
        // ViewScheduleCurrentServices Services
        Task<List<ViewScheduleCurrentService>> GetViewScheduleCurrentServiceListByValue(int offset, int limit, string val); // GET All ViewScheduleCurrentServicess
        Task<ViewScheduleCurrentService> GetViewScheduleCurrentService(string ViewScheduleCurrentService_name); // GET Single ViewScheduleCurrentServices        
        Task<List<ViewScheduleCurrentService>> GetViewScheduleCurrentServiceList(string ViewScheduleCurrentService_name); // GET List ViewScheduleCurrentServices        
        Task<ViewScheduleCurrentService> AddViewScheduleCurrentService(ViewScheduleCurrentService ViewScheduleCurrentService); // POST New ViewScheduleCurrentServices
        Task<ViewScheduleCurrentService> UpdateViewScheduleCurrentService(ViewScheduleCurrentService ViewScheduleCurrentService); // PUT ViewScheduleCurrentServices
        Task<(bool, string)> DeleteViewScheduleCurrentService(ViewScheduleCurrentService ViewScheduleCurrentService); // DELETE ViewScheduleCurrentServices
    }

    public class ViewScheduleCurrentServiceService : IViewScheduleCurrentServiceService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleCurrentServiceService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleCurrentServices

        public async Task<List<ViewScheduleCurrentService>> GetViewScheduleCurrentServiceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleCurrentServices.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleCurrentService> GetViewScheduleCurrentService(string ViewScheduleCurrentService_id)
        {
            try
            {
                return await _db.ViewScheduleCurrentServices.FindAsync(ViewScheduleCurrentService_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleCurrentService>> GetViewScheduleCurrentServiceList(string ViewScheduleCurrentService_id)
        {
            try
            {
                return await _db.ViewScheduleCurrentServices
                    .Where(i => i.ViewScheduleCurrentServiceId == ViewScheduleCurrentService_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleCurrentService> AddViewScheduleCurrentService(ViewScheduleCurrentService ViewScheduleCurrentService)
        {
            try
            {
                await _db.ViewScheduleCurrentServices.AddAsync(ViewScheduleCurrentService);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleCurrentServices.FindAsync(ViewScheduleCurrentService.ViewScheduleCurrentServiceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleCurrentService> UpdateViewScheduleCurrentService(ViewScheduleCurrentService ViewScheduleCurrentService)
        {
            try
            {
                _db.Entry(ViewScheduleCurrentService).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleCurrentService;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleCurrentService(ViewScheduleCurrentService ViewScheduleCurrentService)
        {
            try
            {
                var dbViewScheduleCurrentService = await _db.ViewScheduleCurrentServices.FindAsync(ViewScheduleCurrentService.ViewScheduleCurrentServiceId);

                if (dbViewScheduleCurrentService == null)
                {
                    return (false, "ViewScheduleCurrentService could not be found");
                }

                _db.ViewScheduleCurrentServices.Remove(ViewScheduleCurrentService);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleCurrentService got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleCurrentServices
    }
}
