using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerController : ControllerBase
    {
        private readonly IConsumerService _ConsumerService;

        public ConsumerController(IConsumerService ConsumerService)
        {
            _ConsumerService = ConsumerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Consumers = await _ConsumerService.GetConsumerListByValue(offset, limit, val);

            if (Consumers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Consumers in database");
            }

            return StatusCode(StatusCodes.Status200OK, Consumers);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerList(string Consumer_name)
        {
            var Consumers = await _ConsumerService.GetConsumerList(Consumer_name);

            if (Consumers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Consumer found for uci: {Consumer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Consumers);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumer(string Consumer_name)
        {
            var Consumers = await _ConsumerService.GetConsumer(Consumer_name);

            if (Consumers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Consumer found for uci: {Consumer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Consumers);
        }

        [HttpPost]
        public async Task<ActionResult<Consumer>> AddConsumer(Consumer Consumer)
        {
            var dbConsumer = await _ConsumerService.AddConsumer(Consumer);

            if (dbConsumer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Consumer.TbConsumerName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumer", new { uci = Consumer.TbConsumerName }, Consumer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumer(Consumer Consumer)
        {           
            Consumer dbConsumer = await _ConsumerService.UpdateConsumer(Consumer);

            if (dbConsumer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Consumer.TbConsumerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumer(Consumer Consumer)
        {            
            (bool status, string message) = await _ConsumerService.DeleteConsumer(Consumer);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Consumer);
        }
    }
}
