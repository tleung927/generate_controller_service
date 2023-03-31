using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerAddController : ControllerBase
    {
        private readonly IConsumerAddService _ConsumerAddService;

        public ConsumerAddController(IConsumerAddService ConsumerAddService)
        {
            _ConsumerAddService = ConsumerAddService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAddList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerAdds = await _ConsumerAddService.GetConsumerAddListByValue(offset, limit, val);

            if (ConsumerAdds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerAdds in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAdds);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAddList(string ConsumerAdd_name)
        {
            var ConsumerAdds = await _ConsumerAddService.GetConsumerAddList(ConsumerAdd_name);

            if (ConsumerAdds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerAdd found for uci: {ConsumerAdd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAdds);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAdd(string ConsumerAdd_name)
        {
            var ConsumerAdds = await _ConsumerAddService.GetConsumerAdd(ConsumerAdd_name);

            if (ConsumerAdds == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerAdd found for uci: {ConsumerAdd_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAdds);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerAdd>> AddConsumerAdd(ConsumerAdd ConsumerAdd)
        {
            var dbConsumerAdd = await _ConsumerAddService.AddConsumerAdd(ConsumerAdd);

            if (dbConsumerAdd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerAdd.TbConsumerAddName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerAdd", new { uci = ConsumerAdd.TbConsumerAddName }, ConsumerAdd);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerAdd(ConsumerAdd ConsumerAdd)
        {           
            ConsumerAdd dbConsumerAdd = await _ConsumerAddService.UpdateConsumerAdd(ConsumerAdd);

            if (dbConsumerAdd == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerAdd.TbConsumerAddName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerAdd(ConsumerAdd ConsumerAdd)
        {            
            (bool status, string message) = await _ConsumerAddService.DeleteConsumerAdd(ConsumerAdd);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAdd);
        }
    }
}
