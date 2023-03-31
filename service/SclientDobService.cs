using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclientDobService
    {
        // SclientDobs Services
        Task<List<SclientDob>> GetSclientDobListByValue(int offset, int limit, string val); // GET All SclientDobss
        Task<SclientDob> GetSclientDob(string SclientDob_name); // GET Single SclientDobs        
        Task<List<SclientDob>> GetSclientDobList(string SclientDob_name); // GET List SclientDobs        
        Task<SclientDob> AddSclientDob(SclientDob SclientDob); // POST New SclientDobs
        Task<SclientDob> UpdateSclientDob(SclientDob SclientDob); // PUT SclientDobs
        Task<(bool, string)> DeleteSclientDob(SclientDob SclientDob); // DELETE SclientDobs
    }

    public class SclientDobService : ISclientDobService
    {
        private readonly XixsrvContext _db;

        public SclientDobService(XixsrvContext db)
        {
            _db = db;
        }

        #region SclientDobs

        public async Task<List<SclientDob>> GetSclientDobListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SclientDobs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SclientDob> GetSclientDob(string SclientDob_id)
        {
            try
            {
                return await _db.SclientDobs.FindAsync(SclientDob_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SclientDob>> GetSclientDobList(string SclientDob_id)
        {
            try
            {
                return await _db.SclientDobs
                    .Where(i => i.SclientDobId == SclientDob_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SclientDob> AddSclientDob(SclientDob SclientDob)
        {
            try
            {
                await _db.SclientDobs.AddAsync(SclientDob);
                await _db.SaveChangesAsync();
                return await _db.SclientDobs.FindAsync(SclientDob.SclientDobId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SclientDob> UpdateSclientDob(SclientDob SclientDob)
        {
            try
            {
                _db.Entry(SclientDob).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SclientDob;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclientDob(SclientDob SclientDob)
        {
            try
            {
                var dbSclientDob = await _db.SclientDobs.FindAsync(SclientDob.SclientDobId);

                if (dbSclientDob == null)
                {
                    return (false, "SclientDob could not be found");
                }

                _db.SclientDobs.Remove(SclientDob);
                await _db.SaveChangesAsync();

                return (true, "SclientDob got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SclientDobs
    }
}
