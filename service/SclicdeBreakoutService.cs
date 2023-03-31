using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISclicdeBreakoutService
    {
        // SclicdeBreakouts Services
        Task<List<SclicdeBreakout>> GetSclicdeBreakoutListByValue(int offset, int limit, string val); // GET All SclicdeBreakoutss
        Task<SclicdeBreakout> GetSclicdeBreakout(string SclicdeBreakout_name); // GET Single SclicdeBreakouts        
        Task<List<SclicdeBreakout>> GetSclicdeBreakoutList(string SclicdeBreakout_name); // GET List SclicdeBreakouts        
        Task<SclicdeBreakout> AddSclicdeBreakout(SclicdeBreakout SclicdeBreakout); // POST New SclicdeBreakouts
        Task<SclicdeBreakout> UpdateSclicdeBreakout(SclicdeBreakout SclicdeBreakout); // PUT SclicdeBreakouts
        Task<(bool, string)> DeleteSclicdeBreakout(SclicdeBreakout SclicdeBreakout); // DELETE SclicdeBreakouts
    }

    public class SclicdeBreakoutService : ISclicdeBreakoutService
    {
        private readonly XixsrvContext _db;

        public SclicdeBreakoutService(XixsrvContext db)
        {
            _db = db;
        }

        #region SclicdeBreakouts

        public async Task<List<SclicdeBreakout>> GetSclicdeBreakoutListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SclicdeBreakouts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SclicdeBreakout> GetSclicdeBreakout(string SclicdeBreakout_id)
        {
            try
            {
                return await _db.SclicdeBreakouts.FindAsync(SclicdeBreakout_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SclicdeBreakout>> GetSclicdeBreakoutList(string SclicdeBreakout_id)
        {
            try
            {
                return await _db.SclicdeBreakouts
                    .Where(i => i.SclicdeBreakoutId == SclicdeBreakout_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SclicdeBreakout> AddSclicdeBreakout(SclicdeBreakout SclicdeBreakout)
        {
            try
            {
                await _db.SclicdeBreakouts.AddAsync(SclicdeBreakout);
                await _db.SaveChangesAsync();
                return await _db.SclicdeBreakouts.FindAsync(SclicdeBreakout.SclicdeBreakoutId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SclicdeBreakout> UpdateSclicdeBreakout(SclicdeBreakout SclicdeBreakout)
        {
            try
            {
                _db.Entry(SclicdeBreakout).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SclicdeBreakout;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSclicdeBreakout(SclicdeBreakout SclicdeBreakout)
        {
            try
            {
                var dbSclicdeBreakout = await _db.SclicdeBreakouts.FindAsync(SclicdeBreakout.SclicdeBreakoutId);

                if (dbSclicdeBreakout == null)
                {
                    return (false, "SclicdeBreakout could not be found");
                }

                _db.SclicdeBreakouts.Remove(SclicdeBreakout);
                await _db.SaveChangesAsync();

                return (true, "SclicdeBreakout got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SclicdeBreakouts
    }
}
