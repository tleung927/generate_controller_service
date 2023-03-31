using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerStatsDeleteController : ControllerBase
    {
        private readonly IConsumerStatsDeleteService _ConsumerStatsDeleteService;

        public ConsumerStatsDeleteController(IConsumerStatsDeleteService ConsumerStatsDeleteService)
        {
            _ConsumerStatsDeleteService = ConsumerStatsDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerStatsDeleteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerStatsDeletes = await _ConsumerStatsDeleteService.GetConsumerStatsDeleteListByValue(offset, limit, val);

            if (ConsumerStatsDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerStatsDeletes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStatsDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerStatsDeleteList(string ConsumerStatsDelete_name)
        {
            var ConsumerStatsDeletes = await _ConsumerStatsDeleteService.GetConsumerStatsDeleteList(ConsumerStatsDelete_name);

            if (ConsumerStatsDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerStatsDelete found for uci: {ConsumerStatsDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStatsDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerStatsDelete(string ConsumerStatsDelete_name)
        {
            var ConsumerStatsDeletes = await _ConsumerStatsDeleteService.GetConsumerStatsDelete(ConsumerStatsDelete_name);

            if (ConsumerStatsDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerStatsDelete found for uci: {ConsumerStatsDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStatsDeletes);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerStatsDelete>> AddConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete)
        {
            var dbConsumerStatsDelete = await _ConsumerStatsDeleteService.AddConsumerStatsDelete(ConsumerStatsDelete);

            if (dbConsumerStatsDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerStatsDelete.TbConsumerStatsDeleteName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerStatsDelete", new { uci = ConsumerStatsDelete.TbConsumerStatsDeleteName }, ConsumerStatsDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete)
        {           
            ConsumerStatsDelete dbConsumerStatsDelete = await _ConsumerStatsDeleteService.UpdateConsumerStatsDelete(ConsumerStatsDelete);

            if (dbConsumerStatsDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerStatsDelete.TbConsumerStatsDeleteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerStatsDelete(ConsumerStatsDelete ConsumerStatsDelete)
        {            
            (bool status, string message) = await _ConsumerStatsDeleteService.DeleteConsumerStatsDelete(ConsumerStatsDelete);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStatsDelete);
        }
    }
}
