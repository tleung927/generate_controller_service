using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IForm101Service
    {
        // Form101s Services
        Task<List<Form101>> GetForm101ListByValue(int offset, int limit, string val); // GET All Form101ss
        Task<Form101> GetForm101(string Form101_name); // GET Single Form101s        
        Task<List<Form101>> GetForm101List(string Form101_name); // GET List Form101s        
        Task<Form101> AddForm101(Form101 Form101); // POST New Form101s
        Task<Form101> UpdateForm101(Form101 Form101); // PUT Form101s
        Task<(bool, string)> DeleteForm101(Form101 Form101); // DELETE Form101s
    }

    public class Form101Service : IForm101Service
    {
        private readonly XixsrvContext _db;

        public Form101Service(XixsrvContext db)
        {
            _db = db;
        }

        #region Form101s

        public async Task<List<Form101>> GetForm101ListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Form101s.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Form101> GetForm101(string Form101_id)
        {
            try
            {
                return await _db.Form101s.FindAsync(Form101_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Form101>> GetForm101List(string Form101_id)
        {
            try
            {
                return await _db.Form101s
                    .Where(i => i.Form101Id == Form101_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Form101> AddForm101(Form101 Form101)
        {
            try
            {
                await _db.Form101s.AddAsync(Form101);
                await _db.SaveChangesAsync();
                return await _db.Form101s.FindAsync(Form101.Form101Id); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Form101> UpdateForm101(Form101 Form101)
        {
            try
            {
                _db.Entry(Form101).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Form101;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteForm101(Form101 Form101)
        {
            try
            {
                var dbForm101 = await _db.Form101s.FindAsync(Form101.Form101Id);

                if (dbForm101 == null)
                {
                    return (false, "Form101 could not be found");
                }

                _db.Form101s.Remove(Form101);
                await _db.SaveChangesAsync();

                return (true, "Form101 got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Form101s
    }
}
