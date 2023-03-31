using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISchoolDistrictService
    {
        // SchoolDistricts Services
        Task<List<SchoolDistrict>> GetSchoolDistrictListByValue(int offset, int limit, string val); // GET All SchoolDistrictss
        Task<SchoolDistrict> GetSchoolDistrict(string SchoolDistrict_name); // GET Single SchoolDistricts        
        Task<List<SchoolDistrict>> GetSchoolDistrictList(string SchoolDistrict_name); // GET List SchoolDistricts        
        Task<SchoolDistrict> AddSchoolDistrict(SchoolDistrict SchoolDistrict); // POST New SchoolDistricts
        Task<SchoolDistrict> UpdateSchoolDistrict(SchoolDistrict SchoolDistrict); // PUT SchoolDistricts
        Task<(bool, string)> DeleteSchoolDistrict(SchoolDistrict SchoolDistrict); // DELETE SchoolDistricts
    }

    public class SchoolDistrictService : ISchoolDistrictService
    {
        private readonly XixsrvContext _db;

        public SchoolDistrictService(XixsrvContext db)
        {
            _db = db;
        }

        #region SchoolDistricts

        public async Task<List<SchoolDistrict>> GetSchoolDistrictListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SchoolDistricts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SchoolDistrict> GetSchoolDistrict(string SchoolDistrict_id)
        {
            try
            {
                return await _db.SchoolDistricts.FindAsync(SchoolDistrict_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SchoolDistrict>> GetSchoolDistrictList(string SchoolDistrict_id)
        {
            try
            {
                return await _db.SchoolDistricts
                    .Where(i => i.SchoolDistrictId == SchoolDistrict_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SchoolDistrict> AddSchoolDistrict(SchoolDistrict SchoolDistrict)
        {
            try
            {
                await _db.SchoolDistricts.AddAsync(SchoolDistrict);
                await _db.SaveChangesAsync();
                return await _db.SchoolDistricts.FindAsync(SchoolDistrict.SchoolDistrictId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SchoolDistrict> UpdateSchoolDistrict(SchoolDistrict SchoolDistrict)
        {
            try
            {
                _db.Entry(SchoolDistrict).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SchoolDistrict;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSchoolDistrict(SchoolDistrict SchoolDistrict)
        {
            try
            {
                var dbSchoolDistrict = await _db.SchoolDistricts.FindAsync(SchoolDistrict.SchoolDistrictId);

                if (dbSchoolDistrict == null)
                {
                    return (false, "SchoolDistrict could not be found");
                }

                _db.SchoolDistricts.Remove(SchoolDistrict);
                await _db.SaveChangesAsync();

                return (true, "SchoolDistrict got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SchoolDistricts
    }
}
