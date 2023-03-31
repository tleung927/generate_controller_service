using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerLocation012919Controller : ControllerBase
    {
        private readonly IConsumerLocation012919Service _ConsumerLocation012919Service;

        public ConsumerLocation012919Controller(IConsumerLocation012919Service ConsumerLocation012919Service)
        {
            _ConsumerLocation012919Service = ConsumerLocation012919Service;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocation012919List(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerLocation012919s = await _ConsumerLocation012919Service.GetConsumerLocation012919ListByValue(offset, limit, val);

            if (ConsumerLocation012919s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerLocation012919s in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocation012919s);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocation012919List(string ConsumerLocation012919_name)
        {
            var ConsumerLocation012919s = await _ConsumerLocation012919Service.GetConsumerLocation012919List(ConsumerLocation012919_name);

            if (ConsumerLocation012919s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLocation012919 found for uci: {ConsumerLocation012919_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocation012919s);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLocation012919(string ConsumerLocation012919_name)
        {
            var ConsumerLocation012919s = await _ConsumerLocation012919Service.GetConsumerLocation012919(ConsumerLocation012919_name);

            if (ConsumerLocation012919s == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLocation012919 found for uci: {ConsumerLocation012919_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocation012919s);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerLocation012919>> AddConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919)
        {
            var dbConsumerLocation012919 = await _ConsumerLocation012919Service.AddConsumerLocation012919(ConsumerLocation012919);

            if (dbConsumerLocation012919 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLocation012919.TbConsumerLocation012919Name} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerLocation012919", new { uci = ConsumerLocation012919.TbConsumerLocation012919Name }, ConsumerLocation012919);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919)
        {           
            ConsumerLocation012919 dbConsumerLocation012919 = await _ConsumerLocation012919Service.UpdateConsumerLocation012919(ConsumerLocation012919);

            if (dbConsumerLocation012919 == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLocation012919.TbConsumerLocation012919Name} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerLocation012919(ConsumerLocation012919 ConsumerLocation012919)
        {            
            (bool status, string message) = await _ConsumerLocation012919Service.DeleteConsumerLocation012919(ConsumerLocation012919);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLocation012919);
        }
    }
}
