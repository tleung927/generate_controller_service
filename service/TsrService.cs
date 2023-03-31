using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITsrService
    {
        // Tsrs Services
        Task<List<Tsr>> GetTsrListByValue(int offset, int limit, string val); // GET All Tsrss
        Task<Tsr> GetTsr(string Tsr_name); // GET Single Tsrs        
        Task<List<Tsr>> GetTsrList(string Tsr_name); // GET List Tsrs        
        Task<Tsr> AddTsr(Tsr Tsr); // POST New Tsrs
        Task<Tsr> UpdateTsr(Tsr Tsr); // PUT Tsrs
        Task<(bool, string)> DeleteTsr(Tsr Tsr); // DELETE Tsrs
    }

    public class TsrService : ITsrService
    {
        private readonly XixsrvContext _db;

        public TsrService(XixsrvContext db)
        {
            _db = db;
        }

        #region Tsrs

        public async Task<List<Tsr>> GetTsrListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Tsrs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Tsr> GetTsr(string Tsr_id)
        {
            try
            {
                return await _db.Tsrs.FindAsync(Tsr_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Tsr>> GetTsrList(string Tsr_id)
        {
            try
            {
                return await _db.Tsrs
                    .Where(i => i.TsrId == Tsr_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Tsr> AddTsr(Tsr Tsr)
        {
            try
            {
                await _db.Tsrs.AddAsync(Tsr);
                await _db.SaveChangesAsync();
                return await _db.Tsrs.FindAsync(Tsr.TsrId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Tsr> UpdateTsr(Tsr Tsr)
        {
            try
            {
                _db.Entry(Tsr).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Tsr;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTsr(Tsr Tsr)
        {
            try
            {
                var dbTsr = await _db.Tsrs.FindAsync(Tsr.TsrId);

                if (dbTsr == null)
                {
                    return (false, "Tsr could not be found");
                }

                _db.Tsrs.Remove(Tsr);
                await _db.SaveChangesAsync();

                return (true, "Tsr got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Tsrs
    }
}
