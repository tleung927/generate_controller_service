using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPvndorToResAllService
    {
        // PvndorToResAlls Services
        Task<List<PvndorToResAll>> GetPvndorToResAllListByValue(int offset, int limit, string val); // GET All PvndorToResAllss
        Task<PvndorToResAll> GetPvndorToResAll(string PvndorToResAll_name); // GET Single PvndorToResAlls        
        Task<List<PvndorToResAll>> GetPvndorToResAllList(string PvndorToResAll_name); // GET List PvndorToResAlls        
        Task<PvndorToResAll> AddPvndorToResAll(PvndorToResAll PvndorToResAll); // POST New PvndorToResAlls
        Task<PvndorToResAll> UpdatePvndorToResAll(PvndorToResAll PvndorToResAll); // PUT PvndorToResAlls
        Task<(bool, string)> DeletePvndorToResAll(PvndorToResAll PvndorToResAll); // DELETE PvndorToResAlls
    }

    public class PvndorToResAllService : IPvndorToResAllService
    {
        private readonly XixsrvContext _db;

        public PvndorToResAllService(XixsrvContext db)
        {
            _db = db;
        }

        #region PvndorToResAlls

        public async Task<List<PvndorToResAll>> GetPvndorToResAllListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PvndorToResAlls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PvndorToResAll> GetPvndorToResAll(string PvndorToResAll_id)
        {
            try
            {
                return await _db.PvndorToResAlls.FindAsync(PvndorToResAll_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PvndorToResAll>> GetPvndorToResAllList(string PvndorToResAll_id)
        {
            try
            {
                return await _db.PvndorToResAlls
                    .Where(i => i.PvndorToResAllId == PvndorToResAll_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PvndorToResAll> AddPvndorToResAll(PvndorToResAll PvndorToResAll)
        {
            try
            {
                await _db.PvndorToResAlls.AddAsync(PvndorToResAll);
                await _db.SaveChangesAsync();
                return await _db.PvndorToResAlls.FindAsync(PvndorToResAll.PvndorToResAllId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PvndorToResAll> UpdatePvndorToResAll(PvndorToResAll PvndorToResAll)
        {
            try
            {
                _db.Entry(PvndorToResAll).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PvndorToResAll;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePvndorToResAll(PvndorToResAll PvndorToResAll)
        {
            try
            {
                var dbPvndorToResAll = await _db.PvndorToResAlls.FindAsync(PvndorToResAll.PvndorToResAllId);

                if (dbPvndorToResAll == null)
                {
                    return (false, "PvndorToResAll could not be found");
                }

                _db.PvndorToResAlls.Remove(PvndorToResAll);
                await _db.SaveChangesAsync();

                return (true, "PvndorToResAll got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PvndorToResAlls
    }
}
