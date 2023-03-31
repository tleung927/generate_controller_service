using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IVoterRegistrationService
    {
        // VoterRegistrations Services
        Task<List<VoterRegistration>> GetVoterRegistrationListByValue(int offset, int limit, string val); // GET All VoterRegistrationss
        Task<VoterRegistration> GetVoterRegistration(string VoterRegistration_name); // GET Single VoterRegistrations        
        Task<List<VoterRegistration>> GetVoterRegistrationList(string VoterRegistration_name); // GET List VoterRegistrations        
        Task<VoterRegistration> AddVoterRegistration(VoterRegistration VoterRegistration); // POST New VoterRegistrations
        Task<VoterRegistration> UpdateVoterRegistration(VoterRegistration VoterRegistration); // PUT VoterRegistrations
        Task<(bool, string)> DeleteVoterRegistration(VoterRegistration VoterRegistration); // DELETE VoterRegistrations
    }

    public class VoterRegistrationService : IVoterRegistrationService
    {
        private readonly XixsrvContext _db;

        public VoterRegistrationService(XixsrvContext db)
        {
            _db = db;
        }

        #region VoterRegistrations

        public async Task<List<VoterRegistration>> GetVoterRegistrationListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.VoterRegistrations.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<VoterRegistration> GetVoterRegistration(string VoterRegistration_id)
        {
            try
            {
                return await _db.VoterRegistrations.FindAsync(VoterRegistration_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<VoterRegistration>> GetVoterRegistrationList(string VoterRegistration_id)
        {
            try
            {
                return await _db.VoterRegistrations
                    .Where(i => i.VoterRegistrationId == VoterRegistration_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<VoterRegistration> AddVoterRegistration(VoterRegistration VoterRegistration)
        {
            try
            {
                await _db.VoterRegistrations.AddAsync(VoterRegistration);
                await _db.SaveChangesAsync();
                return await _db.VoterRegistrations.FindAsync(VoterRegistration.VoterRegistrationId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<VoterRegistration> UpdateVoterRegistration(VoterRegistration VoterRegistration)
        {
            try
            {
                _db.Entry(VoterRegistration).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return VoterRegistration;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteVoterRegistration(VoterRegistration VoterRegistration)
        {
            try
            {
                var dbVoterRegistration = await _db.VoterRegistrations.FindAsync(VoterRegistration.VoterRegistrationId);

                if (dbVoterRegistration == null)
                {
                    return (false, "VoterRegistration could not be found");
                }

                _db.VoterRegistrations.Remove(VoterRegistration);
                await _db.SaveChangesAsync();

                return (true, "VoterRegistration got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion VoterRegistrations
    }
}
