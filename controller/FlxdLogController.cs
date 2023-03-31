using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FlxdLogController : ControllerBase
    {
        private readonly IFlxdLogService _FlxdLogService;

        public FlxdLogController(IFlxdLogService FlxdLogService)
        {
            _FlxdLogService = FlxdLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdLogList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FlxdLogs = await _FlxdLogService.GetFlxdLogListByValue(offset, limit, val);

            if (FlxdLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FlxdLogs in database");
            }

            return StatusCode(StatusCodes.Status200OK, FlxdLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdLogList(string FlxdLog_name)
        {
            var FlxdLogs = await _FlxdLogService.GetFlxdLogList(FlxdLog_name);

            if (FlxdLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FlxdLog found for uci: {FlxdLog_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FlxdLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFlxdLog(string FlxdLog_name)
        {
            var FlxdLogs = await _FlxdLogService.GetFlxdLog(FlxdLog_name);

            if (FlxdLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FlxdLog found for uci: {FlxdLog_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FlxdLogs);
        }

        [HttpPost]
        public async Task<ActionResult<FlxdLog>> AddFlxdLog(FlxdLog FlxdLog)
        {
            var dbFlxdLog = await _FlxdLogService.AddFlxdLog(FlxdLog);

            if (dbFlxdLog == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FlxdLog.TbFlxdLogName} could not be added."
                );
            }

            return CreatedAtAction("GetFlxdLog", new { uci = FlxdLog.TbFlxdLogName }, FlxdLog);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFlxdLog(FlxdLog FlxdLog)
        {           
            FlxdLog dbFlxdLog = await _FlxdLogService.UpdateFlxdLog(FlxdLog);

            if (dbFlxdLog == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FlxdLog.TbFlxdLogName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFlxdLog(FlxdLog FlxdLog)
        {            
            (bool status, string message) = await _FlxdLogService.DeleteFlxdLog(FlxdLog);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FlxdLog);
        }
    }
}
