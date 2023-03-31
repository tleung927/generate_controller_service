using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISignificantOtherOldService
    {
        // SignificantOtherOlds Services
        Task<List<SignificantOtherOld>> GetSignificantOtherOldListByValue(int offset, int limit, string val); // GET All SignificantOtherOldss
        Task<SignificantOtherOld> GetSignificantOtherOld(string SignificantOtherOld_name); // GET Single SignificantOtherOlds        
        Task<List<SignificantOtherOld>> GetSignificantOtherOldList(string SignificantOtherOld_name); // GET List SignificantOtherOlds        
        Task<SignificantOtherOld> AddSignificantOtherOld(SignificantOtherOld SignificantOtherOld); // POST New SignificantOtherOlds
        Task<SignificantOtherOld> UpdateSignificantOtherOld(SignificantOtherOld SignificantOtherOld); // PUT SignificantOtherOlds
        Task<(bool, string)> DeleteSignificantOtherOld(SignificantOtherOld SignificantOtherOld); // DELETE SignificantOtherOlds
    }

    public class SignificantOtherOldService : ISignificantOtherOldService
    {
        private readonly XixsrvContext _db;

        public SignificantOtherOldService(XixsrvContext db)
        {
            _db = db;
        }

        #region SignificantOtherOlds

        public async Task<List<SignificantOtherOld>> GetSignificantOtherOldListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SignificantOtherOlds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SignificantOtherOld> GetSignificantOtherOld(string SignificantOtherOld_id)
        {
            try
            {
                return await _db.SignificantOtherOlds.FindAsync(SignificantOtherOld_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SignificantOtherOld>> GetSignificantOtherOldList(string SignificantOtherOld_id)
        {
            try
            {
                return await _db.SignificantOtherOlds
                    .Where(i => i.SignificantOtherOldId == SignificantOtherOld_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SignificantOtherOld> AddSignificantOtherOld(SignificantOtherOld SignificantOtherOld)
        {
            try
            {
                await _db.SignificantOtherOlds.AddAsync(SignificantOtherOld);
                await _db.SaveChangesAsync();
                return await _db.SignificantOtherOlds.FindAsync(SignificantOtherOld.SignificantOtherOldId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SignificantOtherOld> UpdateSignificantOtherOld(SignificantOtherOld SignificantOtherOld)
        {
            try
            {
                _db.Entry(SignificantOtherOld).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SignificantOtherOld;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSignificantOtherOld(SignificantOtherOld SignificantOtherOld)
        {
            try
            {
                var dbSignificantOtherOld = await _db.SignificantOtherOlds.FindAsync(SignificantOtherOld.SignificantOtherOldId);

                if (dbSignificantOtherOld == null)
                {
                    return (false, "SignificantOtherOld could not be found");
                }

                _db.SignificantOtherOlds.Remove(SignificantOtherOld);
                await _db.SaveChangesAsync();

                return (true, "SignificantOtherOld got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SignificantOtherOlds
    }
}
