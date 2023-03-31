using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewDayProgramSchoolService
    {
        // ViewDayProgramSchools Services
        Task<List<ViewDayProgramSchool>> GetViewDayProgramSchoolListByValue(int offset, int limit, string val); // GET All ViewDayProgramSchoolss
        Task<ViewDayProgramSchool> GetViewDayProgramSchool(string ViewDayProgramSchool_name); // GET Single ViewDayProgramSchools        
        Task<List<ViewDayProgramSchool>> GetViewDayProgramSchoolList(string ViewDayProgramSchool_name); // GET List ViewDayProgramSchools        
        Task<ViewDayProgramSchool> AddViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool); // POST New ViewDayProgramSchools
        Task<ViewDayProgramSchool> UpdateViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool); // PUT ViewDayProgramSchools
        Task<(bool, string)> DeleteViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool); // DELETE ViewDayProgramSchools
    }

    public class ViewDayProgramSchoolService : IViewDayProgramSchoolService
    {
        private readonly XixsrvContext _db;

        public ViewDayProgramSchoolService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewDayProgramSchools

        public async Task<List<ViewDayProgramSchool>> GetViewDayProgramSchoolListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewDayProgramSchools.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewDayProgramSchool> GetViewDayProgramSchool(string ViewDayProgramSchool_id)
        {
            try
            {
                return await _db.ViewDayProgramSchools.FindAsync(ViewDayProgramSchool_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewDayProgramSchool>> GetViewDayProgramSchoolList(string ViewDayProgramSchool_id)
        {
            try
            {
                return await _db.ViewDayProgramSchools
                    .Where(i => i.ViewDayProgramSchoolId == ViewDayProgramSchool_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewDayProgramSchool> AddViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool)
        {
            try
            {
                await _db.ViewDayProgramSchools.AddAsync(ViewDayProgramSchool);
                await _db.SaveChangesAsync();
                return await _db.ViewDayProgramSchools.FindAsync(ViewDayProgramSchool.ViewDayProgramSchoolId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewDayProgramSchool> UpdateViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool)
        {
            try
            {
                _db.Entry(ViewDayProgramSchool).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewDayProgramSchool;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool)
        {
            try
            {
                var dbViewDayProgramSchool = await _db.ViewDayProgramSchools.FindAsync(ViewDayProgramSchool.ViewDayProgramSchoolId);

                if (dbViewDayProgramSchool == null)
                {
                    return (false, "ViewDayProgramSchool could not be found");
                }

                _db.ViewDayProgramSchools.Remove(ViewDayProgramSchool);
                await _db.SaveChangesAsync();

                return (true, "ViewDayProgramSchool got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewDayProgramSchools
    }
}
