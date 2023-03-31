using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclientDeleteService
    {
        // SclientDeletes Services
        Task<List<SclientDelete>> GetSclientDeleteListByValue(int offset, int limit, string val); // GET All SclientDeletess
        Task<SclientDelete> GetSclientDelete(string SclientDelete_name); // GET Single SclientDeletes        
        Task<List<SclientDelete>> GetSclientDeleteList(string SclientDelete_name); // GET List SclientDeletes        
        Task<SclientDelete> AddSclientDelete(SclientDelete SclientDelete); // POST New SclientDeletes
        Task<SclientDelete> UpdateSclientDelete(SclientDelete SclientDelete); // PUT SclientDeletes
        Task<(bool, string)> DeleteSclientDelete(SclientDelete SclientDelete); // DELETE SclientDeletes
    }

    public class SclientDeleteService : ISclientDeleteService
    {
        private readonly XixsrvContext _db;

        public SclientDeleteService(XixsrvContext db)
        {
            _db = db;
        }

        #region SclientDeletes

        public async Task<List<SclientDelete>> GetSclientDeleteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SclientDeletes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SclientDelete> GetSclientDelete(string SclientDelete_id)
        {
            try
            {
                return await _db.SclientDeletes.FindAsync(SclientDelete_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SclientDelete>> GetSclientDeleteList(string SclientDelete_id)
        {
            try
            {
                return await _db.SclientDeletes
                    .Where(i => i.SclientDeleteId == SclientDelete_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SclientDelete> AddSclientDelete(SclientDelete SclientDelete)
        {
            try
            {
                await _db.SclientDeletes.AddAsync(SclientDelete);
                await _db.SaveChangesAsync();
                return await _db.SclientDeletes.FindAsync(SclientDelete.SclientDeleteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SclientDelete> UpdateSclientDelete(SclientDelete SclientDelete)
        {
            try
            {
                _db.Entry(SclientDelete).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SclientDelete;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclientDelete(SclientDelete SclientDelete)
        {
            try
            {
                var dbSclientDelete = await _db.SclientDeletes.FindAsync(SclientDelete.SclientDeleteId);

                if (dbSclientDelete == null)
                {
                    return (false, "SclientDelete could not be found");
                }

                _db.SclientDeletes.Remove(SclientDelete);
                await _db.SaveChangesAsync();

                return (true, "SclientDelete got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SclientDeletes
    }
}
