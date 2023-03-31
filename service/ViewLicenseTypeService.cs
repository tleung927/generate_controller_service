using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewLicenseTypeService
    {
        // ViewLicenseTypes Services
        Task<List<ViewLicenseType>> GetViewLicenseTypeListByValue(int offset, int limit, string val); // GET All ViewLicenseTypess
        Task<ViewLicenseType> GetViewLicenseType(string ViewLicenseType_name); // GET Single ViewLicenseTypes        
        Task<List<ViewLicenseType>> GetViewLicenseTypeList(string ViewLicenseType_name); // GET List ViewLicenseTypes        
        Task<ViewLicenseType> AddViewLicenseType(ViewLicenseType ViewLicenseType); // POST New ViewLicenseTypes
        Task<ViewLicenseType> UpdateViewLicenseType(ViewLicenseType ViewLicenseType); // PUT ViewLicenseTypes
        Task<(bool, string)> DeleteViewLicenseType(ViewLicenseType ViewLicenseType); // DELETE ViewLicenseTypes
    }

    public class ViewLicenseTypeService : IViewLicenseTypeService
    {
        private readonly XixsrvContext _db;

        public ViewLicenseTypeService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewLicenseTypes

        public async Task<List<ViewLicenseType>> GetViewLicenseTypeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewLicenseTypes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewLicenseType> GetViewLicenseType(string ViewLicenseType_id)
        {
            try
            {
                return await _db.ViewLicenseTypes.FindAsync(ViewLicenseType_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewLicenseType>> GetViewLicenseTypeList(string ViewLicenseType_id)
        {
            try
            {
                return await _db.ViewLicenseTypes
                    .Where(i => i.ViewLicenseTypeId == ViewLicenseType_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewLicenseType> AddViewLicenseType(ViewLicenseType ViewLicenseType)
        {
            try
            {
                await _db.ViewLicenseTypes.AddAsync(ViewLicenseType);
                await _db.SaveChangesAsync();
                return await _db.ViewLicenseTypes.FindAsync(ViewLicenseType.ViewLicenseTypeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewLicenseType> UpdateViewLicenseType(ViewLicenseType ViewLicenseType)
        {
            try
            {
                _db.Entry(ViewLicenseType).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewLicenseType;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewLicenseType(ViewLicenseType ViewLicenseType)
        {
            try
            {
                var dbViewLicenseType = await _db.ViewLicenseTypes.FindAsync(ViewLicenseType.ViewLicenseTypeId);

                if (dbViewLicenseType == null)
                {
                    return (false, "ViewLicenseType could not be found");
                }

                _db.ViewLicenseTypes.Remove(ViewLicenseType);
                await _db.SaveChangesAsync();

                return (true, "ViewLicenseType got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewLicenseTypes
    }
}
