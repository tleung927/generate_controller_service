using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IIcd9flPService
    {
        // Icd9flPs Services
        Task<List<Icd9flP>> GetIcd9flPListByValue(int offset, int limit, string val); // GET All Icd9flPss
        Task<Icd9flP> GetIcd9flP(string Icd9flP_name); // GET Single Icd9flPs        
        Task<List<Icd9flP>> GetIcd9flPList(string Icd9flP_name); // GET List Icd9flPs        
        Task<Icd9flP> AddIcd9flP(Icd9flP Icd9flP); // POST New Icd9flPs
        Task<Icd9flP> UpdateIcd9flP(Icd9flP Icd9flP); // PUT Icd9flPs
        Task<(bool, string)> DeleteIcd9flP(Icd9flP Icd9flP); // DELETE Icd9flPs
    }

    public class Icd9flPService : IIcd9flPService
    {
        private readonly XixsrvContext _db;

        public Icd9flPService(XixsrvContext db)
        {
            _db = db;
        }

        #region Icd9flPs

        public async Task<List<Icd9flP>> GetIcd9flPListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Icd9flPs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Icd9flP> GetIcd9flP(string Icd9flP_id)
        {
            try
            {
                return await _db.Icd9flPs.FindAsync(Icd9flP_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Icd9flP>> GetIcd9flPList(string Icd9flP_id)
        {
            try
            {
                return await _db.Icd9flPs
                    .Where(i => i.Icd9flPId == Icd9flP_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Icd9flP> AddIcd9flP(Icd9flP Icd9flP)
        {
            try
            {
                await _db.Icd9flPs.AddAsync(Icd9flP);
                await _db.SaveChangesAsync();
                return await _db.Icd9flPs.FindAsync(Icd9flP.Icd9flPId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Icd9flP> UpdateIcd9flP(Icd9flP Icd9flP)
        {
            try
            {
                _db.Entry(Icd9flP).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Icd9flP;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteIcd9flP(Icd9flP Icd9flP)
        {
            try
            {
                var dbIcd9flP = await _db.Icd9flPs.FindAsync(Icd9flP.Icd9flPId);

                if (dbIcd9flP == null)
                {
                    return (false, "Icd9flP could not be found");
                }

                _db.Icd9flPs.Remove(Icd9flP);
                await _db.SaveChangesAsync();

                return (true, "Icd9flP got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Icd9flPs
    }
}
