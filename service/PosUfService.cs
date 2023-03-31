using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPosUfService
    {
        // PosUfs Services
        Task<List<PosUf>> GetPosUfListByValue(int offset, int limit, string val); // GET All PosUfss
        Task<PosUf> GetPosUf(string PosUf_name); // GET Single PosUfs        
        Task<List<PosUf>> GetPosUfList(string PosUf_name); // GET List PosUfs        
        Task<PosUf> AddPosUf(PosUf PosUf); // POST New PosUfs
        Task<PosUf> UpdatePosUf(PosUf PosUf); // PUT PosUfs
        Task<(bool, string)> DeletePosUf(PosUf PosUf); // DELETE PosUfs
    }

    public class PosUfService : IPosUfService
    {
        private readonly XixsrvContext _db;

        public PosUfService(XixsrvContext db)
        {
            _db = db;
        }

        #region PosUfs

        public async Task<List<PosUf>> GetPosUfListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PosUfs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PosUf> GetPosUf(string PosUf_id)
        {
            try
            {
                return await _db.PosUfs.FindAsync(PosUf_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PosUf>> GetPosUfList(string PosUf_id)
        {
            try
            {
                return await _db.PosUfs
                    .Where(i => i.PosUfId == PosUf_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PosUf> AddPosUf(PosUf PosUf)
        {
            try
            {
                await _db.PosUfs.AddAsync(PosUf);
                await _db.SaveChangesAsync();
                return await _db.PosUfs.FindAsync(PosUf.PosUfId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PosUf> UpdatePosUf(PosUf PosUf)
        {
            try
            {
                _db.Entry(PosUf).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PosUf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePosUf(PosUf PosUf)
        {
            try
            {
                var dbPosUf = await _db.PosUfs.FindAsync(PosUf.PosUfId);

                if (dbPosUf == null)
                {
                    return (false, "PosUf could not be found");
                }

                _db.PosUfs.Remove(PosUf);
                await _db.SaveChangesAsync();

                return (true, "PosUf got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PosUfs
    }
}
