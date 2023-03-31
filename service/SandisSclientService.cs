using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSclientService
    {
        // SandisSclients Services
        Task<List<SandisSclient>> GetSandisSclientListByValue(int offset, int limit, string val); // GET All SandisSclientss
        Task<SandisSclient> GetSandisSclient(string SandisSclient_name); // GET Single SandisSclients        
        Task<List<SandisSclient>> GetSandisSclientList(string SandisSclient_name); // GET List SandisSclients        
        Task<SandisSclient> AddSandisSclient(SandisSclient SandisSclient); // POST New SandisSclients
        Task<SandisSclient> UpdateSandisSclient(SandisSclient SandisSclient); // PUT SandisSclients
        Task<(bool, string)> DeleteSandisSclient(SandisSclient SandisSclient); // DELETE SandisSclients
    }

    public class SandisSclientService : ISandisSclientService
    {
        private readonly XixsrvContext _db;

        public SandisSclientService(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSclients

        public async Task<List<SandisSclient>> GetSandisSclientListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSclients.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSclient> GetSandisSclient(string SandisSclient_id)
        {
            try
            {
                return await _db.SandisSclients.FindAsync(SandisSclient_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSclient>> GetSandisSclientList(string SandisSclient_id)
        {
            try
            {
                return await _db.SandisSclients
                    .Where(i => i.SandisSclientId == SandisSclient_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSclient> AddSandisSclient(SandisSclient SandisSclient)
        {
            try
            {
                await _db.SandisSclients.AddAsync(SandisSclient);
                await _db.SaveChangesAsync();
                return await _db.SandisSclients.FindAsync(SandisSclient.SandisSclientId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSclient> UpdateSandisSclient(SandisSclient SandisSclient)
        {
            try
            {
                _db.Entry(SandisSclient).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSclient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSclient(SandisSclient SandisSclient)
        {
            try
            {
                var dbSandisSclient = await _db.SandisSclients.FindAsync(SandisSclient.SandisSclientId);

                if (dbSandisSclient == null)
                {
                    return (false, "SandisSclient could not be found");
                }

                _db.SandisSclients.Remove(SandisSclient);
                await _db.SaveChangesAsync();

                return (true, "SandisSclient got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSclients
    }
}
