using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IEmploymentService
    {
        // Employments Services
        Task<List<Employment>> GetEmploymentListByValue(int offset, int limit, string val); // GET All Employmentss
        Task<Employment> GetEmployment(string Employment_name); // GET Single Employments        
        Task<List<Employment>> GetEmploymentList(string Employment_name); // GET List Employments        
        Task<Employment> AddEmployment(Employment Employment); // POST New Employments
        Task<Employment> UpdateEmployment(Employment Employment); // PUT Employments
        Task<(bool, string)> DeleteEmployment(Employment Employment); // DELETE Employments
    }

    public class EmploymentService : IEmploymentService
    {
        private readonly XixsrvContext _db;

        public EmploymentService(XixsrvContext db)
        {
            _db = db;
        }

        #region Employments

        public async Task<List<Employment>> GetEmploymentListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Employments.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Employment> GetEmployment(string Employment_id)
        {
            try
            {
                return await _db.Employments.FindAsync(Employment_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Employment>> GetEmploymentList(string Employment_id)
        {
            try
            {
                return await _db.Employments
                    .Where(i => i.EmploymentId == Employment_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Employment> AddEmployment(Employment Employment)
        {
            try
            {
                await _db.Employments.AddAsync(Employment);
                await _db.SaveChangesAsync();
                return await _db.Employments.FindAsync(Employment.EmploymentId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Employment> UpdateEmployment(Employment Employment)
        {
            try
            {
                _db.Entry(Employment).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Employment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteEmployment(Employment Employment)
        {
            try
            {
                var dbEmployment = await _db.Employments.FindAsync(Employment.EmploymentId);

                if (dbEmployment == null)
                {
                    return (false, "Employment could not be found");
                }

                _db.Employments.Remove(Employment);
                await _db.SaveChangesAsync();

                return (true, "Employment got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Employments
    }
}
