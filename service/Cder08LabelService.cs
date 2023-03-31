using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface ICder08LabelService
    {
        // Cder08Labels Services
        Task<List<Cder08Label>> GetCder08LabelListByValue(int offset, int limit, string val); // GET All Cder08Labelss
        Task<Cder08Label> GetCder08Label(string Cder08Label_name); // GET Single Cder08Labels        
        Task<List<Cder08Label>> GetCder08LabelList(string Cder08Label_name); // GET List Cder08Labels        
        Task<Cder08Label> AddCder08Label(Cder08Label Cder08Label); // POST New Cder08Labels
        Task<Cder08Label> UpdateCder08Label(Cder08Label Cder08Label); // PUT Cder08Labels
        Task<(bool, string)> DeleteCder08Label(Cder08Label Cder08Label); // DELETE Cder08Labels
    }

    public class Cder08LabelService : ICder08LabelService
    {
        private readonly XixsrvContext _db;

        public Cder08LabelService(XixsrvContext db)
        {
            _db = db;
        }

        #region Cder08Labels

        public async Task<List<Cder08Label>> GetCder08LabelListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Cder08Labels.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Cder08Label> GetCder08Label(string Cder08Label_id)
        {
            try
            {
                return await _db.Cder08Labels.FindAsync(Cder08Label_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Cder08Label>> GetCder08LabelList(string Cder08Label_id)
        {
            try
            {
                return await _db.Cder08Labels
                    .Where(i => i.Cder08LabelId == Cder08Label_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Cder08Label> AddCder08Label(Cder08Label Cder08Label)
        {
            try
            {
                await _db.Cder08Labels.AddAsync(Cder08Label);
                await _db.SaveChangesAsync();
                return await _db.Cder08Labels.FindAsync(Cder08Label.Cder08LabelId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Cder08Label> UpdateCder08Label(Cder08Label Cder08Label)
        {
            try
            {
                _db.Entry(Cder08Label).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Cder08Label;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteCder08Label(Cder08Label Cder08Label)
        {
            try
            {
                var dbCder08Label = await _db.Cder08Labels.FindAsync(Cder08Label.Cder08LabelId);

                if (dbCder08Label == null)
                {
                    return (false, "Cder08Label could not be found");
                }

                _db.Cder08Labels.Remove(Cder08Label);
                await _db.SaveChangesAsync();

                return (true, "Cder08Label got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Cder08Labels
    }
}
