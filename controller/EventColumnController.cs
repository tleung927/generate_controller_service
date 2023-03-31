using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventColumnController : ControllerBase
    {
        private readonly IEventColumnService _EventColumnService;

        public EventColumnController(IEventColumnService EventColumnService)
        {
            _EventColumnService = EventColumnService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventColumnList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventColumns = await _EventColumnService.GetEventColumnListByValue(offset, limit, val);

            if (EventColumns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventColumns in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventColumnList(string EventColumn_name)
        {
            var EventColumns = await _EventColumnService.GetEventColumnList(EventColumn_name);

            if (EventColumns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventColumn found for uci: {EventColumn_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventColumn(string EventColumn_name)
        {
            var EventColumns = await _EventColumnService.GetEventColumn(EventColumn_name);

            if (EventColumns == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventColumn found for uci: {EventColumn_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns);
        }

        [HttpPost]
        public async Task<ActionResult<EventColumn>> AddEventColumn(EventColumn EventColumn)
        {
            var dbEventColumn = await _EventColumnService.AddEventColumn(EventColumn);

            if (dbEventColumn == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventColumn.TbEventColumnName} could not be added."
                );
            }

            return CreatedAtAction("GetEventColumn", new { uci = EventColumn.TbEventColumnName }, EventColumn);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventColumn(EventColumn EventColumn)
        {           
            EventColumn dbEventColumn = await _EventColumnService.UpdateEventColumn(EventColumn);

            if (dbEventColumn == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventColumn.TbEventColumnName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventColumn(EventColumn EventColumn)
        {            
            (bool status, string message) = await _EventColumnService.DeleteEventColumn(EventColumn);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventColumn);
        }
    }
}
