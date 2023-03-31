using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerSdpController : ControllerBase
    {
        private readonly IConsumerSdpService _ConsumerSdpService;

        public ConsumerSdpController(IConsumerSdpService ConsumerSdpService)
        {
            _ConsumerSdpService = ConsumerSdpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerSdpList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerSdps = await _ConsumerSdpService.GetConsumerSdpListByValue(offset, limit, val);

            if (ConsumerSdps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerSdps in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerSdps);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerSdpList(string ConsumerSdp_name)
        {
            var ConsumerSdps = await _ConsumerSdpService.GetConsumerSdpList(ConsumerSdp_name);

            if (ConsumerSdps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerSdp found for uci: {ConsumerSdp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerSdps);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerSdp(string ConsumerSdp_name)
        {
            var ConsumerSdps = await _ConsumerSdpService.GetConsumerSdp(ConsumerSdp_name);

            if (ConsumerSdps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerSdp found for uci: {ConsumerSdp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerSdps);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerSdp>> AddConsumerSdp(ConsumerSdp ConsumerSdp)
        {
            var dbConsumerSdp = await _ConsumerSdpService.AddConsumerSdp(ConsumerSdp);

            if (dbConsumerSdp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerSdp.TbConsumerSdpName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerSdp", new { uci = ConsumerSdp.TbConsumerSdpName }, ConsumerSdp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerSdp(ConsumerSdp ConsumerSdp)
        {           
            ConsumerSdp dbConsumerSdp = await _ConsumerSdpService.UpdateConsumerSdp(ConsumerSdp);

            if (dbConsumerSdp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerSdp.TbConsumerSdpName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerSdp(ConsumerSdp ConsumerSdp)
        {            
            (bool status, string message) = await _ConsumerSdpService.DeleteConsumerSdp(ConsumerSdp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerSdp);
        }
    }
}
