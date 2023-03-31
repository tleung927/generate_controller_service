using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISlService
    {
        // Sls Services
        Task<List<Sl>> GetSlListByValue(int offset, int limit, string val); // GET All Slss
        Task<Sl> GetSl(string Sl_name); // GET Single Sls        
        Task<List<Sl>> GetSlList(string Sl_name); // GET List Sls        
        Task<Sl> AddSl(Sl Sl); // POST New Sls
        Task<Sl> UpdateSl(Sl Sl); // PUT Sls
        Task<(bool, string)> DeleteSl(Sl Sl); // DELETE Sls
    }

    public class SlService : ISlService
    {
        private readonly XixsrvContext _db;

        public SlService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sls

        public async Task<List<Sl>> GetSlListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sl> GetSl(string Sl_id)
        {
            try
            {
                return await _db.Sls.FindAsync(Sl_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sl>> GetSlList(string Sl_id)
        {
            try
            {
                return await _db.Sls
                    .Where(i => i.SlId == Sl_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sl> AddSl(Sl Sl)
        {
            try
            {
                await _db.Sls.AddAsync(Sl);
                await _db.SaveChangesAsync();
                return await _db.Sls.FindAsync(Sl.SlId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sl> UpdateSl(Sl Sl)
        {
            try
            {
                _db.Entry(Sl).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSl(Sl Sl)
        {
            try
            {
                var dbSl = await _db.Sls.FindAsync(Sl.SlId);

                if (dbSl == null)
                {
                    return (false, "Sl could not be found");
                }

                _db.Sls.Remove(Sl);
                await _db.SaveChangesAsync();

                return (true, "Sl got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sls
    }
}
