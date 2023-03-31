using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransferTempService
    {
        // TransferTemps Services
        Task<List<TransferTemp>> GetTransferTempListByValue(int offset, int limit, string val); // GET All TransferTempss
        Task<TransferTemp> GetTransferTemp(string TransferTemp_name); // GET Single TransferTemps        
        Task<List<TransferTemp>> GetTransferTempList(string TransferTemp_name); // GET List TransferTemps        
        Task<TransferTemp> AddTransferTemp(TransferTemp TransferTemp); // POST New TransferTemps
        Task<TransferTemp> UpdateTransferTemp(TransferTemp TransferTemp); // PUT TransferTemps
        Task<(bool, string)> DeleteTransferTemp(TransferTemp TransferTemp); // DELETE TransferTemps
    }

    public class TransferTempService : ITransferTempService
    {
        private readonly XixsrvContext _db;

        public TransferTempService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransferTemps

        public async Task<List<TransferTemp>> GetTransferTempListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransferTemps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransferTemp> GetTransferTemp(string TransferTemp_id)
        {
            try
            {
                return await _db.TransferTemps.FindAsync(TransferTemp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransferTemp>> GetTransferTempList(string TransferTemp_id)
        {
            try
            {
                return await _db.TransferTemps
                    .Where(i => i.TransferTempId == TransferTemp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransferTemp> AddTransferTemp(TransferTemp TransferTemp)
        {
            try
            {
                await _db.TransferTemps.AddAsync(TransferTemp);
                await _db.SaveChangesAsync();
                return await _db.TransferTemps.FindAsync(TransferTemp.TransferTempId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransferTemp> UpdateTransferTemp(TransferTemp TransferTemp)
        {
            try
            {
                _db.Entry(TransferTemp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransferTemp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransferTemp(TransferTemp TransferTemp)
        {
            try
            {
                var dbTransferTemp = await _db.TransferTemps.FindAsync(TransferTemp.TransferTempId);

                if (dbTransferTemp == null)
                {
                    return (false, "TransferTemp could not be found");
                }

                _db.TransferTemps.Remove(TransferTemp);
                await _db.SaveChangesAsync();

                return (true, "TransferTemp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransferTemps
    }
}
