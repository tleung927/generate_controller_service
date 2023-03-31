using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPsppylService
    {
        // Psppyls Services
        Task<List<Psppyl>> GetPsppylListByValue(int offset, int limit, string val); // GET All Psppylss
        Task<Psppyl> GetPsppyl(string Psppyl_name); // GET Single Psppyls        
        Task<List<Psppyl>> GetPsppylList(string Psppyl_name); // GET List Psppyls        
        Task<Psppyl> AddPsppyl(Psppyl Psppyl); // POST New Psppyls
        Task<Psppyl> UpdatePsppyl(Psppyl Psppyl); // PUT Psppyls
        Task<(bool, string)> DeletePsppyl(Psppyl Psppyl); // DELETE Psppyls
    }

    public class PsppylService : IPsppylService
    {
        private readonly XixsrvContext _db;

        public PsppylService(XixsrvContext db)
        {
            _db = db;
        }

        #region Psppyls

        public async Task<List<Psppyl>> GetPsppylListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Psppyls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Psppyl> GetPsppyl(string Psppyl_id)
        {
            try
            {
                return await _db.Psppyls.FindAsync(Psppyl_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Psppyl>> GetPsppylList(string Psppyl_id)
        {
            try
            {
                return await _db.Psppyls
                    .Where(i => i.PsppylId == Psppyl_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Psppyl> AddPsppyl(Psppyl Psppyl)
        {
            try
            {
                await _db.Psppyls.AddAsync(Psppyl);
                await _db.SaveChangesAsync();
                return await _db.Psppyls.FindAsync(Psppyl.PsppylId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Psppyl> UpdatePsppyl(Psppyl Psppyl)
        {
            try
            {
                _db.Entry(Psppyl).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Psppyl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePsppyl(Psppyl Psppyl)
        {
            try
            {
                var dbPsppyl = await _db.Psppyls.FindAsync(Psppyl.PsppylId);

                if (dbPsppyl == null)
                {
                    return (false, "Psppyl could not be found");
                }

                _db.Psppyls.Remove(Psppyl);
                await _db.SaveChangesAsync();

                return (true, "Psppyl got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Psppyls
    }
}
