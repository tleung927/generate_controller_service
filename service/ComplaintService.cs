using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IComplaintService
    {
        // Complaints Services
        Task<List<Complaint>> GetComplaintListByValue(int offset, int limit, string val); // GET All Complaintss
        Task<Complaint> GetComplaint(string Complaint_name); // GET Single Complaints        
        Task<List<Complaint>> GetComplaintList(string Complaint_name); // GET List Complaints        
        Task<Complaint> AddComplaint(Complaint Complaint); // POST New Complaints
        Task<Complaint> UpdateComplaint(Complaint Complaint); // PUT Complaints
        Task<(bool, string)> DeleteComplaint(Complaint Complaint); // DELETE Complaints
    }

    public class ComplaintService : IComplaintService
    {
        private readonly XixsrvContext _db;

        public ComplaintService(XixsrvContext db)
        {
            _db = db;
        }

        #region Complaints

        public async Task<List<Complaint>> GetComplaintListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Complaints.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Complaint> GetComplaint(string Complaint_id)
        {
            try
            {
                return await _db.Complaints.FindAsync(Complaint_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Complaint>> GetComplaintList(string Complaint_id)
        {
            try
            {
                return await _db.Complaints
                    .Where(i => i.ComplaintId == Complaint_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Complaint> AddComplaint(Complaint Complaint)
        {
            try
            {
                await _db.Complaints.AddAsync(Complaint);
                await _db.SaveChangesAsync();
                return await _db.Complaints.FindAsync(Complaint.ComplaintId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Complaint> UpdateComplaint(Complaint Complaint)
        {
            try
            {
                _db.Entry(Complaint).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Complaint;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteComplaint(Complaint Complaint)
        {
            try
            {
                var dbComplaint = await _db.Complaints.FindAsync(Complaint.ComplaintId);

                if (dbComplaint == null)
                {
                    return (false, "Complaint could not be found");
                }

                _db.Complaints.Remove(Complaint);
                await _db.SaveChangesAsync();

                return (true, "Complaint got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Complaints
    }
}
