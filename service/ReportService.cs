using AppScalDB.Models;
using Microsoft.EntityFrameworkCore;

namespace appscal_webapi.Services
{
    public interface IReportService
    {
        // Reports Services
        Task<List<Report>> GetReportListByValue(int offset, int limit, string val); // GET All Reportss
        Task<Report> GetReport(string Report_name); // GET Single Reports        
        Task<List<Report>> GetReportList(string Report_name); // GET List Reports        
        Task<Report> AddReport(Report Report); // POST New Reports
        Task<Report> UpdateReport(Report Report); // PUT Reports
        Task<(bool, string)> DeleteReport(Report Report); // DELETE Reports
    }

    public class ReportService : IReportService
    {
        private readonly XixsrvContext _db;

        public ReportService(XixsrvContext db)
        {
            _db = db;
        }

        #region Reports

        public async Task<List<Report>> GetReportListByValue(int offset, int limit, string val)
        {
            try
            {               
                return await _db.Reports.Skip(offset).Take(limit).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Report> GetReport(string Report_id)
        {
            try
            {
                return await _db.Reports.FindAsync(Report_id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Report>> GetReportList(string Report_id)
        {
            try
            {
                return await _db.Reports
                    .Where(i => i.ReportId == Report_id )
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
       

        public async Task<Report> AddReport(Report Report)
        {
            try
            {
                await _db.Reports.AddAsync(Report);
                await _db.SaveChangesAsync();
                return await _db.Reports.FindAsync(Report.ReportId); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Report> UpdateReport(Report Report)
        {
            try
            {
                _db.Entry(Report).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return Report;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteReport(Report Report)
        {
            try
            {
                var dbReport = await _db.Reports.FindAsync(Report.ReportId);

                if (dbReport == null)
                {
                    return (false, "Report could not be found");
                }

                _db.Reports.Remove(Report);
                await _db.SaveChangesAsync();

                return (true, "Report got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }

        #endregion Reports
    }
}
