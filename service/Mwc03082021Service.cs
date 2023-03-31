using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMwc03082021Service
    {
        // Mwc03082021s Services
        Task<List<Mwc03082021>> GetMwc03082021ListByValue(int offset, int limit, string val); // GET All Mwc03082021ss
        Task<Mwc03082021> GetMwc03082021(string Mwc03082021_name); // GET Single Mwc03082021s        
        Task<List<Mwc03082021>> GetMwc03082021List(string Mwc03082021_name); // GET List Mwc03082021s        
        Task<Mwc03082021> AddMwc03082021(Mwc03082021 Mwc03082021); // POST New Mwc03082021s
        Task<Mwc03082021> UpdateMwc03082021(Mwc03082021 Mwc03082021); // PUT Mwc03082021s
        Task<(bool, string)> DeleteMwc03082021(Mwc03082021 Mwc03082021); // DELETE Mwc03082021s
    }

    public class Mwc03082021Service : IMwc03082021Service
    {
        private readonly XixsrvContext _db;

        public Mwc03082021Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Mwc03082021s

        public async Task<List<Mwc03082021>> GetMwc03082021ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Mwc03082021s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Mwc03082021> GetMwc03082021(string Mwc03082021_id)
        {
            try
            {
                return await _db.Mwc03082021s.FindAsync(Mwc03082021_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Mwc03082021>> GetMwc03082021List(string Mwc03082021_id)
        {
            try
            {
                return await _db.Mwc03082021s
                    .Where(i => i.Mwc03082021Id == Mwc03082021_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Mwc03082021> AddMwc03082021(Mwc03082021 Mwc03082021)
        {
            try
            {
                await _db.Mwc03082021s.AddAsync(Mwc03082021);
                await _db.SaveChangesAsync();
                return await _db.Mwc03082021s.FindAsync(Mwc03082021.Mwc03082021Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Mwc03082021> UpdateMwc03082021(Mwc03082021 Mwc03082021)
        {
            try
            {
                _db.Entry(Mwc03082021).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Mwc03082021;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMwc03082021(Mwc03082021 Mwc03082021)
        {
            try
            {
                var dbMwc03082021 = await _db.Mwc03082021s.FindAsync(Mwc03082021.Mwc03082021Id);

                if (dbMwc03082021 == null)
                {
                    return (false, "Mwc03082021 could not be found");
                }

                _db.Mwc03082021s.Remove(Mwc03082021);
                await _db.SaveChangesAsync();

                return (true, "Mwc03082021 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Mwc03082021s
    }
}
