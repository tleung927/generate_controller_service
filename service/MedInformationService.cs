using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMedInformationService
    {
        // MedInformations Services
        Task<List<MedInformation>> GetMedInformationListByValue(int offset, int limit, string val); // GET All MedInformationss
        Task<MedInformation> GetMedInformation(string MedInformation_name); // GET Single MedInformations        
        Task<List<MedInformation>> GetMedInformationList(string MedInformation_name); // GET List MedInformations        
        Task<MedInformation> AddMedInformation(MedInformation MedInformation); // POST New MedInformations
        Task<MedInformation> UpdateMedInformation(MedInformation MedInformation); // PUT MedInformations
        Task<(bool, string)> DeleteMedInformation(MedInformation MedInformation); // DELETE MedInformations
    }

    public class MedInformationService : IMedInformationService
    {
        private readonly XixsrvContext _db;

        public MedInformationService(XixsrvContext db)
        {
            _db = db;
        }

        #region MedInformations

        public async Task<List<MedInformation>> GetMedInformationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.MedInformations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MedInformation> GetMedInformation(string MedInformation_id)
        {
            try
            {
                return await _db.MedInformations.FindAsync(MedInformation_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<MedInformation>> GetMedInformationList(string MedInformation_id)
        {
            try
            {
                return await _db.MedInformations
                    .Where(i => i.MedInformationId == MedInformation_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<MedInformation> AddMedInformation(MedInformation MedInformation)
        {
            try
            {
                await _db.MedInformations.AddAsync(MedInformation);
                await _db.SaveChangesAsync();
                return await _db.MedInformations.FindAsync(MedInformation.MedInformationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<MedInformation> UpdateMedInformation(MedInformation MedInformation)
        {
            try
            {
                _db.Entry(MedInformation).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return MedInformation;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMedInformation(MedInformation MedInformation)
        {
            try
            {
                var dbMedInformation = await _db.MedInformations.FindAsync(MedInformation.MedInformationId);

                if (dbMedInformation == null)
                {
                    return (false, "MedInformation could not be found");
                }

                _db.MedInformations.Remove(MedInformation);
                await _db.SaveChangesAsync();

                return (true, "MedInformation got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion MedInformations
    }
}
