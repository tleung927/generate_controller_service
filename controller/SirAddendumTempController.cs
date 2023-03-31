using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirAddendumTempController : ControllerBase
    {
        private readonly ISirAddendumTempService _SirAddendumTempService;

        public SirAddendumTempController(ISirAddendumTempService SirAddendumTempService)
        {
            _SirAddendumTempService = SirAddendumTempService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSirAddendumTempList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SirAddendumTemps = await _SirAddendumTempService.GetSirAddendumTempListByValue(offset, limit, val);

            if (SirAddendumTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SirAddendumTemps in database");
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendumTemps);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirAddendumTempList(string SirAddendumTemp_name)
        {
            var SirAddendumTemps = await _SirAddendumTempService.GetSirAddendumTempList(SirAddendumTemp_name);

            if (SirAddendumTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirAddendumTemp found for uci: {SirAddendumTemp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendumTemps);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirAddendumTemp(string SirAddendumTemp_name)
        {
            var SirAddendumTemps = await _SirAddendumTempService.GetSirAddendumTemp(SirAddendumTemp_name);

            if (SirAddendumTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirAddendumTemp found for uci: {SirAddendumTemp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendumTemps);
        }

        [HttpPost]
        public async Task<ActionResult<SirAddendumTemp>> AddSirAddendumTemp(SirAddendumTemp SirAddendumTemp)
        {
            var dbSirAddendumTemp = await _SirAddendumTempService.AddSirAddendumTemp(SirAddendumTemp);

            if (dbSirAddendumTemp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirAddendumTemp.TbSirAddendumTempName} could not be added."
                );
            }

            return CreatedAtAction("GetSirAddendumTemp", new { uci = SirAddendumTemp.TbSirAddendumTempName }, SirAddendumTemp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSirAddendumTemp(SirAddendumTemp SirAddendumTemp)
        {           
            SirAddendumTemp dbSirAddendumTemp = await _SirAddendumTempService.UpdateSirAddendumTemp(SirAddendumTemp);

            if (dbSirAddendumTemp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirAddendumTemp.TbSirAddendumTempName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSirAddendumTemp(SirAddendumTemp SirAddendumTemp)
        {            
            (bool status, string message) = await _SirAddendumTempService.DeleteSirAddendumTemp(SirAddendumTemp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SirAddendumTemp);
        }
    }
}
