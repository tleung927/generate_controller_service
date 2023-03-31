using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IKeaDevFormIfspBakService
    {
        // KeaDevFormIfspBaks Services
        Task<List<KeaDevFormIfspBak>> GetKeaDevFormIfspBakListByValue(int offset, int limit, string val); // GET All KeaDevFormIfspBakss
        Task<KeaDevFormIfspBak> GetKeaDevFormIfspBak(string KeaDevFormIfspBak_name); // GET Single KeaDevFormIfspBaks        
        Task<List<KeaDevFormIfspBak>> GetKeaDevFormIfspBakList(string KeaDevFormIfspBak_name); // GET List KeaDevFormIfspBaks        
        Task<KeaDevFormIfspBak> AddKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak); // POST New KeaDevFormIfspBaks
        Task<KeaDevFormIfspBak> UpdateKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak); // PUT KeaDevFormIfspBaks
        Task<(bool, string)> DeleteKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak); // DELETE KeaDevFormIfspBaks
    }

    public class KeaDevFormIfspBakService : IKeaDevFormIfspBakService
    {
        private readonly XixsrvContext _db;

        public KeaDevFormIfspBakService(XixsrvContext db)
        {
            _db = db;
        }

        #region KeaDevFormIfspBaks

        public async Task<List<KeaDevFormIfspBak>> GetKeaDevFormIfspBakListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.KeaDevFormIfspBaks.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<KeaDevFormIfspBak> GetKeaDevFormIfspBak(string KeaDevFormIfspBak_id)
        {
            try
            {
                return await _db.KeaDevFormIfspBaks.FindAsync(KeaDevFormIfspBak_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<KeaDevFormIfspBak>> GetKeaDevFormIfspBakList(string KeaDevFormIfspBak_id)
        {
            try
            {
                return await _db.KeaDevFormIfspBaks
                    .Where(i => i.KeaDevFormIfspBakId == KeaDevFormIfspBak_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<KeaDevFormIfspBak> AddKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak)
        {
            try
            {
                await _db.KeaDevFormIfspBaks.AddAsync(KeaDevFormIfspBak);
                await _db.SaveChangesAsync();
                return await _db.KeaDevFormIfspBaks.FindAsync(KeaDevFormIfspBak.KeaDevFormIfspBakId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<KeaDevFormIfspBak> UpdateKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak)
        {
            try
            {
                _db.Entry(KeaDevFormIfspBak).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return KeaDevFormIfspBak;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteKeaDevFormIfspBak(KeaDevFormIfspBak KeaDevFormIfspBak)
        {
            try
            {
                var dbKeaDevFormIfspBak = await _db.KeaDevFormIfspBaks.FindAsync(KeaDevFormIfspBak.KeaDevFormIfspBakId);

                if (dbKeaDevFormIfspBak == null)
                {
                    return (false, "KeaDevFormIfspBak could not be found");
                }

                _db.KeaDevFormIfspBaks.Remove(KeaDevFormIfspBak);
                await _db.SaveChangesAsync();

                return (true, "KeaDevFormIfspBak got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion KeaDevFormIfspBaks
    }
}
