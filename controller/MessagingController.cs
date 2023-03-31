using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MessagingController : ControllerBase
    {
        private readonly IMessagingService _MessagingService;

        public MessagingController(IMessagingService MessagingService)
        {
            _MessagingService = MessagingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagingList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Messagings = await _MessagingService.GetMessagingListByValue(offset, limit, val);

            if (Messagings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Messagings in database");
            }

            return StatusCode(StatusCodes.Status200OK, Messagings);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessagingList(string Messaging_name)
        {
            var Messagings = await _MessagingService.GetMessagingList(Messaging_name);

            if (Messagings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Messaging found for uci: {Messaging_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Messagings);
        }

        [HttpGet]
        public async Task<IActionResult> GetMessaging(string Messaging_name)
        {
            var Messagings = await _MessagingService.GetMessaging(Messaging_name);

            if (Messagings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Messaging found for uci: {Messaging_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Messagings);
        }

        [HttpPost]
        public async Task<ActionResult<Messaging>> AddMessaging(Messaging Messaging)
        {
            var dbMessaging = await _MessagingService.AddMessaging(Messaging);

            if (dbMessaging == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Messaging.TbMessagingName} could not be added."
                );
            }

            return CreatedAtAction("GetMessaging", new { uci = Messaging.TbMessagingName }, Messaging);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMessaging(Messaging Messaging)
        {           
            Messaging dbMessaging = await _MessagingService.UpdateMessaging(Messaging);

            if (dbMessaging == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Messaging.TbMessagingName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMessaging(Messaging Messaging)
        {            
            (bool status, string message) = await _MessagingService.DeleteMessaging(Messaging);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Messaging);
        }
    }
}
