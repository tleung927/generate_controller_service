using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IBabyCderCopyService
    {
        // BabyCderCopys Services
        Task<List<BabyCderCopy>> GetBabyCderCopyListByValue(int offset, int limit, string val); // GET All BabyCderCopyss
        Task<BabyCderCopy> GetBabyCderCopy(string BabyCderCopy_name); // GET Single BabyCderCopys        
        Task<List<BabyCderCopy>> GetBabyCderCopyList(string BabyCderCopy_name); // GET List BabyCderCopys        
        Task<BabyCderCopy> AddBabyCderCopy(BabyCderCopy BabyCderCopy); // POST New BabyCderCopys
        Task<BabyCderCopy> UpdateBabyCderCopy(BabyCderCopy BabyCderCopy); // PUT BabyCderCopys
        Task<(bool, string)> DeleteBabyCderCopy(BabyCderCopy BabyCderCopy); // DELETE BabyCderCopys
    }

    public class BabyCderCopyService : IBabyCderCopyService
    {
        private readonly XixsrvContext _db;

        public BabyCderCopyService(XixsrvContext db)
        {
            _db = db;
        }

        #region BabyCderCopys

        public async Task<List<BabyCderCopy>> GetBabyCderCopyListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.BabyCderCopys.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<BabyCderCopy> GetBabyCderCopy(string BabyCderCopy_id)
        {
            try
            {
                return await _db.BabyCderCopys.FindAsync(BabyCderCopy_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<BabyCderCopy>> GetBabyCderCopyList(string BabyCderCopy_id)
        {
            try
            {
                return await _db.BabyCderCopys
                    .Where(i => i.BabyCderCopyId == BabyCderCopy_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<BabyCderCopy> AddBabyCderCopy(BabyCderCopy BabyCderCopy)
        {
            try
            {
                await _db.BabyCderCopys.AddAsync(BabyCderCopy);
                await _db.SaveChangesAsync();
                return await _db.BabyCderCopys.FindAsync(BabyCderCopy.BabyCderCopyId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<BabyCderCopy> UpdateBabyCderCopy(BabyCderCopy BabyCderCopy)
        {
            try
            {
                _db.Entry(BabyCderCopy).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return BabyCderCopy;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteBabyCderCopy(BabyCderCopy BabyCderCopy)
        {
            try
            {
                var dbBabyCderCopy = await _db.BabyCderCopys.FindAsync(BabyCderCopy.BabyCderCopyId);

                if (dbBabyCderCopy == null)
                {
                    return (false, "BabyCderCopy could not be found");
                }

                _db.BabyCderCopys.Remove(BabyCderCopy);
                await _db.SaveChangesAsync();

                return (true, "BabyCderCopy got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion BabyCderCopys
    }
}
