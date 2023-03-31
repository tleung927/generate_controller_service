using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICder08CurrentService
    {
        // Cder08Currents Services
        Task<List<Cder08Current>> GetCder08CurrentListByValue(int offset, int limit, string val); // GET All Cder08Currentss
        Task<Cder08Current> GetCder08Current(string Cder08Current_name); // GET Single Cder08Currents        
        Task<List<Cder08Current>> GetCder08CurrentList(string Cder08Current_name); // GET List Cder08Currents        
        Task<Cder08Current> AddCder08Current(Cder08Current Cder08Current); // POST New Cder08Currents
        Task<Cder08Current> UpdateCder08Current(Cder08Current Cder08Current); // PUT Cder08Currents
        Task<(bool, string)> DeleteCder08Current(Cder08Current Cder08Current); // DELETE Cder08Currents
    }

    public class Cder08CurrentService : ICder08CurrentService
    {
        private readonly XixsrvContext _db;

        public Cder08CurrentService(XixsrvContext db)
        {
            _db = db;
        }

        #region Cder08Currents

        public async Task<List<Cder08Current>> GetCder08CurrentListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Cder08Currents.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Cder08Current> GetCder08Current(string Cder08Current_id)
        {
            try
            {
                return await _db.Cder08Currents.FindAsync(Cder08Current_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Cder08Current>> GetCder08CurrentList(string Cder08Current_id)
        {
            try
            {
                return await _db.Cder08Currents
                    .Where(i => i.Cder08CurrentId == Cder08Current_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Cder08Current> AddCder08Current(Cder08Current Cder08Current)
        {
            try
            {
                await _db.Cder08Currents.AddAsync(Cder08Current);
                await _db.SaveChangesAsync();
                return await _db.Cder08Currents.FindAsync(Cder08Current.Cder08CurrentId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Cder08Current> UpdateCder08Current(Cder08Current Cder08Current)
        {
            try
            {
                _db.Entry(Cder08Current).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Cder08Current;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCder08Current(Cder08Current Cder08Current)
        {
            try
            {
                var dbCder08Current = await _db.Cder08Currents.FindAsync(Cder08Current.Cder08CurrentId);

                if (dbCder08Current == null)
                {
                    return (false, "Cder08Current could not be found");
                }

                _db.Cder08Currents.Remove(Cder08Current);
                await _db.SaveChangesAsync();

                return (true, "Cder08Current got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Cder08Currents
    }
}
