using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerStatController : ControllerBase
    {
        private readonly IConsumerStatService _ConsumerStatService;

        public ConsumerStatController(IConsumerStatService ConsumerStatService)
        {
            _ConsumerStatService = ConsumerStatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerStatList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerStats = await _ConsumerStatService.GetConsumerStatListByValue(offset, limit, val);

            if (ConsumerStats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerStats in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStats);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerStatList(string ConsumerStat_name)
        {
            var ConsumerStats = await _ConsumerStatService.GetConsumerStatList(ConsumerStat_name);

            if (ConsumerStats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerStat found for uci: {ConsumerStat_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStats);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerStat(string ConsumerStat_name)
        {
            var ConsumerStats = await _ConsumerStatService.GetConsumerStat(ConsumerStat_name);

            if (ConsumerStats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerStat found for uci: {ConsumerStat_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStats);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerStat>> AddConsumerStat(ConsumerStat ConsumerStat)
        {
            var dbConsumerStat = await _ConsumerStatService.AddConsumerStat(ConsumerStat);

            if (dbConsumerStat == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerStat.TbConsumerStatName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerStat", new { uci = ConsumerStat.TbConsumerStatName }, ConsumerStat);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerStat(ConsumerStat ConsumerStat)
        {           
            ConsumerStat dbConsumerStat = await _ConsumerStatService.UpdateConsumerStat(ConsumerStat);

            if (dbConsumerStat == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerStat.TbConsumerStatName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerStat(ConsumerStat ConsumerStat)
        {            
            (bool status, string message) = await _ConsumerStatService.DeleteConsumerStat(ConsumerStat);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerStat);
        }
    }
}
