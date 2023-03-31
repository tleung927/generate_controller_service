using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IForm101McService
    {
        // Form101Mcs Services
        Task<List<Form101Mc>> GetForm101McListByValue(int offset, int limit, string val); // GET All Form101Mcss
        Task<Form101Mc> GetForm101Mc(string Form101Mc_name); // GET Single Form101Mcs        
        Task<List<Form101Mc>> GetForm101McList(string Form101Mc_name); // GET List Form101Mcs        
        Task<Form101Mc> AddForm101Mc(Form101Mc Form101Mc); // POST New Form101Mcs
        Task<Form101Mc> UpdateForm101Mc(Form101Mc Form101Mc); // PUT Form101Mcs
        Task<(bool, string)> DeleteForm101Mc(Form101Mc Form101Mc); // DELETE Form101Mcs
    }

    public class Form101McService : IForm101McService
    {
        private readonly XixsrvContext _db;

        public Form101McService(XixsrvContext db)
        {
            _db = db;
        }

        #region Form101Mcs

        public async Task<List<Form101Mc>> GetForm101McListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Form101Mcs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Form101Mc> GetForm101Mc(string Form101Mc_id)
        {
            try
            {
                return await _db.Form101Mcs.FindAsync(Form101Mc_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Form101Mc>> GetForm101McList(string Form101Mc_id)
        {
            try
            {
                return await _db.Form101Mcs
                    .Where(i => i.Form101McId == Form101Mc_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Form101Mc> AddForm101Mc(Form101Mc Form101Mc)
        {
            try
            {
                await _db.Form101Mcs.AddAsync(Form101Mc);
                await _db.SaveChangesAsync();
                return await _db.Form101Mcs.FindAsync(Form101Mc.Form101McId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Form101Mc> UpdateForm101Mc(Form101Mc Form101Mc)
        {
            try
            {
                _db.Entry(Form101Mc).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Form101Mc;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteForm101Mc(Form101Mc Form101Mc)
        {
            try
            {
                var dbForm101Mc = await _db.Form101Mcs.FindAsync(Form101Mc.Form101McId);

                if (dbForm101Mc == null)
                {
                    return (false, "Form101Mc could not be found");
                }

                _db.Form101Mcs.Remove(Form101Mc);
                await _db.SaveChangesAsync();

                return (true, "Form101Mc got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Form101Mcs
    }
}
