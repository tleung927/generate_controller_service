using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IErrorService
    {
        // Errors Services
        Task<List<Error>> GetErrorListByValue(int offset, int limit, string val); // GET All Errorss
        Task<Error> GetError(string Error_name); // GET Single Errors        
        Task<List<Error>> GetErrorList(string Error_name); // GET List Errors        
        Task<Error> AddError(Error Error); // POST New Errors
        Task<Error> UpdateError(Error Error); // PUT Errors
        Task<(bool, string)> DeleteError(Error Error); // DELETE Errors
    }

    public class ErrorService : IErrorService
    {
        private readonly XixsrvContext _db;

        public ErrorService(XixsrvContext db)
        {
            _db = db;
        }

        #region Errors

        public async Task<List<Error>> GetErrorListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Errors.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Error> GetError(string Error_id)
        {
            try
            {
                return await _db.Errors.FindAsync(Error_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Error>> GetErrorList(string Error_id)
        {
            try
            {
                return await _db.Errors
                    .Where(i => i.ErrorId == Error_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Error> AddError(Error Error)
        {
            try
            {
                await _db.Errors.AddAsync(Error);
                await _db.SaveChangesAsync();
                return await _db.Errors.FindAsync(Error.ErrorId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Error> UpdateError(Error Error)
        {
            try
            {
                _db.Entry(Error).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Error;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteError(Error Error)
        {
            try
            {
                var dbError = await _db.Errors.FindAsync(Error.ErrorId);

                if (dbError == null)
                {
                    return (false, "Error could not be found");
                }

                _db.Errors.Remove(Error);
                await _db.SaveChangesAsync();

                return (true, "Error got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Errors
    }
}
