using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerCopayController : ControllerBase
    {
        private readonly IConsumerCopayService _ConsumerCopayService;

        public ConsumerCopayController(IConsumerCopayService ConsumerCopayService)
        {
            _ConsumerCopayService = ConsumerCopayService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerCopayList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerCopays = await _ConsumerCopayService.GetConsumerCopayListByValue(offset, limit, val);

            if (ConsumerCopays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerCopays in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerCopays);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerCopayList(string ConsumerCopay_name)
        {
            var ConsumerCopays = await _ConsumerCopayService.GetConsumerCopayList(ConsumerCopay_name);

            if (ConsumerCopays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerCopay found for uci: {ConsumerCopay_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerCopays);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerCopay(string ConsumerCopay_name)
        {
            var ConsumerCopays = await _ConsumerCopayService.GetConsumerCopay(ConsumerCopay_name);

            if (ConsumerCopays == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerCopay found for uci: {ConsumerCopay_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerCopays);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerCopay>> AddConsumerCopay(ConsumerCopay ConsumerCopay)
        {
            var dbConsumerCopay = await _ConsumerCopayService.AddConsumerCopay(ConsumerCopay);

            if (dbConsumerCopay == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerCopay.TbConsumerCopayName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerCopay", new { uci = ConsumerCopay.TbConsumerCopayName }, ConsumerCopay);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerCopay(ConsumerCopay ConsumerCopay)
        {           
            ConsumerCopay dbConsumerCopay = await _ConsumerCopayService.UpdateConsumerCopay(ConsumerCopay);

            if (dbConsumerCopay == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerCopay.TbConsumerCopayName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerCopay(ConsumerCopay ConsumerCopay)
        {            
            (bool status, string message) = await _ConsumerCopayService.DeleteConsumerCopay(ConsumerCopay);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerCopay);
        }
    }
}
