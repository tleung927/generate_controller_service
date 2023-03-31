using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IIcd9flService
    {
        // Icd9fls Services
        Task<List<Icd9fl>> GetIcd9flListByValue(int offset, int limit, string val); // GET All Icd9flss
        Task<Icd9fl> GetIcd9fl(string Icd9fl_name); // GET Single Icd9fls        
        Task<List<Icd9fl>> GetIcd9flList(string Icd9fl_name); // GET List Icd9fls        
        Task<Icd9fl> AddIcd9fl(Icd9fl Icd9fl); // POST New Icd9fls
        Task<Icd9fl> UpdateIcd9fl(Icd9fl Icd9fl); // PUT Icd9fls
        Task<(bool, string)> DeleteIcd9fl(Icd9fl Icd9fl); // DELETE Icd9fls
    }

    public class Icd9flService : IIcd9flService
    {
        private readonly XixsrvContext _db;

        public Icd9flService(XixsrvContext db)
        {
            _db = db;
        }

        #region Icd9fls

        public async Task<List<Icd9fl>> GetIcd9flListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Icd9fls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Icd9fl> GetIcd9fl(string Icd9fl_id)
        {
            try
            {
                return await _db.Icd9fls.FindAsync(Icd9fl_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Icd9fl>> GetIcd9flList(string Icd9fl_id)
        {
            try
            {
                return await _db.Icd9fls
                    .Where(i => i.Icd9flId == Icd9fl_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Icd9fl> AddIcd9fl(Icd9fl Icd9fl)
        {
            try
            {
                await _db.Icd9fls.AddAsync(Icd9fl);
                await _db.SaveChangesAsync();
                return await _db.Icd9fls.FindAsync(Icd9fl.Icd9flId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Icd9fl> UpdateIcd9fl(Icd9fl Icd9fl)
        {
            try
            {
                _db.Entry(Icd9fl).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Icd9fl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteIcd9fl(Icd9fl Icd9fl)
        {
            try
            {
                var dbIcd9fl = await _db.Icd9fls.FindAsync(Icd9fl.Icd9flId);

                if (dbIcd9fl == null)
                {
                    return (false, "Icd9fl could not be found");
                }

                _db.Icd9fls.Remove(Icd9fl);
                await _db.SaveChangesAsync();

                return (true, "Icd9fl got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Icd9fls
    }
}
