using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IXxxxxxService
    {
        // Xxxxxxs Services
        Task<List<Xxxxxx>> GetXxxxxxListByValue(int offset, int limit, string val); // GET All Xxxxxxss
        Task<Xxxxxx> GetXxxxxx(string Xxxxxx_name); // GET Single Xxxxxxs        
        Task<List<Xxxxxx>> GetXxxxxxList(string Xxxxxx_name); // GET List Xxxxxxs        
        Task<Xxxxxx> AddXxxxxx(Xxxxxx Xxxxxx); // POST New Xxxxxxs
        Task<Xxxxxx> UpdateXxxxxx(Xxxxxx Xxxxxx); // PUT Xxxxxxs
        Task<(bool, string)> DeleteXxxxxx(Xxxxxx Xxxxxx); // DELETE Xxxxxxs
    }

    public class XxxxxxService : IXxxxxxService
    {
        private readonly XixsrvContext _db;

        public XxxxxxService(XixsrvContext db)
        {
            _db = db;
        }

        #region Xxxxxxs

        public async Task<List<Xxxxxx>> GetXxxxxxListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Xxxxxxs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Xxxxxx> GetXxxxxx(string Xxxxxx_id)
        {
            try
            {
                return await _db.Xxxxxxs.FindAsync(Xxxxxx_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Xxxxxx>> GetXxxxxxList(string Xxxxxx_id)
        {
            try
            {
                return await _db.Xxxxxxs
                    .Where(i => i.XxxxxxId == Xxxxxx_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Xxxxxx> AddXxxxxx(Xxxxxx Xxxxxx)
        {
            try
            {
                await _db.Xxxxxxs.AddAsync(Xxxxxx);
                await _db.SaveChangesAsync();
                return await _db.Xxxxxxs.FindAsync(Xxxxxx.XxxxxxId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Xxxxxx> UpdateXxxxxx(Xxxxxx Xxxxxx)
        {
            try
            {
                _db.Entry(Xxxxxx).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Xxxxxx;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteXxxxxx(Xxxxxx Xxxxxx)
        {
            try
            {
                var dbXxxxxx = await _db.Xxxxxxs.FindAsync(Xxxxxx.XxxxxxId);

                if (dbXxxxxx == null)
                {
                    return (false, "Xxxxxx could not be found");
                }

                _db.Xxxxxxs.Remove(Xxxxxx);
                await _db.SaveChangesAsync();

                return (true, "Xxxxxx got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Xxxxxxs
    }
}
