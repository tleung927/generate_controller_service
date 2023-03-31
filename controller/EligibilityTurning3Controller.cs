using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EligibilityTurning3Controller : ControllerBase
    {
        private readonly IEligibilityTurning3Service _EligibilityTurning3Service;

        public EligibilityTurning3Controller(IEligibilityTurning3Service EligibilityTurning3Service)
        {
            _EligibilityTurning3Service = EligibilityTurning3Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityTurning3List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EligibilityTurning3s = await _EligibilityTurning3Service.GetEligibilityTurning3ListByValue(offset, limit, val);

            if (EligibilityTurning3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EligibilityTurning3s in database");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityTurning3s);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityTurning3List(string EligibilityTurning3_name)
        {
            var EligibilityTurning3s = await _EligibilityTurning3Service.GetEligibilityTurning3List(EligibilityTurning3_name);

            if (EligibilityTurning3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EligibilityTurning3 found for uci: {EligibilityTurning3_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityTurning3s);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityTurning3(string EligibilityTurning3_name)
        {
            var EligibilityTurning3s = await _EligibilityTurning3Service.GetEligibilityTurning3(EligibilityTurning3_name);

            if (EligibilityTurning3s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EligibilityTurning3 found for uci: {EligibilityTurning3_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityTurning3s);
        }

        [HttpPost]
        public async Task<ActionResult<EligibilityTurning3>> AddEligibilityTurning3(EligibilityTurning3 EligibilityTurning3)
        {
            var dbEligibilityTurning3 = await _EligibilityTurning3Service.AddEligibilityTurning3(EligibilityTurning3);

            if (dbEligibilityTurning3 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EligibilityTurning3.TbEligibilityTurning3Name} could not be added."
                );
            }

            return CreatedAtAction("GetEligibilityTurning3", new { uci = EligibilityTurning3.TbEligibilityTurning3Name }, EligibilityTurning3);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEligibilityTurning3(EligibilityTurning3 EligibilityTurning3)
        {           
            EligibilityTurning3 dbEligibilityTurning3 = await _EligibilityTurning3Service.UpdateEligibilityTurning3(EligibilityTurning3);

            if (dbEligibilityTurning3 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EligibilityTurning3.TbEligibilityTurning3Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEligibilityTurning3(EligibilityTurning3 EligibilityTurning3)
        {            
            (bool status, string message) = await _EligibilityTurning3Service.DeleteEligibilityTurning3(EligibilityTurning3);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityTurning3);
        }
    }
}
