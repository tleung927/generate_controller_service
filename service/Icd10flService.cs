using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IIcd10flService
    {
        // Icd10fls Services
        Task<List<Icd10fl>> GetIcd10flListByValue(int offset, int limit, string val); // GET All Icd10flss
        Task<Icd10fl> GetIcd10fl(string Icd10fl_name); // GET Single Icd10fls        
        Task<List<Icd10fl>> GetIcd10flList(string Icd10fl_name); // GET List Icd10fls        
        Task<Icd10fl> AddIcd10fl(Icd10fl Icd10fl); // POST New Icd10fls
        Task<Icd10fl> UpdateIcd10fl(Icd10fl Icd10fl); // PUT Icd10fls
        Task<(bool, string)> DeleteIcd10fl(Icd10fl Icd10fl); // DELETE Icd10fls
    }

    public class Icd10flService : IIcd10flService
    {
        private readonly XixsrvContext _db;

        public Icd10flService(XixsrvContext db)
        {
            _db = db;
        }

        #region Icd10fls

        public async Task<List<Icd10fl>> GetIcd10flListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Icd10fls.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Icd10fl> GetIcd10fl(string Icd10fl_id)
        {
            try
            {
                return await _db.Icd10fls.FindAsync(Icd10fl_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Icd10fl>> GetIcd10flList(string Icd10fl_id)
        {
            try
            {
                return await _db.Icd10fls
                    .Where(i => i.Icd10flId == Icd10fl_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Icd10fl> AddIcd10fl(Icd10fl Icd10fl)
        {
            try
            {
                await _db.Icd10fls.AddAsync(Icd10fl);
                await _db.SaveChangesAsync();
                return await _db.Icd10fls.FindAsync(Icd10fl.Icd10flId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Icd10fl> UpdateIcd10fl(Icd10fl Icd10fl)
        {
            try
            {
                _db.Entry(Icd10fl).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Icd10fl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteIcd10fl(Icd10fl Icd10fl)
        {
            try
            {
                var dbIcd10fl = await _db.Icd10fls.FindAsync(Icd10fl.Icd10flId);

                if (dbIcd10fl == null)
                {
                    return (false, "Icd10fl could not be found");
                }

                _db.Icd10fls.Remove(Icd10fl);
                await _db.SaveChangesAsync();

                return (true, "Icd10fl got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Icd10fls
    }
}
