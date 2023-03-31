using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdcliController : ControllerBase
    {
        private readonly IFlxdcliService _FlxdcliService;

        public FlxdcliController(IFlxdcliService FlxdcliService)
        {
            _FlxdcliService = FlxdcliService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcliList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Flxdclis = await _FlxdcliService.GetFlxdcliListByValue(offset, limit, val);

            if (Flxdclis == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Flxdclis in database");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdclis);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcliList(string Flxdcli_name)
        {
            var Flxdclis = await _FlxdcliService.GetFlxdcliList(Flxdcli_name);

            if (Flxdclis == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcli found for uci: {Flxdcli_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdclis);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdcli(string Flxdcli_name)
        {
            var Flxdclis = await _FlxdcliService.GetFlxdcli(Flxdcli_name);

            if (Flxdclis == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Flxdcli found for uci: {Flxdcli_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Flxdclis);
        }

        [HttpPost]
        public async Task<ActionResult<Flxdcli>> AddFlxdcli(Flxdcli Flxdcli)
        {
            var dbFlxdcli = await _FlxdcliService.AddFlxdcli(Flxdcli);

            if (dbFlxdcli == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcli.TbFlxdcliName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdcli", new { uci = Flxdcli.TbFlxdcliName }, Flxdcli);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdcli(Flxdcli Flxdcli)
        {           
            Flxdcli dbFlxdcli = await _FlxdcliService.UpdateFlxdcli(Flxdcli);

            if (dbFlxdcli == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Flxdcli.TbFlxdcliName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdcli(Flxdcli Flxdcli)
        {            
            (bool status, string message) = await _FlxdcliService.DeleteFlxdcli(Flxdcli);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Flxdcli);
        }
    }
}
