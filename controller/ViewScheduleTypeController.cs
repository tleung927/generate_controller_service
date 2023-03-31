using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleTypeController : ControllerBase
    {
        private readonly IViewScheduleTypeService _ViewScheduleTypeService;

        public ViewScheduleTypeController(IViewScheduleTypeService ViewScheduleTypeService)
        {
            _ViewScheduleTypeService = ViewScheduleTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleTypeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleTypes = await _ViewScheduleTypeService.GetViewScheduleTypeListByValue(offset, limit, val);

            if (ViewScheduleTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleTypes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleTypeList(string ViewScheduleType_name)
        {
            var ViewScheduleTypes = await _ViewScheduleTypeService.GetViewScheduleTypeList(ViewScheduleType_name);

            if (ViewScheduleTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleType found for uci: {ViewScheduleType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleType(string ViewScheduleType_name)
        {
            var ViewScheduleTypes = await _ViewScheduleTypeService.GetViewScheduleType(ViewScheduleType_name);

            if (ViewScheduleTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleType found for uci: {ViewScheduleType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleTypes);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleType>> AddViewScheduleType(ViewScheduleType ViewScheduleType)
        {
            var dbViewScheduleType = await _ViewScheduleTypeService.AddViewScheduleType(ViewScheduleType);

            if (dbViewScheduleType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleType.TbViewScheduleTypeName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleType", new { uci = ViewScheduleType.TbViewScheduleTypeName }, ViewScheduleType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleType(ViewScheduleType ViewScheduleType)
        {           
            ViewScheduleType dbViewScheduleType = await _ViewScheduleTypeService.UpdateViewScheduleType(ViewScheduleType);

            if (dbViewScheduleType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleType.TbViewScheduleTypeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleType(ViewScheduleType ViewScheduleType)
        {            
            (bool status, string message) = await _ViewScheduleTypeService.DeleteViewScheduleType(ViewScheduleType);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleType);
        }
    }
}
