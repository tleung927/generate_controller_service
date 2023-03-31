using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EligibilityController : ControllerBase
    {
        private readonly IEligibilityService _EligibilityService;

        public EligibilityController(IEligibilityService EligibilityService)
        {
            _EligibilityService = EligibilityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Eligibilitys = await _EligibilityService.GetEligibilityListByValue(offset, limit, val);

            if (Eligibilitys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Eligibilitys in database");
            }

            return StatusCode(StatusCodes.Status200OK, Eligibilitys);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityList(string Eligibility_name)
        {
            var Eligibilitys = await _EligibilityService.GetEligibilityList(Eligibility_name);

            if (Eligibilitys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Eligibility found for uci: {Eligibility_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Eligibilitys);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibility(string Eligibility_name)
        {
            var Eligibilitys = await _EligibilityService.GetEligibility(Eligibility_name);

            if (Eligibilitys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Eligibility found for uci: {Eligibility_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Eligibilitys);
        }

        [HttpPost]
        public async Task<ActionResult<Eligibility>> AddEligibility(Eligibility Eligibility)
        {
            var dbEligibility = await _EligibilityService.AddEligibility(Eligibility);

            if (dbEligibility == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Eligibility.TbEligibilityName} could not be added."
                );
            }

            return CreatedAtAction("GetEligibility", new { uci = Eligibility.TbEligibilityName }, Eligibility);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEligibility(Eligibility Eligibility)
        {           
            Eligibility dbEligibility = await _EligibilityService.UpdateEligibility(Eligibility);

            if (dbEligibility == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Eligibility.TbEligibilityName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEligibility(Eligibility Eligibility)
        {            
            (bool status, string message) = await _EligibilityService.DeleteEligibility(Eligibility);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Eligibility);
        }
    }
}
