using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPvdslvService
    {
        // Pvdslvs Services
        Task<List<Pvdslv>> GetPvdslvListByValue(int offset, int limit, string val); // GET All Pvdslvss
        Task<Pvdslv> GetPvdslv(string Pvdslv_name); // GET Single Pvdslvs        
        Task<List<Pvdslv>> GetPvdslvList(string Pvdslv_name); // GET List Pvdslvs        
        Task<Pvdslv> AddPvdslv(Pvdslv Pvdslv); // POST New Pvdslvs
        Task<Pvdslv> UpdatePvdslv(Pvdslv Pvdslv); // PUT Pvdslvs
        Task<(bool, string)> DeletePvdslv(Pvdslv Pvdslv); // DELETE Pvdslvs
    }

    public class PvdslvService : IPvdslvService
    {
        private readonly XixsrvContext _db;

        public PvdslvService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pvdslvs

        public async Task<List<Pvdslv>> GetPvdslvListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pvdslvs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Pvdslv> GetPvdslv(string Pvdslv_id)
        {
            try
            {
                return await _db.Pvdslvs.FindAsync(Pvdslv_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Pvdslv>> GetPvdslvList(string Pvdslv_id)
        {
            try
            {
                return await _db.Pvdslvs
                    .Where(i => i.PvdslvId == Pvdslv_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Pvdslv> AddPvdslv(Pvdslv Pvdslv)
        {
            try
            {
                await _db.Pvdslvs.AddAsync(Pvdslv);
                await _db.SaveChangesAsync();
                return await _db.Pvdslvs.FindAsync(Pvdslv.PvdslvId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Pvdslv> UpdatePvdslv(Pvdslv Pvdslv)
        {
            try
            {
                _db.Entry(Pvdslv).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Pvdslv;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePvdslv(Pvdslv Pvdslv)
        {
            try
            {
                var dbPvdslv = await _db.Pvdslvs.FindAsync(Pvdslv.PvdslvId);

                if (dbPvdslv == null)
                {
                    return (false, "Pvdslv could not be found");
                }

                _db.Pvdslvs.Remove(Pvdslv);
                await _db.SaveChangesAsync();

                return (true, "Pvdslv got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pvdslvs
    }
}
