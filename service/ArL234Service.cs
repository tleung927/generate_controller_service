using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IArL234Service
    {
        // ArL234s Services
        Task<List<ArL234>> GetArL234ListByValue(int offset, int limit, string val); // GET All ArL234ss
        Task<ArL234> GetArL234(string ArL234_name); // GET Single ArL234s        
        Task<List<ArL234>> GetArL234List(string ArL234_name); // GET List ArL234s        
        Task<ArL234> AddArL234(ArL234 ArL234); // POST New ArL234s
        Task<ArL234> UpdateArL234(ArL234 ArL234); // PUT ArL234s
        Task<(bool, string)> DeleteArL234(ArL234 ArL234); // DELETE ArL234s
    }

    public class ArL234Service : IArL234Service
    {
        private readonly XixsrvContext _db;

        public ArL234Service(XixsrvContext db)
        {
            _db = db;
        }

        #region ArL234s

        public async Task<List<ArL234>> GetArL234ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ArL234s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ArL234> GetArL234(string ArL234_id)
        {
            try
            {
                return await _db.ArL234s.FindAsync(ArL234_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ArL234>> GetArL234List(string ArL234_id)
        {
            try
            {
                return await _db.ArL234s
                    .Where(i => i.ArL234Id == ArL234_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ArL234> AddArL234(ArL234 ArL234)
        {
            try
            {
                await _db.ArL234s.AddAsync(ArL234);
                await _db.SaveChangesAsync();
                return await _db.ArL234s.FindAsync(ArL234.ArL234Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ArL234> UpdateArL234(ArL234 ArL234)
        {
            try
            {
                _db.Entry(ArL234).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ArL234;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteArL234(ArL234 ArL234)
        {
            try
            {
                var dbArL234 = await _db.ArL234s.FindAsync(ArL234.ArL234Id);

                if (dbArL234 == null)
                {
                    return (false, "ArL234 could not be found");
                }

                _db.ArL234s.Remove(ArL234);
                await _db.SaveChangesAsync();

                return (true, "ArL234 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ArL234s
    }
}
