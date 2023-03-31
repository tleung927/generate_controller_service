using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGeneralTxtyClientService
    {
        // GeneralTxtyClients Services
        Task<List<GeneralTxtyClient>> GetGeneralTxtyClientListByValue(int offset, int limit, string val); // GET All GeneralTxtyClientss
        Task<GeneralTxtyClient> GetGeneralTxtyClient(string GeneralTxtyClient_name); // GET Single GeneralTxtyClients        
        Task<List<GeneralTxtyClient>> GetGeneralTxtyClientList(string GeneralTxtyClient_name); // GET List GeneralTxtyClients        
        Task<GeneralTxtyClient> AddGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient); // POST New GeneralTxtyClients
        Task<GeneralTxtyClient> UpdateGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient); // PUT GeneralTxtyClients
        Task<(bool, string)> DeleteGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient); // DELETE GeneralTxtyClients
    }

    public class GeneralTxtyClientService : IGeneralTxtyClientService
    {
        private readonly XixsrvContext _db;

        public GeneralTxtyClientService(XixsrvContext db)
        {
            _db = db;
        }

        #region GeneralTxtyClients

        public async Task<List<GeneralTxtyClient>> GetGeneralTxtyClientListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.GeneralTxtyClients.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GeneralTxtyClient> GetGeneralTxtyClient(string GeneralTxtyClient_id)
        {
            try
            {
                return await _db.GeneralTxtyClients.FindAsync(GeneralTxtyClient_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GeneralTxtyClient>> GetGeneralTxtyClientList(string GeneralTxtyClient_id)
        {
            try
            {
                return await _db.GeneralTxtyClients
                    .Where(i => i.GeneralTxtyClientId == GeneralTxtyClient_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<GeneralTxtyClient> AddGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient)
        {
            try
            {
                await _db.GeneralTxtyClients.AddAsync(GeneralTxtyClient);
                await _db.SaveChangesAsync();
                return await _db.GeneralTxtyClients.FindAsync(GeneralTxtyClient.GeneralTxtyClientId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<GeneralTxtyClient> UpdateGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient)
        {
            try
            {
                _db.Entry(GeneralTxtyClient).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return GeneralTxtyClient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGeneralTxtyClient(GeneralTxtyClient GeneralTxtyClient)
        {
            try
            {
                var dbGeneralTxtyClient = await _db.GeneralTxtyClients.FindAsync(GeneralTxtyClient.GeneralTxtyClientId);

                if (dbGeneralTxtyClient == null)
                {
                    return (false, "GeneralTxtyClient could not be found");
                }

                _db.GeneralTxtyClients.Remove(GeneralTxtyClient);
                await _db.SaveChangesAsync();

                return (true, "GeneralTxtyClient got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion GeneralTxtyClients
    }
}
