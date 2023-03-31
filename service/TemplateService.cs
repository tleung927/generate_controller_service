using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ITemplateService
    {
        // Templates Services
        Task<List<Template>> GetTemplateListByValue(int offset, int limit, string val); // GET All Templatess
        Task<Template> GetTemplate(string Template_name); // GET Single Templates        
        Task<List<Template>> GetTemplateList(string Template_name); // GET List Templates        
        Task<Template> AddTemplate(Template Template); // POST New Templates
        Task<Template> UpdateTemplate(Template Template); // PUT Templates
        Task<(bool, string)> DeleteTemplate(Template Template); // DELETE Templates
    }

    public class TemplateService : ITemplateService
    {
        private readonly XixsrvContext _db;

        public TemplateService(XixsrvContext db)
        {
            _db = db;
        }

        #region Templates

        public async Task<List<Template>> GetTemplateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Templates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Template> GetTemplate(string Template_id)
        {
            try
            {
                return await _db.Templates.FindAsync(Template_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Template>> GetTemplateList(string Template_id)
        {
            try
            {
                return await _db.Templates
                    .Where(i => i.TemplateId == Template_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Template> AddTemplate(Template Template)
        {
            try
            {
                await _db.Templates.AddAsync(Template);
                await _db.SaveChangesAsync();
                return await _db.Templates.FindAsync(Template.TemplateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Template> UpdateTemplate(Template Template)
        {
            try
            {
                _db.Entry(Template).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Template;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteTemplate(Template Template)
        {
            try
            {
                var dbTemplate = await _db.Templates.FindAsync(Template.TemplateId);

                if (dbTemplate == null)
                {
                    return (false, "Template could not be found");
                }

                _db.Templates.Remove(Template);
                await _db.SaveChangesAsync();

                return (true, "Template got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Templates
    }
}
