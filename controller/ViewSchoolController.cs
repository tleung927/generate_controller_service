using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewSchoolController : ControllerBase
    {
        private readonly IViewSchoolService _ViewSchoolService;

        public ViewSchoolController(IViewSchoolService ViewSchoolService)
        {
            _ViewSchoolService = ViewSchoolService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSchoolList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewSchools = await _ViewSchoolService.GetViewSchoolListByValue(offset, limit, val);

            if (ViewSchools == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewSchools in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSchools);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSchoolList(string ViewSchool_name)
        {
            var ViewSchools = await _ViewSchoolService.GetViewSchoolList(ViewSchool_name);

            if (ViewSchools == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewSchool found for uci: {ViewSchool_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSchools);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSchool(string ViewSchool_name)
        {
            var ViewSchools = await _ViewSchoolService.GetViewSchool(ViewSchool_name);

            if (ViewSchools == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewSchool found for uci: {ViewSchool_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSchools);
        }

        [HttpPost]
        public async Task<ActionResult<ViewSchool>> AddViewSchool(ViewSchool ViewSchool)
        {
            var dbViewSchool = await _ViewSchoolService.AddViewSchool(ViewSchool);

            if (dbViewSchool == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewSchool.TbViewSchoolName} could not be added."
                );
            }

            return CreatedAtAction("GetViewSchool", new { uci = ViewSchool.TbViewSchoolName }, ViewSchool);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewSchool(ViewSchool ViewSchool)
        {           
            ViewSchool dbViewSchool = await _ViewSchoolService.UpdateViewSchool(ViewSchool);

            if (dbViewSchool == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewSchool.TbViewSchoolName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewSchool(ViewSchool ViewSchool)
        {            
            (bool status, string message) = await _ViewSchoolService.DeleteViewSchool(ViewSchool);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewSchool);
        }
    }
}
