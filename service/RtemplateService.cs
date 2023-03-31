using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IRtemplateService
    {
        // Rtemplates Services
        Task<List<Rtemplate>> GetRtemplateListByValue(int offset, int limit, string val); // GET All Rtemplatess
        Task<Rtemplate> GetRtemplate(string Rtemplate_name); // GET Single Rtemplates        
        Task<List<Rtemplate>> GetRtemplateList(string Rtemplate_name); // GET List Rtemplates        
        Task<Rtemplate> AddRtemplate(Rtemplate Rtemplate); // POST New Rtemplates
        Task<Rtemplate> UpdateRtemplate(Rtemplate Rtemplate); // PUT Rtemplates
        Task<(bool, string)> DeleteRtemplate(Rtemplate Rtemplate); // DELETE Rtemplates
    }

    public class RtemplateService : IRtemplateService
    {
        private readonly XixsrvContext _db;

        public RtemplateService(XixsrvContext db)
        {
            _db = db;
        }

        #region Rtemplates

        public async Task<List<Rtemplate>> GetRtemplateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Rtemplates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Rtemplate> GetRtemplate(string Rtemplate_id)
        {
            try
            {
                return await _db.Rtemplates.FindAsync(Rtemplate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Rtemplate>> GetRtemplateList(string Rtemplate_id)
        {
            try
            {
                return await _db.Rtemplates
                    .Where(i => i.RtemplateId == Rtemplate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Rtemplate> AddRtemplate(Rtemplate Rtemplate)
        {
            try
            {
                await _db.Rtemplates.AddAsync(Rtemplate);
                await _db.SaveChangesAsync();
                return await _db.Rtemplates.FindAsync(Rtemplate.RtemplateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Rtemplate> UpdateRtemplate(Rtemplate Rtemplate)
        {
            try
            {
                _db.Entry(Rtemplate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Rtemplate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteRtemplate(Rtemplate Rtemplate)
        {
            try
            {
                var dbRtemplate = await _db.Rtemplates.FindAsync(Rtemplate.RtemplateId);

                if (dbRtemplate == null)
                {
                    return (false, "Rtemplate could not be found");
                }

                _db.Rtemplates.Remove(Rtemplate);
                await _db.SaveChangesAsync();

                return (true, "Rtemplate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Rtemplates
    }
}
