using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _ReportService;

        public ReportController(IReportService ReportService)
        {
            _ReportService = ReportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReportList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Reports = await _ReportService.GetReportListByValue(offset, limit, val);

            if (Reports == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Reports in database");
            }

            return StatusCode(StatusCodes.Status200OK, Reports);
        }

        [HttpGet]
        public async Task<IActionResult> GetReportList(string Report_name)
        {
            var Reports = await _ReportService.GetReportList(Report_name);

            if (Reports == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Report found for uci: {Report_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Reports);
        }

        [HttpGet]
        public async Task<IActionResult> GetReport(string Report_name)
        {
            var Reports = await _ReportService.GetReport(Report_name);

            if (Reports == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Report found for uci: {Report_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Reports);
        }

        [HttpPost]
        public async Task<ActionResult<Report>> AddReport(Report Report)
        {
            var dbReport = await _ReportService.AddReport(Report);

            if (dbReport == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Report.TbReportName} could not be added."
                );
            }

            return CreatedAtAction("GetReport", new { uci = Report.TbReportName }, Report);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateReport(Report Report)
        {           
            Report dbReport = await _ReportService.UpdateReport(Report);

            if (dbReport == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Report.TbReportName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReport(Report Report)
        {            
            (bool status, string message) = await _ReportService.DeleteReport(Report);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Report);
        }
    }
}
