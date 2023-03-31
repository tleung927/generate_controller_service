using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerLocController : ControllerBase
    {
        private readonly IConsumerLocService _ConsumerLocService;

        public ConsumerLocController(IConsumerLocService ConsumerLocService)
        {
            _ConsumerLocService = ConsumerLocService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerLocs = await _ConsumerLocService.GetConsumerLocListByValue(offset, limit, val);

            if (ConsumerLocs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerLocs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocs);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocList(string ConsumerLoc_name)
        {
            var ConsumerLocs = await _ConsumerLocService.GetConsumerLocList(ConsumerLoc_name);

            if (ConsumerLocs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLoc found for uci: {ConsumerLoc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocs);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLoc(string ConsumerLoc_name)
        {
            var ConsumerLocs = await _ConsumerLocService.GetConsumerLoc(ConsumerLoc_name);

            if (ConsumerLocs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLoc found for uci: {ConsumerLoc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocs);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerLoc>> AddConsumerLoc(ConsumerLoc ConsumerLoc)
        {
            var dbConsumerLoc = await _ConsumerLocService.AddConsumerLoc(ConsumerLoc);

            if (dbConsumerLoc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLoc.TbConsumerLocName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerLoc", new { uci = ConsumerLoc.TbConsumerLocName }, ConsumerLoc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerLoc(ConsumerLoc ConsumerLoc)
        {           
            ConsumerLoc dbConsumerLoc = await _ConsumerLocService.UpdateConsumerLoc(ConsumerLoc);

            if (dbConsumerLoc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLoc.TbConsumerLocName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerLoc(ConsumerLoc ConsumerLoc)
        {            
            (bool status, string message) = await _ConsumerLocService.DeleteConsumerLoc(ConsumerLoc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLoc);
        }
    }
}
