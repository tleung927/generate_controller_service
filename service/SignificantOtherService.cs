using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISignificantOtherService
    {
        // SignificantOthers Services
        Task<List<SignificantOther>> GetSignificantOtherListByValue(int offset, int limit, string val); // GET All SignificantOtherss
        Task<SignificantOther> GetSignificantOther(string SignificantOther_name); // GET Single SignificantOthers        
        Task<List<SignificantOther>> GetSignificantOtherList(string SignificantOther_name); // GET List SignificantOthers        
        Task<SignificantOther> AddSignificantOther(SignificantOther SignificantOther); // POST New SignificantOthers
        Task<SignificantOther> UpdateSignificantOther(SignificantOther SignificantOther); // PUT SignificantOthers
        Task<(bool, string)> DeleteSignificantOther(SignificantOther SignificantOther); // DELETE SignificantOthers
    }

    public class SignificantOtherService : ISignificantOtherService
    {
        private readonly XixsrvContext _db;

        public SignificantOtherService(XixsrvContext db)
        {
            _db = db;
        }

        #region SignificantOthers

        public async Task<List<SignificantOther>> GetSignificantOtherListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SignificantOthers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SignificantOther> GetSignificantOther(string SignificantOther_id)
        {
            try
            {
                return await _db.SignificantOthers.FindAsync(SignificantOther_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SignificantOther>> GetSignificantOtherList(string SignificantOther_id)
        {
            try
            {
                return await _db.SignificantOthers
                    .Where(i => i.SignificantOtherId == SignificantOther_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SignificantOther> AddSignificantOther(SignificantOther SignificantOther)
        {
            try
            {
                await _db.SignificantOthers.AddAsync(SignificantOther);
                await _db.SaveChangesAsync();
                return await _db.SignificantOthers.FindAsync(SignificantOther.SignificantOtherId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SignificantOther> UpdateSignificantOther(SignificantOther SignificantOther)
        {
            try
            {
                _db.Entry(SignificantOther).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SignificantOther;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSignificantOther(SignificantOther SignificantOther)
        {
            try
            {
                var dbSignificantOther = await _db.SignificantOthers.FindAsync(SignificantOther.SignificantOtherId);

                if (dbSignificantOther == null)
                {
                    return (false, "SignificantOther could not be found");
                }

                _db.SignificantOthers.Remove(SignificantOther);
                await _db.SaveChangesAsync();

                return (true, "SignificantOther got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SignificantOthers
    }
}
