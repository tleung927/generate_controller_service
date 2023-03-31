using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DeletedConsumerController : ControllerBase
    {
        private readonly IDeletedConsumerService _DeletedConsumerService;

        public DeletedConsumerController(IDeletedConsumerService DeletedConsumerService)
        {
            _DeletedConsumerService = DeletedConsumerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedConsumerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var DeletedConsumers = await _DeletedConsumerService.GetDeletedConsumerListByValue(offset, limit, val);

            if (DeletedConsumers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No DeletedConsumers in database");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedConsumers);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedConsumerList(string DeletedConsumer_name)
        {
            var DeletedConsumers = await _DeletedConsumerService.GetDeletedConsumerList(DeletedConsumer_name);

            if (DeletedConsumers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedConsumer found for uci: {DeletedConsumer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedConsumers);
        }

        [HttpGet]
        public async Task<IActionResult> GetDeletedConsumer(string DeletedConsumer_name)
        {
            var DeletedConsumers = await _DeletedConsumerService.GetDeletedConsumer(DeletedConsumer_name);

            if (DeletedConsumers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No DeletedConsumer found for uci: {DeletedConsumer_name}");
            }

            return StatusCode(StatusCodes.Status200OK, DeletedConsumers);
        }

        [HttpPost]
        public async Task<ActionResult<DeletedConsumer>> AddDeletedConsumer(DeletedConsumer DeletedConsumer)
        {
            var dbDeletedConsumer = await _DeletedConsumerService.AddDeletedConsumer(DeletedConsumer);

            if (dbDeletedConsumer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedConsumer.TbDeletedConsumerName} could not be added."
                );
            }

            return CreatedAtAction("GetDeletedConsumer", new { uci = DeletedConsumer.TbDeletedConsumerName }, DeletedConsumer);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeletedConsumer(DeletedConsumer DeletedConsumer)
        {           
            DeletedConsumer dbDeletedConsumer = await _DeletedConsumerService.UpdateDeletedConsumer(DeletedConsumer);

            if (dbDeletedConsumer == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{DeletedConsumer.TbDeletedConsumerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteDeletedConsumer(DeletedConsumer DeletedConsumer)
        {            
            (bool status, string message) = await _DeletedConsumerService.DeleteDeletedConsumer(DeletedConsumer);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, DeletedConsumer);
        }
    }
}
