using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPcpTableService
    {
        // PcpTables Services
        Task<List<PcpTable>> GetPcpTableListByValue(int offset, int limit, string val); // GET All PcpTabless
        Task<PcpTable> GetPcpTable(string PcpTable_name); // GET Single PcpTables        
        Task<List<PcpTable>> GetPcpTableList(string PcpTable_name); // GET List PcpTables        
        Task<PcpTable> AddPcpTable(PcpTable PcpTable); // POST New PcpTables
        Task<PcpTable> UpdatePcpTable(PcpTable PcpTable); // PUT PcpTables
        Task<(bool, string)> DeletePcpTable(PcpTable PcpTable); // DELETE PcpTables
    }

    public class PcpTableService : IPcpTableService
    {
        private readonly XixsrvContext _db;

        public PcpTableService(XixsrvContext db)
        {
            _db = db;
        }

        #region PcpTables

        public async Task<List<PcpTable>> GetPcpTableListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PcpTables.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PcpTable> GetPcpTable(string PcpTable_id)
        {
            try
            {
                return await _db.PcpTables.FindAsync(PcpTable_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PcpTable>> GetPcpTableList(string PcpTable_id)
        {
            try
            {
                return await _db.PcpTables
                    .Where(i => i.PcpTableId == PcpTable_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PcpTable> AddPcpTable(PcpTable PcpTable)
        {
            try
            {
                await _db.PcpTables.AddAsync(PcpTable);
                await _db.SaveChangesAsync();
                return await _db.PcpTables.FindAsync(PcpTable.PcpTableId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PcpTable> UpdatePcpTable(PcpTable PcpTable)
        {
            try
            {
                _db.Entry(PcpTable).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PcpTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePcpTable(PcpTable PcpTable)
        {
            try
            {
                var dbPcpTable = await _db.PcpTables.FindAsync(PcpTable.PcpTableId);

                if (dbPcpTable == null)
                {
                    return (false, "PcpTable could not be found");
                }

                _db.PcpTables.Remove(PcpTable);
                await _db.SaveChangesAsync();

                return (true, "PcpTable got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PcpTables
    }
}
