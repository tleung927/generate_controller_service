using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventColumns2Controller : ControllerBase
    {
        private readonly IEventColumns2Service _EventColumns2Service;

        public EventColumns2Controller(IEventColumns2Service EventColumns2Service)
        {
            _EventColumns2Service = EventColumns2Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventColumns2List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventColumns2s = await _EventColumns2Service.GetEventColumns2ListByValue(offset, limit, val);

            if (EventColumns2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventColumns2s in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventColumns2List(string EventColumns2_name)
        {
            var EventColumns2s = await _EventColumns2Service.GetEventColumns2List(EventColumns2_name);

            if (EventColumns2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventColumns2 found for uci: {EventColumns2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns2s);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventColumns2(string EventColumns2_name)
        {
            var EventColumns2s = await _EventColumns2Service.GetEventColumns2(EventColumns2_name);

            if (EventColumns2s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventColumns2 found for uci: {EventColumns2_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns2s);
        }

        [HttpPost]
        public async Task<ActionResult<EventColumns2>> AddEventColumns2(EventColumns2 EventColumns2)
        {
            var dbEventColumns2 = await _EventColumns2Service.AddEventColumns2(EventColumns2);

            if (dbEventColumns2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventColumns2.TbEventColumns2Name} could not be added."
                );
            }

            return CreatedAtAction("GetEventColumns2", new { uci = EventColumns2.TbEventColumns2Name }, EventColumns2);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventColumns2(EventColumns2 EventColumns2)
        {           
            EventColumns2 dbEventColumns2 = await _EventColumns2Service.UpdateEventColumns2(EventColumns2);

            if (dbEventColumns2 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventColumns2.TbEventColumns2Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventColumns2(EventColumns2 EventColumns2)
        {            
            (bool status, string message) = await _EventColumns2Service.DeleteEventColumns2(EventColumns2);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventColumns2);
        }
    }
}
