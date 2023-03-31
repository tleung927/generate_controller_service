using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleServiceDescService
    {
        // ViewScheduleServiceDescs Services
        Task<List<ViewScheduleServiceDesc>> GetViewScheduleServiceDescListByValue(int offset, int limit, string val); // GET All ViewScheduleServiceDescss
        Task<ViewScheduleServiceDesc> GetViewScheduleServiceDesc(string ViewScheduleServiceDesc_name); // GET Single ViewScheduleServiceDescs        
        Task<List<ViewScheduleServiceDesc>> GetViewScheduleServiceDescList(string ViewScheduleServiceDesc_name); // GET List ViewScheduleServiceDescs        
        Task<ViewScheduleServiceDesc> AddViewScheduleServiceDesc(ViewScheduleServiceDesc ViewScheduleServiceDesc); // POST New ViewScheduleServiceDescs
        Task<ViewScheduleServiceDesc> UpdateViewScheduleServiceDesc(ViewScheduleServiceDesc ViewScheduleServiceDesc); // PUT ViewScheduleServiceDescs
        Task<(bool, string)> DeleteViewScheduleServiceDesc(ViewScheduleServiceDesc ViewScheduleServiceDesc); // DELETE ViewScheduleServiceDescs
    }

    public class ViewScheduleServiceDescService : IViewScheduleServiceDescService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleServiceDescService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleServiceDescs

        public async Task<List<ViewScheduleServiceDesc>> GetViewScheduleServiceDescListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleServiceDescs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleServiceDesc> GetViewScheduleServiceDesc(string ViewScheduleServiceDesc_id)
        {
            try
            {
                return await _db.ViewScheduleServiceDescs.FindAsync(ViewScheduleServiceDesc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleServiceDesc>> GetViewScheduleServiceDescList(string ViewScheduleServiceDesc_id)
        {
            try
            {
                return await _db.ViewScheduleServiceDescs
                    .Where(i => i.ViewScheduleServiceDescId == ViewScheduleServiceDesc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleServiceDesc> AddViewScheduleServiceDesc(ViewScheduleServiceDesc ViewScheduleServiceDesc)
        {
            try
            {
                await _db.ViewScheduleServiceDescs.AddAsync(ViewScheduleServiceDesc);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleServiceDescs.FindAsync(ViewScheduleServiceDesc.ViewScheduleServiceDescId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleServiceDesc> UpdateViewScheduleServiceDesc(ViewScheduleServiceDesc ViewScheduleServiceDesc)
        {
            try
            {
                _db.Entry(ViewScheduleServiceDesc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleServiceDesc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleServiceDesc(ViewScheduleServiceDesc ViewScheduleServiceDesc)
        {
            try
            {
                var dbViewScheduleServiceDesc = await _db.ViewScheduleServiceDescs.FindAsync(ViewScheduleServiceDesc.ViewScheduleServiceDescId);

                if (dbViewScheduleServiceDesc == null)
                {
                    return (false, "ViewScheduleServiceDesc could not be found");
                }

                _db.ViewScheduleServiceDescs.Remove(ViewScheduleServiceDesc);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleServiceDesc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleServiceDescs
    }
}
