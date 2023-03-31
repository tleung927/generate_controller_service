using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPoService
    {
        // Pos Services
        Task<List<Po>> GetPoListByValue(int offset, int limit, string val); // GET All Poss
        Task<Po> GetPo(string Po_name); // GET Single Pos        
        Task<List<Po>> GetPoList(string Po_name); // GET List Pos        
        Task<Po> AddPo(Po Po); // POST New Pos
        Task<Po> UpdatePo(Po Po); // PUT Pos
        Task<(bool, string)> DeletePo(Po Po); // DELETE Pos
    }

    public class PoService : IPoService
    {
        private readonly XixsrvContext _db;

        public PoService(XixsrvContext db)
        {
            _db = db;
        }

        #region Pos

        public async Task<List<Po>> GetPoListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Pos.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Po> GetPo(string Po_id)
        {
            try
            {
                return await _db.Pos.FindAsync(Po_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Po>> GetPoList(string Po_id)
        {
            try
            {
                return await _db.Pos
                    .Where(i => i.PoId == Po_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Po> AddPo(Po Po)
        {
            try
            {
                await _db.Pos.AddAsync(Po);
                await _db.SaveChangesAsync();
                return await _db.Pos.FindAsync(Po.PoId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Po> UpdatePo(Po Po)
        {
            try
            {
                _db.Entry(Po).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Po;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePo(Po Po)
        {
            try
            {
                var dbPo = await _db.Pos.FindAsync(Po.PoId);

                if (dbPo == null)
                {
                    return (false, "Po could not be found");
                }

                _db.Pos.Remove(Po);
                await _db.SaveChangesAsync();

                return (true, "Po got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Pos
    }
}
