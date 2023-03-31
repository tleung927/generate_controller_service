using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventDefinitionController : ControllerBase
    {
        private readonly IEventDefinitionService _EventDefinitionService;

        public EventDefinitionController(IEventDefinitionService EventDefinitionService)
        {
            _EventDefinitionService = EventDefinitionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventDefinitionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventDefinitions = await _EventDefinitionService.GetEventDefinitionListByValue(offset, limit, val);

            if (EventDefinitions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventDefinitions in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventDefinitions);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventDefinitionList(string EventDefinition_name)
        {
            var EventDefinitions = await _EventDefinitionService.GetEventDefinitionList(EventDefinition_name);

            if (EventDefinitions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventDefinition found for uci: {EventDefinition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventDefinitions);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventDefinition(string EventDefinition_name)
        {
            var EventDefinitions = await _EventDefinitionService.GetEventDefinition(EventDefinition_name);

            if (EventDefinitions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventDefinition found for uci: {EventDefinition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventDefinitions);
        }

        [HttpPost]
        public async Task<ActionResult<EventDefinition>> AddEventDefinition(EventDefinition EventDefinition)
        {
            var dbEventDefinition = await _EventDefinitionService.AddEventDefinition(EventDefinition);

            if (dbEventDefinition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventDefinition.TbEventDefinitionName} could not be added."
                );
            }

            return CreatedAtAction("GetEventDefinition", new { uci = EventDefinition.TbEventDefinitionName }, EventDefinition);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventDefinition(EventDefinition EventDefinition)
        {           
            EventDefinition dbEventDefinition = await _EventDefinitionService.UpdateEventDefinition(EventDefinition);

            if (dbEventDefinition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventDefinition.TbEventDefinitionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventDefinition(EventDefinition EventDefinition)
        {            
            (bool status, string message) = await _EventDefinitionService.DeleteEventDefinition(EventDefinition);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventDefinition);
        }
    }
}
