using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDeletedSirService
    {
        // DeletedSirs Services
        Task<List<DeletedSir>> GetDeletedSirListByValue(int offset, int limit, string val); // GET All DeletedSirss
        Task<DeletedSir> GetDeletedSir(string DeletedSir_name); // GET Single DeletedSirs        
        Task<List<DeletedSir>> GetDeletedSirList(string DeletedSir_name); // GET List DeletedSirs        
        Task<DeletedSir> AddDeletedSir(DeletedSir DeletedSir); // POST New DeletedSirs
        Task<DeletedSir> UpdateDeletedSir(DeletedSir DeletedSir); // PUT DeletedSirs
        Task<(bool, string)> DeleteDeletedSir(DeletedSir DeletedSir); // DELETE DeletedSirs
    }

    public class DeletedSirService : IDeletedSirService
    {
        private readonly XixsrvContext _db;

        public DeletedSirService(XixsrvContext db)
        {
            _db = db;
        }

        #region DeletedSirs

        public async Task<List<DeletedSir>> GetDeletedSirListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.DeletedSirs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<DeletedSir> GetDeletedSir(string DeletedSir_id)
        {
            try
            {
                return await _db.DeletedSirs.FindAsync(DeletedSir_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<DeletedSir>> GetDeletedSirList(string DeletedSir_id)
        {
            try
            {
                return await _db.DeletedSirs
                    .Where(i => i.DeletedSirId == DeletedSir_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<DeletedSir> AddDeletedSir(DeletedSir DeletedSir)
        {
            try
            {
                await _db.DeletedSirs.AddAsync(DeletedSir);
                await _db.SaveChangesAsync();
                return await _db.DeletedSirs.FindAsync(DeletedSir.DeletedSirId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<DeletedSir> UpdateDeletedSir(DeletedSir DeletedSir)
        {
            try
            {
                _db.Entry(DeletedSir).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return DeletedSir;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDeletedSir(DeletedSir DeletedSir)
        {
            try
            {
                var dbDeletedSir = await _db.DeletedSirs.FindAsync(DeletedSir.DeletedSirId);

                if (dbDeletedSir == null)
                {
                    return (false, "DeletedSir could not be found");
                }

                _db.DeletedSirs.Remove(DeletedSir);
                await _db.SaveChangesAsync();

                return (true, "DeletedSir got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion DeletedSirs
    }
}
