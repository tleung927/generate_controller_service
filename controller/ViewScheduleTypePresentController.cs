using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleTypePresentController : ControllerBase
    {
        private readonly IViewScheduleTypePresentService _ViewScheduleTypePresentService;

        public ViewScheduleTypePresentController(IViewScheduleTypePresentService ViewScheduleTypePresentService)
        {
            _ViewScheduleTypePresentService = ViewScheduleTypePresentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleTypePresentList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleTypePresents = await _ViewScheduleTypePresentService.GetViewScheduleTypePresentListByValue(offset, limit, val);

            if (ViewScheduleTypePresents == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleTypePresents in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypePresents);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleTypePresentList(string ViewScheduleTypePresent_name)
        {
            var ViewScheduleTypePresents = await _ViewScheduleTypePresentService.GetViewScheduleTypePresentList(ViewScheduleTypePresent_name);

            if (ViewScheduleTypePresents == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleTypePresent found for uci: {ViewScheduleTypePresent_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypePresents);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleTypePresent(string ViewScheduleTypePresent_name)
        {
            var ViewScheduleTypePresents = await _ViewScheduleTypePresentService.GetViewScheduleTypePresent(ViewScheduleTypePresent_name);

            if (ViewScheduleTypePresents == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleTypePresent found for uci: {ViewScheduleTypePresent_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypePresents);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleTypePresent>> AddViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent)
        {
            var dbViewScheduleTypePresent = await _ViewScheduleTypePresentService.AddViewScheduleTypePresent(ViewScheduleTypePresent);

            if (dbViewScheduleTypePresent == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleTypePresent.TbViewScheduleTypePresentName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleTypePresent", new { uci = ViewScheduleTypePresent.TbViewScheduleTypePresentName }, ViewScheduleTypePresent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent)
        {           
            ViewScheduleTypePresent dbViewScheduleTypePresent = await _ViewScheduleTypePresentService.UpdateViewScheduleTypePresent(ViewScheduleTypePresent);

            if (dbViewScheduleTypePresent == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleTypePresent.TbViewScheduleTypePresentName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleTypePresent(ViewScheduleTypePresent ViewScheduleTypePresent)
        {            
            (bool status, string message) = await _ViewScheduleTypePresentService.DeleteViewScheduleTypePresent(ViewScheduleTypePresent);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypePresent);
        }
    }
}
