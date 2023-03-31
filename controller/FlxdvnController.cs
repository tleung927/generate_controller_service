using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdvnController : ControllerBase
    {
        private readonly IFlxdvnService _FlxdvnService;

        public FlxdvnController(IFlxdvnService FlxdvnService)
        {
            _FlxdvnService = FlxdvnService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvnList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdvns = await _FlxdvnService.GetFlxdvnListByValue(offset, limit, val);

            if (Flxdvns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdvns in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvns);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvnList(string Flxdvn_name)
        {
            var Flxdvns = await _FlxdvnService.GetFlxdvnList(Flxdvn_name);

            if (Flxdvns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdvn found for uci: {Flxdvn_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvns);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvn(string Flxdvn_name)
        {
            var Flxdvns = await _FlxdvnService.GetFlxdvn(Flxdvn_name);

            if (Flxdvns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdvn found for uci: {Flxdvn_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvns);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdvn>> AddFlxdvn(Flxdvn Flxdvn)
        {
            var dbFlxdvn = await _FlxdvnService.AddFlxdvn(Flxdvn);

            if (dbFlxdvn == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdvn.TbFlxdvnName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdvn", new { uci = Flxdvn.TbFlxdvnName }, Flxdvn);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdvn(Flxdvn Flxdvn)
        {           
            Flxdvn dbFlxdvn = await _FlxdvnService.UpdateFlxdvn(Flxdvn);

            if (dbFlxdvn == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdvn.TbFlxdvnName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdvn(Flxdvn Flxdvn)
        {            
            (bool status, string message) = await _FlxdvnService.DeleteFlxdvn(Flxdvn);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvn);
        }
    }
}
