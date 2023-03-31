using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPbcatcolService
    {
        // Pbcatcols Services
        Task<List<Pbcatcol>> GetPbcatcolListByValue(int offset, int limit, string val); // GET All Pbcatcolss
        Task<Pbcatcol> GetPbcatcol(string Pbcatcol_name); // GET Single Pbcatcols        
        Task<List<Pbcatcol>> GetPbcatcolList(string Pbcatcol_name); // GET List Pbcatcols        
        Task<Pbcatcol> AddPbcatcol(Pbcatcol Pbcatcol); // POST New Pbcatcols
        Task<Pbcatcol> UpdatePbcatcol(Pbcatcol Pbcatcol); // PUT Pbcatcols
        Task<(bool, string)> DeletePbcatcol(Pbcatcol Pbcatcol); // DELETE Pbcatcols
    }

    public class PbcatcolService : IPbcatcolService
    {
        private readonly XixsrvContext _db;

        public PbcatcolService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pbcatcols

        public async Task<List<Pbcatcol>> GetPbcatcolListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pbcatcols.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pbcatcol> GetPbcatcol(string Pbcatcol_id)
        {
            try
            {
                return await _db.Pbcatcols.FindAsync(Pbcatcol_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pbcatcol>> GetPbcatcolList(string Pbcatcol_id)
        {
            try
            {
                return await _db.Pbcatcols
                    .Where(i => i.PbcatcolId == Pbcatcol_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pbcatcol> AddPbcatcol(Pbcatcol Pbcatcol)
        {
            try
            {
                await _db.Pbcatcols.AddAsync(Pbcatcol);
                await _db.SaveChangesAsync();
                return await _db.Pbcatcols.FindAsync(Pbcatcol.PbcatcolId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pbcatcol> UpdatePbcatcol(Pbcatcol Pbcatcol)
        {
            try
            {
                _db.Entry(Pbcatcol).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pbcatcol;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePbcatcol(Pbcatcol Pbcatcol)
        {
            try
            {
                var dbPbcatcol = await _db.Pbcatcols.FindAsync(Pbcatcol.PbcatcolId);

                if (dbPbcatcol == null)
                {
                    return (false, "Pbcatcol could not be found");
                }

                _db.Pbcatcols.Remove(Pbcatcol);
                await _db.SaveChangesAsync();

                return (true, "Pbcatcol got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pbcatcols
    }
}
