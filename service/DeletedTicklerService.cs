using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedTicklerService
    {
        // DeletedTicklers Services
        Task<List<DeletedTickler>> GetDeletedTicklerListByValue(int offset, int limit, string val); // GET All DeletedTicklerss
        Task<DeletedTickler> GetDeletedTickler(string DeletedTickler_name); // GET Single DeletedTicklers        
        Task<List<DeletedTickler>> GetDeletedTicklerList(string DeletedTickler_name); // GET List DeletedTicklers        
        Task<DeletedTickler> AddDeletedTickler(DeletedTickler DeletedTickler); // POST New DeletedTicklers
        Task<DeletedTickler> UpdateDeletedTickler(DeletedTickler DeletedTickler); // PUT DeletedTicklers
        Task<(bool, string)> DeleteDeletedTickler(DeletedTickler DeletedTickler); // DELETE DeletedTicklers
    }

    public class DeletedTicklerService : IDeletedTicklerService
    {
        private readonly XixsrvContext _db;

        public DeletedTicklerService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedTicklers

        public async Task<List<DeletedTickler>> GetDeletedTicklerListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedTicklers.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedTickler> GetDeletedTickler(string DeletedTickler_id)
        {
            try
            {
                return await _db.DeletedTicklers.FindAsync(DeletedTickler_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedTickler>> GetDeletedTicklerList(string DeletedTickler_id)
        {
            try
            {
                return await _db.DeletedTicklers
                    .Where(i => i.DeletedTicklerId == DeletedTickler_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedTickler> AddDeletedTickler(DeletedTickler DeletedTickler)
        {
            try
            {
                await _db.DeletedTicklers.AddAsync(DeletedTickler);
                await _db.SaveChangesAsync();
                return await _db.DeletedTicklers.FindAsync(DeletedTickler.DeletedTicklerId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedTickler> UpdateDeletedTickler(DeletedTickler DeletedTickler)
        {
            try
            {
                _db.Entry(DeletedTickler).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedTickler;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedTickler(DeletedTickler DeletedTickler)
        {
            try
            {
                var dbDeletedTickler = await _db.DeletedTicklers.FindAsync(DeletedTickler.DeletedTicklerId);

                if (dbDeletedTickler == null)
                {
                    return (false, "DeletedTickler could not be found");
                }

                _db.DeletedTicklers.Remove(DeletedTickler);
                await _db.SaveChangesAsync();

                return (true, "DeletedTickler got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedTicklers
    }
}
