using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerLabeltipController : ControllerBase
    {
        private readonly IConsumerLabeltipService _ConsumerLabeltipService;

        public ConsumerLabeltipController(IConsumerLabeltipService ConsumerLabeltipService)
        {
            _ConsumerLabeltipService = ConsumerLabeltipService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabeltipList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerLabeltips = await _ConsumerLabeltipService.GetConsumerLabeltipListByValue(offset, limit, val);

            if (ConsumerLabeltips == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerLabeltips in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabeltips);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabeltipList(string ConsumerLabeltip_name)
        {
            var ConsumerLabeltips = await _ConsumerLabeltipService.GetConsumerLabeltipList(ConsumerLabeltip_name);

            if (ConsumerLabeltips == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLabeltip found for uci: {ConsumerLabeltip_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabeltips);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabeltip(string ConsumerLabeltip_name)
        {
            var ConsumerLabeltips = await _ConsumerLabeltipService.GetConsumerLabeltip(ConsumerLabeltip_name);

            if (ConsumerLabeltips == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLabeltip found for uci: {ConsumerLabeltip_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabeltips);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerLabeltip>> AddConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip)
        {
            var dbConsumerLabeltip = await _ConsumerLabeltipService.AddConsumerLabeltip(ConsumerLabeltip);

            if (dbConsumerLabeltip == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLabeltip.TbConsumerLabeltipName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerLabeltip", new { uci = ConsumerLabeltip.TbConsumerLabeltipName }, ConsumerLabeltip);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip)
        {           
            ConsumerLabeltip dbConsumerLabeltip = await _ConsumerLabeltipService.UpdateConsumerLabeltip(ConsumerLabeltip);

            if (dbConsumerLabeltip == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLabeltip.TbConsumerLabeltipName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerLabeltip(ConsumerLabeltip ConsumerLabeltip)
        {            
            (bool status, string message) = await _ConsumerLabeltipService.DeleteConsumerLabeltip(ConsumerLabeltip);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabeltip);
        }
    }
}
