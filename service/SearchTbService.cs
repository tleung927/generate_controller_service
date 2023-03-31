using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISearchTbService
    {
        // SearchTbs Services
        Task<List<SearchTb>> GetSearchTbListByValue(int offset, int limit, string val); // GET All SearchTbss
        Task<SearchTb> GetSearchTb(string SearchTb_name); // GET Single SearchTbs        
        Task<List<SearchTb>> GetSearchTbList(string SearchTb_name); // GET List SearchTbs        
        Task<SearchTb> AddSearchTb(SearchTb SearchTb); // POST New SearchTbs
        Task<SearchTb> UpdateSearchTb(SearchTb SearchTb); // PUT SearchTbs
        Task<(bool, string)> DeleteSearchTb(SearchTb SearchTb); // DELETE SearchTbs
    }

    public class SearchTbService : ISearchTbService
    {
        private readonly XixsrvContext _db;

        public SearchTbService(XixsrvContext db)
        {
            _db = db;
        }

        #region SearchTbs

        public async Task<List<SearchTb>> GetSearchTbListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.SearchTbs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<SearchTb> GetSearchTb(string SearchTb_id)
        {
            try
            {
                return await _db.SearchTbs.FindAsync(SearchTb_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<SearchTb>> GetSearchTbList(string SearchTb_id)
        {
            try
            {
                return await _db.SearchTbs
                    .Where(i => i.SearchTbId == SearchTb_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<SearchTb> AddSearchTb(SearchTb SearchTb)
        {
            try
            {
                await _db.SearchTbs.AddAsync(SearchTb);
                await _db.SaveChangesAsync();
                return await _db.SearchTbs.FindAsync(SearchTb.SearchTbId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<SearchTb> UpdateSearchTb(SearchTb SearchTb)
        {
            try
            {
                _db.Entry(SearchTb).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return SearchTb;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSearchTb(SearchTb SearchTb)
        {
            try
            {
                var dbSearchTb = await _db.SearchTbs.FindAsync(SearchTb.SearchTbId);

                if (dbSearchTb == null)
                {
                    return (false, "SearchTb could not be found");
                }

                _db.SearchTbs.Remove(SearchTb);
                await _db.SaveChangesAsync();

                return (true, "SearchTb got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion SearchTbs
    }
}
