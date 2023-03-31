using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISecurityset04262022Service
    {
        // Securityset04262022s Services
        Task<List<Securityset04262022>> GetSecurityset04262022ListByValue(int offset, int limit, string val); // GET All Securityset04262022ss
        Task<Securityset04262022> GetSecurityset04262022(string Securityset04262022_name); // GET Single Securityset04262022s        
        Task<List<Securityset04262022>> GetSecurityset04262022List(string Securityset04262022_name); // GET List Securityset04262022s        
        Task<Securityset04262022> AddSecurityset04262022(Securityset04262022 Securityset04262022); // POST New Securityset04262022s
        Task<Securityset04262022> UpdateSecurityset04262022(Securityset04262022 Securityset04262022); // PUT Securityset04262022s
        Task<(bool, string)> DeleteSecurityset04262022(Securityset04262022 Securityset04262022); // DELETE Securityset04262022s
    }

    public class Securityset04262022Service : ISecurityset04262022Service
    {
        private readonly XixsrvContext _db;

        public Securityset04262022Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Securityset04262022s

        public async Task<List<Securityset04262022>> GetSecurityset04262022ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Securityset04262022s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Securityset04262022> GetSecurityset04262022(string Securityset04262022_id)
        {
            try
            {
                return await _db.Securityset04262022s.FindAsync(Securityset04262022_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Securityset04262022>> GetSecurityset04262022List(string Securityset04262022_id)
        {
            try
            {
                return await _db.Securityset04262022s
                    .Where(i => i.Securityset04262022Id == Securityset04262022_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Securityset04262022> AddSecurityset04262022(Securityset04262022 Securityset04262022)
        {
            try
            {
                await _db.Securityset04262022s.AddAsync(Securityset04262022);
                await _db.SaveChangesAsync();
                return await _db.Securityset04262022s.FindAsync(Securityset04262022.Securityset04262022Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Securityset04262022> UpdateSecurityset04262022(Securityset04262022 Securityset04262022)
        {
            try
            {
                _db.Entry(Securityset04262022).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Securityset04262022;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSecurityset04262022(Securityset04262022 Securityset04262022)
        {
            try
            {
                var dbSecurityset04262022 = await _db.Securityset04262022s.FindAsync(Securityset04262022.Securityset04262022Id);

                if (dbSecurityset04262022 == null)
                {
                    return (false, "Securityset04262022 could not be found");
                }

                _db.Securityset04262022s.Remove(Securityset04262022);
                await _db.SaveChangesAsync();

                return (true, "Securityset04262022 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Securityset04262022s
    }
}
