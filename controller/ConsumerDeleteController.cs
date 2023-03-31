using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerDeleteController : ControllerBase
    {
        private readonly IConsumerDeleteService _ConsumerDeleteService;

        public ConsumerDeleteController(IConsumerDeleteService ConsumerDeleteService)
        {
            _ConsumerDeleteService = ConsumerDeleteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerDeleteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerDeletes = await _ConsumerDeleteService.GetConsumerDeleteListByValue(offset, limit, val);

            if (ConsumerDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerDeletes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerDeleteList(string ConsumerDelete_name)
        {
            var ConsumerDeletes = await _ConsumerDeleteService.GetConsumerDeleteList(ConsumerDelete_name);

            if (ConsumerDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerDelete found for uci: {ConsumerDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerDeletes);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerDelete(string ConsumerDelete_name)
        {
            var ConsumerDeletes = await _ConsumerDeleteService.GetConsumerDelete(ConsumerDelete_name);

            if (ConsumerDeletes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerDelete found for uci: {ConsumerDelete_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerDeletes);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerDelete>> AddConsumerDelete(ConsumerDelete ConsumerDelete)
        {
            var dbConsumerDelete = await _ConsumerDeleteService.AddConsumerDelete(ConsumerDelete);

            if (dbConsumerDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerDelete.TbConsumerDeleteName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerDelete", new { uci = ConsumerDelete.TbConsumerDeleteName }, ConsumerDelete);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerDelete(ConsumerDelete ConsumerDelete)
        {           
            ConsumerDelete dbConsumerDelete = await _ConsumerDeleteService.UpdateConsumerDelete(ConsumerDelete);

            if (dbConsumerDelete == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerDelete.TbConsumerDeleteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerDelete(ConsumerDelete ConsumerDelete)
        {            
            (bool status, string message) = await _ConsumerDeleteService.DeleteConsumerDelete(ConsumerDelete);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerDelete);
        }
    }
}
