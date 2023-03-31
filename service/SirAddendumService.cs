using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISirAddendumService
    {
        // SirAddendums Services
        Task<List<SirAddendum>> GetSirAddendumListByValue(int offset, int limit, string val); // GET All SirAddendumss
        Task<SirAddendum> GetSirAddendum(string SirAddendum_name); // GET Single SirAddendums        
        Task<List<SirAddendum>> GetSirAddendumList(string SirAddendum_name); // GET List SirAddendums        
        Task<SirAddendum> AddSirAddendum(SirAddendum SirAddendum); // POST New SirAddendums
        Task<SirAddendum> UpdateSirAddendum(SirAddendum SirAddendum); // PUT SirAddendums
        Task<(bool, string)> DeleteSirAddendum(SirAddendum SirAddendum); // DELETE SirAddendums
    }

    public class SirAddendumService : ISirAddendumService
    {
        private readonly XixsrvContext _db;

        public SirAddendumService(XixsrvContext db)
        {
            _db = db;
        }

        #region SirAddendums

        public async Task<List<SirAddendum>> GetSirAddendumListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SirAddendums.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SirAddendum> GetSirAddendum(string SirAddendum_id)
        {
            try
            {
                return await _db.SirAddendums.FindAsync(SirAddendum_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SirAddendum>> GetSirAddendumList(string SirAddendum_id)
        {
            try
            {
                return await _db.SirAddendums
                    .Where(i => i.SirAddendumId == SirAddendum_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SirAddendum> AddSirAddendum(SirAddendum SirAddendum)
        {
            try
            {
                await _db.SirAddendums.AddAsync(SirAddendum);
                await _db.SaveChangesAsync();
                return await _db.SirAddendums.FindAsync(SirAddendum.SirAddendumId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SirAddendum> UpdateSirAddendum(SirAddendum SirAddendum)
        {
            try
            {
                _db.Entry(SirAddendum).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SirAddendum;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSirAddendum(SirAddendum SirAddendum)
        {
            try
            {
                var dbSirAddendum = await _db.SirAddendums.FindAsync(SirAddendum.SirAddendumId);

                if (dbSirAddendum == null)
                {
                    return (false, "SirAddendum could not be found");
                }

                _db.SirAddendums.Remove(SirAddendum);
                await _db.SaveChangesAsync();

                return (true, "SirAddendum got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SirAddendums
    }
}
