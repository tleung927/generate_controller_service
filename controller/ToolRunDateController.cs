using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ToolRunDateController : ControllerBase
    {
        private readonly IToolRunDateService _ToolRunDateService;

        public ToolRunDateController(IToolRunDateService ToolRunDateService)
        {
            _ToolRunDateService = ToolRunDateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetToolRunDateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ToolRunDates = await _ToolRunDateService.GetToolRunDateListByValue(offset, limit, val);

            if (ToolRunDates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ToolRunDates in database");
            }

            return StatusCode(StatusCodes.Status200OK, ToolRunDates);
        }

        [HttpGet]
        public async Task<IActionResult> GetToolRunDateList(string ToolRunDate_name)
        {
            var ToolRunDates = await _ToolRunDateService.GetToolRunDateList(ToolRunDate_name);

            if (ToolRunDates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ToolRunDate found for uci: {ToolRunDate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ToolRunDates);
        }

        [HttpGet]
        public async Task<IActionResult> GetToolRunDate(string ToolRunDate_name)
        {
            var ToolRunDates = await _ToolRunDateService.GetToolRunDate(ToolRunDate_name);

            if (ToolRunDates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ToolRunDate found for uci: {ToolRunDate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ToolRunDates);
        }

        [HttpPost]
        public async Task<ActionResult<ToolRunDate>> AddToolRunDate(ToolRunDate ToolRunDate)
        {
            var dbToolRunDate = await _ToolRunDateService.AddToolRunDate(ToolRunDate);

            if (dbToolRunDate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ToolRunDate.TbToolRunDateName} could not be added."
                );
            }

            return CreatedAtAction("GetToolRunDate", new { uci = ToolRunDate.TbToolRunDateName }, ToolRunDate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateToolRunDate(ToolRunDate ToolRunDate)
        {           
            ToolRunDate dbToolRunDate = await _ToolRunDateService.UpdateToolRunDate(ToolRunDate);

            if (dbToolRunDate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ToolRunDate.TbToolRunDateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToolRunDate(ToolRunDate ToolRunDate)
        {            
            (bool status, string message) = await _ToolRunDateService.DeleteToolRunDate(ToolRunDate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ToolRunDate);
        }
    }
}
