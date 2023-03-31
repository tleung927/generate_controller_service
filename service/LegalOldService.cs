using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILegalOldService
    {
        // LegalOlds Services
        Task<List<LegalOld>> GetLegalOldListByValue(int offset, int limit, string val); // GET All LegalOldss
        Task<LegalOld> GetLegalOld(string LegalOld_name); // GET Single LegalOlds        
        Task<List<LegalOld>> GetLegalOldList(string LegalOld_name); // GET List LegalOlds        
        Task<LegalOld> AddLegalOld(LegalOld LegalOld); // POST New LegalOlds
        Task<LegalOld> UpdateLegalOld(LegalOld LegalOld); // PUT LegalOlds
        Task<(bool, string)> DeleteLegalOld(LegalOld LegalOld); // DELETE LegalOlds
    }

    public class LegalOldService : ILegalOldService
    {
        private readonly XixsrvContext _db;

        public LegalOldService(XixsrvContext db)
        {
            _db = db;
        }

        #region LegalOlds

        public async Task<List<LegalOld>> GetLegalOldListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.LegalOlds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LegalOld> GetLegalOld(string LegalOld_id)
        {
            try
            {
                return await _db.LegalOlds.FindAsync(LegalOld_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LegalOld>> GetLegalOldList(string LegalOld_id)
        {
            try
            {
                return await _db.LegalOlds
                    .Where(i => i.LegalOldId == LegalOld_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<LegalOld> AddLegalOld(LegalOld LegalOld)
        {
            try
            {
                await _db.LegalOlds.AddAsync(LegalOld);
                await _db.SaveChangesAsync();
                return await _db.LegalOlds.FindAsync(LegalOld.LegalOldId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<LegalOld> UpdateLegalOld(LegalOld LegalOld)
        {
            try
            {
                _db.Entry(LegalOld).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return LegalOld;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLegalOld(LegalOld LegalOld)
        {
            try
            {
                var dbLegalOld = await _db.LegalOlds.FindAsync(LegalOld.LegalOldId);

                if (dbLegalOld == null)
                {
                    return (false, "LegalOld could not be found");
                }

                _db.LegalOlds.Remove(LegalOld);
                await _db.SaveChangesAsync();

                return (true, "LegalOld got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion LegalOlds
    }
}
