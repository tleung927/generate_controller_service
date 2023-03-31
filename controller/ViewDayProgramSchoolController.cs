using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewDayProgramSchoolController : ControllerBase
    {
        private readonly IViewDayProgramSchoolService _ViewDayProgramSchoolService;

        public ViewDayProgramSchoolController(IViewDayProgramSchoolService ViewDayProgramSchoolService)
        {
            _ViewDayProgramSchoolService = ViewDayProgramSchoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewDayProgramSchoolList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewDayProgramSchools = await _ViewDayProgramSchoolService.GetViewDayProgramSchoolListByValue(offset, limit, val);

            if (ViewDayProgramSchools == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewDayProgramSchools in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewDayProgramSchools);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewDayProgramSchoolList(string ViewDayProgramSchool_name)
        {
            var ViewDayProgramSchools = await _ViewDayProgramSchoolService.GetViewDayProgramSchoolList(ViewDayProgramSchool_name);

            if (ViewDayProgramSchools == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewDayProgramSchool found for uci: {ViewDayProgramSchool_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewDayProgramSchools);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewDayProgramSchool(string ViewDayProgramSchool_name)
        {
            var ViewDayProgramSchools = await _ViewDayProgramSchoolService.GetViewDayProgramSchool(ViewDayProgramSchool_name);

            if (ViewDayProgramSchools == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewDayProgramSchool found for uci: {ViewDayProgramSchool_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewDayProgramSchools);
        }

        [HttpPost]
        public async Task<ActionResult<ViewDayProgramSchool>> AddViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool)
        {
            var dbViewDayProgramSchool = await _ViewDayProgramSchoolService.AddViewDayProgramSchool(ViewDayProgramSchool);

            if (dbViewDayProgramSchool == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewDayProgramSchool.TbViewDayProgramSchoolName} could not be added."
                );
            }

            return CreatedAtAction("GetViewDayProgramSchool", new { uci = ViewDayProgramSchool.TbViewDayProgramSchoolName }, ViewDayProgramSchool);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool)
        {           
            ViewDayProgramSchool dbViewDayProgramSchool = await _ViewDayProgramSchoolService.UpdateViewDayProgramSchool(ViewDayProgramSchool);

            if (dbViewDayProgramSchool == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewDayProgramSchool.TbViewDayProgramSchoolName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewDayProgramSchool(ViewDayProgramSchool ViewDayProgramSchool)
        {            
            (bool status, string message) = await _ViewDayProgramSchoolService.DeleteViewDayProgramSchool(ViewDayProgramSchool);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewDayProgramSchool);
        }
    }
}
