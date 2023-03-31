using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPbcatedtService
    {
        // Pbcatedts Services
        Task<List<Pbcatedt>> GetPbcatedtListByValue(int offset, int limit, string val); // GET All Pbcatedtss
        Task<Pbcatedt> GetPbcatedt(string Pbcatedt_name); // GET Single Pbcatedts        
        Task<List<Pbcatedt>> GetPbcatedtList(string Pbcatedt_name); // GET List Pbcatedts        
        Task<Pbcatedt> AddPbcatedt(Pbcatedt Pbcatedt); // POST New Pbcatedts
        Task<Pbcatedt> UpdatePbcatedt(Pbcatedt Pbcatedt); // PUT Pbcatedts
        Task<(bool, string)> DeletePbcatedt(Pbcatedt Pbcatedt); // DELETE Pbcatedts
    }

    public class PbcatedtService : IPbcatedtService
    {
        private readonly XixsrvContext _db;

        public PbcatedtService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pbcatedts

        public async Task<List<Pbcatedt>> GetPbcatedtListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pbcatedts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pbcatedt> GetPbcatedt(string Pbcatedt_id)
        {
            try
            {
                return await _db.Pbcatedts.FindAsync(Pbcatedt_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pbcatedt>> GetPbcatedtList(string Pbcatedt_id)
        {
            try
            {
                return await _db.Pbcatedts
                    .Where(i => i.PbcatedtId == Pbcatedt_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pbcatedt> AddPbcatedt(Pbcatedt Pbcatedt)
        {
            try
            {
                await _db.Pbcatedts.AddAsync(Pbcatedt);
                await _db.SaveChangesAsync();
                return await _db.Pbcatedts.FindAsync(Pbcatedt.PbcatedtId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pbcatedt> UpdatePbcatedt(Pbcatedt Pbcatedt)
        {
            try
            {
                _db.Entry(Pbcatedt).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pbcatedt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePbcatedt(Pbcatedt Pbcatedt)
        {
            try
            {
                var dbPbcatedt = await _db.Pbcatedts.FindAsync(Pbcatedt.PbcatedtId);

                if (dbPbcatedt == null)
                {
                    return (false, "Pbcatedt could not be found");
                }

                _db.Pbcatedts.Remove(Pbcatedt);
                await _db.SaveChangesAsync();

                return (true, "Pbcatedt got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pbcatedts
    }
}
