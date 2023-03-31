using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclirelDobService
    {
        // SclirelDobs Services
        Task<List<SclirelDob>> GetSclirelDobListByValue(int offset, int limit, string val); // GET All SclirelDobss
        Task<SclirelDob> GetSclirelDob(string SclirelDob_name); // GET Single SclirelDobs        
        Task<List<SclirelDob>> GetSclirelDobList(string SclirelDob_name); // GET List SclirelDobs        
        Task<SclirelDob> AddSclirelDob(SclirelDob SclirelDob); // POST New SclirelDobs
        Task<SclirelDob> UpdateSclirelDob(SclirelDob SclirelDob); // PUT SclirelDobs
        Task<(bool, string)> DeleteSclirelDob(SclirelDob SclirelDob); // DELETE SclirelDobs
    }

    public class SclirelDobService : ISclirelDobService
    {
        private readonly XixsrvContext _db;

        public SclirelDobService(XixsrvContext db)
        {
            _db = db;
        }

        #region SclirelDobs

        public async Task<List<SclirelDob>> GetSclirelDobListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SclirelDobs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SclirelDob> GetSclirelDob(string SclirelDob_id)
        {
            try
            {
                return await _db.SclirelDobs.FindAsync(SclirelDob_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SclirelDob>> GetSclirelDobList(string SclirelDob_id)
        {
            try
            {
                return await _db.SclirelDobs
                    .Where(i => i.SclirelDobId == SclirelDob_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SclirelDob> AddSclirelDob(SclirelDob SclirelDob)
        {
            try
            {
                await _db.SclirelDobs.AddAsync(SclirelDob);
                await _db.SaveChangesAsync();
                return await _db.SclirelDobs.FindAsync(SclirelDob.SclirelDobId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SclirelDob> UpdateSclirelDob(SclirelDob SclirelDob)
        {
            try
            {
                _db.Entry(SclirelDob).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SclirelDob;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclirelDob(SclirelDob SclirelDob)
        {
            try
            {
                var dbSclirelDob = await _db.SclirelDobs.FindAsync(SclirelDob.SclirelDobId);

                if (dbSclirelDob == null)
                {
                    return (false, "SclirelDob could not be found");
                }

                _db.SclirelDobs.Remove(SclirelDob);
                await _db.SaveChangesAsync();

                return (true, "SclirelDob got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SclirelDobs
    }
}
