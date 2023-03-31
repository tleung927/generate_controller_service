using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITicklersSetService
    {
        // TicklersSets Services
        Task<List<TicklersSet>> GetTicklersSetListByValue(int offset, int limit, string val); // GET All TicklersSetss
        Task<TicklersSet> GetTicklersSet(string TicklersSet_name); // GET Single TicklersSets        
        Task<List<TicklersSet>> GetTicklersSetList(string TicklersSet_name); // GET List TicklersSets        
        Task<TicklersSet> AddTicklersSet(TicklersSet TicklersSet); // POST New TicklersSets
        Task<TicklersSet> UpdateTicklersSet(TicklersSet TicklersSet); // PUT TicklersSets
        Task<(bool, string)> DeleteTicklersSet(TicklersSet TicklersSet); // DELETE TicklersSets
    }

    public class TicklersSetService : ITicklersSetService
    {
        private readonly XixsrvContext _db;

        public TicklersSetService(XixsrvContext db)
        {
            _db = db;
        }

        #region TicklersSets

        public async Task<List<TicklersSet>> GetTicklersSetListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.TicklersSets.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<TicklersSet> GetTicklersSet(string TicklersSet_id)
        {
            try
            {
                return await _db.TicklersSets.FindAsync(TicklersSet_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<TicklersSet>> GetTicklersSetList(string TicklersSet_id)
        {
            try
            {
                return await _db.TicklersSets
                    .Where(i => i.TicklersSetId == TicklersSet_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<TicklersSet> AddTicklersSet(TicklersSet TicklersSet)
        {
            try
            {
                await _db.TicklersSets.AddAsync(TicklersSet);
                await _db.SaveChangesAsync();
                return await _db.TicklersSets.FindAsync(TicklersSet.TicklersSetId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<TicklersSet> UpdateTicklersSet(TicklersSet TicklersSet)
        {
            try
            {
                _db.Entry(TicklersSet).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return TicklersSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTicklersSet(TicklersSet TicklersSet)
        {
            try
            {
                var dbTicklersSet = await _db.TicklersSets.FindAsync(TicklersSet.TicklersSetId);

                if (dbTicklersSet == null)
                {
                    return (false, "TicklersSet could not be found");
                }

                _db.TicklersSets.Remove(TicklersSet);
                await _db.SaveChangesAsync();

                return (true, "TicklersSet got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion TicklersSets
    }
}
