using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEsrDelayService
    {
        // EsrDelays Services
        Task<List<EsrDelay>> GetEsrDelayListByValue(int offset, int limit, string val); // GET All EsrDelayss
        Task<EsrDelay> GetEsrDelay(string EsrDelay_name); // GET Single EsrDelays        
        Task<List<EsrDelay>> GetEsrDelayList(string EsrDelay_name); // GET List EsrDelays        
        Task<EsrDelay> AddEsrDelay(EsrDelay EsrDelay); // POST New EsrDelays
        Task<EsrDelay> UpdateEsrDelay(EsrDelay EsrDelay); // PUT EsrDelays
        Task<(bool, string)> DeleteEsrDelay(EsrDelay EsrDelay); // DELETE EsrDelays
    }

    public class EsrDelayService : IEsrDelayService
    {
        private readonly XixsrvContext _db;

        public EsrDelayService(XixsrvContext db)
        {
            _db = db;
        }

        #region EsrDelays

        public async Task<List<EsrDelay>> GetEsrDelayListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.EsrDelays.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<EsrDelay> GetEsrDelay(string EsrDelay_id)
        {
            try
            {
                return await _db.EsrDelays.FindAsync(EsrDelay_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<EsrDelay>> GetEsrDelayList(string EsrDelay_id)
        {
            try
            {
                return await _db.EsrDelays
                    .Where(i => i.EsrDelayId == EsrDelay_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<EsrDelay> AddEsrDelay(EsrDelay EsrDelay)
        {
            try
            {
                await _db.EsrDelays.AddAsync(EsrDelay);
                await _db.SaveChangesAsync();
                return await _db.EsrDelays.FindAsync(EsrDelay.EsrDelayId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<EsrDelay> UpdateEsrDelay(EsrDelay EsrDelay)
        {
            try
            {
                _db.Entry(EsrDelay).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return EsrDelay;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEsrDelay(EsrDelay EsrDelay)
        {
            try
            {
                var dbEsrDelay = await _db.EsrDelays.FindAsync(EsrDelay.EsrDelayId);

                if (dbEsrDelay == null)
                {
                    return (false, "EsrDelay could not be found");
                }

                _db.EsrDelays.Remove(EsrDelay);
                await _db.SaveChangesAsync();

                return (true, "EsrDelay got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion EsrDelays
    }
}
