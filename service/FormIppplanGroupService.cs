using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormIppplanGroupService
    {
        // FormIppplanGroups Services
        Task<List<FormIppplanGroup>> GetFormIppplanGroupListByValue(int offset, int limit, string val); // GET All FormIppplanGroupss
        Task<FormIppplanGroup> GetFormIppplanGroup(string FormIppplanGroup_name); // GET Single FormIppplanGroups        
        Task<List<FormIppplanGroup>> GetFormIppplanGroupList(string FormIppplanGroup_name); // GET List FormIppplanGroups        
        Task<FormIppplanGroup> AddFormIppplanGroup(FormIppplanGroup FormIppplanGroup); // POST New FormIppplanGroups
        Task<FormIppplanGroup> UpdateFormIppplanGroup(FormIppplanGroup FormIppplanGroup); // PUT FormIppplanGroups
        Task<(bool, string)> DeleteFormIppplanGroup(FormIppplanGroup FormIppplanGroup); // DELETE FormIppplanGroups
    }

    public class FormIppplanGroupService : IFormIppplanGroupService
    {
        private readonly XixsrvContext _db;

        public FormIppplanGroupService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormIppplanGroups

        public async Task<List<FormIppplanGroup>> GetFormIppplanGroupListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormIppplanGroups.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormIppplanGroup> GetFormIppplanGroup(string FormIppplanGroup_id)
        {
            try
            {
                return await _db.FormIppplanGroups.FindAsync(FormIppplanGroup_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormIppplanGroup>> GetFormIppplanGroupList(string FormIppplanGroup_id)
        {
            try
            {
                return await _db.FormIppplanGroups
                    .Where(i => i.FormIppplanGroupId == FormIppplanGroup_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormIppplanGroup> AddFormIppplanGroup(FormIppplanGroup FormIppplanGroup)
        {
            try
            {
                await _db.FormIppplanGroups.AddAsync(FormIppplanGroup);
                await _db.SaveChangesAsync();
                return await _db.FormIppplanGroups.FindAsync(FormIppplanGroup.FormIppplanGroupId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormIppplanGroup> UpdateFormIppplanGroup(FormIppplanGroup FormIppplanGroup)
        {
            try
            {
                _db.Entry(FormIppplanGroup).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormIppplanGroup;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormIppplanGroup(FormIppplanGroup FormIppplanGroup)
        {
            try
            {
                var dbFormIppplanGroup = await _db.FormIppplanGroups.FindAsync(FormIppplanGroup.FormIppplanGroupId);

                if (dbFormIppplanGroup == null)
                {
                    return (false, "FormIppplanGroup could not be found");
                }

                _db.FormIppplanGroups.Remove(FormIppplanGroup);
                await _db.SaveChangesAsync();

                return (true, "FormIppplanGroup got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormIppplanGroups
    }
}
