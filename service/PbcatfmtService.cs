using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPbcatfmtService
    {
        // Pbcatfmts Services
        Task<List<Pbcatfmt>> GetPbcatfmtListByValue(int offset, int limit, string val); // GET All Pbcatfmtss
        Task<Pbcatfmt> GetPbcatfmt(string Pbcatfmt_name); // GET Single Pbcatfmts        
        Task<List<Pbcatfmt>> GetPbcatfmtList(string Pbcatfmt_name); // GET List Pbcatfmts        
        Task<Pbcatfmt> AddPbcatfmt(Pbcatfmt Pbcatfmt); // POST New Pbcatfmts
        Task<Pbcatfmt> UpdatePbcatfmt(Pbcatfmt Pbcatfmt); // PUT Pbcatfmts
        Task<(bool, string)> DeletePbcatfmt(Pbcatfmt Pbcatfmt); // DELETE Pbcatfmts
    }

    public class PbcatfmtService : IPbcatfmtService
    {
        private readonly XixsrvContext _db;

        public PbcatfmtService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pbcatfmts

        public async Task<List<Pbcatfmt>> GetPbcatfmtListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pbcatfmts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pbcatfmt> GetPbcatfmt(string Pbcatfmt_id)
        {
            try
            {
                return await _db.Pbcatfmts.FindAsync(Pbcatfmt_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pbcatfmt>> GetPbcatfmtList(string Pbcatfmt_id)
        {
            try
            {
                return await _db.Pbcatfmts
                    .Where(i => i.PbcatfmtId == Pbcatfmt_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pbcatfmt> AddPbcatfmt(Pbcatfmt Pbcatfmt)
        {
            try
            {
                await _db.Pbcatfmts.AddAsync(Pbcatfmt);
                await _db.SaveChangesAsync();
                return await _db.Pbcatfmts.FindAsync(Pbcatfmt.PbcatfmtId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pbcatfmt> UpdatePbcatfmt(Pbcatfmt Pbcatfmt)
        {
            try
            {
                _db.Entry(Pbcatfmt).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pbcatfmt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePbcatfmt(Pbcatfmt Pbcatfmt)
        {
            try
            {
                var dbPbcatfmt = await _db.Pbcatfmts.FindAsync(Pbcatfmt.PbcatfmtId);

                if (dbPbcatfmt == null)
                {
                    return (false, "Pbcatfmt could not be found");
                }

                _db.Pbcatfmts.Remove(Pbcatfmt);
                await _db.SaveChangesAsync();

                return (true, "Pbcatfmt got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pbcatfmts
    }
}
