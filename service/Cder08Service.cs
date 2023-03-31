using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICder08Service
    {
        // Cder08s Services
        Task<List<Cder08>> GetCder08ListByValue(int offset, int limit, string val); // GET All Cder08ss
        Task<Cder08> GetCder08(string Cder08_name); // GET Single Cder08s        
        Task<List<Cder08>> GetCder08List(string Cder08_name); // GET List Cder08s        
        Task<Cder08> AddCder08(Cder08 Cder08); // POST New Cder08s
        Task<Cder08> UpdateCder08(Cder08 Cder08); // PUT Cder08s
        Task<(bool, string)> DeleteCder08(Cder08 Cder08); // DELETE Cder08s
    }

    public class Cder08Service : ICder08Service
    {
        private readonly XixsrvContext _db;

        public Cder08Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Cder08s

        public async Task<List<Cder08>> GetCder08ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Cder08s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Cder08> GetCder08(string Cder08_id)
        {
            try
            {
                return await _db.Cder08s.FindAsync(Cder08_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Cder08>> GetCder08List(string Cder08_id)
        {
            try
            {
                return await _db.Cder08s
                    .Where(i => i.Cder08Id == Cder08_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Cder08> AddCder08(Cder08 Cder08)
        {
            try
            {
                await _db.Cder08s.AddAsync(Cder08);
                await _db.SaveChangesAsync();
                return await _db.Cder08s.FindAsync(Cder08.Cder08Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Cder08> UpdateCder08(Cder08 Cder08)
        {
            try
            {
                _db.Entry(Cder08).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Cder08;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCder08(Cder08 Cder08)
        {
            try
            {
                var dbCder08 = await _db.Cder08s.FindAsync(Cder08.Cder08Id);

                if (dbCder08 == null)
                {
                    return (false, "Cder08 could not be found");
                }

                _db.Cder08s.Remove(Cder08);
                await _db.SaveChangesAsync();

                return (true, "Cder08 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Cder08s
    }
}
