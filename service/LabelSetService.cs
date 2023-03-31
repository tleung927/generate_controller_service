using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ILabelSetService
    {
        // LabelSets Services
        Task<List<LabelSet>> GetLabelSetListByValue(int offset, int limit, string val); // GET All LabelSetss
        Task<LabelSet> GetLabelSet(string LabelSet_name); // GET Single LabelSets        
        Task<List<LabelSet>> GetLabelSetList(string LabelSet_name); // GET List LabelSets        
        Task<LabelSet> AddLabelSet(LabelSet LabelSet); // POST New LabelSets
        Task<LabelSet> UpdateLabelSet(LabelSet LabelSet); // PUT LabelSets
        Task<(bool, string)> DeleteLabelSet(LabelSet LabelSet); // DELETE LabelSets
    }

    public class LabelSetService : ILabelSetService
    {
        private readonly XixsrvContext _db;

        public LabelSetService(XixsrvContext db)
        {
            _db = db;
        }

        #region LabelSets

        public async Task<List<LabelSet>> GetLabelSetListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.LabelSets.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<LabelSet> GetLabelSet(string LabelSet_id)
        {
            try
            {
                return await _db.LabelSets.FindAsync(LabelSet_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<LabelSet>> GetLabelSetList(string LabelSet_id)
        {
            try
            {
                return await _db.LabelSets
                    .Where(i => i.LabelSetId == LabelSet_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<LabelSet> AddLabelSet(LabelSet LabelSet)
        {
            try
            {
                await _db.LabelSets.AddAsync(LabelSet);
                await _db.SaveChangesAsync();
                return await _db.LabelSets.FindAsync(LabelSet.LabelSetId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<LabelSet> UpdateLabelSet(LabelSet LabelSet)
        {
            try
            {
                _db.Entry(LabelSet).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return LabelSet;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteLabelSet(LabelSet LabelSet)
        {
            try
            {
                var dbLabelSet = await _db.LabelSets.FindAsync(LabelSet.LabelSetId);

                if (dbLabelSet == null)
                {
                    return (false, "LabelSet could not be found");
                }

                _db.LabelSets.Remove(LabelSet);
                await _db.SaveChangesAsync();

                return (true, "LabelSet got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion LabelSets
    }
}
