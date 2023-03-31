using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventTrxDefController : ControllerBase
    {
        private readonly IEventTrxDefService _EventTrxDefService;

        public EventTrxDefController(IEventTrxDefService EventTrxDefService)
        {
            _EventTrxDefService = EventTrxDefService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxDefList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventTrxDefs = await _EventTrxDefService.GetEventTrxDefListByValue(offset, limit, val);

            if (EventTrxDefs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventTrxDefs in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDefs);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxDefList(string EventTrxDef_name)
        {
            var EventTrxDefs = await _EventTrxDefService.GetEventTrxDefList(EventTrxDef_name);

            if (EventTrxDefs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventTrxDef found for uci: {EventTrxDef_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDefs);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxDef(string EventTrxDef_name)
        {
            var EventTrxDefs = await _EventTrxDefService.GetEventTrxDef(EventTrxDef_name);

            if (EventTrxDefs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventTrxDef found for uci: {EventTrxDef_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDefs);
        }

        [HttpPost]
        public async Task<ActionResult<EventTrxDef>> AddEventTrxDef(EventTrxDef EventTrxDef)
        {
            var dbEventTrxDef = await _EventTrxDefService.AddEventTrxDef(EventTrxDef);

            if (dbEventTrxDef == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventTrxDef.TbEventTrxDefName} could not be added."
                );
            }

            return CreatedAtAction("GetEventTrxDef", new { uci = EventTrxDef.TbEventTrxDefName }, EventTrxDef);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventTrxDef(EventTrxDef EventTrxDef)
        {           
            EventTrxDef dbEventTrxDef = await _EventTrxDefService.UpdateEventTrxDef(EventTrxDef);

            if (dbEventTrxDef == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventTrxDef.TbEventTrxDefName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventTrxDef(EventTrxDef EventTrxDef)
        {            
            (bool status, string message) = await _EventTrxDefService.DeleteEventTrxDef(EventTrxDef);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDef);
        }
    }
}
