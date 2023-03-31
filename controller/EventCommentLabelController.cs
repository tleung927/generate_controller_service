using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventCommentLabelController : ControllerBase
    {
        private readonly IEventCommentLabelService _EventCommentLabelService;

        public EventCommentLabelController(IEventCommentLabelService EventCommentLabelService)
        {
            _EventCommentLabelService = EventCommentLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventCommentLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventCommentLabels = await _EventCommentLabelService.GetEventCommentLabelListByValue(offset, limit, val);

            if (EventCommentLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventCommentLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventCommentLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventCommentLabelList(string EventCommentLabel_name)
        {
            var EventCommentLabels = await _EventCommentLabelService.GetEventCommentLabelList(EventCommentLabel_name);

            if (EventCommentLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventCommentLabel found for uci: {EventCommentLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventCommentLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventCommentLabel(string EventCommentLabel_name)
        {
            var EventCommentLabels = await _EventCommentLabelService.GetEventCommentLabel(EventCommentLabel_name);

            if (EventCommentLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventCommentLabel found for uci: {EventCommentLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventCommentLabels);
        }

        [HttpPost]
        public async Task<ActionResult<EventCommentLabel>> AddEventCommentLabel(EventCommentLabel EventCommentLabel)
        {
            var dbEventCommentLabel = await _EventCommentLabelService.AddEventCommentLabel(EventCommentLabel);

            if (dbEventCommentLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventCommentLabel.TbEventCommentLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetEventCommentLabel", new { uci = EventCommentLabel.TbEventCommentLabelName }, EventCommentLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventCommentLabel(EventCommentLabel EventCommentLabel)
        {           
            EventCommentLabel dbEventCommentLabel = await _EventCommentLabelService.UpdateEventCommentLabel(EventCommentLabel);

            if (dbEventCommentLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventCommentLabel.TbEventCommentLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventCommentLabel(EventCommentLabel EventCommentLabel)
        {            
            (bool status, string message) = await _EventCommentLabelService.DeleteEventCommentLabel(EventCommentLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventCommentLabel);
        }
    }
}
