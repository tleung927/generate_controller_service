using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFcppService
    {
        // Fcpps Services
        Task<List<Fcpp>> GetFcppListByValue(int offset, int limit, string val); // GET All Fcppss
        Task<Fcpp> GetFcpp(string Fcpp_name); // GET Single Fcpps        
        Task<List<Fcpp>> GetFcppList(string Fcpp_name); // GET List Fcpps        
        Task<Fcpp> AddFcpp(Fcpp Fcpp); // POST New Fcpps
        Task<Fcpp> UpdateFcpp(Fcpp Fcpp); // PUT Fcpps
        Task<(bool, string)> DeleteFcpp(Fcpp Fcpp); // DELETE Fcpps
    }

    public class FcppService : IFcppService
    {
        private readonly XixsrvContext _db;

        public FcppService(XixsrvContext db)
        {
            _db = db;
        }

        #region Fcpps

        public async Task<List<Fcpp>> GetFcppListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Fcpps.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Fcpp> GetFcpp(string Fcpp_id)
        {
            try
            {
                return await _db.Fcpps.FindAsync(Fcpp_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Fcpp>> GetFcppList(string Fcpp_id)
        {
            try
            {
                return await _db.Fcpps
                    .Where(i => i.FcppId == Fcpp_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Fcpp> AddFcpp(Fcpp Fcpp)
        {
            try
            {
                await _db.Fcpps.AddAsync(Fcpp);
                await _db.SaveChangesAsync();
                return await _db.Fcpps.FindAsync(Fcpp.FcppId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Fcpp> UpdateFcpp(Fcpp Fcpp)
        {
            try
            {
                _db.Entry(Fcpp).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Fcpp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFcpp(Fcpp Fcpp)
        {
            try
            {
                var dbFcpp = await _db.Fcpps.FindAsync(Fcpp.FcppId);

                if (dbFcpp == null)
                {
                    return (false, "Fcpp could not be found");
                }

                _db.Fcpps.Remove(Fcpp);
                await _db.SaveChangesAsync();

                return (true, "Fcpp got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Fcpps
    }
}
