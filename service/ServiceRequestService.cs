using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IServiceRequestService
    {
        // ServiceRequests Services
        Task<List<ServiceRequest>> GetServiceRequestListByValue(int offset, int limit, string val); // GET All ServiceRequestss
        Task<ServiceRequest> GetServiceRequest(string ServiceRequest_name); // GET Single ServiceRequests        
        Task<List<ServiceRequest>> GetServiceRequestList(string ServiceRequest_name); // GET List ServiceRequests        
        Task<ServiceRequest> AddServiceRequest(ServiceRequest ServiceRequest); // POST New ServiceRequests
        Task<ServiceRequest> UpdateServiceRequest(ServiceRequest ServiceRequest); // PUT ServiceRequests
        Task<(bool, string)> DeleteServiceRequest(ServiceRequest ServiceRequest); // DELETE ServiceRequests
    }

    public class ServiceRequestService : IServiceRequestService
    {
        private readonly XixsrvContext _db;

        public ServiceRequestService(XixsrvContext db)
        {
            _db = db;
        }

        #region ServiceRequests

        public async Task<List<ServiceRequest>> GetServiceRequestListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.ServiceRequests.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<ServiceRequest> GetServiceRequest(string ServiceRequest_id)
        {
            try
            {
                return await _db.ServiceRequests.FindAsync(ServiceRequest_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<ServiceRequest>> GetServiceRequestList(string ServiceRequest_id)
        {
            try
            {
                return await _db.ServiceRequests
                    .Where(i => i.ServiceRequestId == ServiceRequest_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<ServiceRequest> AddServiceRequest(ServiceRequest ServiceRequest)
        {
            try
            {
                await _db.ServiceRequests.AddAsync(ServiceRequest);
                await _db.SaveChangesAsync();
                return await _db.ServiceRequests.FindAsync(ServiceRequest.ServiceRequestId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<ServiceRequest> UpdateServiceRequest(ServiceRequest ServiceRequest)
        {
            try
            {
                _db.Entry(ServiceRequest).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return ServiceRequest;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteServiceRequest(ServiceRequest ServiceRequest)
        {
            try
            {
                var dbServiceRequest = await _db.ServiceRequests.FindAsync(ServiceRequest.ServiceRequestId);

                if (dbServiceRequest == null)
                {
                    return (false, "ServiceRequest could not be found");
                }

                _db.ServiceRequests.Remove(ServiceRequest);
                await _db.SaveChangesAsync();

                return (true, "ServiceRequest got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion ServiceRequests
    }
}
