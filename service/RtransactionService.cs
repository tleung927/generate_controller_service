using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRtransactionService
    {
        // Rtransactions Services
        Task<List<Rtransaction>> GetRtransactionListByValue(int offset, int limit, string val); // GET All Rtransactionss
        Task<Rtransaction> GetRtransaction(string Rtransaction_name); // GET Single Rtransactions        
        Task<List<Rtransaction>> GetRtransactionList(string Rtransaction_name); // GET List Rtransactions        
        Task<Rtransaction> AddRtransaction(Rtransaction Rtransaction); // POST New Rtransactions
        Task<Rtransaction> UpdateRtransaction(Rtransaction Rtransaction); // PUT Rtransactions
        Task<(bool, string)> DeleteRtransaction(Rtransaction Rtransaction); // DELETE Rtransactions
    }

    public class RtransactionService : IRtransactionService
    {
        private readonly XixsrvContext _db;

        public RtransactionService(XixsrvContext db)
        {
            _db = db;
        }

        #region Rtransactions

        public async Task<List<Rtransaction>> GetRtransactionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Rtransactions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Rtransaction> GetRtransaction(string Rtransaction_id)
        {
            try
            {
                return await _db.Rtransactions.FindAsync(Rtransaction_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Rtransaction>> GetRtransactionList(string Rtransaction_id)
        {
            try
            {
                return await _db.Rtransactions
                    .Where(i => i.RtransactionId == Rtransaction_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Rtransaction> AddRtransaction(Rtransaction Rtransaction)
        {
            try
            {
                await _db.Rtransactions.AddAsync(Rtransaction);
                await _db.SaveChangesAsync();
                return await _db.Rtransactions.FindAsync(Rtransaction.RtransactionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Rtransaction> UpdateRtransaction(Rtransaction Rtransaction)
        {
            try
            {
                _db.Entry(Rtransaction).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Rtransaction;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRtransaction(Rtransaction Rtransaction)
        {
            try
            {
                var dbRtransaction = await _db.Rtransactions.FindAsync(Rtransaction.RtransactionId);

                if (dbRtransaction == null)
                {
                    return (false, "Rtransaction could not be found");
                }

                _db.Rtransactions.Remove(Rtransaction);
                await _db.SaveChangesAsync();

                return (true, "Rtransaction got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Rtransactions
    }
}
