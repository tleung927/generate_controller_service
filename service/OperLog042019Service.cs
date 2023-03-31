using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IOperLog042019Service
    {
        // OperLog042019s Services
        Task<List<OperLog042019>> GetOperLog042019ListByValue(int offset, int limit, string val); // GET All OperLog042019ss
        Task<OperLog042019> GetOperLog042019(string OperLog042019_name); // GET Single OperLog042019s        
        Task<List<OperLog042019>> GetOperLog042019List(string OperLog042019_name); // GET List OperLog042019s        
        Task<OperLog042019> AddOperLog042019(OperLog042019 OperLog042019); // POST New OperLog042019s
        Task<OperLog042019> UpdateOperLog042019(OperLog042019 OperLog042019); // PUT OperLog042019s
        Task<(bool, string)> DeleteOperLog042019(OperLog042019 OperLog042019); // DELETE OperLog042019s
    }

    public class OperLog042019Service : IOperLog042019Service
    {
        private readonly XixsrvContext _db;

        public OperLog042019Service(XixsrvContext db)
        {
            _db = db;
        }

        #region OperLog042019s

        public async Task<List<OperLog042019>> GetOperLog042019ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.OperLog042019s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<OperLog042019> GetOperLog042019(string OperLog042019_id)
        {
            try
            {
                return await _db.OperLog042019s.FindAsync(OperLog042019_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<OperLog042019>> GetOperLog042019List(string OperLog042019_id)
        {
            try
            {
                return await _db.OperLog042019s
                    .Where(i => i.OperLog042019Id == OperLog042019_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<OperLog042019> AddOperLog042019(OperLog042019 OperLog042019)
        {
            try
            {
                await _db.OperLog042019s.AddAsync(OperLog042019);
                await _db.SaveChangesAsync();
                return await _db.OperLog042019s.FindAsync(OperLog042019.OperLog042019Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<OperLog042019> UpdateOperLog042019(OperLog042019 OperLog042019)
        {
            try
            {
                _db.Entry(OperLog042019).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return OperLog042019;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteOperLog042019(OperLog042019 OperLog042019)
        {
            try
            {
                var dbOperLog042019 = await _db.OperLog042019s.FindAsync(OperLog042019.OperLog042019Id);

                if (dbOperLog042019 == null)
                {
                    return (false, "OperLog042019 could not be found");
                }

                _db.OperLog042019s.Remove(OperLog042019);
                await _db.SaveChangesAsync();

                return (true, "OperLog042019 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion OperLog042019s
    }
}
