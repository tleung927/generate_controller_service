using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdadrController : ControllerBase
    {
        private readonly IFlxdadrService _FlxdadrService;

        public FlxdadrController(IFlxdadrService FlxdadrService)
        {
            _FlxdadrService = FlxdadrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdadrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdadrs = await _FlxdadrService.GetFlxdadrListByValue(offset, limit, val);

            if (Flxdadrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdadrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdadrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdadrList(string Flxdadr_name)
        {
            var Flxdadrs = await _FlxdadrService.GetFlxdadrList(Flxdadr_name);

            if (Flxdadrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdadr found for uci: {Flxdadr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdadrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdadr(string Flxdadr_name)
        {
            var Flxdadrs = await _FlxdadrService.GetFlxdadr(Flxdadr_name);

            if (Flxdadrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdadr found for uci: {Flxdadr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdadrs);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdadr>> AddFlxdadr(Flxdadr Flxdadr)
        {
            var dbFlxdadr = await _FlxdadrService.AddFlxdadr(Flxdadr);

            if (dbFlxdadr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdadr.TbFlxdadrName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdadr", new { uci = Flxdadr.TbFlxdadrName }, Flxdadr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdadr(Flxdadr Flxdadr)
        {           
            Flxdadr dbFlxdadr = await _FlxdadrService.UpdateFlxdadr(Flxdadr);

            if (dbFlxdadr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdadr.TbFlxdadrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdadr(Flxdadr Flxdadr)
        {            
            (bool status, string message) = await _FlxdadrService.DeleteFlxdadr(Flxdadr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdadr);
        }
    }
}
