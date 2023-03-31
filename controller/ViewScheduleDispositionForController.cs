using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleDispositionForController : ControllerBase
    {
        private readonly IViewScheduleDispositionForService _ViewScheduleDispositionForService;

        public ViewScheduleDispositionForController(IViewScheduleDispositionForService ViewScheduleDispositionForService)
        {
            _ViewScheduleDispositionForService = ViewScheduleDispositionForService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleDispositionForList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleDispositionFors = await _ViewScheduleDispositionForService.GetViewScheduleDispositionForListByValue(offset, limit, val);

            if (ViewScheduleDispositionFors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleDispositionFors in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositionFors);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleDispositionForList(string ViewScheduleDispositionFor_name)
        {
            var ViewScheduleDispositionFors = await _ViewScheduleDispositionForService.GetViewScheduleDispositionForList(ViewScheduleDispositionFor_name);

            if (ViewScheduleDispositionFors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleDispositionFor found for uci: {ViewScheduleDispositionFor_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositionFors);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleDispositionFor(string ViewScheduleDispositionFor_name)
        {
            var ViewScheduleDispositionFors = await _ViewScheduleDispositionForService.GetViewScheduleDispositionFor(ViewScheduleDispositionFor_name);

            if (ViewScheduleDispositionFors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleDispositionFor found for uci: {ViewScheduleDispositionFor_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositionFors);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleDispositionFor>> AddViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor)
        {
            var dbViewScheduleDispositionFor = await _ViewScheduleDispositionForService.AddViewScheduleDispositionFor(ViewScheduleDispositionFor);

            if (dbViewScheduleDispositionFor == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleDispositionFor.TbViewScheduleDispositionForName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleDispositionFor", new { uci = ViewScheduleDispositionFor.TbViewScheduleDispositionForName }, ViewScheduleDispositionFor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor)
        {           
            ViewScheduleDispositionFor dbViewScheduleDispositionFor = await _ViewScheduleDispositionForService.UpdateViewScheduleDispositionFor(ViewScheduleDispositionFor);

            if (dbViewScheduleDispositionFor == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleDispositionFor.TbViewScheduleDispositionForName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleDispositionFor(ViewScheduleDispositionFor ViewScheduleDispositionFor)
        {            
            (bool status, string message) = await _ViewScheduleDispositionForService.DeleteViewScheduleDispositionFor(ViewScheduleDispositionFor);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleDispositionFor);
        }
    }
}
