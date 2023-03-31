using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclicdrService
    {
        // Sclicdrs Services
        Task<List<Sclicdr>> GetSclicdrListByValue(int offset, int limit, string val); // GET All Sclicdrss
        Task<Sclicdr> GetSclicdr(string Sclicdr_name); // GET Single Sclicdrs        
        Task<List<Sclicdr>> GetSclicdrList(string Sclicdr_name); // GET List Sclicdrs        
        Task<Sclicdr> AddSclicdr(Sclicdr Sclicdr); // POST New Sclicdrs
        Task<Sclicdr> UpdateSclicdr(Sclicdr Sclicdr); // PUT Sclicdrs
        Task<(bool, string)> DeleteSclicdr(Sclicdr Sclicdr); // DELETE Sclicdrs
    }

    public class SclicdrService : ISclicdrService
    {
        private readonly XixsrvContext _db;

        public SclicdrService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sclicdrs

        public async Task<List<Sclicdr>> GetSclicdrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sclicdrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sclicdr> GetSclicdr(string Sclicdr_id)
        {
            try
            {
                return await _db.Sclicdrs.FindAsync(Sclicdr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sclicdr>> GetSclicdrList(string Sclicdr_id)
        {
            try
            {
                return await _db.Sclicdrs
                    .Where(i => i.SclicdrId == Sclicdr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sclicdr> AddSclicdr(Sclicdr Sclicdr)
        {
            try
            {
                await _db.Sclicdrs.AddAsync(Sclicdr);
                await _db.SaveChangesAsync();
                return await _db.Sclicdrs.FindAsync(Sclicdr.SclicdrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sclicdr> UpdateSclicdr(Sclicdr Sclicdr)
        {
            try
            {
                _db.Entry(Sclicdr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sclicdr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclicdr(Sclicdr Sclicdr)
        {
            try
            {
                var dbSclicdr = await _db.Sclicdrs.FindAsync(Sclicdr.SclicdrId);

                if (dbSclicdr == null)
                {
                    return (false, "Sclicdr could not be found");
                }

                _db.Sclicdrs.Remove(Sclicdr);
                await _db.SaveChangesAsync();

                return (true, "Sclicdr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sclicdrs
    }
}
