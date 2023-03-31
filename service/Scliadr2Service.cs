using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IScliadr2Service
    {
        // Scliadr2s Services
        Task<List<Scliadr2>> GetScliadr2ListByValue(int offset, int limit, string val); // GET All Scliadr2ss
        Task<Scliadr2> GetScliadr2(string Scliadr2_name); // GET Single Scliadr2s        
        Task<List<Scliadr2>> GetScliadr2List(string Scliadr2_name); // GET List Scliadr2s        
        Task<Scliadr2> AddScliadr2(Scliadr2 Scliadr2); // POST New Scliadr2s
        Task<Scliadr2> UpdateScliadr2(Scliadr2 Scliadr2); // PUT Scliadr2s
        Task<(bool, string)> DeleteScliadr2(Scliadr2 Scliadr2); // DELETE Scliadr2s
    }

    public class Scliadr2Service : IScliadr2Service
    {
        private readonly XixsrvContext _db;

        public Scliadr2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Scliadr2s

        public async Task<List<Scliadr2>> GetScliadr2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Scliadr2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Scliadr2> GetScliadr2(string Scliadr2_id)
        {
            try
            {
                return await _db.Scliadr2s.FindAsync(Scliadr2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Scliadr2>> GetScliadr2List(string Scliadr2_id)
        {
            try
            {
                return await _db.Scliadr2s
                    .Where(i => i.Scliadr2Id == Scliadr2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Scliadr2> AddScliadr2(Scliadr2 Scliadr2)
        {
            try
            {
                await _db.Scliadr2s.AddAsync(Scliadr2);
                await _db.SaveChangesAsync();
                return await _db.Scliadr2s.FindAsync(Scliadr2.Scliadr2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Scliadr2> UpdateScliadr2(Scliadr2 Scliadr2)
        {
            try
            {
                _db.Entry(Scliadr2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Scliadr2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteScliadr2(Scliadr2 Scliadr2)
        {
            try
            {
                var dbScliadr2 = await _db.Scliadr2s.FindAsync(Scliadr2.Scliadr2Id);

                if (dbScliadr2 == null)
                {
                    return (false, "Scliadr2 could not be found");
                }

                _db.Scliadr2s.Remove(Scliadr2);
                await _db.SaveChangesAsync();

                return (true, "Scliadr2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Scliadr2s
    }
}
