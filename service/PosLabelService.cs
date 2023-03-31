using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPosLabelService
    {
        // PosLabels Services
        Task<List<PosLabel>> GetPosLabelListByValue(int offset, int limit, string val); // GET All PosLabelss
        Task<PosLabel> GetPosLabel(string PosLabel_name); // GET Single PosLabels        
        Task<List<PosLabel>> GetPosLabelList(string PosLabel_name); // GET List PosLabels        
        Task<PosLabel> AddPosLabel(PosLabel PosLabel); // POST New PosLabels
        Task<PosLabel> UpdatePosLabel(PosLabel PosLabel); // PUT PosLabels
        Task<(bool, string)> DeletePosLabel(PosLabel PosLabel); // DELETE PosLabels
    }

    public class PosLabelService : IPosLabelService
    {
        private readonly XixsrvContext _db;

        public PosLabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region PosLabels

        public async Task<List<PosLabel>> GetPosLabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PosLabels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PosLabel> GetPosLabel(string PosLabel_id)
        {
            try
            {
                return await _db.PosLabels.FindAsync(PosLabel_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PosLabel>> GetPosLabelList(string PosLabel_id)
        {
            try
            {
                return await _db.PosLabels
                    .Where(i => i.PosLabelId == PosLabel_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PosLabel> AddPosLabel(PosLabel PosLabel)
        {
            try
            {
                await _db.PosLabels.AddAsync(PosLabel);
                await _db.SaveChangesAsync();
                return await _db.PosLabels.FindAsync(PosLabel.PosLabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PosLabel> UpdatePosLabel(PosLabel PosLabel)
        {
            try
            {
                _db.Entry(PosLabel).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PosLabel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePosLabel(PosLabel PosLabel)
        {
            try
            {
                var dbPosLabel = await _db.PosLabels.FindAsync(PosLabel.PosLabelId);

                if (dbPosLabel == null)
                {
                    return (false, "PosLabel could not be found");
                }

                _db.PosLabels.Remove(PosLabel);
                await _db.SaveChangesAsync();

                return (true, "PosLabel got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PosLabels
    }
}
