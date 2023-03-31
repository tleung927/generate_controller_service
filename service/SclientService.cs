using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclientService
    {
        // Sclients Services
        Task<List<Sclient>> GetSclientListByValue(int offset, int limit, string val); // GET All Sclientss
        Task<Sclient> GetSclient(string Sclient_name); // GET Single Sclients        
        Task<List<Sclient>> GetSclientList(string Sclient_name); // GET List Sclients        
        Task<Sclient> AddSclient(Sclient Sclient); // POST New Sclients
        Task<Sclient> UpdateSclient(Sclient Sclient); // PUT Sclients
        Task<(bool, string)> DeleteSclient(Sclient Sclient); // DELETE Sclients
    }

    public class SclientService : ISclientService
    {
        private readonly XixsrvContext _db;

        public SclientService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclients

        public async Task<List<Sclient>> GetSclientListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclients.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclient> GetSclient(string Sclient_id)
        {
            try
            {
                return await _db.Sclients.FindAsync(Sclient_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclient>> GetSclientList(string Sclient_id)
        {
            try
            {
                return await _db.Sclients
                    .Where(i => i.SclientId == Sclient_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclient> AddSclient(Sclient Sclient)
        {
            try
            {
                await _db.Sclients.AddAsync(Sclient);
                await _db.SaveChangesAsync();
                return await _db.Sclients.FindAsync(Sclient.SclientId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclient> UpdateSclient(Sclient Sclient)
        {
            try
            {
                _db.Entry(Sclient).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclient(Sclient Sclient)
        {
            try
            {
                var dbSclient = await _db.Sclients.FindAsync(Sclient.SclientId);

                if (dbSclient == null)
                {
                    return (false, "Sclient could not be found");
                }

                _db.Sclients.Remove(Sclient);
                await _db.SaveChangesAsync();

                return (true, "Sclient got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclients
    }
}
