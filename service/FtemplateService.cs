using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFtemplateService
    {
        // Ftemplates Services
        Task<List<Ftemplate>> GetFtemplateListByValue(int offset, int limit, string val); // GET All Ftemplatess
        Task<Ftemplate> GetFtemplate(string Ftemplate_name); // GET Single Ftemplates        
        Task<List<Ftemplate>> GetFtemplateList(string Ftemplate_name); // GET List Ftemplates        
        Task<Ftemplate> AddFtemplate(Ftemplate Ftemplate); // POST New Ftemplates
        Task<Ftemplate> UpdateFtemplate(Ftemplate Ftemplate); // PUT Ftemplates
        Task<(bool, string)> DeleteFtemplate(Ftemplate Ftemplate); // DELETE Ftemplates
    }

    public class FtemplateService : IFtemplateService
    {
        private readonly XixsrvContext _db;

        public FtemplateService(XixsrvContext db)
        {
            _db = db;
        }

        #region Ftemplates

        public async Task<List<Ftemplate>> GetFtemplateListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Ftemplates.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Ftemplate> GetFtemplate(string Ftemplate_id)
        {
            try
            {
                return await _db.Ftemplates.FindAsync(Ftemplate_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Ftemplate>> GetFtemplateList(string Ftemplate_id)
        {
            try
            {
                return await _db.Ftemplates
                    .Where(i => i.FtemplateId == Ftemplate_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Ftemplate> AddFtemplate(Ftemplate Ftemplate)
        {
            try
            {
                await _db.Ftemplates.AddAsync(Ftemplate);
                await _db.SaveChangesAsync();
                return await _db.Ftemplates.FindAsync(Ftemplate.FtemplateId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Ftemplate> UpdateFtemplate(Ftemplate Ftemplate)
        {
            try
            {
                _db.Entry(Ftemplate).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Ftemplate;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFtemplate(Ftemplate Ftemplate)
        {
            try
            {
                var dbFtemplate = await _db.Ftemplates.FindAsync(Ftemplate.FtemplateId);

                if (dbFtemplate == null)
                {
                    return (false, "Ftemplate could not be found");
                }

                _db.Ftemplates.Remove(Ftemplate);
                await _db.SaveChangesAsync();

                return (true, "Ftemplate got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Ftemplates
    }
}
