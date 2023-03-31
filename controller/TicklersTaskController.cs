using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TicklersTaskController : ControllerBase
    {
        private readonly ITicklersTaskService _TicklersTaskService;

        public TicklersTaskController(ITicklersTaskService TicklersTaskService)
        {
            _TicklersTaskService = TicklersTaskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersTaskList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TicklersTasks = await _TicklersTaskService.GetTicklersTaskListByValue(offset, limit, val);

            if (TicklersTasks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TicklersTasks in database");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTasks);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersTaskList(string TicklersTask_name)
        {
            var TicklersTasks = await _TicklersTaskService.GetTicklersTaskList(TicklersTask_name);

            if (TicklersTasks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TicklersTask found for uci: {TicklersTask_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTasks);
        }

        [HttpGet]
        public async Task<IActionResult> GetTicklersTask(string TicklersTask_name)
        {
            var TicklersTasks = await _TicklersTaskService.GetTicklersTask(TicklersTask_name);

            if (TicklersTasks == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TicklersTask found for uci: {TicklersTask_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTasks);
        }

        [HttpPost]
        public async Task<ActionResult<TicklersTask>> AddTicklersTask(TicklersTask TicklersTask)
        {
            var dbTicklersTask = await _TicklersTaskService.AddTicklersTask(TicklersTask);

            if (dbTicklersTask == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TicklersTask.TbTicklersTaskName} could not be added."
                );
            }

            return CreatedAtAction("GetTicklersTask", new { uci = TicklersTask.TbTicklersTaskName }, TicklersTask);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTicklersTask(TicklersTask TicklersTask)
        {           
            TicklersTask dbTicklersTask = await _TicklersTaskService.UpdateTicklersTask(TicklersTask);

            if (dbTicklersTask == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TicklersTask.TbTicklersTaskName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTicklersTask(TicklersTask TicklersTask)
        {            
            (bool status, string message) = await _TicklersTaskService.DeleteTicklersTask(TicklersTask);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TicklersTask);
        }
    }
}
