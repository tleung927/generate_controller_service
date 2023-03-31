using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclicdeCder08Service
    {
        // SclicdeCder08s Services
        Task<List<SclicdeCder08>> GetSclicdeCder08ListByValue(int offset, int limit, string val); // GET All SclicdeCder08ss
        Task<SclicdeCder08> GetSclicdeCder08(string SclicdeCder08_name); // GET Single SclicdeCder08s        
        Task<List<SclicdeCder08>> GetSclicdeCder08List(string SclicdeCder08_name); // GET List SclicdeCder08s        
        Task<SclicdeCder08> AddSclicdeCder08(SclicdeCder08 SclicdeCder08); // POST New SclicdeCder08s
        Task<SclicdeCder08> UpdateSclicdeCder08(SclicdeCder08 SclicdeCder08); // PUT SclicdeCder08s
        Task<(bool, string)> DeleteSclicdeCder08(SclicdeCder08 SclicdeCder08); // DELETE SclicdeCder08s
    }

    public class SclicdeCder08Service : ISclicdeCder08Service
    {
        private readonly XixsrvContext _db;

        public SclicdeCder08Service(XixsrvContext db)
        {
            _db = db;
        }

        #region SclicdeCder08s

        public async Task<List<SclicdeCder08>> GetSclicdeCder08ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SclicdeCder08s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SclicdeCder08> GetSclicdeCder08(string SclicdeCder08_id)
        {
            try
            {
                return await _db.SclicdeCder08s.FindAsync(SclicdeCder08_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SclicdeCder08>> GetSclicdeCder08List(string SclicdeCder08_id)
        {
            try
            {
                return await _db.SclicdeCder08s
                    .Where(i => i.SclicdeCder08Id == SclicdeCder08_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SclicdeCder08> AddSclicdeCder08(SclicdeCder08 SclicdeCder08)
        {
            try
            {
                await _db.SclicdeCder08s.AddAsync(SclicdeCder08);
                await _db.SaveChangesAsync();
                return await _db.SclicdeCder08s.FindAsync(SclicdeCder08.SclicdeCder08Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SclicdeCder08> UpdateSclicdeCder08(SclicdeCder08 SclicdeCder08)
        {
            try
            {
                _db.Entry(SclicdeCder08).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SclicdeCder08;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclicdeCder08(SclicdeCder08 SclicdeCder08)
        {
            try
            {
                var dbSclicdeCder08 = await _db.SclicdeCder08s.FindAsync(SclicdeCder08.SclicdeCder08Id);

                if (dbSclicdeCder08 == null)
                {
                    return (false, "SclicdeCder08 could not be found");
                }

                _db.SclicdeCder08s.Remove(SclicdeCder08);
                await _db.SaveChangesAsync();

                return (true, "SclicdeCder08 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SclicdeCder08s
    }
}
