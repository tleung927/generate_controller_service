using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicklersSetController : ControllerBase
    {
        private readonly ITicklersSetService _TicklersSetService;

        public TicklersSetController(ITicklersSetService TicklersSetService)
        {
            _TicklersSetService = TicklersSetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersSetList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TicklersSets = await _TicklersSetService.GetTicklersSetListByValue(offset, limit, val);

            if (TicklersSets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TicklersSets in database");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersSets);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersSetList(string TicklersSet_name)
        {
            var TicklersSets = await _TicklersSetService.GetTicklersSetList(TicklersSet_name);

            if (TicklersSets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TicklersSet found for uci: {TicklersSet_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersSets);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersSet(string TicklersSet_name)
        {
            var TicklersSets = await _TicklersSetService.GetTicklersSet(TicklersSet_name);

            if (TicklersSets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TicklersSet found for uci: {TicklersSet_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersSets);
        }

        [HttpPost]
        public async Task<ActionResult<TicklersSet>> AddTicklersSet(TicklersSet TicklersSet)
        {
            var dbTicklersSet = await _TicklersSetService.AddTicklersSet(TicklersSet);

            if (dbTicklersSet == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TicklersSet.TbTicklersSetName} could not be added."
                );
            }

            return CreatedAtAction("GetTicklersSet", new { uci = TicklersSet.TbTicklersSetName }, TicklersSet);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicklersSet(TicklersSet TicklersSet)
        {           
            TicklersSet dbTicklersSet = await _TicklersSetService.UpdateTicklersSet(TicklersSet);

            if (dbTicklersSet == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TicklersSet.TbTicklersSetName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTicklersSet(TicklersSet TicklersSet)
        {            
            (bool status, string message) = await _TicklersSetService.DeleteTicklersSet(TicklersSet);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TicklersSet);
        }
    }
}
