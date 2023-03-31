using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormTransitionService
    {
        // FormTransitions Services
        Task<List<FormTransition>> GetFormTransitionListByValue(int offset, int limit, string val); // GET All FormTransitionss
        Task<FormTransition> GetFormTransition(string FormTransition_name); // GET Single FormTransitions        
        Task<List<FormTransition>> GetFormTransitionList(string FormTransition_name); // GET List FormTransitions        
        Task<FormTransition> AddFormTransition(FormTransition FormTransition); // POST New FormTransitions
        Task<FormTransition> UpdateFormTransition(FormTransition FormTransition); // PUT FormTransitions
        Task<(bool, string)> DeleteFormTransition(FormTransition FormTransition); // DELETE FormTransitions
    }

    public class FormTransitionService : IFormTransitionService
    {
        private readonly XixsrvContext _db;

        public FormTransitionService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormTransitions

        public async Task<List<FormTransition>> GetFormTransitionListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormTransitions.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormTransition> GetFormTransition(string FormTransition_id)
        {
            try
            {
                return await _db.FormTransitions.FindAsync(FormTransition_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormTransition>> GetFormTransitionList(string FormTransition_id)
        {
            try
            {
                return await _db.FormTransitions
                    .Where(i => i.FormTransitionId == FormTransition_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormTransition> AddFormTransition(FormTransition FormTransition)
        {
            try
            {
                await _db.FormTransitions.AddAsync(FormTransition);
                await _db.SaveChangesAsync();
                return await _db.FormTransitions.FindAsync(FormTransition.FormTransitionId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormTransition> UpdateFormTransition(FormTransition FormTransition)
        {
            try
            {
                _db.Entry(FormTransition).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormTransition;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormTransition(FormTransition FormTransition)
        {
            try
            {
                var dbFormTransition = await _db.FormTransitions.FindAsync(FormTransition.FormTransitionId);

                if (dbFormTransition == null)
                {
                    return (false, "FormTransition could not be found");
                }

                _db.FormTransitions.Remove(FormTransition);
                await _db.SaveChangesAsync();

                return (true, "FormTransition got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormTransitions
    }
}
