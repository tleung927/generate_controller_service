using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IOperTraceService
    {
        // OperTraces Services
        Task<List<OperTrace>> GetOperTraceListByValue(int offset, int limit, string val); // GET All OperTracess
        Task<OperTrace> GetOperTrace(string OperTrace_name); // GET Single OperTraces        
        Task<List<OperTrace>> GetOperTraceList(string OperTrace_name); // GET List OperTraces        
        Task<OperTrace> AddOperTrace(OperTrace OperTrace); // POST New OperTraces
        Task<OperTrace> UpdateOperTrace(OperTrace OperTrace); // PUT OperTraces
        Task<(bool, string)> DeleteOperTrace(OperTrace OperTrace); // DELETE OperTraces
    }

    public class OperTraceService : IOperTraceService
    {
        private readonly XixsrvContext _db;

        public OperTraceService(XixsrvContext db)
        {
            _db = db;
        }

        #region OperTraces

        public async Task<List<OperTrace>> GetOperTraceListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.OperTraces.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OperTrace> GetOperTrace(string OperTrace_id)
        {
            try
            {
                return await _db.OperTraces.FindAsync(OperTrace_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<OperTrace>> GetOperTraceList(string OperTrace_id)
        {
            try
            {
                return await _db.OperTraces
                    .Where(i => i.OperTraceId == OperTrace_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<OperTrace> AddOperTrace(OperTrace OperTrace)
        {
            try
            {
                await _db.OperTraces.AddAsync(OperTrace);
                await _db.SaveChangesAsync();
                return await _db.OperTraces.FindAsync(OperTrace.OperTraceId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<OperTrace> UpdateOperTrace(OperTrace OperTrace)
        {
            try
            {
                _db.Entry(OperTrace).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return OperTrace;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteOperTrace(OperTrace OperTrace)
        {
            try
            {
                var dbOperTrace = await _db.OperTraces.FindAsync(OperTrace.OperTraceId);

                if (dbOperTrace == null)
                {
                    return (false, "OperTrace could not be found");
                }

                _db.OperTraces.Remove(OperTrace);
                await _db.SaveChangesAsync();

                return (true, "OperTrace got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion OperTraces
    }
}
