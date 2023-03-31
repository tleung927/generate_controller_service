using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITransferReqService
    {
        // TransferReqs Services
        Task<List<TransferReq>> GetTransferReqListByValue(int offset, int limit, string val); // GET All TransferReqss
        Task<TransferReq> GetTransferReq(string TransferReq_name); // GET Single TransferReqs        
        Task<List<TransferReq>> GetTransferReqList(string TransferReq_name); // GET List TransferReqs        
        Task<TransferReq> AddTransferReq(TransferReq TransferReq); // POST New TransferReqs
        Task<TransferReq> UpdateTransferReq(TransferReq TransferReq); // PUT TransferReqs
        Task<(bool, string)> DeleteTransferReq(TransferReq TransferReq); // DELETE TransferReqs
    }

    public class TransferReqService : ITransferReqService
    {
        private readonly XixsrvContext _db;

        public TransferReqService(XixsrvContext db)
        {
            _db = db;
        }

        #region TransferReqs

        public async Task<List<TransferReq>> GetTransferReqListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TransferReqs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TransferReq> GetTransferReq(string TransferReq_id)
        {
            try
            {
                return await _db.TransferReqs.FindAsync(TransferReq_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TransferReq>> GetTransferReqList(string TransferReq_id)
        {
            try
            {
                return await _db.TransferReqs
                    .Where(i => i.TransferReqId == TransferReq_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TransferReq> AddTransferReq(TransferReq TransferReq)
        {
            try
            {
                await _db.TransferReqs.AddAsync(TransferReq);
                await _db.SaveChangesAsync();
                return await _db.TransferReqs.FindAsync(TransferReq.TransferReqId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TransferReq> UpdateTransferReq(TransferReq TransferReq)
        {
            try
            {
                _db.Entry(TransferReq).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TransferReq;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTransferReq(TransferReq TransferReq)
        {
            try
            {
                var dbTransferReq = await _db.TransferReqs.FindAsync(TransferReq.TransferReqId);

                if (dbTransferReq == null)
                {
                    return (false, "TransferReq could not be found");
                }

                _db.TransferReqs.Remove(TransferReq);
                await _db.SaveChangesAsync();

                return (true, "TransferReq got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TransferReqs
    }
}
