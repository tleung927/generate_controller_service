using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdsIfspformCorrectionController : ControllerBase
    {
        private readonly IAdsIfspformCorrectionService _AdsIfspformCorrectionService;

        public AdsIfspformCorrectionController(IAdsIfspformCorrectionService AdsIfspformCorrectionService)
        {
            _AdsIfspformCorrectionService = AdsIfspformCorrectionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAdsIfspformCorrectionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var AdsIfspformCorrections = await _AdsIfspformCorrectionService.GetAdsIfspformCorrectionListByValue(offset, limit, val);

            if (AdsIfspformCorrections == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No AdsIfspformCorrections in database");
            }

            return StatusCode(StatusCodes.Status200OK, AdsIfspformCorrections);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdsIfspformCorrectionList(string AdsIfspformCorrection_name)
        {
            var AdsIfspformCorrections = await _AdsIfspformCorrectionService.GetAdsIfspformCorrectionList(AdsIfspformCorrection_name);

            if (AdsIfspformCorrections == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No AdsIfspformCorrection found for uci: {AdsIfspformCorrection_name}");
            }

            return StatusCode(StatusCodes.Status200OK, AdsIfspformCorrections);
        }

        [HttpGet]
        public async Task<IActionResult> GetAdsIfspformCorrection(string AdsIfspformCorrection_name)
        {
            var AdsIfspformCorrections = await _AdsIfspformCorrectionService.GetAdsIfspformCorrection(AdsIfspformCorrection_name);

            if (AdsIfspformCorrections == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No AdsIfspformCorrection found for uci: {AdsIfspformCorrection_name}");
            }

            return StatusCode(StatusCodes.Status200OK, AdsIfspformCorrections);
        }

        [HttpPost]
        public async Task<ActionResult<AdsIfspformCorrection>> AddAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection)
        {
            var dbAdsIfspformCorrection = await _AdsIfspformCorrectionService.AddAdsIfspformCorrection(AdsIfspformCorrection);

            if (dbAdsIfspformCorrection == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{AdsIfspformCorrection.TbAdsIfspformCorrectionName} could not be added."
                );
            }

            return CreatedAtAction("GetAdsIfspformCorrection", new { uci = AdsIfspformCorrection.TbAdsIfspformCorrectionName }, AdsIfspformCorrection);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection)
        {           
            AdsIfspformCorrection dbAdsIfspformCorrection = await _AdsIfspformCorrectionService.UpdateAdsIfspformCorrection(AdsIfspformCorrection);

            if (dbAdsIfspformCorrection == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{AdsIfspformCorrection.TbAdsIfspformCorrectionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAdsIfspformCorrection(AdsIfspformCorrection AdsIfspformCorrection)
        {            
            (bool status, string message) = await _AdsIfspformCorrectionService.DeleteAdsIfspformCorrection(AdsIfspformCorrection);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, AdsIfspformCorrection);
        }
    }
}
