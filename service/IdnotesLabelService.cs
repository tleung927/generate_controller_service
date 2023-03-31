using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IIdnotesLabelService
    {
        // IdnotesLabels Services
        Task<List<IdnotesLabel>> GetIdnotesLabelListByValue(int offset, int limit, string val); // GET All IdnotesLabelss
        Task<IdnotesLabel> GetIdnotesLabel(string IdnotesLabel_name); // GET Single IdnotesLabels        
        Task<List<IdnotesLabel>> GetIdnotesLabelList(string IdnotesLabel_name); // GET List IdnotesLabels        
        Task<IdnotesLabel> AddIdnotesLabel(IdnotesLabel IdnotesLabel); // POST New IdnotesLabels
        Task<IdnotesLabel> UpdateIdnotesLabel(IdnotesLabel IdnotesLabel); // PUT IdnotesLabels
        Task<(bool, string)> DeleteIdnotesLabel(IdnotesLabel IdnotesLabel); // DELETE IdnotesLabels
    }

    public class IdnotesLabelService : IIdnotesLabelService
    {
        private readonly XixsrvContext _db;

        public IdnotesLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region IdnotesLabels

        public async Task<List<IdnotesLabel>> GetIdnotesLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.IdnotesLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IdnotesLabel> GetIdnotesLabel(string IdnotesLabel_id)
        {
            try
            {
                return await _db.IdnotesLabels.FindAsync(IdnotesLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<IdnotesLabel>> GetIdnotesLabelList(string IdnotesLabel_id)
        {
            try
            {
                return await _db.IdnotesLabels
                    .Where(i => i.IdnotesLabelId == IdnotesLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<IdnotesLabel> AddIdnotesLabel(IdnotesLabel IdnotesLabel)
        {
            try
            {
                await _db.IdnotesLabels.AddAsync(IdnotesLabel);
                await _db.SaveChangesAsync();
                return await _db.IdnotesLabels.FindAsync(IdnotesLabel.IdnotesLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<IdnotesLabel> UpdateIdnotesLabel(IdnotesLabel IdnotesLabel)
        {
            try
            {
                _db.Entry(IdnotesLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return IdnotesLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteIdnotesLabel(IdnotesLabel IdnotesLabel)
        {
            try
            {
                var dbIdnotesLabel = await _db.IdnotesLabels.FindAsync(IdnotesLabel.IdnotesLabelId);

                if (dbIdnotesLabel == null)
                {
                    return (false, "IdnotesLabel could not be found");
                }

                _db.IdnotesLabels.Remove(IdnotesLabel);
                await _db.SaveChangesAsync();

                return (true, "IdnotesLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion IdnotesLabels
    }
}
