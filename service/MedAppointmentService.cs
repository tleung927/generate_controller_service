using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IMedAppointmentService
    {
        // MedAppointments Services
        Task<List<MedAppointment>> GetMedAppointmentListByValue(int offset, int limit, string val); // GET All MedAppointmentss
        Task<MedAppointment> GetMedAppointment(string MedAppointment_name); // GET Single MedAppointments        
        Task<List<MedAppointment>> GetMedAppointmentList(string MedAppointment_name); // GET List MedAppointments        
        Task<MedAppointment> AddMedAppointment(MedAppointment MedAppointment); // POST New MedAppointments
        Task<MedAppointment> UpdateMedAppointment(MedAppointment MedAppointment); // PUT MedAppointments
        Task<(bool, string)> DeleteMedAppointment(MedAppointment MedAppointment); // DELETE MedAppointments
    }

    public class MedAppointmentService : IMedAppointmentService
    {
        private readonly XixsrvContext _db;

        public MedAppointmentService(XixsrvContext db)
        {
            _db = db;
        }

        #region MedAppointments

        public async Task<List<MedAppointment>> GetMedAppointmentListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.MedAppointments.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<MedAppointment> GetMedAppointment(string MedAppointment_id)
        {
            try
            {
                return await _db.MedAppointments.FindAsync(MedAppointment_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<MedAppointment>> GetMedAppointmentList(string MedAppointment_id)
        {
            try
            {
                return await _db.MedAppointments
                    .Where(i => i.MedAppointmentId == MedAppointment_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<MedAppointment> AddMedAppointment(MedAppointment MedAppointment)
        {
            try
            {
                await _db.MedAppointments.AddAsync(MedAppointment);
                await _db.SaveChangesAsync();
                return await _db.MedAppointments.FindAsync(MedAppointment.MedAppointmentId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<MedAppointment> UpdateMedAppointment(MedAppointment MedAppointment)
        {
            try
            {
                _db.Entry(MedAppointment).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return MedAppointment;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteMedAppointment(MedAppointment MedAppointment)
        {
            try
            {
                var dbMedAppointment = await _db.MedAppointments.FindAsync(MedAppointment.MedAppointmentId);

                if (dbMedAppointment == null)
                {
                    return (false, "MedAppointment could not be found");
                }

                _db.MedAppointments.Remove(MedAppointment);
                await _db.SaveChangesAsync();

                return (true, "MedAppointment got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion MedAppointments
    }
}
