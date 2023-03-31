using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedMessageController : ControllerBase
    {
        private readonly IDeletedMessageService _DeletedMessageService;

        public DeletedMessageController(IDeletedMessageService DeletedMessageService)
        {
            _DeletedMessageService = DeletedMessageService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedMessageList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedMessages = await _DeletedMessageService.GetDeletedMessageListByValue(offset, limit, val);

            if (DeletedMessages == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedMessages in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedMessages);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedMessageList(string DeletedMessage_name)
        {
            var DeletedMessages = await _DeletedMessageService.GetDeletedMessageList(DeletedMessage_name);

            if (DeletedMessages == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedMessage found for uci: {DeletedMessage_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedMessages);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedMessage(string DeletedMessage_name)
        {
            var DeletedMessages = await _DeletedMessageService.GetDeletedMessage(DeletedMessage_name);

            if (DeletedMessages == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedMessage found for uci: {DeletedMessage_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedMessages);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedMessage>> AddDeletedMessage(DeletedMessage DeletedMessage)
        {
            var dbDeletedMessage = await _DeletedMessageService.AddDeletedMessage(DeletedMessage);

            if (dbDeletedMessage == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedMessage.TbDeletedMessageName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedMessage", new { uci = DeletedMessage.TbDeletedMessageName }, DeletedMessage);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedMessage(DeletedMessage DeletedMessage)
        {           
            DeletedMessage dbDeletedMessage = await _DeletedMessageService.UpdateDeletedMessage(DeletedMessage);

            if (dbDeletedMessage == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedMessage.TbDeletedMessageName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedMessage(DeletedMessage DeletedMessage)
        {            
            (bool status, string message) = await _DeletedMessageService.DeleteDeletedMessage(DeletedMessage);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedMessage);
        }
    }
}
