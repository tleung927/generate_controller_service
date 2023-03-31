using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerAllDeleteController : ControllerBase
    {
        private readonly IConsumerAllDeleteService _ConsumerAllDeleteService;

        public ConsumerAllDeleteController(IConsumerAllDeleteService ConsumerAllDeleteService)
        {
            _ConsumerAllDeleteService = ConsumerAllDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAllDeleteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerAllDeletes = await _ConsumerAllDeleteService.GetConsumerAllDeleteListByValue(offset, limit, val);

            if (ConsumerAllDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerAllDeletes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAllDeleteList(string ConsumerAllDelete_name)
        {
            var ConsumerAllDeletes = await _ConsumerAllDeleteService.GetConsumerAllDeleteList(ConsumerAllDelete_name);

            if (ConsumerAllDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerAllDelete found for uci: {ConsumerAllDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAllDelete(string ConsumerAllDelete_name)
        {
            var ConsumerAllDeletes = await _ConsumerAllDeleteService.GetConsumerAllDelete(ConsumerAllDelete_name);

            if (ConsumerAllDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerAllDelete found for uci: {ConsumerAllDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllDeletes);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerAllDelete>> AddConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete)
        {
            var dbConsumerAllDelete = await _ConsumerAllDeleteService.AddConsumerAllDelete(ConsumerAllDelete);

            if (dbConsumerAllDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerAllDelete.TbConsumerAllDeleteName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerAllDelete", new { uci = ConsumerAllDelete.TbConsumerAllDeleteName }, ConsumerAllDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete)
        {           
            ConsumerAllDelete dbConsumerAllDelete = await _ConsumerAllDeleteService.UpdateConsumerAllDelete(ConsumerAllDelete);

            if (dbConsumerAllDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerAllDelete.TbConsumerAllDeleteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerAllDelete(ConsumerAllDelete ConsumerAllDelete)
        {            
            (bool status, string message) = await _ConsumerAllDeleteService.DeleteConsumerAllDelete(ConsumerAllDelete);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllDelete);
        }
    }
}
