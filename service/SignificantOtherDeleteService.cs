using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISignificantOtherDeleteService
    {
        // SignificantOtherDeletes Services
        Task<List<SignificantOtherDelete>> GetSignificantOtherDeleteListByValue(int offset, int limit, string val); // GET All SignificantOtherDeletess
        Task<SignificantOtherDelete> GetSignificantOtherDelete(string SignificantOtherDelete_name); // GET Single SignificantOtherDeletes        
        Task<List<SignificantOtherDelete>> GetSignificantOtherDeleteList(string SignificantOtherDelete_name); // GET List SignificantOtherDeletes        
        Task<SignificantOtherDelete> AddSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete); // POST New SignificantOtherDeletes
        Task<SignificantOtherDelete> UpdateSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete); // PUT SignificantOtherDeletes
        Task<(bool, string)> DeleteSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete); // DELETE SignificantOtherDeletes
    }

    public class SignificantOtherDeleteService : ISignificantOtherDeleteService
    {
        private readonly XixsrvContext _db;

        public SignificantOtherDeleteService(XixsrvContext db)
        {
            _db = db;
        }

        #region SignificantOtherDeletes

        public async Task<List<SignificantOtherDelete>> GetSignificantOtherDeleteListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SignificantOtherDeletes.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SignificantOtherDelete> GetSignificantOtherDelete(string SignificantOtherDelete_id)
        {
            try
            {
                return await _db.SignificantOtherDeletes.FindAsync(SignificantOtherDelete_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SignificantOtherDelete>> GetSignificantOtherDeleteList(string SignificantOtherDelete_id)
        {
            try
            {
                return await _db.SignificantOtherDeletes
                    .Where(i => i.SignificantOtherDeleteId == SignificantOtherDelete_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SignificantOtherDelete> AddSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete)
        {
            try
            {
                await _db.SignificantOtherDeletes.AddAsync(SignificantOtherDelete);
                await _db.SaveChangesAsync();
                return await _db.SignificantOtherDeletes.FindAsync(SignificantOtherDelete.SignificantOtherDeleteId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SignificantOtherDelete> UpdateSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete)
        {
            try
            {
                _db.Entry(SignificantOtherDelete).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SignificantOtherDelete;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSignificantOtherDelete(SignificantOtherDelete SignificantOtherDelete)
        {
            try
            {
                var dbSignificantOtherDelete = await _db.SignificantOtherDeletes.FindAsync(SignificantOtherDelete.SignificantOtherDeleteId);

                if (dbSignificantOtherDelete == null)
                {
                    return (false, "SignificantOtherDelete could not be found");
                }

                _db.SignificantOtherDeletes.Remove(SignificantOtherDelete);
                await _db.SaveChangesAsync();

                return (true, "SignificantOtherDelete got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SignificantOtherDeletes
    }
}
