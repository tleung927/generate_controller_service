using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IScliadr3Service
    {
        // Scliadr3s Services
        Task<List<Scliadr3>> GetScliadr3ListByValue(int offset, int limit, string val); // GET All Scliadr3ss
        Task<Scliadr3> GetScliadr3(string Scliadr3_name); // GET Single Scliadr3s        
        Task<List<Scliadr3>> GetScliadr3List(string Scliadr3_name); // GET List Scliadr3s        
        Task<Scliadr3> AddScliadr3(Scliadr3 Scliadr3); // POST New Scliadr3s
        Task<Scliadr3> UpdateScliadr3(Scliadr3 Scliadr3); // PUT Scliadr3s
        Task<(bool, string)> DeleteScliadr3(Scliadr3 Scliadr3); // DELETE Scliadr3s
    }

    public class Scliadr3Service : IScliadr3Service
    {
        private readonly XixsrvContext _db;

        public Scliadr3Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Scliadr3s

        public async Task<List<Scliadr3>> GetScliadr3ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Scliadr3s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Scliadr3> GetScliadr3(string Scliadr3_id)
        {
            try
            {
                return await _db.Scliadr3s.FindAsync(Scliadr3_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Scliadr3>> GetScliadr3List(string Scliadr3_id)
        {
            try
            {
                return await _db.Scliadr3s
                    .Where(i => i.Scliadr3Id == Scliadr3_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Scliadr3> AddScliadr3(Scliadr3 Scliadr3)
        {
            try
            {
                await _db.Scliadr3s.AddAsync(Scliadr3);
                await _db.SaveChangesAsync();
                return await _db.Scliadr3s.FindAsync(Scliadr3.Scliadr3Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Scliadr3> UpdateScliadr3(Scliadr3 Scliadr3)
        {
            try
            {
                _db.Entry(Scliadr3).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Scliadr3;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteScliadr3(Scliadr3 Scliadr3)
        {
            try
            {
                var dbScliadr3 = await _db.Scliadr3s.FindAsync(Scliadr3.Scliadr3Id);

                if (dbScliadr3 == null)
                {
                    return (false, "Scliadr3 could not be found");
                }

                _db.Scliadr3s.Remove(Scliadr3);
                await _db.SaveChangesAsync();

                return (true, "Scliadr3 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Scliadr3s
    }
}
