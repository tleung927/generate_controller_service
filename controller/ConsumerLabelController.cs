using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConsumerLabelController : ControllerBase
    {
        private readonly IConsumerLabelService _ConsumerLabelService;

        public ConsumerLabelController(IConsumerLabelService ConsumerLabelService)
        {
            _ConsumerLabelService = ConsumerLabelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ConsumerLabels = await _ConsumerLabelService.GetConsumerLabelListByValue(offset, limit, val);

            if (ConsumerLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ConsumerLabels in database");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabelList(string ConsumerLabel_name)
        {
            var ConsumerLabels = await _ConsumerLabelService.GetConsumerLabelList(ConsumerLabel_name);

            if (ConsumerLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLabel found for uci: {ConsumerLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabels);
        }

        [HttpGet]
        public async Task<IActionResult> GetConsumerLabel(string ConsumerLabel_name)
        {
            var ConsumerLabels = await _ConsumerLabelService.GetConsumerLabel(ConsumerLabel_name);

            if (ConsumerLabels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ConsumerLabel found for uci: {ConsumerLabel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabels);
        }

        [HttpPost]
        public async Task<ActionResult<ConsumerLabel>> AddConsumerLabel(ConsumerLabel ConsumerLabel)
        {
            var dbConsumerLabel = await _ConsumerLabelService.AddConsumerLabel(ConsumerLabel);

            if (dbConsumerLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLabel.TbConsumerLabelName} could not be added."
                );
            }

            return CreatedAtAction("GetConsumerLabel", new { uci = ConsumerLabel.TbConsumerLabelName }, ConsumerLabel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateConsumerLabel(ConsumerLabel ConsumerLabel)
        {           
            ConsumerLabel dbConsumerLabel = await _ConsumerLabelService.UpdateConsumerLabel(ConsumerLabel);

            if (dbConsumerLabel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ConsumerLabel.TbConsumerLabelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteConsumerLabel(ConsumerLabel ConsumerLabel)
        {            
            (bool status, string message) = await _ConsumerLabelService.DeleteConsumerLabel(ConsumerLabel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ConsumerLabel);
        }
    }
}
