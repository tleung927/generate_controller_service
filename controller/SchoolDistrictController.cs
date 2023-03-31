using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SchoolDistrictController : ControllerBase
    {
        private readonly ISchoolDistrictService _SchoolDistrictService;

        public SchoolDistrictController(ISchoolDistrictService SchoolDistrictService)
        {
            _SchoolDistrictService = SchoolDistrictService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolDistrictList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SchoolDistricts = await _SchoolDistrictService.GetSchoolDistrictListByValue(offset, limit, val);

            if (SchoolDistricts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SchoolDistricts in database");
            }

            return StatusCode(StatusCodes.Status200OK, SchoolDistricts);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolDistrictList(string SchoolDistrict_name)
        {
            var SchoolDistricts = await _SchoolDistrictService.GetSchoolDistrictList(SchoolDistrict_name);

            if (SchoolDistricts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SchoolDistrict found for uci: {SchoolDistrict_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SchoolDistricts);
        }

        [HttpGet]
        public async Task<IActionResult> GetSchoolDistrict(string SchoolDistrict_name)
        {
            var SchoolDistricts = await _SchoolDistrictService.GetSchoolDistrict(SchoolDistrict_name);

            if (SchoolDistricts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SchoolDistrict found for uci: {SchoolDistrict_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SchoolDistricts);
        }

        [HttpPost]
        public async Task<ActionResult<SchoolDistrict>> AddSchoolDistrict(SchoolDistrict SchoolDistrict)
        {
            var dbSchoolDistrict = await _SchoolDistrictService.AddSchoolDistrict(SchoolDistrict);

            if (dbSchoolDistrict == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SchoolDistrict.TbSchoolDistrictName} could not be added."
                );
            }

            return CreatedAtAction("GetSchoolDistrict", new { uci = SchoolDistrict.TbSchoolDistrictName }, SchoolDistrict);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSchoolDistrict(SchoolDistrict SchoolDistrict)
        {           
            SchoolDistrict dbSchoolDistrict = await _SchoolDistrictService.UpdateSchoolDistrict(SchoolDistrict);

            if (dbSchoolDistrict == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SchoolDistrict.TbSchoolDistrictName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSchoolDistrict(SchoolDistrict SchoolDistrict)
        {            
            (bool status, string message) = await _SchoolDistrictService.DeleteSchoolDistrict(SchoolDistrict);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SchoolDistrict);
        }
    }
}
