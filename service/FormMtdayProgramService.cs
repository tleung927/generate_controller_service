using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormMtdayProgramService
    {
        // FormMtdayPrograms Services
        Task<List<FormMtdayProgram>> GetFormMtdayProgramListByValue(int offset, int limit, string val); // GET All FormMtdayProgramss
        Task<FormMtdayProgram> GetFormMtdayProgram(string FormMtdayProgram_name); // GET Single FormMtdayPrograms        
        Task<List<FormMtdayProgram>> GetFormMtdayProgramList(string FormMtdayProgram_name); // GET List FormMtdayPrograms        
        Task<FormMtdayProgram> AddFormMtdayProgram(FormMtdayProgram FormMtdayProgram); // POST New FormMtdayPrograms
        Task<FormMtdayProgram> UpdateFormMtdayProgram(FormMtdayProgram FormMtdayProgram); // PUT FormMtdayPrograms
        Task<(bool, string)> DeleteFormMtdayProgram(FormMtdayProgram FormMtdayProgram); // DELETE FormMtdayPrograms
    }

    public class FormMtdayProgramService : IFormMtdayProgramService
    {
        private readonly XixsrvContext _db;

        public FormMtdayProgramService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormMtdayPrograms

        public async Task<List<FormMtdayProgram>> GetFormMtdayProgramListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormMtdayPrograms.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormMtdayProgram> GetFormMtdayProgram(string FormMtdayProgram_id)
        {
            try
            {
                return await _db.FormMtdayPrograms.FindAsync(FormMtdayProgram_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormMtdayProgram>> GetFormMtdayProgramList(string FormMtdayProgram_id)
        {
            try
            {
                return await _db.FormMtdayPrograms
                    .Where(i => i.FormMtdayProgramId == FormMtdayProgram_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormMtdayProgram> AddFormMtdayProgram(FormMtdayProgram FormMtdayProgram)
        {
            try
            {
                await _db.FormMtdayPrograms.AddAsync(FormMtdayProgram);
                await _db.SaveChangesAsync();
                return await _db.FormMtdayPrograms.FindAsync(FormMtdayProgram.FormMtdayProgramId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormMtdayProgram> UpdateFormMtdayProgram(FormMtdayProgram FormMtdayProgram)
        {
            try
            {
                _db.Entry(FormMtdayProgram).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormMtdayProgram;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormMtdayProgram(FormMtdayProgram FormMtdayProgram)
        {
            try
            {
                var dbFormMtdayProgram = await _db.FormMtdayPrograms.FindAsync(FormMtdayProgram.FormMtdayProgramId);

                if (dbFormMtdayProgram == null)
                {
                    return (false, "FormMtdayProgram could not be found");
                }

                _db.FormMtdayPrograms.Remove(FormMtdayProgram);
                await _db.SaveChangesAsync();

                return (true, "FormMtdayProgram got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormMtdayPrograms
    }
}
