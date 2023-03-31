using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormTransCaseLogController : ControllerBase
    {
        private readonly IFormTransCaseLogService _FormTransCaseLogService;

        public FormTransCaseLogController(IFormTransCaseLogService FormTransCaseLogService)
        {
            _FormTransCaseLogService = FormTransCaseLogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransCaseLogList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormTransCaseLogs = await _FormTransCaseLogService.GetFormTransCaseLogListByValue(offset, limit, val);

            if (FormTransCaseLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormTransCaseLogs in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransCaseLogList(string FormTransCaseLog_name)
        {
            var FormTransCaseLogs = await _FormTransCaseLogService.GetFormTransCaseLogList(FormTransCaseLog_name);

            if (FormTransCaseLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormTransCaseLog found for uci: {FormTransCaseLog_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogs);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransCaseLog(string FormTransCaseLog_name)
        {
            var FormTransCaseLogs = await _FormTransCaseLogService.GetFormTransCaseLog(FormTransCaseLog_name);

            if (FormTransCaseLogs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormTransCaseLog found for uci: {FormTransCaseLog_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogs);
        }

        [HttpPost]
        public async Task<ActionResult<FormTransCaseLog>> AddFormTransCaseLog(FormTransCaseLog FormTransCaseLog)
        {
            var dbFormTransCaseLog = await _FormTransCaseLogService.AddFormTransCaseLog(FormTransCaseLog);

            if (dbFormTransCaseLog == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormTransCaseLog.TbFormTransCaseLogName} could not be added."
                );
            }

            return CreatedAtAction("GetFormTransCaseLog", new { uci = FormTransCaseLog.TbFormTransCaseLogName }, FormTransCaseLog);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormTransCaseLog(FormTransCaseLog FormTransCaseLog)
        {           
            FormTransCaseLog dbFormTransCaseLog = await _FormTransCaseLogService.UpdateFormTransCaseLog(FormTransCaseLog);

            if (dbFormTransCaseLog == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormTransCaseLog.TbFormTransCaseLogName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormTransCaseLog(FormTransCaseLog FormTransCaseLog)
        {            
            (bool status, string message) = await _FormTransCaseLogService.DeleteFormTransCaseLog(FormTransCaseLog);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLog);
        }
    }
}
