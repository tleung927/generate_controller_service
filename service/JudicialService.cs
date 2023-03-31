using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IJudicialService
    {
        // Judicials Services
        Task<List<Judicial>> GetJudicialListByValue(int offset, int limit, string val); // GET All Judicialss
        Task<Judicial> GetJudicial(string Judicial_name); // GET Single Judicials        
        Task<List<Judicial>> GetJudicialList(string Judicial_name); // GET List Judicials        
        Task<Judicial> AddJudicial(Judicial Judicial); // POST New Judicials
        Task<Judicial> UpdateJudicial(Judicial Judicial); // PUT Judicials
        Task<(bool, string)> DeleteJudicial(Judicial Judicial); // DELETE Judicials
    }

    public class JudicialService : IJudicialService
    {
        private readonly XixsrvContext _db;

        public JudicialService(XixsrvContext db)
        {
            _db = db;
        }

        #region Judicials

        public async Task<List<Judicial>> GetJudicialListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Judicials.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Judicial> GetJudicial(string Judicial_id)
        {
            try
            {
                return await _db.Judicials.FindAsync(Judicial_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Judicial>> GetJudicialList(string Judicial_id)
        {
            try
            {
                return await _db.Judicials
                    .Where(i => i.JudicialId == Judicial_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Judicial> AddJudicial(Judicial Judicial)
        {
            try
            {
                await _db.Judicials.AddAsync(Judicial);
                await _db.SaveChangesAsync();
                return await _db.Judicials.FindAsync(Judicial.JudicialId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Judicial> UpdateJudicial(Judicial Judicial)
        {
            try
            {
                _db.Entry(Judicial).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Judicial;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteJudicial(Judicial Judicial)
        {
            try
            {
                var dbJudicial = await _db.Judicials.FindAsync(Judicial.JudicialId);

                if (dbJudicial == null)
                {
                    return (false, "Judicial could not be found");
                }

                _db.Judicials.Remove(Judicial);
                await _db.SaveChangesAsync();

                return (true, "Judicial got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Judicials
    }
}
