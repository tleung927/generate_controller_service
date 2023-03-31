using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IAs400OutputService
    {
        // As400Outputs Services
        Task<List<As400Output>> GetAs400OutputListByValue(int offset, int limit, string val); // GET All As400Outputss
        Task<As400Output> GetAs400Output(string As400Output_name); // GET Single As400Outputs        
        Task<List<As400Output>> GetAs400OutputList(string As400Output_name); // GET List As400Outputs        
        Task<As400Output> AddAs400Output(As400Output As400Output); // POST New As400Outputs
        Task<As400Output> UpdateAs400Output(As400Output As400Output); // PUT As400Outputs
        Task<(bool, string)> DeleteAs400Output(As400Output As400Output); // DELETE As400Outputs
    }

    public class As400OutputService : IAs400OutputService
    {
        private readonly XixsrvContext _db;

        public As400OutputService(XixsrvContext db)
        {
            _db = db;
        }

        #region As400Outputs

        public async Task<List<As400Output>> GetAs400OutputListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.As400Outputs.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<As400Output> GetAs400Output(string As400Output_id)
        {
            try
            {
                return await _db.As400Outputs.FindAsync(As400Output_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<As400Output>> GetAs400OutputList(string As400Output_id)
        {
            try
            {
                return await _db.As400Outputs
                    .Where(i => i.As400OutputId == As400Output_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<As400Output> AddAs400Output(As400Output As400Output)
        {
            try
            {
                await _db.As400Outputs.AddAsync(As400Output);
                await _db.SaveChangesAsync();
                return await _db.As400Outputs.FindAsync(As400Output.As400OutputId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<As400Output> UpdateAs400Output(As400Output As400Output)
        {
            try
            {
                _db.Entry(As400Output).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return As400Output;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAs400Output(As400Output As400Output)
        {
            try
            {
                var dbAs400Output = await _db.As400Outputs.FindAsync(As400Output.As400OutputId);

                if (dbAs400Output == null)
                {
                    return (false, "As400Output could not be found");
                }

                _db.As400Outputs.Remove(As400Output);
                await _db.SaveChangesAsync();

                return (true, "As400Output got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion As400Outputs
    }
}
