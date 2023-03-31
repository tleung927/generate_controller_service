using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMwcService
    {
        // Mwcs Services
        Task<List<Mwc>> GetMwcListByValue(int offset, int limit, string val); // GET All Mwcss
        Task<Mwc> GetMwc(string Mwc_name); // GET Single Mwcs        
        Task<List<Mwc>> GetMwcList(string Mwc_name); // GET List Mwcs        
        Task<Mwc> AddMwc(Mwc Mwc); // POST New Mwcs
        Task<Mwc> UpdateMwc(Mwc Mwc); // PUT Mwcs
        Task<(bool, string)> DeleteMwc(Mwc Mwc); // DELETE Mwcs
    }

    public class MwcService : IMwcService
    {
        private readonly XixsrvContext _db;

        public MwcService(XixsrvContext db)
        {
            _db = db;
        }

        #region Mwcs

        public async Task<List<Mwc>> GetMwcListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Mwcs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Mwc> GetMwc(string Mwc_id)
        {
            try
            {
                return await _db.Mwcs.FindAsync(Mwc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Mwc>> GetMwcList(string Mwc_id)
        {
            try
            {
                return await _db.Mwcs
                    .Where(i => i.MwcId == Mwc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Mwc> AddMwc(Mwc Mwc)
        {
            try
            {
                await _db.Mwcs.AddAsync(Mwc);
                await _db.SaveChangesAsync();
                return await _db.Mwcs.FindAsync(Mwc.MwcId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Mwc> UpdateMwc(Mwc Mwc)
        {
            try
            {
                _db.Entry(Mwc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Mwc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMwc(Mwc Mwc)
        {
            try
            {
                var dbMwc = await _db.Mwcs.FindAsync(Mwc.MwcId);

                if (dbMwc == null)
                {
                    return (false, "Mwc could not be found");
                }

                _db.Mwcs.Remove(Mwc);
                await _db.SaveChangesAsync();

                return (true, "Mwc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Mwcs
    }
}
