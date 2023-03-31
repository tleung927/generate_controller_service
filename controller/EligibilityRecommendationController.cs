using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EligibilityRecommendationController : ControllerBase
    {
        private readonly IEligibilityRecommendationService _EligibilityRecommendationService;

        public EligibilityRecommendationController(IEligibilityRecommendationService EligibilityRecommendationService)
        {
            _EligibilityRecommendationService = EligibilityRecommendationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityRecommendationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EligibilityRecommendations = await _EligibilityRecommendationService.GetEligibilityRecommendationListByValue(offset, limit, val);

            if (EligibilityRecommendations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EligibilityRecommendations in database");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityRecommendations);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityRecommendationList(string EligibilityRecommendation_name)
        {
            var EligibilityRecommendations = await _EligibilityRecommendationService.GetEligibilityRecommendationList(EligibilityRecommendation_name);

            if (EligibilityRecommendations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EligibilityRecommendation found for uci: {EligibilityRecommendation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityRecommendations);
        }

        [HttpGet]
        public async Task<IActionResult> GetEligibilityRecommendation(string EligibilityRecommendation_name)
        {
            var EligibilityRecommendations = await _EligibilityRecommendationService.GetEligibilityRecommendation(EligibilityRecommendation_name);

            if (EligibilityRecommendations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EligibilityRecommendation found for uci: {EligibilityRecommendation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityRecommendations);
        }

        [HttpPost]
        public async Task<ActionResult<EligibilityRecommendation>> AddEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation)
        {
            var dbEligibilityRecommendation = await _EligibilityRecommendationService.AddEligibilityRecommendation(EligibilityRecommendation);

            if (dbEligibilityRecommendation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EligibilityRecommendation.TbEligibilityRecommendationName} could not be added."
                );
            }

            return CreatedAtAction("GetEligibilityRecommendation", new { uci = EligibilityRecommendation.TbEligibilityRecommendationName }, EligibilityRecommendation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation)
        {           
            EligibilityRecommendation dbEligibilityRecommendation = await _EligibilityRecommendationService.UpdateEligibilityRecommendation(EligibilityRecommendation);

            if (dbEligibilityRecommendation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EligibilityRecommendation.TbEligibilityRecommendationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEligibilityRecommendation(EligibilityRecommendation EligibilityRecommendation)
        {            
            (bool status, string message) = await _EligibilityRecommendationService.DeleteEligibilityRecommendation(EligibilityRecommendation);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EligibilityRecommendation);
        }
    }
}
