using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerHistoryController : ControllerBase
    {
        private readonly IConsumerHistoryService _ConsumerHistoryService;

        public ConsumerHistoryController(IConsumerHistoryService ConsumerHistoryService)
        {
            _ConsumerHistoryService = ConsumerHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerHistoryList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerHistorys = await _ConsumerHistoryService.GetConsumerHistoryListByValue(offset, limit, val);

            if (ConsumerHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerHistorys in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerHistorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerHistoryList(string ConsumerHistory_name)
        {
            var ConsumerHistorys = await _ConsumerHistoryService.GetConsumerHistoryList(ConsumerHistory_name);

            if (ConsumerHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerHistory found for uci: {ConsumerHistory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerHistorys);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerHistory(string ConsumerHistory_name)
        {
            var ConsumerHistorys = await _ConsumerHistoryService.GetConsumerHistory(ConsumerHistory_name);

            if (ConsumerHistorys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerHistory found for uci: {ConsumerHistory_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerHistorys);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerHistory>> AddConsumerHistory(ConsumerHistory ConsumerHistory)
        {
            var dbConsumerHistory = await _ConsumerHistoryService.AddConsumerHistory(ConsumerHistory);

            if (dbConsumerHistory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerHistory.TbConsumerHistoryName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerHistory", new { uci = ConsumerHistory.TbConsumerHistoryName }, ConsumerHistory);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerHistory(ConsumerHistory ConsumerHistory)
        {           
            ConsumerHistory dbConsumerHistory = await _ConsumerHistoryService.UpdateConsumerHistory(ConsumerHistory);

            if (dbConsumerHistory == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerHistory.TbConsumerHistoryName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerHistory(ConsumerHistory ConsumerHistory)
        {            
            (bool status, string message) = await _ConsumerHistoryService.DeleteConsumerHistory(ConsumerHistory);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerHistory);
        }
    }
}
