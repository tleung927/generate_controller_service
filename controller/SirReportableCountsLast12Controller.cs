using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirReportableCountsLast12Controller : ControllerBase
    {
        private readonly ISirReportableCountsLast12Service _SirReportableCountsLast12Service;

        public SirReportableCountsLast12Controller(ISirReportableCountsLast12Service SirReportableCountsLast12Service)
        {
            _SirReportableCountsLast12Service = SirReportableCountsLast12Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetSirReportableCountsLast12List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SirReportableCountsLast12s = await _SirReportableCountsLast12Service.GetSirReportableCountsLast12ListByValue(offset, limit, val);

            if (SirReportableCountsLast12s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SirReportableCountsLast12s in database");
            }

            return StatusCode(StatusCodes.Status200OK, SirReportableCountsLast12s);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirReportableCountsLast12List(string SirReportableCountsLast12_name)
        {
            var SirReportableCountsLast12s = await _SirReportableCountsLast12Service.GetSirReportableCountsLast12List(SirReportableCountsLast12_name);

            if (SirReportableCountsLast12s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirReportableCountsLast12 found for uci: {SirReportableCountsLast12_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirReportableCountsLast12s);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirReportableCountsLast12(string SirReportableCountsLast12_name)
        {
            var SirReportableCountsLast12s = await _SirReportableCountsLast12Service.GetSirReportableCountsLast12(SirReportableCountsLast12_name);

            if (SirReportableCountsLast12s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirReportableCountsLast12 found for uci: {SirReportableCountsLast12_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirReportableCountsLast12s);
        }

        [HttpPost]
        public async Task<ActionResult<SirReportableCountsLast12>> AddSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12)
        {
            var dbSirReportableCountsLast12 = await _SirReportableCountsLast12Service.AddSirReportableCountsLast12(SirReportableCountsLast12);

            if (dbSirReportableCountsLast12 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirReportableCountsLast12.TbSirReportableCountsLast12Name} could not be added."
                );
            }

            return CreatedAtAction("GetSirReportableCountsLast12", new { uci = SirReportableCountsLast12.TbSirReportableCountsLast12Name }, SirReportableCountsLast12);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12)
        {           
            SirReportableCountsLast12 dbSirReportableCountsLast12 = await _SirReportableCountsLast12Service.UpdateSirReportableCountsLast12(SirReportableCountsLast12);

            if (dbSirReportableCountsLast12 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirReportableCountsLast12.TbSirReportableCountsLast12Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSirReportableCountsLast12(SirReportableCountsLast12 SirReportableCountsLast12)
        {            
            (bool status, string message) = await _SirReportableCountsLast12Service.DeleteSirReportableCountsLast12(SirReportableCountsLast12);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SirReportableCountsLast12);
        }
    }
}
