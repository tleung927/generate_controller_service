using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdvncController : ControllerBase
    {
        private readonly IFlxdvncService _FlxdvncService;

        public FlxdvncController(IFlxdvncService FlxdvncService)
        {
            _FlxdvncService = FlxdvncService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvncList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdvncs = await _FlxdvncService.GetFlxdvncListByValue(offset, limit, val);

            if (Flxdvncs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdvncs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvncs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvncList(string Flxdvnc_name)
        {
            var Flxdvncs = await _FlxdvncService.GetFlxdvncList(Flxdvnc_name);

            if (Flxdvncs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdvnc found for uci: {Flxdvnc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvncs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvnc(string Flxdvnc_name)
        {
            var Flxdvncs = await _FlxdvncService.GetFlxdvnc(Flxdvnc_name);

            if (Flxdvncs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdvnc found for uci: {Flxdvnc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvncs);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdvnc>> AddFlxdvnc(Flxdvnc Flxdvnc)
        {
            var dbFlxdvnc = await _FlxdvncService.AddFlxdvnc(Flxdvnc);

            if (dbFlxdvnc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdvnc.TbFlxdvncName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdvnc", new { uci = Flxdvnc.TbFlxdvncName }, Flxdvnc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdvnc(Flxdvnc Flxdvnc)
        {           
            Flxdvnc dbFlxdvnc = await _FlxdvncService.UpdateFlxdvnc(Flxdvnc);

            if (dbFlxdvnc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdvnc.TbFlxdvncName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdvnc(Flxdvnc Flxdvnc)
        {            
            (bool status, string message) = await _FlxdvncService.DeleteFlxdvnc(Flxdvnc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvnc);
        }
    }
}
