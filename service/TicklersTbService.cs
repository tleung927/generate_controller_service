using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITicklersTbService
    {
        // TicklersTbs Services
        Task<List<TicklersTb>> GetTicklersTbListByValue(int offset, int limit, string val); // GET All TicklersTbss
        Task<TicklersTb> GetTicklersTb(string TicklersTb_name); // GET Single TicklersTbs        
        Task<List<TicklersTb>> GetTicklersTbList(string TicklersTb_name); // GET List TicklersTbs        
        Task<TicklersTb> AddTicklersTb(TicklersTb TicklersTb); // POST New TicklersTbs
        Task<TicklersTb> UpdateTicklersTb(TicklersTb TicklersTb); // PUT TicklersTbs
        Task<(bool, string)> DeleteTicklersTb(TicklersTb TicklersTb); // DELETE TicklersTbs
    }

    public class TicklersTbService : ITicklersTbService
    {
        private readonly XixsrvContext _db;

        public TicklersTbService(XixsrvContext db)
        {
            _db = db;
        }

        #region TicklersTbs

        public async Task<List<TicklersTb>> GetTicklersTbListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TicklersTbs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TicklersTb> GetTicklersTb(string TicklersTb_id)
        {
            try
            {
                return await _db.TicklersTbs.FindAsync(TicklersTb_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TicklersTb>> GetTicklersTbList(string TicklersTb_id)
        {
            try
            {
                return await _db.TicklersTbs
                    .Where(i => i.TicklersTbId == TicklersTb_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TicklersTb> AddTicklersTb(TicklersTb TicklersTb)
        {
            try
            {
                await _db.TicklersTbs.AddAsync(TicklersTb);
                await _db.SaveChangesAsync();
                return await _db.TicklersTbs.FindAsync(TicklersTb.TicklersTbId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TicklersTb> UpdateTicklersTb(TicklersTb TicklersTb)
        {
            try
            {
                _db.Entry(TicklersTb).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TicklersTb;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTicklersTb(TicklersTb TicklersTb)
        {
            try
            {
                var dbTicklersTb = await _db.TicklersTbs.FindAsync(TicklersTb.TicklersTbId);

                if (dbTicklersTb == null)
                {
                    return (false, "TicklersTb could not be found");
                }

                _db.TicklersTbs.Remove(TicklersTb);
                await _db.SaveChangesAsync();

                return (true, "TicklersTb got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TicklersTbs
    }
}
