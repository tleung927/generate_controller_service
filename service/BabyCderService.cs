using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IBabyCderService
    {
        // BabyCders Services
        Task<List<BabyCder>> GetBabyCderListByValue(int offset, int limit, string val); // GET All BabyCderss
        Task<BabyCder> GetBabyCder(string BabyCder_name); // GET Single BabyCders        
        Task<List<BabyCder>> GetBabyCderList(string BabyCder_name); // GET List BabyCders        
        Task<BabyCder> AddBabyCder(BabyCder BabyCder); // POST New BabyCders
        Task<BabyCder> UpdateBabyCder(BabyCder BabyCder); // PUT BabyCders
        Task<(bool, string)> DeleteBabyCder(BabyCder BabyCder); // DELETE BabyCders
    }

    public class BabyCderService : IBabyCderService
    {
        private readonly XixsrvContext _db;

        public BabyCderService(XixsrvContext db)
        {
            _db = db;
        }

        #region BabyCders

        public async Task<List<BabyCder>> GetBabyCderListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.BabyCders.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BabyCder> GetBabyCder(string BabyCder_id)
        {
            try
            {
                return await _db.BabyCders.FindAsync(BabyCder_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BabyCder>> GetBabyCderList(string BabyCder_id)
        {
            try
            {
                return await _db.BabyCders
                    .Where(i => i.BabyCderId == BabyCder_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<BabyCder> AddBabyCder(BabyCder BabyCder)
        {
            try
            {
                await _db.BabyCders.AddAsync(BabyCder);
                await _db.SaveChangesAsync();
                return await _db.BabyCders.FindAsync(BabyCder.BabyCderId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<BabyCder> UpdateBabyCder(BabyCder BabyCder)
        {
            try
            {
                _db.Entry(BabyCder).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return BabyCder;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteBabyCder(BabyCder BabyCder)
        {
            try
            {
                var dbBabyCder = await _db.BabyCders.FindAsync(BabyCder.BabyCderId);

                if (dbBabyCder == null)
                {
                    return (false, "BabyCder could not be found");
                }

                _db.BabyCders.Remove(BabyCder);
                await _db.SaveChangesAsync();

                return (true, "BabyCder got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion BabyCders
    }
}
