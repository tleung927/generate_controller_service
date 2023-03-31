using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicklerController : ControllerBase
    {
        private readonly ITicklerService _TicklerService;

        public TicklerController(ITicklerService TicklerService)
        {
            _TicklerService = TicklerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Ticklers = await _TicklerService.GetTicklerListByValue(offset, limit, val);

            if (Ticklers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Ticklers in database");
            }

            return StatusCode(StatusCodes.Status200OK, Ticklers);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklerList(string Tickler_name)
        {
            var Ticklers = await _TicklerService.GetTicklerList(Tickler_name);

            if (Ticklers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tickler found for uci: {Tickler_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Ticklers);
        }

        [HttpGet]
        public async Task<IActionResult> GetTickler(string Tickler_name)
        {
            var Ticklers = await _TicklerService.GetTickler(Tickler_name);

            if (Ticklers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Tickler found for uci: {Tickler_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Ticklers);
        }

        [HttpPost]
        public async Task<ActionResult<Tickler>> AddTickler(Tickler Tickler)
        {
            var dbTickler = await _TicklerService.AddTickler(Tickler);

            if (dbTickler == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tickler.TbTicklerName} could not be added."
                );
            }

            return CreatedAtAction("GetTickler", new { uci = Tickler.TbTicklerName }, Tickler);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTickler(Tickler Tickler)
        {           
            Tickler dbTickler = await _TicklerService.UpdateTickler(Tickler);

            if (dbTickler == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Tickler.TbTicklerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTickler(Tickler Tickler)
        {            
            (bool status, string message) = await _TicklerService.DeleteTickler(Tickler);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Tickler);
        }
    }
}
