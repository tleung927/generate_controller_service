using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventTrxCommentController : ControllerBase
    {
        private readonly IEventTrxCommentService _EventTrxCommentService;

        public EventTrxCommentController(IEventTrxCommentService EventTrxCommentService)
        {
            _EventTrxCommentService = EventTrxCommentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxCommentList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var EventTrxComments = await _EventTrxCommentService.GetEventTrxCommentListByValue(offset, limit, val);

            if (EventTrxComments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No EventTrxComments in database");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxComments);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxCommentList(string EventTrxComment_name)
        {
            var EventTrxComments = await _EventTrxCommentService.GetEventTrxCommentList(EventTrxComment_name);

            if (EventTrxComments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventTrxComment found for uci: {EventTrxComment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxComments);
        }

        [HttpGet]
        public async Task<IActionResult> GetEventTrxComment(string EventTrxComment_name)
        {
            var EventTrxComments = await _EventTrxCommentService.GetEventTrxComment(EventTrxComment_name);

            if (EventTrxComments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No EventTrxComment found for uci: {EventTrxComment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxComments);
        }

        [HttpPost]
        public async Task<ActionResult<EventTrxComment>> AddEventTrxComment(EventTrxComment EventTrxComment)
        {
            var dbEventTrxComment = await _EventTrxCommentService.AddEventTrxComment(EventTrxComment);

            if (dbEventTrxComment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventTrxComment.TbEventTrxCommentName} could not be added."
                );
            }

            return CreatedAtAction("GetEventTrxComment", new { uci = EventTrxComment.TbEventTrxCommentName }, EventTrxComment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEventTrxComment(EventTrxComment EventTrxComment)
        {           
            EventTrxComment dbEventTrxComment = await _EventTrxCommentService.UpdateEventTrxComment(EventTrxComment);

            if (dbEventTrxComment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{EventTrxComment.TbEventTrxCommentName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEventTrxComment(EventTrxComment EventTrxComment)
        {            
            (bool status, string message) = await _EventTrxCommentService.DeleteEventTrxComment(EventTrxComment);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, EventTrxComment);
        }
    }
}
