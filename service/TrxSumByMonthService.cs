using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITrxSumByMonthService
    {
        // TrxSumByMonths Services
        Task<List<TrxSumByMonth>> GetTrxSumByMonthListByValue(int offset, int limit, string val); // GET All TrxSumByMonthss
        Task<TrxSumByMonth> GetTrxSumByMonth(string TrxSumByMonth_name); // GET Single TrxSumByMonths        
        Task<List<TrxSumByMonth>> GetTrxSumByMonthList(string TrxSumByMonth_name); // GET List TrxSumByMonths        
        Task<TrxSumByMonth> AddTrxSumByMonth(TrxSumByMonth TrxSumByMonth); // POST New TrxSumByMonths
        Task<TrxSumByMonth> UpdateTrxSumByMonth(TrxSumByMonth TrxSumByMonth); // PUT TrxSumByMonths
        Task<(bool, string)> DeleteTrxSumByMonth(TrxSumByMonth TrxSumByMonth); // DELETE TrxSumByMonths
    }

    public class TrxSumByMonthService : ITrxSumByMonthService
    {
        private readonly XixsrvContext _db;

        public TrxSumByMonthService(XixsrvContext db)
        {
            _db = db;
        }

        #region TrxSumByMonths

        public async Task<List<TrxSumByMonth>> GetTrxSumByMonthListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TrxSumByMonths.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TrxSumByMonth> GetTrxSumByMonth(string TrxSumByMonth_id)
        {
            try
            {
                return await _db.TrxSumByMonths.FindAsync(TrxSumByMonth_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TrxSumByMonth>> GetTrxSumByMonthList(string TrxSumByMonth_id)
        {
            try
            {
                return await _db.TrxSumByMonths
                    .Where(i => i.TrxSumByMonthId == TrxSumByMonth_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TrxSumByMonth> AddTrxSumByMonth(TrxSumByMonth TrxSumByMonth)
        {
            try
            {
                await _db.TrxSumByMonths.AddAsync(TrxSumByMonth);
                await _db.SaveChangesAsync();
                return await _db.TrxSumByMonths.FindAsync(TrxSumByMonth.TrxSumByMonthId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TrxSumByMonth> UpdateTrxSumByMonth(TrxSumByMonth TrxSumByMonth)
        {
            try
            {
                _db.Entry(TrxSumByMonth).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TrxSumByMonth;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTrxSumByMonth(TrxSumByMonth TrxSumByMonth)
        {
            try
            {
                var dbTrxSumByMonth = await _db.TrxSumByMonths.FindAsync(TrxSumByMonth.TrxSumByMonthId);

                if (dbTrxSumByMonth == null)
                {
                    return (false, "TrxSumByMonth could not be found");
                }

                _db.TrxSumByMonths.Remove(TrxSumByMonth);
                await _db.SaveChangesAsync();

                return (true, "TrxSumByMonth got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TrxSumByMonths
    }
}
