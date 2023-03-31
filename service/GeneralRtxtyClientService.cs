using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IGeneralRtxtyClientService
    {
        // GeneralRtxtyClients Services
        Task<List<GeneralRtxtyClient>> GetGeneralRtxtyClientListByValue(int offset, int limit, string val); // GET All GeneralRtxtyClientss
        Task<GeneralRtxtyClient> GetGeneralRtxtyClient(string GeneralRtxtyClient_name); // GET Single GeneralRtxtyClients        
        Task<List<GeneralRtxtyClient>> GetGeneralRtxtyClientList(string GeneralRtxtyClient_name); // GET List GeneralRtxtyClients        
        Task<GeneralRtxtyClient> AddGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient); // POST New GeneralRtxtyClients
        Task<GeneralRtxtyClient> UpdateGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient); // PUT GeneralRtxtyClients
        Task<(bool, string)> DeleteGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient); // DELETE GeneralRtxtyClients
    }

    public class GeneralRtxtyClientService : IGeneralRtxtyClientService
    {
        private readonly XixsrvContext _db;

        public GeneralRtxtyClientService(XixsrvContext db)
        {
            _db = db;
        }

        #region GeneralRtxtyClients

        public async Task<List<GeneralRtxtyClient>> GetGeneralRtxtyClientListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.GeneralRtxtyClients.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<GeneralRtxtyClient> GetGeneralRtxtyClient(string GeneralRtxtyClient_id)
        {
            try
            {
                return await _db.GeneralRtxtyClients.FindAsync(GeneralRtxtyClient_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<GeneralRtxtyClient>> GetGeneralRtxtyClientList(string GeneralRtxtyClient_id)
        {
            try
            {
                return await _db.GeneralRtxtyClients
                    .Where(i => i.GeneralRtxtyClientId == GeneralRtxtyClient_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<GeneralRtxtyClient> AddGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient)
        {
            try
            {
                await _db.GeneralRtxtyClients.AddAsync(GeneralRtxtyClient);
                await _db.SaveChangesAsync();
                return await _db.GeneralRtxtyClients.FindAsync(GeneralRtxtyClient.GeneralRtxtyClientId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<GeneralRtxtyClient> UpdateGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient)
        {
            try
            {
                _db.Entry(GeneralRtxtyClient).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return GeneralRtxtyClient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteGeneralRtxtyClient(GeneralRtxtyClient GeneralRtxtyClient)
        {
            try
            {
                var dbGeneralRtxtyClient = await _db.GeneralRtxtyClients.FindAsync(GeneralRtxtyClient.GeneralRtxtyClientId);

                if (dbGeneralRtxtyClient == null)
                {
                    return (false, "GeneralRtxtyClient could not be found");
                }

                _db.GeneralRtxtyClients.Remove(GeneralRtxtyClient);
                await _db.SaveChangesAsync();

                return (true, "GeneralRtxtyClient got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion GeneralRtxtyClients
    }
}
