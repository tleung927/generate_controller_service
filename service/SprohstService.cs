using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ISprohstService
    {
        // Sprohsts Services
        Task<List<Sprohst>> GetSprohstListByValue(int offset, int limit, string val); // GET All Sprohstss
        Task<Sprohst> GetSprohst(string Sprohst_name); // GET Single Sprohsts        
        Task<List<Sprohst>> GetSprohstList(string Sprohst_name); // GET List Sprohsts        
        Task<Sprohst> AddSprohst(Sprohst Sprohst); // POST New Sprohsts
        Task<Sprohst> UpdateSprohst(Sprohst Sprohst); // PUT Sprohsts
        Task<(bool, string)> DeleteSprohst(Sprohst Sprohst); // DELETE Sprohsts
    }

    public class SprohstService : ISprohstService
    {
        private readonly XixsrvContext _db;

        public SprohstService(XixsrvContext db)
        {
            _db = db;
        }

        #region Sprohsts

        public async Task<List<Sprohst>> GetSprohstListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Sprohsts.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Sprohst> GetSprohst(string Sprohst_id)
        {
            try
            {
                return await _db.Sprohsts.FindAsync(Sprohst_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Sprohst>> GetSprohstList(string Sprohst_id)
        {
            try
            {
                return await _db.Sprohsts
                    .Where(i => i.SprohstId == Sprohst_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Sprohst> AddSprohst(Sprohst Sprohst)
        {
            try
            {
                await _db.Sprohsts.AddAsync(Sprohst);
                await _db.SaveChangesAsync();
                return await _db.Sprohsts.FindAsync(Sprohst.SprohstId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Sprohst> UpdateSprohst(Sprohst Sprohst)
        {
            try
            {
                _db.Entry(Sprohst).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Sprohst;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteSprohst(Sprohst Sprohst)
        {
            try
            {
                var dbSprohst = await _db.Sprohsts.FindAsync(Sprohst.SprohstId);

                if (dbSprohst == null)
                {
                    return (false, "Sprohst could not be found");
                }

                _db.Sprohsts.Remove(Sprohst);
                await _db.SaveChangesAsync();

                return (true, "Sprohst got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Sprohsts
    }
}
