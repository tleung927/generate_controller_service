using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdvndController : ControllerBase
    {
        private readonly IFlxdvndService _FlxdvndService;

        public FlxdvndController(IFlxdvndService FlxdvndService)
        {
            _FlxdvndService = FlxdvndService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvndList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdvnds = await _FlxdvndService.GetFlxdvndListByValue(offset, limit, val);

            if (Flxdvnds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdvnds in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvnds);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvndList(string Flxdvnd_name)
        {
            var Flxdvnds = await _FlxdvndService.GetFlxdvndList(Flxdvnd_name);

            if (Flxdvnds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdvnd found for uci: {Flxdvnd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvnds);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdvnd(string Flxdvnd_name)
        {
            var Flxdvnds = await _FlxdvndService.GetFlxdvnd(Flxdvnd_name);

            if (Flxdvnds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdvnd found for uci: {Flxdvnd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvnds);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdvnd>> AddFlxdvnd(Flxdvnd Flxdvnd)
        {
            var dbFlxdvnd = await _FlxdvndService.AddFlxdvnd(Flxdvnd);

            if (dbFlxdvnd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdvnd.TbFlxdvndName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdvnd", new { uci = Flxdvnd.TbFlxdvndName }, Flxdvnd);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdvnd(Flxdvnd Flxdvnd)
        {           
            Flxdvnd dbFlxdvnd = await _FlxdvndService.UpdateFlxdvnd(Flxdvnd);

            if (dbFlxdvnd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdvnd.TbFlxdvndName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdvnd(Flxdvnd Flxdvnd)
        {            
            (bool status, string message) = await _FlxdvndService.DeleteFlxdvnd(Flxdvnd);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdvnd);
        }
    }
}
