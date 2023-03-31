using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleDispositionController : ControllerBase
    {
        private readonly IViewScheduleDispositionService _ViewScheduleDispositionService;

        public ViewScheduleDispositionController(IViewScheduleDispositionService ViewScheduleDispositionService)
        {
            _ViewScheduleDispositionService = ViewScheduleDispositionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleDispositionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleDispositions = await _ViewScheduleDispositionService.GetViewScheduleDispositionListByValue(offset, limit, val);

            if (ViewScheduleDispositions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleDispositions in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositions);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleDispositionList(string ViewScheduleDisposition_name)
        {
            var ViewScheduleDispositions = await _ViewScheduleDispositionService.GetViewScheduleDispositionList(ViewScheduleDisposition_name);

            if (ViewScheduleDispositions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleDisposition found for uci: {ViewScheduleDisposition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositions);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleDisposition(string ViewScheduleDisposition_name)
        {
            var ViewScheduleDispositions = await _ViewScheduleDispositionService.GetViewScheduleDisposition(ViewScheduleDisposition_name);

            if (ViewScheduleDispositions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleDisposition found for uci: {ViewScheduleDisposition_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositions);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleDisposition>> AddViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition)
        {
            var dbViewScheduleDisposition = await _ViewScheduleDispositionService.AddViewScheduleDisposition(ViewScheduleDisposition);

            if (dbViewScheduleDisposition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleDisposition.TbViewScheduleDispositionName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleDisposition", new { uci = ViewScheduleDisposition.TbViewScheduleDispositionName }, ViewScheduleDisposition);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition)
        {           
            ViewScheduleDisposition dbViewScheduleDisposition = await _ViewScheduleDispositionService.UpdateViewScheduleDisposition(ViewScheduleDisposition);

            if (dbViewScheduleDisposition == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleDisposition.TbViewScheduleDispositionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleDisposition(ViewScheduleDisposition ViewScheduleDisposition)
        {            
            (bool status, string message) = await _ViewScheduleDispositionService.DeleteViewScheduleDisposition(ViewScheduleDisposition);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDisposition);
        }
    }
}
