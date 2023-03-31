using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdcsupController : ControllerBase
    {
        private readonly IFlxdcsupService _FlxdcsupService;

        public FlxdcsupController(IFlxdcsupService FlxdcsupService)
        {
            _FlxdcsupService = FlxdcsupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcsupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdcsups = await _FlxdcsupService.GetFlxdcsupListByValue(offset, limit, val);

            if (Flxdcsups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdcsups in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcsups);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcsupList(string Flxdcsup_name)
        {
            var Flxdcsups = await _FlxdcsupService.GetFlxdcsupList(Flxdcsup_name);

            if (Flxdcsups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcsup found for uci: {Flxdcsup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcsups);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcsup(string Flxdcsup_name)
        {
            var Flxdcsups = await _FlxdcsupService.GetFlxdcsup(Flxdcsup_name);

            if (Flxdcsups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcsup found for uci: {Flxdcsup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcsups);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdcsup>> AddFlxdcsup(Flxdcsup Flxdcsup)
        {
            var dbFlxdcsup = await _FlxdcsupService.AddFlxdcsup(Flxdcsup);

            if (dbFlxdcsup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcsup.TbFlxdcsupName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdcsup", new { uci = Flxdcsup.TbFlxdcsupName }, Flxdcsup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdcsup(Flxdcsup Flxdcsup)
        {           
            Flxdcsup dbFlxdcsup = await _FlxdcsupService.UpdateFlxdcsup(Flxdcsup);

            if (dbFlxdcsup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcsup.TbFlxdcsupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdcsup(Flxdcsup Flxdcsup)
        {            
            (bool status, string message) = await _FlxdcsupService.DeleteFlxdcsup(Flxdcsup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcsup);
        }
    }
}
