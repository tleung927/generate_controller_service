using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdrelController : ControllerBase
    {
        private readonly IFlxdrelService _FlxdrelService;

        public FlxdrelController(IFlxdrelService FlxdrelService)
        {
            _FlxdrelService = FlxdrelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdrelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdrels = await _FlxdrelService.GetFlxdrelListByValue(offset, limit, val);

            if (Flxdrels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdrels in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdrels);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdrelList(string Flxdrel_name)
        {
            var Flxdrels = await _FlxdrelService.GetFlxdrelList(Flxdrel_name);

            if (Flxdrels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdrel found for uci: {Flxdrel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdrels);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdrel(string Flxdrel_name)
        {
            var Flxdrels = await _FlxdrelService.GetFlxdrel(Flxdrel_name);

            if (Flxdrels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdrel found for uci: {Flxdrel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdrels);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdrel>> AddFlxdrel(Flxdrel Flxdrel)
        {
            var dbFlxdrel = await _FlxdrelService.AddFlxdrel(Flxdrel);

            if (dbFlxdrel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdrel.TbFlxdrelName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdrel", new { uci = Flxdrel.TbFlxdrelName }, Flxdrel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdrel(Flxdrel Flxdrel)
        {           
            Flxdrel dbFlxdrel = await _FlxdrelService.UpdateFlxdrel(Flxdrel);

            if (dbFlxdrel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdrel.TbFlxdrelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdrel(Flxdrel Flxdrel)
        {            
            (bool status, string message) = await _FlxdrelService.DeleteFlxdrel(Flxdrel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdrel);
        }
    }
}
