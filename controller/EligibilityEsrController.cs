using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EligibilityEsrController : ControllerBase
    {
        private readonly IEligibilityEsrService _EligibilityEsrService;

        public EligibilityEsrController(IEligibilityEsrService EligibilityEsrService)
        {
            _EligibilityEsrService = EligibilityEsrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityEsrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EligibilityEsrs = await _EligibilityEsrService.GetEligibilityEsrListByValue(offset, limit, val);

            if (EligibilityEsrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EligibilityEsrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityEsrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityEsrList(string EligibilityEsr_name)
        {
            var EligibilityEsrs = await _EligibilityEsrService.GetEligibilityEsrList(EligibilityEsr_name);

            if (EligibilityEsrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EligibilityEsr found for uci: {EligibilityEsr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityEsrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityEsr(string EligibilityEsr_name)
        {
            var EligibilityEsrs = await _EligibilityEsrService.GetEligibilityEsr(EligibilityEsr_name);

            if (EligibilityEsrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EligibilityEsr found for uci: {EligibilityEsr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityEsrs);
        }

        [HttpPost]
        public async Task<ActionResult<EligibilityEsr>> AddEligibilityEsr(EligibilityEsr EligibilityEsr)
        {
            var dbEligibilityEsr = await _EligibilityEsrService.AddEligibilityEsr(EligibilityEsr);

            if (dbEligibilityEsr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EligibilityEsr.TbEligibilityEsrName} could not be added."
                );
            }

            return CreatedAtAction("GetEligibilityEsr", new { uci = EligibilityEsr.TbEligibilityEsrName }, EligibilityEsr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEligibilityEsr(EligibilityEsr EligibilityEsr)
        {           
            EligibilityEsr dbEligibilityEsr = await _EligibilityEsrService.UpdateEligibilityEsr(EligibilityEsr);

            if (dbEligibilityEsr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EligibilityEsr.TbEligibilityEsrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEligibilityEsr(EligibilityEsr EligibilityEsr)
        {            
            (bool status, string message) = await _EligibilityEsrService.DeleteEligibilityEsr(EligibilityEsr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityEsr);
        }
    }
}
