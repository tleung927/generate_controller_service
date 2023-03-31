using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISirAddendumTempService
    {
        // SirAddendumTemps Services
        Task<List<SirAddendumTemp>> GetSirAddendumTempListByValue(int offset, int limit, string val); // GET All SirAddendumTempss
        Task<SirAddendumTemp> GetSirAddendumTemp(string SirAddendumTemp_name); // GET Single SirAddendumTemps        
        Task<List<SirAddendumTemp>> GetSirAddendumTempList(string SirAddendumTemp_name); // GET List SirAddendumTemps        
        Task<SirAddendumTemp> AddSirAddendumTemp(SirAddendumTemp SirAddendumTemp); // POST New SirAddendumTemps
        Task<SirAddendumTemp> UpdateSirAddendumTemp(SirAddendumTemp SirAddendumTemp); // PUT SirAddendumTemps
        Task<(bool, string)> DeleteSirAddendumTemp(SirAddendumTemp SirAddendumTemp); // DELETE SirAddendumTemps
    }

    public class SirAddendumTempService : ISirAddendumTempService
    {
        private readonly XixsrvContext _db;

        public SirAddendumTempService(XixsrvContext db)
        {
            _db = db;
        }

        #region SirAddendumTemps

        public async Task<List<SirAddendumTemp>> GetSirAddendumTempListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SirAddendumTemps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SirAddendumTemp> GetSirAddendumTemp(string SirAddendumTemp_id)
        {
            try
            {
                return await _db.SirAddendumTemps.FindAsync(SirAddendumTemp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SirAddendumTemp>> GetSirAddendumTempList(string SirAddendumTemp_id)
        {
            try
            {
                return await _db.SirAddendumTemps
                    .Where(i => i.SirAddendumTempId == SirAddendumTemp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SirAddendumTemp> AddSirAddendumTemp(SirAddendumTemp SirAddendumTemp)
        {
            try
            {
                await _db.SirAddendumTemps.AddAsync(SirAddendumTemp);
                await _db.SaveChangesAsync();
                return await _db.SirAddendumTemps.FindAsync(SirAddendumTemp.SirAddendumTempId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SirAddendumTemp> UpdateSirAddendumTemp(SirAddendumTemp SirAddendumTemp)
        {
            try
            {
                _db.Entry(SirAddendumTemp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SirAddendumTemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSirAddendumTemp(SirAddendumTemp SirAddendumTemp)
        {
            try
            {
                var dbSirAddendumTemp = await _db.SirAddendumTemps.FindAsync(SirAddendumTemp.SirAddendumTempId);

                if (dbSirAddendumTemp == null)
                {
                    return (false, "SirAddendumTemp could not be found");
                }

                _db.SirAddendumTemps.Remove(SirAddendumTemp);
                await _db.SaveChangesAsync();

                return (true, "SirAddendumTemp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SirAddendumTemps
    }
}
