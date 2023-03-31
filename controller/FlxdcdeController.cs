using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdcdeController : ControllerBase
    {
        private readonly IFlxdcdeService _FlxdcdeService;

        public FlxdcdeController(IFlxdcdeService FlxdcdeService)
        {
            _FlxdcdeService = FlxdcdeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcdeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdcdes = await _FlxdcdeService.GetFlxdcdeListByValue(offset, limit, val);

            if (Flxdcdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdcdes in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdes);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcdeList(string Flxdcde_name)
        {
            var Flxdcdes = await _FlxdcdeService.GetFlxdcdeList(Flxdcde_name);

            if (Flxdcdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcde found for uci: {Flxdcde_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdes);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcde(string Flxdcde_name)
        {
            var Flxdcdes = await _FlxdcdeService.GetFlxdcde(Flxdcde_name);

            if (Flxdcdes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcde found for uci: {Flxdcde_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcdes);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdcde>> AddFlxdcde(Flxdcde Flxdcde)
        {
            var dbFlxdcde = await _FlxdcdeService.AddFlxdcde(Flxdcde);

            if (dbFlxdcde == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcde.TbFlxdcdeName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdcde", new { uci = Flxdcde.TbFlxdcdeName }, Flxdcde);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdcde(Flxdcde Flxdcde)
        {           
            Flxdcde dbFlxdcde = await _FlxdcdeService.UpdateFlxdcde(Flxdcde);

            if (dbFlxdcde == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcde.TbFlxdcdeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdcde(Flxdcde Flxdcde)
        {            
            (bool status, string message) = await _FlxdcdeService.DeleteFlxdcde(Flxdcde);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcde);
        }
    }
}
