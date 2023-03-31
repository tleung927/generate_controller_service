using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITables2Service
    {
        // Tables2s Services
        Task<List<Tables2>> GetTables2ListByValue(int offset, int limit, string val); // GET All Tables2ss
        Task<Tables2> GetTables2(string Tables2_name); // GET Single Tables2s        
        Task<List<Tables2>> GetTables2List(string Tables2_name); // GET List Tables2s        
        Task<Tables2> AddTables2(Tables2 Tables2); // POST New Tables2s
        Task<Tables2> UpdateTables2(Tables2 Tables2); // PUT Tables2s
        Task<(bool, string)> DeleteTables2(Tables2 Tables2); // DELETE Tables2s
    }

    public class Tables2Service : ITables2Service
    {
        private readonly XixsrvContext _db;

        public Tables2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Tables2s

        public async Task<List<Tables2>> GetTables2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Tables2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Tables2> GetTables2(string Tables2_id)
        {
            try
            {
                return await _db.Tables2s.FindAsync(Tables2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Tables2>> GetTables2List(string Tables2_id)
        {
            try
            {
                return await _db.Tables2s
                    .Where(i => i.Tables2Id == Tables2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Tables2> AddTables2(Tables2 Tables2)
        {
            try
            {
                await _db.Tables2s.AddAsync(Tables2);
                await _db.SaveChangesAsync();
                return await _db.Tables2s.FindAsync(Tables2.Tables2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Tables2> UpdateTables2(Tables2 Tables2)
        {
            try
            {
                _db.Entry(Tables2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Tables2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTables2(Tables2 Tables2)
        {
            try
            {
                var dbTables2 = await _db.Tables2s.FindAsync(Tables2.Tables2Id);

                if (dbTables2 == null)
                {
                    return (false, "Tables2 could not be found");
                }

                _db.Tables2s.Remove(Tables2);
                await _db.SaveChangesAsync();

                return (true, "Tables2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Tables2s
    }
}
