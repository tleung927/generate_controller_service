using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IInitializationService
    {
        // Initializations Services
        Task<List<Initialization>> GetInitializationListByValue(int offset, int limit, string val); // GET All Initializationss
        Task<Initialization> GetInitialization(string Initialization_name); // GET Single Initializations        
        Task<List<Initialization>> GetInitializationList(string Initialization_name); // GET List Initializations        
        Task<Initialization> AddInitialization(Initialization Initialization); // POST New Initializations
        Task<Initialization> UpdateInitialization(Initialization Initialization); // PUT Initializations
        Task<(bool, string)> DeleteInitialization(Initialization Initialization); // DELETE Initializations
    }

    public class InitializationService : IInitializationService
    {
        private readonly XixsrvContext _db;

        public InitializationService(XixsrvContext db)
        {
            _db = db;
        }

        #region Initializations

        public async Task<List<Initialization>> GetInitializationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Initializations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Initialization> GetInitialization(string Initialization_id)
        {
            try
            {
                return await _db.Initializations.FindAsync(Initialization_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Initialization>> GetInitializationList(string Initialization_id)
        {
            try
            {
                return await _db.Initializations
                    .Where(i => i.InitializationId == Initialization_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Initialization> AddInitialization(Initialization Initialization)
        {
            try
            {
                await _db.Initializations.AddAsync(Initialization);
                await _db.SaveChangesAsync();
                return await _db.Initializations.FindAsync(Initialization.InitializationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Initialization> UpdateInitialization(Initialization Initialization)
        {
            try
            {
                _db.Entry(Initialization).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Initialization;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteInitialization(Initialization Initialization)
        {
            try
            {
                var dbInitialization = await _db.Initializations.FindAsync(Initialization.InitializationId);

                if (dbInitialization == null)
                {
                    return (false, "Initialization could not be found");
                }

                _db.Initializations.Remove(Initialization);
                await _db.SaveChangesAsync();

                return (true, "Initialization got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Initializations
    }
}
