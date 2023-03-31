using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerAllBackUpController : ControllerBase
    {
        private readonly IConsumerAllBackUpService _ConsumerAllBackUpService;

        public ConsumerAllBackUpController(IConsumerAllBackUpService ConsumerAllBackUpService)
        {
            _ConsumerAllBackUpService = ConsumerAllBackUpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAllBackUpList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerAllBackUps = await _ConsumerAllBackUpService.GetConsumerAllBackUpListByValue(offset, limit, val);

            if (ConsumerAllBackUps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerAllBackUps in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllBackUps);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAllBackUpList(string ConsumerAllBackUp_name)
        {
            var ConsumerAllBackUps = await _ConsumerAllBackUpService.GetConsumerAllBackUpList(ConsumerAllBackUp_name);

            if (ConsumerAllBackUps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerAllBackUp found for uci: {ConsumerAllBackUp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllBackUps);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerAllBackUp(string ConsumerAllBackUp_name)
        {
            var ConsumerAllBackUps = await _ConsumerAllBackUpService.GetConsumerAllBackUp(ConsumerAllBackUp_name);

            if (ConsumerAllBackUps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerAllBackUp found for uci: {ConsumerAllBackUp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllBackUps);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerAllBackUp>> AddConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp)
        {
            var dbConsumerAllBackUp = await _ConsumerAllBackUpService.AddConsumerAllBackUp(ConsumerAllBackUp);

            if (dbConsumerAllBackUp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerAllBackUp.TbConsumerAllBackUpName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerAllBackUp", new { uci = ConsumerAllBackUp.TbConsumerAllBackUpName }, ConsumerAllBackUp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp)
        {           
            ConsumerAllBackUp dbConsumerAllBackUp = await _ConsumerAllBackUpService.UpdateConsumerAllBackUp(ConsumerAllBackUp);

            if (dbConsumerAllBackUp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerAllBackUp.TbConsumerAllBackUpName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerAllBackUp(ConsumerAllBackUp ConsumerAllBackUp)
        {            
            (bool status, string message) = await _ConsumerAllBackUpService.DeleteConsumerAllBackUp(ConsumerAllBackUp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerAllBackUp);
        }
    }
}
