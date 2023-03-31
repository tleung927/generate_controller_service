using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPbcattblService
    {
        // Pbcattbls Services
        Task<List<Pbcattbl>> GetPbcattblListByValue(int offset, int limit, string val); // GET All Pbcattblss
        Task<Pbcattbl> GetPbcattbl(string Pbcattbl_name); // GET Single Pbcattbls        
        Task<List<Pbcattbl>> GetPbcattblList(string Pbcattbl_name); // GET List Pbcattbls        
        Task<Pbcattbl> AddPbcattbl(Pbcattbl Pbcattbl); // POST New Pbcattbls
        Task<Pbcattbl> UpdatePbcattbl(Pbcattbl Pbcattbl); // PUT Pbcattbls
        Task<(bool, string)> DeletePbcattbl(Pbcattbl Pbcattbl); // DELETE Pbcattbls
    }

    public class PbcattblService : IPbcattblService
    {
        private readonly XixsrvContext _db;

        public PbcattblService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pbcattbls

        public async Task<List<Pbcattbl>> GetPbcattblListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pbcattbls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pbcattbl> GetPbcattbl(string Pbcattbl_id)
        {
            try
            {
                return await _db.Pbcattbls.FindAsync(Pbcattbl_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pbcattbl>> GetPbcattblList(string Pbcattbl_id)
        {
            try
            {
                return await _db.Pbcattbls
                    .Where(i => i.PbcattblId == Pbcattbl_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pbcattbl> AddPbcattbl(Pbcattbl Pbcattbl)
        {
            try
            {
                await _db.Pbcattbls.AddAsync(Pbcattbl);
                await _db.SaveChangesAsync();
                return await _db.Pbcattbls.FindAsync(Pbcattbl.PbcattblId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pbcattbl> UpdatePbcattbl(Pbcattbl Pbcattbl)
        {
            try
            {
                _db.Entry(Pbcattbl).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pbcattbl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePbcattbl(Pbcattbl Pbcattbl)
        {
            try
            {
                var dbPbcattbl = await _db.Pbcattbls.FindAsync(Pbcattbl.PbcattblId);

                if (dbPbcattbl == null)
                {
                    return (false, "Pbcattbl could not be found");
                }

                _db.Pbcattbls.Remove(Pbcattbl);
                await _db.SaveChangesAsync();

                return (true, "Pbcattbl got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pbcattbls
    }
}
