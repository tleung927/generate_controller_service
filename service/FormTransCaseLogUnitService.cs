using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormTransCaseLogUnitService
    {
        // FormTransCaseLogUnits Services
        Task<List<FormTransCaseLogUnit>> GetFormTransCaseLogUnitListByValue(int offset, int limit, string val); // GET All FormTransCaseLogUnitss
        Task<FormTransCaseLogUnit> GetFormTransCaseLogUnit(string FormTransCaseLogUnit_name); // GET Single FormTransCaseLogUnits        
        Task<List<FormTransCaseLogUnit>> GetFormTransCaseLogUnitList(string FormTransCaseLogUnit_name); // GET List FormTransCaseLogUnits        
        Task<FormTransCaseLogUnit> AddFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit); // POST New FormTransCaseLogUnits
        Task<FormTransCaseLogUnit> UpdateFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit); // PUT FormTransCaseLogUnits
        Task<(bool, string)> DeleteFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit); // DELETE FormTransCaseLogUnits
    }

    public class FormTransCaseLogUnitService : IFormTransCaseLogUnitService
    {
        private readonly XixsrvContext _db;

        public FormTransCaseLogUnitService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormTransCaseLogUnits

        public async Task<List<FormTransCaseLogUnit>> GetFormTransCaseLogUnitListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormTransCaseLogUnits.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormTransCaseLogUnit> GetFormTransCaseLogUnit(string FormTransCaseLogUnit_id)
        {
            try
            {
                return await _db.FormTransCaseLogUnits.FindAsync(FormTransCaseLogUnit_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormTransCaseLogUnit>> GetFormTransCaseLogUnitList(string FormTransCaseLogUnit_id)
        {
            try
            {
                return await _db.FormTransCaseLogUnits
                    .Where(i => i.FormTransCaseLogUnitId == FormTransCaseLogUnit_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormTransCaseLogUnit> AddFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit)
        {
            try
            {
                await _db.FormTransCaseLogUnits.AddAsync(FormTransCaseLogUnit);
                await _db.SaveChangesAsync();
                return await _db.FormTransCaseLogUnits.FindAsync(FormTransCaseLogUnit.FormTransCaseLogUnitId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormTransCaseLogUnit> UpdateFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit)
        {
            try
            {
                _db.Entry(FormTransCaseLogUnit).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormTransCaseLogUnit;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit)
        {
            try
            {
                var dbFormTransCaseLogUnit = await _db.FormTransCaseLogUnits.FindAsync(FormTransCaseLogUnit.FormTransCaseLogUnitId);

                if (dbFormTransCaseLogUnit == null)
                {
                    return (false, "FormTransCaseLogUnit could not be found");
                }

                _db.FormTransCaseLogUnits.Remove(FormTransCaseLogUnit);
                await _db.SaveChangesAsync();

                return (true, "FormTransCaseLogUnit got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormTransCaseLogUnits
    }
}
