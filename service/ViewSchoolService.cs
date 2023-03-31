using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewSchoolService
    {
        // ViewSchools Services
        Task<List<ViewSchool>> GetViewSchoolListByValue(int offset, int limit, string val); // GET All ViewSchoolss
        Task<ViewSchool> GetViewSchool(string ViewSchool_name); // GET Single ViewSchools        
        Task<List<ViewSchool>> GetViewSchoolList(string ViewSchool_name); // GET List ViewSchools        
        Task<ViewSchool> AddViewSchool(ViewSchool ViewSchool); // POST New ViewSchools
        Task<ViewSchool> UpdateViewSchool(ViewSchool ViewSchool); // PUT ViewSchools
        Task<(bool, string)> DeleteViewSchool(ViewSchool ViewSchool); // DELETE ViewSchools
    }

    public class ViewSchoolService : IViewSchoolService
    {
        private readonly XixsrvContext _db;

        public ViewSchoolService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewSchools

        public async Task<List<ViewSchool>> GetViewSchoolListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewSchools.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewSchool> GetViewSchool(string ViewSchool_id)
        {
            try
            {
                return await _db.ViewSchools.FindAsync(ViewSchool_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewSchool>> GetViewSchoolList(string ViewSchool_id)
        {
            try
            {
                return await _db.ViewSchools
                    .Where(i => i.ViewSchoolId == ViewSchool_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewSchool> AddViewSchool(ViewSchool ViewSchool)
        {
            try
            {
                await _db.ViewSchools.AddAsync(ViewSchool);
                await _db.SaveChangesAsync();
                return await _db.ViewSchools.FindAsync(ViewSchool.ViewSchoolId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewSchool> UpdateViewSchool(ViewSchool ViewSchool)
        {
            try
            {
                _db.Entry(ViewSchool).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewSchool;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewSchool(ViewSchool ViewSchool)
        {
            try
            {
                var dbViewSchool = await _db.ViewSchools.FindAsync(ViewSchool.ViewSchoolId);

                if (dbViewSchool == null)
                {
                    return (false, "ViewSchool could not be found");
                }

                _db.ViewSchools.Remove(ViewSchool);
                await _db.SaveChangesAsync();

                return (true, "ViewSchool got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewSchools
    }
}
