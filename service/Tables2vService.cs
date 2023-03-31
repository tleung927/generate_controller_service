using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITables2vService
    {
        // Tables2vs Services
        Task<List<Tables2v>> GetTables2vListByValue(int offset, int limit, string val); // GET All Tables2vss
        Task<Tables2v> GetTables2v(string Tables2v_name); // GET Single Tables2vs        
        Task<List<Tables2v>> GetTables2vList(string Tables2v_name); // GET List Tables2vs        
        Task<Tables2v> AddTables2v(Tables2v Tables2v); // POST New Tables2vs
        Task<Tables2v> UpdateTables2v(Tables2v Tables2v); // PUT Tables2vs
        Task<(bool, string)> DeleteTables2v(Tables2v Tables2v); // DELETE Tables2vs
    }

    public class Tables2vService : ITables2vService
    {
        private readonly XixsrvContext _db;

        public Tables2vService(XixsrvContext db)
        {
            _db = db;
        }

        #region Tables2vs

        public async Task<List<Tables2v>> GetTables2vListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Tables2vs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Tables2v> GetTables2v(string Tables2v_id)
        {
            try
            {
                return await _db.Tables2vs.FindAsync(Tables2v_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Tables2v>> GetTables2vList(string Tables2v_id)
        {
            try
            {
                return await _db.Tables2vs
                    .Where(i => i.Tables2vId == Tables2v_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Tables2v> AddTables2v(Tables2v Tables2v)
        {
            try
            {
                await _db.Tables2vs.AddAsync(Tables2v);
                await _db.SaveChangesAsync();
                return await _db.Tables2vs.FindAsync(Tables2v.Tables2vId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Tables2v> UpdateTables2v(Tables2v Tables2v)
        {
            try
            {
                _db.Entry(Tables2v).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Tables2v;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTables2v(Tables2v Tables2v)
        {
            try
            {
                var dbTables2v = await _db.Tables2vs.FindAsync(Tables2v.Tables2vId);

                if (dbTables2v == null)
                {
                    return (false, "Tables2v could not be found");
                }

                _db.Tables2vs.Remove(Tables2v);
                await _db.SaveChangesAsync();

                return (true, "Tables2v got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Tables2vs
    }
}
