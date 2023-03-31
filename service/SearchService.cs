using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISearchService
    {
        // Searchs Services
        Task<List<Search>> GetSearchListByValue(int offset, int limit, string val); // GET All Searchss
        Task<Search> GetSearch(string Search_name); // GET Single Searchs        
        Task<List<Search>> GetSearchList(string Search_name); // GET List Searchs        
        Task<Search> AddSearch(Search Search); // POST New Searchs
        Task<Search> UpdateSearch(Search Search); // PUT Searchs
        Task<(bool, string)> DeleteSearch(Search Search); // DELETE Searchs
    }

    public class SearchService : ISearchService
    {
        private readonly XixsrvContext _db;

        public SearchService(XixsrvContext db)
        {
            _db = db;
        }

        #region Searchs

        public async Task<List<Search>> GetSearchListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Searchs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Search> GetSearch(string Search_id)
        {
            try
            {
                return await _db.Searchs.FindAsync(Search_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Search>> GetSearchList(string Search_id)
        {
            try
            {
                return await _db.Searchs
                    .Where(i => i.SearchId == Search_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Search> AddSearch(Search Search)
        {
            try
            {
                await _db.Searchs.AddAsync(Search);
                await _db.SaveChangesAsync();
                return await _db.Searchs.FindAsync(Search.SearchId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Search> UpdateSearch(Search Search)
        {
            try
            {
                _db.Entry(Search).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Search;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSearch(Search Search)
        {
            try
            {
                var dbSearch = await _db.Searchs.FindAsync(Search.SearchId);

                if (dbSearch == null)
                {
                    return (false, "Search could not be found");
                }

                _db.Searchs.Remove(Search);
                await _db.SaveChangesAsync();

                return (true, "Search got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Searchs
    }
}
