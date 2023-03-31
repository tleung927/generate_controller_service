using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICderService
    {
        // Cders Services
        Task<List<Cder>> GetCderListByValue(int offset, int limit, string val); // GET All Cderss
        Task<Cder> GetCder(string Cder_name); // GET Single Cders        
        Task<List<Cder>> GetCderList(string Cder_name); // GET List Cders        
        Task<Cder> AddCder(Cder Cder); // POST New Cders
        Task<Cder> UpdateCder(Cder Cder); // PUT Cders
        Task<(bool, string)> DeleteCder(Cder Cder); // DELETE Cders
    }

    public class CderService : ICderService
    {
        private readonly XixsrvContext _db;

        public CderService(XixsrvContext db)
        {
            _db = db;
        }

        #region Cders

        public async Task<List<Cder>> GetCderListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Cders.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Cder> GetCder(string Cder_id)
        {
            try
            {
                return await _db.Cders.FindAsync(Cder_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Cder>> GetCderList(string Cder_id)
        {
            try
            {
                return await _db.Cders
                    .Where(i => i.CderId == Cder_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Cder> AddCder(Cder Cder)
        {
            try
            {
                await _db.Cders.AddAsync(Cder);
                await _db.SaveChangesAsync();
                return await _db.Cders.FindAsync(Cder.CderId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Cder> UpdateCder(Cder Cder)
        {
            try
            {
                _db.Entry(Cder).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Cder;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCder(Cder Cder)
        {
            try
            {
                var dbCder = await _db.Cders.FindAsync(Cder.CderId);

                if (dbCder == null)
                {
                    return (false, "Cder could not be found");
                }

                _db.Cders.Remove(Cder);
                await _db.SaveChangesAsync();

                return (true, "Cder got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Cders
    }
}
