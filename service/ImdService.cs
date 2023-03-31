using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IImdService
    {
        // Imds Services
        Task<List<Imd>> GetImdListByValue(int offset, int limit, string val); // GET All Imdss
        Task<Imd> GetImd(string Imd_name); // GET Single Imds        
        Task<List<Imd>> GetImdList(string Imd_name); // GET List Imds        
        Task<Imd> AddImd(Imd Imd); // POST New Imds
        Task<Imd> UpdateImd(Imd Imd); // PUT Imds
        Task<(bool, string)> DeleteImd(Imd Imd); // DELETE Imds
    }

    public class ImdService : IImdService
    {
        private readonly XixsrvContext _db;

        public ImdService(XixsrvContext db)
        {
            _db = db;
        }

        #region Imds

        public async Task<List<Imd>> GetImdListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Imds.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Imd> GetImd(string Imd_id)
        {
            try
            {
                return await _db.Imds.FindAsync(Imd_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Imd>> GetImdList(string Imd_id)
        {
            try
            {
                return await _db.Imds
                    .Where(i => i.ImdId == Imd_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Imd> AddImd(Imd Imd)
        {
            try
            {
                await _db.Imds.AddAsync(Imd);
                await _db.SaveChangesAsync();
                return await _db.Imds.FindAsync(Imd.ImdId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Imd> UpdateImd(Imd Imd)
        {
            try
            {
                _db.Entry(Imd).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Imd;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteImd(Imd Imd)
        {
            try
            {
                var dbImd = await _db.Imds.FindAsync(Imd.ImdId);

                if (dbImd == null)
                {
                    return (false, "Imd could not be found");
                }

                _db.Imds.Remove(Imd);
                await _db.SaveChangesAsync();

                return (true, "Imd got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Imds
    }
}
