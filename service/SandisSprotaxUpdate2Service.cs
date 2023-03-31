using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISandisSprotaxUpdate2Service
    {
        // SandisSprotaxUpdate2s Services
        Task<List<SandisSprotaxUpdate2>> GetSandisSprotaxUpdate2ListByValue(int offset, int limit, string val); // GET All SandisSprotaxUpdate2ss
        Task<SandisSprotaxUpdate2> GetSandisSprotaxUpdate2(string SandisSprotaxUpdate2_name); // GET Single SandisSprotaxUpdate2s        
        Task<List<SandisSprotaxUpdate2>> GetSandisSprotaxUpdate2List(string SandisSprotaxUpdate2_name); // GET List SandisSprotaxUpdate2s        
        Task<SandisSprotaxUpdate2> AddSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2); // POST New SandisSprotaxUpdate2s
        Task<SandisSprotaxUpdate2> UpdateSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2); // PUT SandisSprotaxUpdate2s
        Task<(bool, string)> DeleteSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2); // DELETE SandisSprotaxUpdate2s
    }

    public class SandisSprotaxUpdate2Service : ISandisSprotaxUpdate2Service
    {
        private readonly XixsrvContext _db;

        public SandisSprotaxUpdate2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region SandisSprotaxUpdate2s

        public async Task<List<SandisSprotaxUpdate2>> GetSandisSprotaxUpdate2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SandisSprotaxUpdate2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SandisSprotaxUpdate2> GetSandisSprotaxUpdate2(string SandisSprotaxUpdate2_id)
        {
            try
            {
                return await _db.SandisSprotaxUpdate2s.FindAsync(SandisSprotaxUpdate2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SandisSprotaxUpdate2>> GetSandisSprotaxUpdate2List(string SandisSprotaxUpdate2_id)
        {
            try
            {
                return await _db.SandisSprotaxUpdate2s
                    .Where(i => i.SandisSprotaxUpdate2Id == SandisSprotaxUpdate2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SandisSprotaxUpdate2> AddSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2)
        {
            try
            {
                await _db.SandisSprotaxUpdate2s.AddAsync(SandisSprotaxUpdate2);
                await _db.SaveChangesAsync();
                return await _db.SandisSprotaxUpdate2s.FindAsync(SandisSprotaxUpdate2.SandisSprotaxUpdate2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SandisSprotaxUpdate2> UpdateSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2)
        {
            try
            {
                _db.Entry(SandisSprotaxUpdate2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SandisSprotaxUpdate2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSandisSprotaxUpdate2(SandisSprotaxUpdate2 SandisSprotaxUpdate2)
        {
            try
            {
                var dbSandisSprotaxUpdate2 = await _db.SandisSprotaxUpdate2s.FindAsync(SandisSprotaxUpdate2.SandisSprotaxUpdate2Id);

                if (dbSandisSprotaxUpdate2 == null)
                {
                    return (false, "SandisSprotaxUpdate2 could not be found");
                }

                _db.SandisSprotaxUpdate2s.Remove(SandisSprotaxUpdate2);
                await _db.SaveChangesAsync();

                return (true, "SandisSprotaxUpdate2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SandisSprotaxUpdate2s
    }
}
