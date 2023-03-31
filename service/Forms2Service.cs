using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IForms2Service
    {
        // Forms2s Services
        Task<List<Forms2>> GetForms2ListByValue(int offset, int limit, string val); // GET All Forms2ss
        Task<Forms2> GetForms2(string Forms2_name); // GET Single Forms2s        
        Task<List<Forms2>> GetForms2List(string Forms2_name); // GET List Forms2s        
        Task<Forms2> AddForms2(Forms2 Forms2); // POST New Forms2s
        Task<Forms2> UpdateForms2(Forms2 Forms2); // PUT Forms2s
        Task<(bool, string)> DeleteForms2(Forms2 Forms2); // DELETE Forms2s
    }

    public class Forms2Service : IForms2Service
    {
        private readonly XixsrvContext _db;

        public Forms2Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Forms2s

        public async Task<List<Forms2>> GetForms2ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Forms2s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Forms2> GetForms2(string Forms2_id)
        {
            try
            {
                return await _db.Forms2s.FindAsync(Forms2_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Forms2>> GetForms2List(string Forms2_id)
        {
            try
            {
                return await _db.Forms2s
                    .Where(i => i.Forms2Id == Forms2_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Forms2> AddForms2(Forms2 Forms2)
        {
            try
            {
                await _db.Forms2s.AddAsync(Forms2);
                await _db.SaveChangesAsync();
                return await _db.Forms2s.FindAsync(Forms2.Forms2Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Forms2> UpdateForms2(Forms2 Forms2)
        {
            try
            {
                _db.Entry(Forms2).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Forms2;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteForms2(Forms2 Forms2)
        {
            try
            {
                var dbForms2 = await _db.Forms2s.FindAsync(Forms2.Forms2Id);

                if (dbForms2 == null)
                {
                    return (false, "Forms2 could not be found");
                }

                _db.Forms2s.Remove(Forms2);
                await _db.SaveChangesAsync();

                return (true, "Forms2 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Forms2s
    }
}
