using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IClassService
    {
        // Classs Services
        Task<List<Class>> GetClassListByValue(int offset, int limit, string val); // GET All Classss
        Task<Class> GetClass(string Class_name); // GET Single Classs        
        Task<List<Class>> GetClassList(string Class_name); // GET List Classs        
        Task<Class> AddClass(Class Class); // POST New Classs
        Task<Class> UpdateClass(Class Class); // PUT Classs
        Task<(bool, string)> DeleteClass(Class Class); // DELETE Classs
    }

    public class ClassService : IClassService
    {
        private readonly XixsrvContext _db;

        public ClassService(XixsrvContext db)
        {
            _db = db;
        }

        #region Classs

        public async Task<List<Class>> GetClassListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Classs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Class> GetClass(string Class_id)
        {
            try
            {
                return await _db.Classs.FindAsync(Class_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Class>> GetClassList(string Class_id)
        {
            try
            {
                return await _db.Classs
                    .Where(i => i.ClassId == Class_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Class> AddClass(Class Class)
        {
            try
            {
                await _db.Classs.AddAsync(Class);
                await _db.SaveChangesAsync();
                return await _db.Classs.FindAsync(Class.ClassId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Class> UpdateClass(Class Class)
        {
            try
            {
                _db.Entry(Class).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Class;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteClass(Class Class)
        {
            try
            {
                var dbClass = await _db.Classs.FindAsync(Class.ClassId);

                if (dbClass == null)
                {
                    return (false, "Class could not be found");
                }

                _db.Classs.Remove(Class);
                await _db.SaveChangesAsync();

                return (true, "Class got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Classs
    }
}
