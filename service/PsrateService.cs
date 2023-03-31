using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPsrateService
    {
        // Psrates Services
        Task<List<Psrate>> GetPsrateListByValue(int offset, int limit, string val); // GET All Psratess
        Task<Psrate> GetPsrate(string Psrate_name); // GET Single Psrates        
        Task<List<Psrate>> GetPsrateList(string Psrate_name); // GET List Psrates        
        Task<Psrate> AddPsrate(Psrate Psrate); // POST New Psrates
        Task<Psrate> UpdatePsrate(Psrate Psrate); // PUT Psrates
        Task<(bool, string)> DeletePsrate(Psrate Psrate); // DELETE Psrates
    }

    public class PsrateService : IPsrateService
    {
        private readonly XixsrvContext _db;

        public PsrateService(XixsrvContext db)
        {
            _db = db;
        }

        #region Psrates

        public async Task<List<Psrate>> GetPsrateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Psrates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Psrate> GetPsrate(string Psrate_id)
        {
            try
            {
                return await _db.Psrates.FindAsync(Psrate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Psrate>> GetPsrateList(string Psrate_id)
        {
            try
            {
                return await _db.Psrates
                    .Where(i => i.PsrateId == Psrate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Psrate> AddPsrate(Psrate Psrate)
        {
            try
            {
                await _db.Psrates.AddAsync(Psrate);
                await _db.SaveChangesAsync();
                return await _db.Psrates.FindAsync(Psrate.PsrateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Psrate> UpdatePsrate(Psrate Psrate)
        {
            try
            {
                _db.Entry(Psrate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Psrate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePsrate(Psrate Psrate)
        {
            try
            {
                var dbPsrate = await _db.Psrates.FindAsync(Psrate.PsrateId);

                if (dbPsrate == null)
                {
                    return (false, "Psrate could not be found");
                }

                _db.Psrates.Remove(Psrate);
                await _db.SaveChangesAsync();

                return (true, "Psrate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Psrates
    }
}
