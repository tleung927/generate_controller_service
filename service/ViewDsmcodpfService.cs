using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IViewDsmcodpfService
    {
        // ViewDsmcodpfs Services
        Task<List<ViewDsmcodpf>> GetViewDsmcodpfListByValue(int offset, int limit, string val); // GET All ViewDsmcodpfss
        Task<ViewDsmcodpf> GetViewDsmcodpf(string ViewDsmcodpf_name); // GET Single ViewDsmcodpfs        
        Task<List<ViewDsmcodpf>> GetViewDsmcodpfList(string ViewDsmcodpf_name); // GET List ViewDsmcodpfs        
        Task<ViewDsmcodpf> AddViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf); // POST New ViewDsmcodpfs
        Task<ViewDsmcodpf> UpdateViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf); // PUT ViewDsmcodpfs
        Task<(bool, string)> DeleteViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf); // DELETE ViewDsmcodpfs
    }

    public class ViewDsmcodpfService : IViewDsmcodpfService
    {
        private readonly XixsrvContext _db;

        public ViewDsmcodpfService(XixsrvContext db)
        {
            _db = db;
        }

        #region ViewDsmcodpfs

        public async Task<List<ViewDsmcodpf>> GetViewDsmcodpfListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ViewDsmcodpfs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ViewDsmcodpf> GetViewDsmcodpf(string ViewDsmcodpf_id)
        {
            try
            {
                return await _db.ViewDsmcodpfs.FindAsync(ViewDsmcodpf_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ViewDsmcodpf>> GetViewDsmcodpfList(string ViewDsmcodpf_id)
        {
            try
            {
                return await _db.ViewDsmcodpfs
                    .Where(i => i.ViewDsmcodpfId == ViewDsmcodpf_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ViewDsmcodpf> AddViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf)
        {
            try
            {
                await _db.ViewDsmcodpfs.AddAsync(ViewDsmcodpf);
                await _db.SaveChangesAsync();
                return await _db.ViewDsmcodpfs.FindAsync(ViewDsmcodpf.ViewDsmcodpfId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ViewDsmcodpf> UpdateViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf)
        {
            try
            {
                _db.Entry(ViewDsmcodpf).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ViewDsmcodpf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteViewDsmcodpf(ViewDsmcodpf ViewDsmcodpf)
        {
            try
            {
                var dbViewDsmcodpf = await _db.ViewDsmcodpfs.FindAsync(ViewDsmcodpf.ViewDsmcodpfId);

                if (dbViewDsmcodpf == null)
                {
                    return (false, "ViewDsmcodpf could not be found");
                }

                _db.ViewDsmcodpfs.Remove(ViewDsmcodpf);
                await _db.SaveChangesAsync();

                return (true, "ViewDsmcodpf got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ViewDsmcodpfs
    }
}
