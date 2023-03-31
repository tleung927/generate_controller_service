using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclicdeService
    {
        // Sclicdes Services
        Task<List<Sclicde>> GetSclicdeListByValue(int offset, int limit, string val); // GET All Sclicdess
        Task<Sclicde> GetSclicde(string Sclicde_name); // GET Single Sclicdes        
        Task<List<Sclicde>> GetSclicdeList(string Sclicde_name); // GET List Sclicdes        
        Task<Sclicde> AddSclicde(Sclicde Sclicde); // POST New Sclicdes
        Task<Sclicde> UpdateSclicde(Sclicde Sclicde); // PUT Sclicdes
        Task<(bool, string)> DeleteSclicde(Sclicde Sclicde); // DELETE Sclicdes
    }

    public class SclicdeService : ISclicdeService
    {
        private readonly XixsrvContext _db;

        public SclicdeService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclicdes

        public async Task<List<Sclicde>> GetSclicdeListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclicdes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclicde> GetSclicde(string Sclicde_id)
        {
            try
            {
                return await _db.Sclicdes.FindAsync(Sclicde_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclicde>> GetSclicdeList(string Sclicde_id)
        {
            try
            {
                return await _db.Sclicdes
                    .Where(i => i.SclicdeId == Sclicde_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclicde> AddSclicde(Sclicde Sclicde)
        {
            try
            {
                await _db.Sclicdes.AddAsync(Sclicde);
                await _db.SaveChangesAsync();
                return await _db.Sclicdes.FindAsync(Sclicde.SclicdeId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclicde> UpdateSclicde(Sclicde Sclicde)
        {
            try
            {
                _db.Entry(Sclicde).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclicde;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclicde(Sclicde Sclicde)
        {
            try
            {
                var dbSclicde = await _db.Sclicdes.FindAsync(Sclicde.SclicdeId);

                if (dbSclicde == null)
                {
                    return (false, "Sclicde could not be found");
                }

                _db.Sclicdes.Remove(Sclicde);
                await _db.SaveChangesAsync();

                return (true, "Sclicde got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclicdes
    }
}
