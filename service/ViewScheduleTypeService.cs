using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewScheduleTypeService
    {
        // ViewScheduleTypes Services
        Task<List<ViewScheduleType>> GetViewScheduleTypeListByValue(int offset, int limit, string val); // GET All ViewScheduleTypess
        Task<ViewScheduleType> GetViewScheduleType(string ViewScheduleType_name); // GET Single ViewScheduleTypes        
        Task<List<ViewScheduleType>> GetViewScheduleTypeList(string ViewScheduleType_name); // GET List ViewScheduleTypes        
        Task<ViewScheduleType> AddViewScheduleType(ViewScheduleType ViewScheduleType); // POST New ViewScheduleTypes
        Task<ViewScheduleType> UpdateViewScheduleType(ViewScheduleType ViewScheduleType); // PUT ViewScheduleTypes
        Task<(bool, string)> DeleteViewScheduleType(ViewScheduleType ViewScheduleType); // DELETE ViewScheduleTypes
    }

    public class ViewScheduleTypeService : IViewScheduleTypeService
    {
        private readonly XixsrvContext _db;

        public ViewScheduleTypeService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewScheduleTypes

        public async Task<List<ViewScheduleType>> GetViewScheduleTypeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewScheduleTypes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewScheduleType> GetViewScheduleType(string ViewScheduleType_id)
        {
            try
            {
                return await _db.ViewScheduleTypes.FindAsync(ViewScheduleType_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewScheduleType>> GetViewScheduleTypeList(string ViewScheduleType_id)
        {
            try
            {
                return await _db.ViewScheduleTypes
                    .Where(i => i.ViewScheduleTypeId == ViewScheduleType_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewScheduleType> AddViewScheduleType(ViewScheduleType ViewScheduleType)
        {
            try
            {
                await _db.ViewScheduleTypes.AddAsync(ViewScheduleType);
                await _db.SaveChangesAsync();
                return await _db.ViewScheduleTypes.FindAsync(ViewScheduleType.ViewScheduleTypeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewScheduleType> UpdateViewScheduleType(ViewScheduleType ViewScheduleType)
        {
            try
            {
                _db.Entry(ViewScheduleType).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewScheduleType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewScheduleType(ViewScheduleType ViewScheduleType)
        {
            try
            {
                var dbViewScheduleType = await _db.ViewScheduleTypes.FindAsync(ViewScheduleType.ViewScheduleTypeId);

                if (dbViewScheduleType == null)
                {
                    return (false, "ViewScheduleType could not be found");
                }

                _db.ViewScheduleTypes.Remove(ViewScheduleType);
                await _db.SaveChangesAsync();

                return (true, "ViewScheduleType got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewScheduleTypes
    }
}
