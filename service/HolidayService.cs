using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IHolidayService
    {
        // Holidays Services
        Task<List<Holiday>> GetHolidayListByValue(int offset, int limit, string val); // GET All Holidayss
        Task<Holiday> GetHoliday(string Holiday_name); // GET Single Holidays        
        Task<List<Holiday>> GetHolidayList(string Holiday_name); // GET List Holidays        
        Task<Holiday> AddHoliday(Holiday Holiday); // POST New Holidays
        Task<Holiday> UpdateHoliday(Holiday Holiday); // PUT Holidays
        Task<(bool, string)> DeleteHoliday(Holiday Holiday); // DELETE Holidays
    }

    public class HolidayService : IHolidayService
    {
        private readonly XixsrvContext _db;

        public HolidayService(XixsrvContext db)
        {
            _db = db;
        }

        #region Holidays

        public async Task<List<Holiday>> GetHolidayListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Holidays.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Holiday> GetHoliday(string Holiday_id)
        {
            try
            {
                return await _db.Holidays.FindAsync(Holiday_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Holiday>> GetHolidayList(string Holiday_id)
        {
            try
            {
                return await _db.Holidays
                    .Where(i => i.HolidayId == Holiday_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Holiday> AddHoliday(Holiday Holiday)
        {
            try
            {
                await _db.Holidays.AddAsync(Holiday);
                await _db.SaveChangesAsync();
                return await _db.Holidays.FindAsync(Holiday.HolidayId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Holiday> UpdateHoliday(Holiday Holiday)
        {
            try
            {
                _db.Entry(Holiday).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Holiday;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteHoliday(Holiday Holiday)
        {
            try
            {
                var dbHoliday = await _db.Holidays.FindAsync(Holiday.HolidayId);

                if (dbHoliday == null)
                {
                    return (false, "Holiday could not be found");
                }

                _db.Holidays.Remove(Holiday);
                await _db.SaveChangesAsync();

                return (true, "Holiday got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Holidays
    }
}
