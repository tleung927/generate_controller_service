using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITableService
    {
        // Tables Services
        Task<List<Table>> GetTableListByValue(int offset, int limit, string val); // GET All Tabless
        Task<Table> GetTable(string Table_name); // GET Single Tables        
        Task<List<Table>> GetTableList(string Table_name); // GET List Tables        
        Task<Table> AddTable(Table Table); // POST New Tables
        Task<Table> UpdateTable(Table Table); // PUT Tables
        Task<(bool, string)> DeleteTable(Table Table); // DELETE Tables
    }

    public class TableService : ITableService
    {
        private readonly XixsrvContext _db;

        public TableService(XixsrvContext db)
        {
            _db = db;
        }

        #region Tables

        public async Task<List<Table>> GetTableListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Tables.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Table> GetTable(string Table_id)
        {
            try
            {
                return await _db.Tables.FindAsync(Table_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Table>> GetTableList(string Table_id)
        {
            try
            {
                return await _db.Tables
                    .Where(i => i.TableId == Table_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Table> AddTable(Table Table)
        {
            try
            {
                await _db.Tables.AddAsync(Table);
                await _db.SaveChangesAsync();
                return await _db.Tables.FindAsync(Table.TableId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Table> UpdateTable(Table Table)
        {
            try
            {
                _db.Entry(Table).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Table;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTable(Table Table)
        {
            try
            {
                var dbTable = await _db.Tables.FindAsync(Table.TableId);

                if (dbTable == null)
                {
                    return (false, "Table could not be found");
                }

                _db.Tables.Remove(Table);
                await _db.SaveChangesAsync();

                return (true, "Table got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Tables
    }
}
