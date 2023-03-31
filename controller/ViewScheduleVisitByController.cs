using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleVisitByController : ControllerBase
    {
        private readonly IViewScheduleVisitByService _ViewScheduleVisitByService;

        public ViewScheduleVisitByController(IViewScheduleVisitByService ViewScheduleVisitByService)
        {
            _ViewScheduleVisitByService = ViewScheduleVisitByService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleVisitByList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleVisitBys = await _ViewScheduleVisitByService.GetViewScheduleVisitByListByValue(offset, limit, val);

            if (ViewScheduleVisitBys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleVisitBys in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleVisitBys);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleVisitByList(string ViewScheduleVisitBy_name)
        {
            var ViewScheduleVisitBys = await _ViewScheduleVisitByService.GetViewScheduleVisitByList(ViewScheduleVisitBy_name);

            if (ViewScheduleVisitBys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleVisitBy found for uci: {ViewScheduleVisitBy_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleVisitBys);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleVisitBy(string ViewScheduleVisitBy_name)
        {
            var ViewScheduleVisitBys = await _ViewScheduleVisitByService.GetViewScheduleVisitBy(ViewScheduleVisitBy_name);

            if (ViewScheduleVisitBys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleVisitBy found for uci: {ViewScheduleVisitBy_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleVisitBys);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleVisitBy>> AddViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy)
        {
            var dbViewScheduleVisitBy = await _ViewScheduleVisitByService.AddViewScheduleVisitBy(ViewScheduleVisitBy);

            if (dbViewScheduleVisitBy == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleVisitBy.TbViewScheduleVisitByName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleVisitBy", new { uci = ViewScheduleVisitBy.TbViewScheduleVisitByName }, ViewScheduleVisitBy);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy)
        {           
            ViewScheduleVisitBy dbViewScheduleVisitBy = await _ViewScheduleVisitByService.UpdateViewScheduleVisitBy(ViewScheduleVisitBy);

            if (dbViewScheduleVisitBy == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleVisitBy.TbViewScheduleVisitByName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleVisitBy(ViewScheduleVisitBy ViewScheduleVisitBy)
        {            
            (bool status, string message) = await _ViewScheduleVisitByService.DeleteViewScheduleVisitBy(ViewScheduleVisitBy);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleVisitBy);
        }
    }
}
