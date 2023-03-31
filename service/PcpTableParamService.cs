using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IPcpTableParamService
    {
        // PcpTableParams Services
        Task<List<PcpTableParam>> GetPcpTableParamListByValue(int offset, int limit, string val); // GET All PcpTableParamss
        Task<PcpTableParam> GetPcpTableParam(string PcpTableParam_name); // GET Single PcpTableParams        
        Task<List<PcpTableParam>> GetPcpTableParamList(string PcpTableParam_name); // GET List PcpTableParams        
        Task<PcpTableParam> AddPcpTableParam(PcpTableParam PcpTableParam); // POST New PcpTableParams
        Task<PcpTableParam> UpdatePcpTableParam(PcpTableParam PcpTableParam); // PUT PcpTableParams
        Task<(bool, string)> DeletePcpTableParam(PcpTableParam PcpTableParam); // DELETE PcpTableParams
    }

    public class PcpTableParamService : IPcpTableParamService
    {
        private readonly XixsrvContext _db;

        public PcpTableParamService(XixsrvContext db)
        {
            _db = db;
        }

        #region PcpTableParams

        public async Task<List<PcpTableParam>> GetPcpTableParamListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.PcpTableParams.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<PcpTableParam> GetPcpTableParam(string PcpTableParam_id)
        {
            try
            {
                return await _db.PcpTableParams.FindAsync(PcpTableParam_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PcpTableParam>> GetPcpTableParamList(string PcpTableParam_id)
        {
            try
            {
                return await _db.PcpTableParams
                    .Where(i => i.PcpTableParamId == PcpTableParam_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<PcpTableParam> AddPcpTableParam(PcpTableParam PcpTableParam)
        {
            try
            {
                await _db.PcpTableParams.AddAsync(PcpTableParam);
                await _db.SaveChangesAsync();
                return await _db.PcpTableParams.FindAsync(PcpTableParam.PcpTableParamId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<PcpTableParam> UpdatePcpTableParam(PcpTableParam PcpTableParam)
        {
            try
            {
                _db.Entry(PcpTableParam).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return PcpTableParam;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeletePcpTableParam(PcpTableParam PcpTableParam)
        {
            try
            {
                var dbPcpTableParam = await _db.PcpTableParams.FindAsync(PcpTableParam.PcpTableParamId);

                if (dbPcpTableParam == null)
                {
                    return (false, "PcpTableParam could not be found");
                }

                _db.PcpTableParams.Remove(PcpTableParam);
                await _db.SaveChangesAsync();

                return (true, "PcpTableParam got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion PcpTableParams
    }
}
