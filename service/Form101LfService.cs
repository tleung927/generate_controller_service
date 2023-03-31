using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IForm101LfService
    {
        // Form101Lfs Services
        Task<List<Form101Lf>> GetForm101LfListByValue(int offset, int limit, string val); // GET All Form101Lfss
        Task<Form101Lf> GetForm101Lf(string Form101Lf_name); // GET Single Form101Lfs        
        Task<List<Form101Lf>> GetForm101LfList(string Form101Lf_name); // GET List Form101Lfs        
        Task<Form101Lf> AddForm101Lf(Form101Lf Form101Lf); // POST New Form101Lfs
        Task<Form101Lf> UpdateForm101Lf(Form101Lf Form101Lf); // PUT Form101Lfs
        Task<(bool, string)> DeleteForm101Lf(Form101Lf Form101Lf); // DELETE Form101Lfs
    }

    public class Form101LfService : IForm101LfService
    {
        private readonly XixsrvContext _db;

        public Form101LfService(XixsrvContext db)
        {
            _db = db;
        }

        #region Form101Lfs

        public async Task<List<Form101Lf>> GetForm101LfListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Form101Lfs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Form101Lf> GetForm101Lf(string Form101Lf_id)
        {
            try
            {
                return await _db.Form101Lfs.FindAsync(Form101Lf_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Form101Lf>> GetForm101LfList(string Form101Lf_id)
        {
            try
            {
                return await _db.Form101Lfs
                    .Where(i => i.Form101LfId == Form101Lf_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Form101Lf> AddForm101Lf(Form101Lf Form101Lf)
        {
            try
            {
                await _db.Form101Lfs.AddAsync(Form101Lf);
                await _db.SaveChangesAsync();
                return await _db.Form101Lfs.FindAsync(Form101Lf.Form101LfId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Form101Lf> UpdateForm101Lf(Form101Lf Form101Lf)
        {
            try
            {
                _db.Entry(Form101Lf).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Form101Lf;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteForm101Lf(Form101Lf Form101Lf)
        {
            try
            {
                var dbForm101Lf = await _db.Form101Lfs.FindAsync(Form101Lf.Form101LfId);

                if (dbForm101Lf == null)
                {
                    return (false, "Form101Lf could not be found");
                }

                _db.Form101Lfs.Remove(Form101Lf);
                await _db.SaveChangesAsync();

                return (true, "Form101Lf got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Form101Lfs
    }
}
