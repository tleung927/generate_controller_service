using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRcService
    {
        // Rcs Services
        Task<List<Rc>> GetRcListByValue(int offset, int limit, string val); // GET All Rcss
        Task<Rc> GetRc(string Rc_name); // GET Single Rcs        
        Task<List<Rc>> GetRcList(string Rc_name); // GET List Rcs        
        Task<Rc> AddRc(Rc Rc); // POST New Rcs
        Task<Rc> UpdateRc(Rc Rc); // PUT Rcs
        Task<(bool, string)> DeleteRc(Rc Rc); // DELETE Rcs
    }

    public class RcService : IRcService
    {
        private readonly XixsrvContext _db;

        public RcService(XixsrvContext db)
        {
            _db = db;
        }

        #region Rcs

        public async Task<List<Rc>> GetRcListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Rcs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Rc> GetRc(string Rc_id)
        {
            try
            {
                return await _db.Rcs.FindAsync(Rc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Rc>> GetRcList(string Rc_id)
        {
            try
            {
                return await _db.Rcs
                    .Where(i => i.RcId == Rc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Rc> AddRc(Rc Rc)
        {
            try
            {
                await _db.Rcs.AddAsync(Rc);
                await _db.SaveChangesAsync();
                return await _db.Rcs.FindAsync(Rc.RcId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Rc> UpdateRc(Rc Rc)
        {
            try
            {
                _db.Entry(Rc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Rc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRc(Rc Rc)
        {
            try
            {
                var dbRc = await _db.Rcs.FindAsync(Rc.RcId);

                if (dbRc == null)
                {
                    return (false, "Rc could not be found");
                }

                _db.Rcs.Remove(Rc);
                await _db.SaveChangesAsync();

                return (true, "Rc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Rcs
    }
}
