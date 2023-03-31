using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OperLogController : ControllerBase
    {
        private readonly IOperLogService _OperLogService;

        public OperLogController(IOperLogService OperLogService)
        {
            _OperLogService = OperLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetOperLogList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var OperLogs = await _OperLogService.GetOperLogListByValue(offset, limit, val);

            if (OperLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No OperLogs in database");
            }

            return StatusCode(StatusCodes.Status200OK, OperLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperLogList(string OperLog_name)
        {
            var OperLogs = await _OperLogService.GetOperLogList(OperLog_name);

            if (OperLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No OperLog found for uci: {OperLog_name}");
            }

            return StatusCode(StatusCodes.Status200OK, OperLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetOperLog(string OperLog_name)
        {
            var OperLogs = await _OperLogService.GetOperLog(OperLog_name);

            if (OperLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No OperLog found for uci: {OperLog_name}");
            }

            return StatusCode(StatusCodes.Status200OK, OperLogs);
        }

        [HttpPost]
        public async Task<ActionResult<OperLog>> AddOperLog(OperLog OperLog)
        {
            var dbOperLog = await _OperLogService.AddOperLog(OperLog);

            if (dbOperLog == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{OperLog.TbOperLogName} could not be added."
                );
            }

            return CreatedAtAction("GetOperLog", new { uci = OperLog.TbOperLogName }, OperLog);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOperLog(OperLog OperLog)
        {           
            OperLog dbOperLog = await _OperLogService.UpdateOperLog(OperLog);

            if (dbOperLog == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{OperLog.TbOperLogName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOperLog(OperLog OperLog)
        {            
            (bool status, string message) = await _OperLogService.DeleteOperLog(OperLog);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, OperLog);
        }
    }
}
