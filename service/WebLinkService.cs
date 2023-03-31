using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IWebLinkService
    {
        // WebLinks Services
        Task<List<WebLink>> GetWebLinkListByValue(int offset, int limit, string val); // GET All WebLinkss
        Task<WebLink> GetWebLink(string WebLink_name); // GET Single WebLinks        
        Task<List<WebLink>> GetWebLinkList(string WebLink_name); // GET List WebLinks        
        Task<WebLink> AddWebLink(WebLink WebLink); // POST New WebLinks
        Task<WebLink> UpdateWebLink(WebLink WebLink); // PUT WebLinks
        Task<(bool, string)> DeleteWebLink(WebLink WebLink); // DELETE WebLinks
    }

    public class WebLinkService : IWebLinkService
    {
        private readonly XixsrvContext _db;

        public WebLinkService(XixsrvContext db)
        {
            _db = db;
        }

        #region WebLinks

        public async Task<List<WebLink>> GetWebLinkListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.WebLinks.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<WebLink> GetWebLink(string WebLink_id)
        {
            try
            {
                return await _db.WebLinks.FindAsync(WebLink_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<WebLink>> GetWebLinkList(string WebLink_id)
        {
            try
            {
                return await _db.WebLinks
                    .Where(i => i.WebLinkId == WebLink_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<WebLink> AddWebLink(WebLink WebLink)
        {
            try
            {
                await _db.WebLinks.AddAsync(WebLink);
                await _db.SaveChangesAsync();
                return await _db.WebLinks.FindAsync(WebLink.WebLinkId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<WebLink> UpdateWebLink(WebLink WebLink)
        {
            try
            {
                _db.Entry(WebLink).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return WebLink;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteWebLink(WebLink WebLink)
        {
            try
            {
                var dbWebLink = await _db.WebLinks.FindAsync(WebLink.WebLinkId);

                if (dbWebLink == null)
                {
                    return (false, "WebLink could not be found");
                }

                _db.WebLinks.Remove(WebLink);
                await _db.SaveChangesAsync();

                return (true, "WebLink got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion WebLinks
    }
}
