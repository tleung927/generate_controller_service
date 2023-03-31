using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEsr11Service
    {
        // Esr11s Services
        Task<List<Esr11>> GetEsr11ListByValue(int offset, int limit, string val); // GET All Esr11ss
        Task<Esr11> GetEsr11(string Esr11_name); // GET Single Esr11s        
        Task<List<Esr11>> GetEsr11List(string Esr11_name); // GET List Esr11s        
        Task<Esr11> AddEsr11(Esr11 Esr11); // POST New Esr11s
        Task<Esr11> UpdateEsr11(Esr11 Esr11); // PUT Esr11s
        Task<(bool, string)> DeleteEsr11(Esr11 Esr11); // DELETE Esr11s
    }

    public class Esr11Service : IEsr11Service
    {
        private readonly XixsrvContext _db;

        public Esr11Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Esr11s

        public async Task<List<Esr11>> GetEsr11ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Esr11s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Esr11> GetEsr11(string Esr11_id)
        {
            try
            {
                return await _db.Esr11s.FindAsync(Esr11_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Esr11>> GetEsr11List(string Esr11_id)
        {
            try
            {
                return await _db.Esr11s
                    .Where(i => i.Esr11Id == Esr11_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Esr11> AddEsr11(Esr11 Esr11)
        {
            try
            {
                await _db.Esr11s.AddAsync(Esr11);
                await _db.SaveChangesAsync();
                return await _db.Esr11s.FindAsync(Esr11.Esr11Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Esr11> UpdateEsr11(Esr11 Esr11)
        {
            try
            {
                _db.Entry(Esr11).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Esr11;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEsr11(Esr11 Esr11)
        {
            try
            {
                var dbEsr11 = await _db.Esr11s.FindAsync(Esr11.Esr11Id);

                if (dbEsr11 == null)
                {
                    return (false, "Esr11 could not be found");
                }

                _db.Esr11s.Remove(Esr11);
                await _db.SaveChangesAsync();

                return (true, "Esr11 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Esr11s
    }
}
