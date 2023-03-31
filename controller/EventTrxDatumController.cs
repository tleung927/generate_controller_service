using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventTrxDatumController : ControllerBase
    {
        private readonly IEventTrxDatumService _EventTrxDatumService;

        public EventTrxDatumController(IEventTrxDatumService EventTrxDatumService)
        {
            _EventTrxDatumService = EventTrxDatumService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxDatumList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventTrxDatums = await _EventTrxDatumService.GetEventTrxDatumListByValue(offset, limit, val);

            if (EventTrxDatums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventTrxDatums in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDatums);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxDatumList(string EventTrxDatum_name)
        {
            var EventTrxDatums = await _EventTrxDatumService.GetEventTrxDatumList(EventTrxDatum_name);

            if (EventTrxDatums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventTrxDatum found for uci: {EventTrxDatum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDatums);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxDatum(string EventTrxDatum_name)
        {
            var EventTrxDatums = await _EventTrxDatumService.GetEventTrxDatum(EventTrxDatum_name);

            if (EventTrxDatums == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventTrxDatum found for uci: {EventTrxDatum_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDatums);
        }

        [HttpPost]
        public async Task<ActionResult<EventTrxDatum>> AddEventTrxDatum(EventTrxDatum EventTrxDatum)
        {
            var dbEventTrxDatum = await _EventTrxDatumService.AddEventTrxDatum(EventTrxDatum);

            if (dbEventTrxDatum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventTrxDatum.TbEventTrxDatumName} could not be added."
                );
            }

            return CreatedAtAction("GetEventTrxDatum", new { uci = EventTrxDatum.TbEventTrxDatumName }, EventTrxDatum);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventTrxDatum(EventTrxDatum EventTrxDatum)
        {           
            EventTrxDatum dbEventTrxDatum = await _EventTrxDatumService.UpdateEventTrxDatum(EventTrxDatum);

            if (dbEventTrxDatum == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventTrxDatum.TbEventTrxDatumName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventTrxDatum(EventTrxDatum EventTrxDatum)
        {            
            (bool status, string message) = await _EventTrxDatumService.DeleteEventTrxDatum(EventTrxDatum);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxDatum);
        }
    }
}
