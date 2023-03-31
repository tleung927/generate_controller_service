using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmploymentController : ControllerBase
    {
        private readonly IEmploymentService _EmploymentService;

        public EmploymentController(IEmploymentService EmploymentService)
        {
            _EmploymentService = EmploymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmploymentList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Employments = await _EmploymentService.GetEmploymentListByValue(offset, limit, val);

            if (Employments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Employments in database");
            }

            return StatusCode(StatusCodes.Status200OK, Employments);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmploymentList(string Employment_name)
        {
            var Employments = await _EmploymentService.GetEmploymentList(Employment_name);

            if (Employments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Employment found for uci: {Employment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Employments);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployment(string Employment_name)
        {
            var Employments = await _EmploymentService.GetEmployment(Employment_name);

            if (Employments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Employment found for uci: {Employment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Employments);
        }

        [HttpPost]
        public async Task<ActionResult<Employment>> AddEmployment(Employment Employment)
        {
            var dbEmployment = await _EmploymentService.AddEmployment(Employment);

            if (dbEmployment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Employment.TbEmploymentName} could not be added."
                );
            }

            return CreatedAtAction("GetEmployment", new { uci = Employment.TbEmploymentName }, Employment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEmployment(Employment Employment)
        {           
            Employment dbEmployment = await _EmploymentService.UpdateEmployment(Employment);

            if (dbEmployment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Employment.TbEmploymentName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteEmployment(Employment Employment)
        {            
            (bool status, string message) = await _EmploymentService.DeleteEmployment(Employment);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Employment);
        }
    }
}
