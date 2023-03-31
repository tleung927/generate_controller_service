using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewLangService
    {
        // ViewLangs Services
        Task<List<ViewLang>> GetViewLangListByValue(int offset, int limit, string val); // GET All ViewLangss
        Task<ViewLang> GetViewLang(string ViewLang_name); // GET Single ViewLangs        
        Task<List<ViewLang>> GetViewLangList(string ViewLang_name); // GET List ViewLangs        
        Task<ViewLang> AddViewLang(ViewLang ViewLang); // POST New ViewLangs
        Task<ViewLang> UpdateViewLang(ViewLang ViewLang); // PUT ViewLangs
        Task<(bool, string)> DeleteViewLang(ViewLang ViewLang); // DELETE ViewLangs
    }

    public class ViewLangService : IViewLangService
    {
        private readonly XixsrvContext _db;

        public ViewLangService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewLangs

        public async Task<List<ViewLang>> GetViewLangListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewLangs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewLang> GetViewLang(string ViewLang_id)
        {
            try
            {
                return await _db.ViewLangs.FindAsync(ViewLang_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewLang>> GetViewLangList(string ViewLang_id)
        {
            try
            {
                return await _db.ViewLangs
                    .Where(i => i.ViewLangId == ViewLang_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewLang> AddViewLang(ViewLang ViewLang)
        {
            try
            {
                await _db.ViewLangs.AddAsync(ViewLang);
                await _db.SaveChangesAsync();
                return await _db.ViewLangs.FindAsync(ViewLang.ViewLangId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewLang> UpdateViewLang(ViewLang ViewLang)
        {
            try
            {
                _db.Entry(ViewLang).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewLang;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewLang(ViewLang ViewLang)
        {
            try
            {
                var dbViewLang = await _db.ViewLangs.FindAsync(ViewLang.ViewLangId);

                if (dbViewLang == null)
                {
                    return (false, "ViewLang could not be found");
                }

                _db.ViewLangs.Remove(ViewLang);
                await _db.SaveChangesAsync();

                return (true, "ViewLang got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewLangs
    }
}
