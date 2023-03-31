using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IScliadr1Service
    {
        // Scliadr1s Services
        Task<List<Scliadr1>> GetScliadr1ListByValue(int offset, int limit, string val); // GET All Scliadr1ss
        Task<Scliadr1> GetScliadr1(string Scliadr1_name); // GET Single Scliadr1s        
        Task<List<Scliadr1>> GetScliadr1List(string Scliadr1_name); // GET List Scliadr1s        
        Task<Scliadr1> AddScliadr1(Scliadr1 Scliadr1); // POST New Scliadr1s
        Task<Scliadr1> UpdateScliadr1(Scliadr1 Scliadr1); // PUT Scliadr1s
        Task<(bool, string)> DeleteScliadr1(Scliadr1 Scliadr1); // DELETE Scliadr1s
    }

    public class Scliadr1Service : IScliadr1Service
    {
        private readonly XixsrvContext _db;

        public Scliadr1Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Scliadr1s

        public async Task<List<Scliadr1>> GetScliadr1ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Scliadr1s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Scliadr1> GetScliadr1(string Scliadr1_id)
        {
            try
            {
                return await _db.Scliadr1s.FindAsync(Scliadr1_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Scliadr1>> GetScliadr1List(string Scliadr1_id)
        {
            try
            {
                return await _db.Scliadr1s
                    .Where(i => i.Scliadr1Id == Scliadr1_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Scliadr1> AddScliadr1(Scliadr1 Scliadr1)
        {
            try
            {
                await _db.Scliadr1s.AddAsync(Scliadr1);
                await _db.SaveChangesAsync();
                return await _db.Scliadr1s.FindAsync(Scliadr1.Scliadr1Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Scliadr1> UpdateScliadr1(Scliadr1 Scliadr1)
        {
            try
            {
                _db.Entry(Scliadr1).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Scliadr1;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteScliadr1(Scliadr1 Scliadr1)
        {
            try
            {
                var dbScliadr1 = await _db.Scliadr1s.FindAsync(Scliadr1.Scliadr1Id);

                if (dbScliadr1 == null)
                {
                    return (false, "Scliadr1 could not be found");
                }

                _db.Scliadr1s.Remove(Scliadr1);
                await _db.SaveChangesAsync();

                return (true, "Scliadr1 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Scliadr1s
    }
}
