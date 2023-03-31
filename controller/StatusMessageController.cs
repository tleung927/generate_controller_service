using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StatusMessageController : ControllerBase
    {
        private readonly IStatusMessageService _StatusMessageService;

        public StatusMessageController(IStatusMessageService StatusMessageService)
        {
            _StatusMessageService = StatusMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusMessageList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var StatusMessages = await _StatusMessageService.GetStatusMessageListByValue(offset, limit, val);

            if (StatusMessages == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No StatusMessages in database");
            }

            return StatusCode(StatusCodes.Status200OK, StatusMessages);
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusMessageList(string StatusMessage_name)
        {
            var StatusMessages = await _StatusMessageService.GetStatusMessageList(StatusMessage_name);

            if (StatusMessages == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No StatusMessage found for uci: {StatusMessage_name}");
            }

            return StatusCode(StatusCodes.Status200OK, StatusMessages);
        }

        [HttpGet]
        public async Task<IActionResult> GetStatusMessage(string StatusMessage_name)
        {
            var StatusMessages = await _StatusMessageService.GetStatusMessage(StatusMessage_name);

            if (StatusMessages == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No StatusMessage found for uci: {StatusMessage_name}");
            }

            return StatusCode(StatusCodes.Status200OK, StatusMessages);
        }

        [HttpPost]
        public async Task<ActionResult<StatusMessage>> AddStatusMessage(StatusMessage StatusMessage)
        {
            var dbStatusMessage = await _StatusMessageService.AddStatusMessage(StatusMessage);

            if (dbStatusMessage == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{StatusMessage.TbStatusMessageName} could not be added."
                );
            }

            return CreatedAtAction("GetStatusMessage", new { uci = StatusMessage.TbStatusMessageName }, StatusMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStatusMessage(StatusMessage StatusMessage)
        {           
            StatusMessage dbStatusMessage = await _StatusMessageService.UpdateStatusMessage(StatusMessage);

            if (dbStatusMessage == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{StatusMessage.TbStatusMessageName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteStatusMessage(StatusMessage StatusMessage)
        {            
            (bool status, string message) = await _StatusMessageService.DeleteStatusMessage(StatusMessage);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, StatusMessage);
        }
    }
}
