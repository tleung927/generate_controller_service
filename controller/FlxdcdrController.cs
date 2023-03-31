using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdcdrController : ControllerBase
    {
        private readonly IFlxdcdrService _FlxdcdrService;

        public FlxdcdrController(IFlxdcdrService FlxdcdrService)
        {
            _FlxdcdrService = FlxdcdrService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcdrList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdcdrs = await _FlxdcdrService.GetFlxdcdrListByValue(offset, limit, val);

            if (Flxdcdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdcdrs in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcdrList(string Flxdcdr_name)
        {
            var Flxdcdrs = await _FlxdcdrService.GetFlxdcdrList(Flxdcdr_name);

            if (Flxdcdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcdr found for uci: {Flxdcdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdrs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcdr(string Flxdcdr_name)
        {
            var Flxdcdrs = await _FlxdcdrService.GetFlxdcdr(Flxdcdr_name);

            if (Flxdcdrs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcdr found for uci: {Flxdcdr_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdrs);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdcdr>> AddFlxdcdr(Flxdcdr Flxdcdr)
        {
            var dbFlxdcdr = await _FlxdcdrService.AddFlxdcdr(Flxdcdr);

            if (dbFlxdcdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcdr.TbFlxdcdrName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdcdr", new { uci = Flxdcdr.TbFlxdcdrName }, Flxdcdr);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdcdr(Flxdcdr Flxdcdr)
        {           
            Flxdcdr dbFlxdcdr = await _FlxdcdrService.UpdateFlxdcdr(Flxdcdr);

            if (dbFlxdcdr == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcdr.TbFlxdcdrName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdcdr(Flxdcdr Flxdcdr)
        {            
            (bool status, string message) = await _FlxdcdrService.DeleteFlxdcdr(Flxdcdr);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdr);
        }
    }
}
