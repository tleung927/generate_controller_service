using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISirTempService
    {
        // SirTemps Services
        Task<List<SirTemp>> GetSirTempListByValue(int offset, int limit, string val); // GET All SirTempss
        Task<SirTemp> GetSirTemp(string SirTemp_name); // GET Single SirTemps        
        Task<List<SirTemp>> GetSirTempList(string SirTemp_name); // GET List SirTemps        
        Task<SirTemp> AddSirTemp(SirTemp SirTemp); // POST New SirTemps
        Task<SirTemp> UpdateSirTemp(SirTemp SirTemp); // PUT SirTemps
        Task<(bool, string)> DeleteSirTemp(SirTemp SirTemp); // DELETE SirTemps
    }

    public class SirTempService : ISirTempService
    {
        private readonly XixsrvContext _db;

        public SirTempService(XixsrvContext db)
        {
            _db = db;
        }

        #region SirTemps

        public async Task<List<SirTemp>> GetSirTempListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SirTemps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SirTemp> GetSirTemp(string SirTemp_id)
        {
            try
            {
                return await _db.SirTemps.FindAsync(SirTemp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SirTemp>> GetSirTempList(string SirTemp_id)
        {
            try
            {
                return await _db.SirTemps
                    .Where(i => i.SirTempId == SirTemp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SirTemp> AddSirTemp(SirTemp SirTemp)
        {
            try
            {
                await _db.SirTemps.AddAsync(SirTemp);
                await _db.SaveChangesAsync();
                return await _db.SirTemps.FindAsync(SirTemp.SirTempId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SirTemp> UpdateSirTemp(SirTemp SirTemp)
        {
            try
            {
                _db.Entry(SirTemp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SirTemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSirTemp(SirTemp SirTemp)
        {
            try
            {
                var dbSirTemp = await _db.SirTemps.FindAsync(SirTemp.SirTempId);

                if (dbSirTemp == null)
                {
                    return (false, "SirTemp could not be found");
                }

                _db.SirTemps.Remove(SirTemp);
                await _db.SaveChangesAsync();

                return (true, "SirTemp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SirTemps
    }
}
