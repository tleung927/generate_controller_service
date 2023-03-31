using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMwc11052020Service
    {
        // Mwc11052020s Services
        Task<List<Mwc11052020>> GetMwc11052020ListByValue(int offset, int limit, string val); // GET All Mwc11052020ss
        Task<Mwc11052020> GetMwc11052020(string Mwc11052020_name); // GET Single Mwc11052020s        
        Task<List<Mwc11052020>> GetMwc11052020List(string Mwc11052020_name); // GET List Mwc11052020s        
        Task<Mwc11052020> AddMwc11052020(Mwc11052020 Mwc11052020); // POST New Mwc11052020s
        Task<Mwc11052020> UpdateMwc11052020(Mwc11052020 Mwc11052020); // PUT Mwc11052020s
        Task<(bool, string)> DeleteMwc11052020(Mwc11052020 Mwc11052020); // DELETE Mwc11052020s
    }

    public class Mwc11052020Service : IMwc11052020Service
    {
        private readonly XixsrvContext _db;

        public Mwc11052020Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Mwc11052020s

        public async Task<List<Mwc11052020>> GetMwc11052020ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Mwc11052020s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Mwc11052020> GetMwc11052020(string Mwc11052020_id)
        {
            try
            {
                return await _db.Mwc11052020s.FindAsync(Mwc11052020_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Mwc11052020>> GetMwc11052020List(string Mwc11052020_id)
        {
            try
            {
                return await _db.Mwc11052020s
                    .Where(i => i.Mwc11052020Id == Mwc11052020_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Mwc11052020> AddMwc11052020(Mwc11052020 Mwc11052020)
        {
            try
            {
                await _db.Mwc11052020s.AddAsync(Mwc11052020);
                await _db.SaveChangesAsync();
                return await _db.Mwc11052020s.FindAsync(Mwc11052020.Mwc11052020Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Mwc11052020> UpdateMwc11052020(Mwc11052020 Mwc11052020)
        {
            try
            {
                _db.Entry(Mwc11052020).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Mwc11052020;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMwc11052020(Mwc11052020 Mwc11052020)
        {
            try
            {
                var dbMwc11052020 = await _db.Mwc11052020s.FindAsync(Mwc11052020.Mwc11052020Id);

                if (dbMwc11052020 == null)
                {
                    return (false, "Mwc11052020 could not be found");
                }

                _db.Mwc11052020s.Remove(Mwc11052020);
                await _db.SaveChangesAsync();

                return (true, "Mwc11052020 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Mwc11052020s
    }
}
