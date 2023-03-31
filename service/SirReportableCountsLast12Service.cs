using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISirReportableCountsLast12Service
    {
        // SirReportableCountsLast12s Services
        Task<List<SirReportableCountsLast12>> GetSirReportableCountsLast12ListByValue(int offset, int limit, string val); // GET All SirReportableCountsLast12ss
        Task<SirReportableCountsLast12> GetSirReportableCountsLast12(string SirReportableCountsLast12_name); // GET Single SirReportableCountsLast12s        
        Task<List<SirReportableCountsLast12>> GetSirReportableCountsLast12List(string SirReportableCountsLast12_name); // GET List SirReportableCountsLast12s        
        Task<SirReportableCountsLast12> AddSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12); // POST New SirReportableCountsLast12s
        Task<SirReportableCountsLast12> UpdateSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12); // PUT SirReportableCountsLast12s
        Task<(bool, string)> DeleteSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12); // DELETE SirReportableCountsLast12s
    }

    public class SirReportableCountsLast12Service : ISirReportableCountsLast12Service
    {
        private readonly XixsrvContext _db;

        public SirReportableCountsLast12Service(XixsrvContext db)
        {
            _db = db;
        }

        #region SirReportableCountsLast12s

        public async Task<List<SirReportableCountsLast12>> GetSirReportableCountsLast12ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SirReportableCountsLast12s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SirReportableCountsLast12> GetSirReportableCountsLast12(string SirReportableCountsLast12_id)
        {
            try
            {
                return await _db.SirReportableCountsLast12s.FindAsync(SirReportableCountsLast12_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SirReportableCountsLast12>> GetSirReportableCountsLast12List(string SirReportableCountsLast12_id)
        {
            try
            {
                return await _db.SirReportableCountsLast12s
                    .Where(i => i.SirReportableCountsLast12Id == SirReportableCountsLast12_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SirReportableCountsLast12> AddSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12)
        {
            try
            {
                await _db.SirReportableCountsLast12s.AddAsync(SirReportableCountsLast12);
                await _db.SaveChangesAsync();
                return await _db.SirReportableCountsLast12s.FindAsync(SirReportableCountsLast12.SirReportableCountsLast12Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SirReportableCountsLast12> UpdateSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12)
        {
            try
            {
                _db.Entry(SirReportableCountsLast12).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SirReportableCountsLast12;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12)
        {
            try
            {
                var dbSirReportableCountsLast12 = await _db.SirReportableCountsLast12s.FindAsync(SirReportableCountsLast12.SirReportableCountsLast12Id);

                if (dbSirReportableCountsLast12 == null)
                {
                    return (false, "SirReportableCountsLast12 could not be found");
                }

                _db.SirReportableCountsLast12s.Remove(SirReportableCountsLast12);
                await _db.SaveChangesAsync();

                return (true, "SirReportableCountsLast12 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SirReportableCountsLast12s
    }
}
