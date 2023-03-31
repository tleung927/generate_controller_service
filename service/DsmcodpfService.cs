using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IDsmcodpfService
    {
        // Dsmcodpfs Services
        Task<List<Dsmcodpf>> GetDsmcodpfListByValue(int offset, int limit, string val); // GET All Dsmcodpfss
        Task<Dsmcodpf> GetDsmcodpf(string Dsmcodpf_name); // GET Single Dsmcodpfs        
        Task<List<Dsmcodpf>> GetDsmcodpfList(string Dsmcodpf_name); // GET List Dsmcodpfs        
        Task<Dsmcodpf> AddDsmcodpf(Dsmcodpf Dsmcodpf); // POST New Dsmcodpfs
        Task<Dsmcodpf> UpdateDsmcodpf(Dsmcodpf Dsmcodpf); // PUT Dsmcodpfs
        Task<(bool, string)> DeleteDsmcodpf(Dsmcodpf Dsmcodpf); // DELETE Dsmcodpfs
    }

    public class DsmcodpfService : IDsmcodpfService
    {
        private readonly XixsrvContext _db;

        public DsmcodpfService(XixsrvContext db)
        {
            _db = db;
        }

        #region Dsmcodpfs

        public async Task<List<Dsmcodpf>> GetDsmcodpfListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Dsmcodpfs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Dsmcodpf> GetDsmcodpf(string Dsmcodpf_id)
        {
            try
            {
                return await _db.Dsmcodpfs.FindAsync(Dsmcodpf_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Dsmcodpf>> GetDsmcodpfList(string Dsmcodpf_id)
        {
            try
            {
                return await _db.Dsmcodpfs
                    .Where(i => i.DsmcodpfId == Dsmcodpf_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Dsmcodpf> AddDsmcodpf(Dsmcodpf Dsmcodpf)
        {
            try
            {
                await _db.Dsmcodpfs.AddAsync(Dsmcodpf);
                await _db.SaveChangesAsync();
                return await _db.Dsmcodpfs.FindAsync(Dsmcodpf.DsmcodpfId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Dsmcodpf> UpdateDsmcodpf(Dsmcodpf Dsmcodpf)
        {
            try
            {
                _db.Entry(Dsmcodpf).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Dsmcodpf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteDsmcodpf(Dsmcodpf Dsmcodpf)
        {
            try
            {
                var dbDsmcodpf = await _db.Dsmcodpfs.FindAsync(Dsmcodpf.DsmcodpfId);

                if (dbDsmcodpf == null)
                {
                    return (false, "Dsmcodpf could not be found");
                }

                _db.Dsmcodpfs.Remove(Dsmcodpf);
                await _db.SaveChangesAsync();

                return (true, "Dsmcodpf got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Dsmcodpfs
    }
}
