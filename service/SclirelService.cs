using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclirelService
    {
        // Sclirels Services
        Task<List<Sclirel>> GetSclirelListByValue(int offset, int limit, string val); // GET All Sclirelss
        Task<Sclirel> GetSclirel(string Sclirel_name); // GET Single Sclirels        
        Task<List<Sclirel>> GetSclirelList(string Sclirel_name); // GET List Sclirels        
        Task<Sclirel> AddSclirel(Sclirel Sclirel); // POST New Sclirels
        Task<Sclirel> UpdateSclirel(Sclirel Sclirel); // PUT Sclirels
        Task<(bool, string)> DeleteSclirel(Sclirel Sclirel); // DELETE Sclirels
    }

    public class SclirelService : ISclirelService
    {
        private readonly XixsrvContext _db;

        public SclirelService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclirels

        public async Task<List<Sclirel>> GetSclirelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclirels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclirel> GetSclirel(string Sclirel_id)
        {
            try
            {
                return await _db.Sclirels.FindAsync(Sclirel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclirel>> GetSclirelList(string Sclirel_id)
        {
            try
            {
                return await _db.Sclirels
                    .Where(i => i.SclirelId == Sclirel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclirel> AddSclirel(Sclirel Sclirel)
        {
            try
            {
                await _db.Sclirels.AddAsync(Sclirel);
                await _db.SaveChangesAsync();
                return await _db.Sclirels.FindAsync(Sclirel.SclirelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclirel> UpdateSclirel(Sclirel Sclirel)
        {
            try
            {
                _db.Entry(Sclirel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclirel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclirel(Sclirel Sclirel)
        {
            try
            {
                var dbSclirel = await _db.Sclirels.FindAsync(Sclirel.SclirelId);

                if (dbSclirel == null)
                {
                    return (false, "Sclirel could not be found");
                }

                _db.Sclirels.Remove(Sclirel);
                await _db.SaveChangesAsync();

                return (true, "Sclirel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclirels
    }
}
