using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IFormPermissionAssignService
    {
        // FormPermissionAssigns Services
        Task<List<FormPermissionAssign>> GetFormPermissionAssignListByValue(int offset, int limit, string val); // GET All FormPermissionAssignss
        Task<FormPermissionAssign> GetFormPermissionAssign(string FormPermissionAssign_name); // GET Single FormPermissionAssigns        
        Task<List<FormPermissionAssign>> GetFormPermissionAssignList(string FormPermissionAssign_name); // GET List FormPermissionAssigns        
        Task<FormPermissionAssign> AddFormPermissionAssign(FormPermissionAssign FormPermissionAssign); // POST New FormPermissionAssigns
        Task<FormPermissionAssign> UpdateFormPermissionAssign(FormPermissionAssign FormPermissionAssign); // PUT FormPermissionAssigns
        Task<(bool, string)> DeleteFormPermissionAssign(FormPermissionAssign FormPermissionAssign); // DELETE FormPermissionAssigns
    }

    public class FormPermissionAssignService : IFormPermissionAssignService
    {
        private readonly XixsrvContext _db;

        public FormPermissionAssignService(XixsrvContext db)
        {
            _db = db;
        }

        #region FormPermissionAssigns

        public async Task<List<FormPermissionAssign>> GetFormPermissionAssignListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.FormPermissionAssigns.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<FormPermissionAssign> GetFormPermissionAssign(string FormPermissionAssign_id)
        {
            try
            {
                return await _db.FormPermissionAssigns.FindAsync(FormPermissionAssign_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<FormPermissionAssign>> GetFormPermissionAssignList(string FormPermissionAssign_id)
        {
            try
            {
                return await _db.FormPermissionAssigns
                    .Where(i => i.FormPermissionAssignId == FormPermissionAssign_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<FormPermissionAssign> AddFormPermissionAssign(FormPermissionAssign FormPermissionAssign)
        {
            try
            {
                await _db.FormPermissionAssigns.AddAsync(FormPermissionAssign);
                await _db.SaveChangesAsync();
                return await _db.FormPermissionAssigns.FindAsync(FormPermissionAssign.FormPermissionAssignId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<FormPermissionAssign> UpdateFormPermissionAssign(FormPermissionAssign FormPermissionAssign)
        {
            try
            {
                _db.Entry(FormPermissionAssign).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return FormPermissionAssign;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteFormPermissionAssign(FormPermissionAssign FormPermissionAssign)
        {
            try
            {
                var dbFormPermissionAssign = await _db.FormPermissionAssigns.FindAsync(FormPermissionAssign.FormPermissionAssignId);

                if (dbFormPermissionAssign == null)
                {
                    return (false, "FormPermissionAssign could not be found");
                }

                _db.FormPermissionAssigns.Remove(FormPermissionAssign);
                await _db.SaveChangesAsync();

                return (true, "FormPermissionAssign got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion FormPermissionAssigns
    }
}
