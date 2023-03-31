using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPbcatvldService
    {
        // Pbcatvlds Services
        Task<List<Pbcatvld>> GetPbcatvldListByValue(int offset, int limit, string val); // GET All Pbcatvldss
        Task<Pbcatvld> GetPbcatvld(string Pbcatvld_name); // GET Single Pbcatvlds        
        Task<List<Pbcatvld>> GetPbcatvldList(string Pbcatvld_name); // GET List Pbcatvlds        
        Task<Pbcatvld> AddPbcatvld(Pbcatvld Pbcatvld); // POST New Pbcatvlds
        Task<Pbcatvld> UpdatePbcatvld(Pbcatvld Pbcatvld); // PUT Pbcatvlds
        Task<(bool, string)> DeletePbcatvld(Pbcatvld Pbcatvld); // DELETE Pbcatvlds
    }

    public class PbcatvldService : IPbcatvldService
    {
        private readonly XixsrvContext _db;

        public PbcatvldService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pbcatvlds

        public async Task<List<Pbcatvld>> GetPbcatvldListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pbcatvlds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pbcatvld> GetPbcatvld(string Pbcatvld_id)
        {
            try
            {
                return await _db.Pbcatvlds.FindAsync(Pbcatvld_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pbcatvld>> GetPbcatvldList(string Pbcatvld_id)
        {
            try
            {
                return await _db.Pbcatvlds
                    .Where(i => i.PbcatvldId == Pbcatvld_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pbcatvld> AddPbcatvld(Pbcatvld Pbcatvld)
        {
            try
            {
                await _db.Pbcatvlds.AddAsync(Pbcatvld);
                await _db.SaveChangesAsync();
                return await _db.Pbcatvlds.FindAsync(Pbcatvld.PbcatvldId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pbcatvld> UpdatePbcatvld(Pbcatvld Pbcatvld)
        {
            try
            {
                _db.Entry(Pbcatvld).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pbcatvld;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePbcatvld(Pbcatvld Pbcatvld)
        {
            try
            {
                var dbPbcatvld = await _db.Pbcatvlds.FindAsync(Pbcatvld.PbcatvldId);

                if (dbPbcatvld == null)
                {
                    return (false, "Pbcatvld could not be found");
                }

                _db.Pbcatvlds.Remove(Pbcatvld);
                await _db.SaveChangesAsync();

                return (true, "Pbcatvld got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pbcatvlds
    }
}
