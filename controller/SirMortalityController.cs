using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirMortalityController : ControllerBase
    {
        private readonly ISirMortalityService _SirMortalityService;

        public SirMortalityController(ISirMortalityService SirMortalityService)
        {
            _SirMortalityService = SirMortalityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSirMortalityList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SirMortalitys = await _SirMortalityService.GetSirMortalityListByValue(offset, limit, val);

            if (SirMortalitys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SirMortalitys in database");
            }

            return StatusCode(StatusCodes.Status200OK, SirMortalitys);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirMortalityList(string SirMortality_name)
        {
            var SirMortalitys = await _SirMortalityService.GetSirMortalityList(SirMortality_name);

            if (SirMortalitys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirMortality found for uci: {SirMortality_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirMortalitys);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirMortality(string SirMortality_name)
        {
            var SirMortalitys = await _SirMortalityService.GetSirMortality(SirMortality_name);

            if (SirMortalitys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirMortality found for uci: {SirMortality_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirMortalitys);
        }

        [HttpPost]
        public async Task<ActionResult<SirMortality>> AddSirMortality(SirMortality SirMortality)
        {
            var dbSirMortality = await _SirMortalityService.AddSirMortality(SirMortality);

            if (dbSirMortality == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirMortality.TbSirMortalityName} could not be added."
                );
            }

            return CreatedAtAction("GetSirMortality", new { uci = SirMortality.TbSirMortalityName }, SirMortality);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSirMortality(SirMortality SirMortality)
        {           
            SirMortality dbSirMortality = await _SirMortalityService.UpdateSirMortality(SirMortality);

            if (dbSirMortality == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirMortality.TbSirMortalityName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSirMortality(SirMortality SirMortality)
        {            
            (bool status, string message) = await _SirMortalityService.DeleteSirMortality(SirMortality);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SirMortality);
        }
    }
}
