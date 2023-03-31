using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGeneralInfoClientService
    {
        // GeneralInfoClients Services
        Task<List<GeneralInfoClient>> GetGeneralInfoClientListByValue(int offset, int limit, string val); // GET All GeneralInfoClientss
        Task<GeneralInfoClient> GetGeneralInfoClient(string GeneralInfoClient_name); // GET Single GeneralInfoClients        
        Task<List<GeneralInfoClient>> GetGeneralInfoClientList(string GeneralInfoClient_name); // GET List GeneralInfoClients        
        Task<GeneralInfoClient> AddGeneralInfoClient(GeneralInfoClient GeneralInfoClient); // POST New GeneralInfoClients
        Task<GeneralInfoClient> UpdateGeneralInfoClient(GeneralInfoClient GeneralInfoClient); // PUT GeneralInfoClients
        Task<(bool, string)> DeleteGeneralInfoClient(GeneralInfoClient GeneralInfoClient); // DELETE GeneralInfoClients
    }

    public class GeneralInfoClientService : IGeneralInfoClientService
    {
        private readonly XixsrvContext _db;

        public GeneralInfoClientService(XixsrvContext db)
        {
            _db = db;
        }

        #region GeneralInfoClients

        public async Task<List<GeneralInfoClient>> GetGeneralInfoClientListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.GeneralInfoClients.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GeneralInfoClient> GetGeneralInfoClient(string GeneralInfoClient_id)
        {
            try
            {
                return await _db.GeneralInfoClients.FindAsync(GeneralInfoClient_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GeneralInfoClient>> GetGeneralInfoClientList(string GeneralInfoClient_id)
        {
            try
            {
                return await _db.GeneralInfoClients
                    .Where(i => i.GeneralInfoClientId == GeneralInfoClient_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<GeneralInfoClient> AddGeneralInfoClient(GeneralInfoClient GeneralInfoClient)
        {
            try
            {
                await _db.GeneralInfoClients.AddAsync(GeneralInfoClient);
                await _db.SaveChangesAsync();
                return await _db.GeneralInfoClients.FindAsync(GeneralInfoClient.GeneralInfoClientId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<GeneralInfoClient> UpdateGeneralInfoClient(GeneralInfoClient GeneralInfoClient)
        {
            try
            {
                _db.Entry(GeneralInfoClient).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return GeneralInfoClient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGeneralInfoClient(GeneralInfoClient GeneralInfoClient)
        {
            try
            {
                var dbGeneralInfoClient = await _db.GeneralInfoClients.FindAsync(GeneralInfoClient.GeneralInfoClientId);

                if (dbGeneralInfoClient == null)
                {
                    return (false, "GeneralInfoClient could not be found");
                }

                _db.GeneralInfoClients.Remove(GeneralInfoClient);
                await _db.SaveChangesAsync();

                return (true, "GeneralInfoClient got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion GeneralInfoClients
    }
}
