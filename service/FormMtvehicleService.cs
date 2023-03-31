using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtvehicleService
    {
        // FormMtvehicles Services
        Task<List<FormMtvehicle>> GetFormMtvehicleListByValue(int offset, int limit, string val); // GET All FormMtvehicless
        Task<FormMtvehicle> GetFormMtvehicle(string FormMtvehicle_name); // GET Single FormMtvehicles        
        Task<List<FormMtvehicle>> GetFormMtvehicleList(string FormMtvehicle_name); // GET List FormMtvehicles        
        Task<FormMtvehicle> AddFormMtvehicle(FormMtvehicle FormMtvehicle); // POST New FormMtvehicles
        Task<FormMtvehicle> UpdateFormMtvehicle(FormMtvehicle FormMtvehicle); // PUT FormMtvehicles
        Task<(bool, string)> DeleteFormMtvehicle(FormMtvehicle FormMtvehicle); // DELETE FormMtvehicles
    }

    public class FormMtvehicleService : IFormMtvehicleService
    {
        private readonly XixsrvContext _db;

        public FormMtvehicleService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtvehicles

        public async Task<List<FormMtvehicle>> GetFormMtvehicleListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtvehicles.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtvehicle> GetFormMtvehicle(string FormMtvehicle_id)
        {
            try
            {
                return await _db.FormMtvehicles.FindAsync(FormMtvehicle_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtvehicle>> GetFormMtvehicleList(string FormMtvehicle_id)
        {
            try
            {
                return await _db.FormMtvehicles
                    .Where(i => i.FormMtvehicleId == FormMtvehicle_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtvehicle> AddFormMtvehicle(FormMtvehicle FormMtvehicle)
        {
            try
            {
                await _db.FormMtvehicles.AddAsync(FormMtvehicle);
                await _db.SaveChangesAsync();
                return await _db.FormMtvehicles.FindAsync(FormMtvehicle.FormMtvehicleId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtvehicle> UpdateFormMtvehicle(FormMtvehicle FormMtvehicle)
        {
            try
            {
                _db.Entry(FormMtvehicle).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtvehicle;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtvehicle(FormMtvehicle FormMtvehicle)
        {
            try
            {
                var dbFormMtvehicle = await _db.FormMtvehicles.FindAsync(FormMtvehicle.FormMtvehicleId);

                if (dbFormMtvehicle == null)
                {
                    return (false, "FormMtvehicle could not be found");
                }

                _db.FormMtvehicles.Remove(FormMtvehicle);
                await _db.SaveChangesAsync();

                return (true, "FormMtvehicle got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtvehicles
    }
}
