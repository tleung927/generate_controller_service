using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SirTempController : ControllerBase
    {
        private readonly ISirTempService _SirTempService;

        public SirTempController(ISirTempService SirTempService)
        {
            _SirTempService = SirTempService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSirTempList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SirTemps = await _SirTempService.GetSirTempListByValue(offset, limit, val);

            if (SirTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SirTemps in database");
            }

            return StatusCode(StatusCodes.Status200OK, SirTemps);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirTempList(string SirTemp_name)
        {
            var SirTemps = await _SirTempService.GetSirTempList(SirTemp_name);

            if (SirTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirTemp found for uci: {SirTemp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirTemps);
        }

        [HttpGet]
        public async Task<IActionResult> GetSirTemp(string SirTemp_name)
        {
            var SirTemps = await _SirTempService.GetSirTemp(SirTemp_name);

            if (SirTemps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SirTemp found for uci: {SirTemp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SirTemps);
        }

        [HttpPost]
        public async Task<ActionResult<SirTemp>> AddSirTemp(SirTemp SirTemp)
        {
            var dbSirTemp = await _SirTempService.AddSirTemp(SirTemp);

            if (dbSirTemp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirTemp.TbSirTempName} could not be added."
                );
            }

            return CreatedAtAction("GetSirTemp", new { uci = SirTemp.TbSirTempName }, SirTemp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSirTemp(SirTemp SirTemp)
        {           
            SirTemp dbSirTemp = await _SirTempService.UpdateSirTemp(SirTemp);

            if (dbSirTemp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SirTemp.TbSirTempName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSirTemp(SirTemp SirTemp)
        {            
            (bool status, string message) = await _SirTempService.DeleteSirTemp(SirTemp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SirTemp);
        }
    }
}
