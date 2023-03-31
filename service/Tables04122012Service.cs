using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITables04122012Service
    {
        // Tables04122012s Services
        Task<List<Tables04122012>> GetTables04122012ListByValue(int offset, int limit, string val); // GET All Tables04122012ss
        Task<Tables04122012> GetTables04122012(string Tables04122012_name); // GET Single Tables04122012s        
        Task<List<Tables04122012>> GetTables04122012List(string Tables04122012_name); // GET List Tables04122012s        
        Task<Tables04122012> AddTables04122012(Tables04122012 Tables04122012); // POST New Tables04122012s
        Task<Tables04122012> UpdateTables04122012(Tables04122012 Tables04122012); // PUT Tables04122012s
        Task<(bool, string)> DeleteTables04122012(Tables04122012 Tables04122012); // DELETE Tables04122012s
    }

    public class Tables04122012Service : ITables04122012Service
    {
        private readonly XixsrvContext _db;

        public Tables04122012Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Tables04122012s

        public async Task<List<Tables04122012>> GetTables04122012ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Tables04122012s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Tables04122012> GetTables04122012(string Tables04122012_id)
        {
            try
            {
                return await _db.Tables04122012s.FindAsync(Tables04122012_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Tables04122012>> GetTables04122012List(string Tables04122012_id)
        {
            try
            {
                return await _db.Tables04122012s
                    .Where(i => i.Tables04122012Id == Tables04122012_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Tables04122012> AddTables04122012(Tables04122012 Tables04122012)
        {
            try
            {
                await _db.Tables04122012s.AddAsync(Tables04122012);
                await _db.SaveChangesAsync();
                return await _db.Tables04122012s.FindAsync(Tables04122012.Tables04122012Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Tables04122012> UpdateTables04122012(Tables04122012 Tables04122012)
        {
            try
            {
                _db.Entry(Tables04122012).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Tables04122012;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTables04122012(Tables04122012 Tables04122012)
        {
            try
            {
                var dbTables04122012 = await _db.Tables04122012s.FindAsync(Tables04122012.Tables04122012Id);

                if (dbTables04122012 == null)
                {
                    return (false, "Tables04122012 could not be found");
                }

                _db.Tables04122012s.Remove(Tables04122012);
                await _db.SaveChangesAsync();

                return (true, "Tables04122012 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Tables04122012s
    }
}
