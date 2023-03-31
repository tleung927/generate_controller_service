using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PoliceDeptController : ControllerBase
    {
        private readonly IPoliceDeptService _PoliceDeptService;

        public PoliceDeptController(IPoliceDeptService PoliceDeptService)
        {
            _PoliceDeptService = PoliceDeptService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPoliceDeptList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var PoliceDepts = await _PoliceDeptService.GetPoliceDeptListByValue(offset, limit, val);

            if (PoliceDepts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No PoliceDepts in database");
            }

            return StatusCode(StatusCodes.Status200OK, PoliceDepts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPoliceDeptList(string PoliceDept_name)
        {
            var PoliceDepts = await _PoliceDeptService.GetPoliceDeptList(PoliceDept_name);

            if (PoliceDepts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PoliceDept found for uci: {PoliceDept_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PoliceDepts);
        }

        [HttpGet]
        public async Task<IActionResult> GetPoliceDept(string PoliceDept_name)
        {
            var PoliceDepts = await _PoliceDeptService.GetPoliceDept(PoliceDept_name);

            if (PoliceDepts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No PoliceDept found for uci: {PoliceDept_name}");
            }

            return StatusCode(StatusCodes.Status200OK, PoliceDepts);
        }

        [HttpPost]
        public async Task<ActionResult<PoliceDept>> AddPoliceDept(PoliceDept PoliceDept)
        {
            var dbPoliceDept = await _PoliceDeptService.AddPoliceDept(PoliceDept);

            if (dbPoliceDept == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PoliceDept.TbPoliceDeptName} could not be added."
                );
            }

            return CreatedAtAction("GetPoliceDept", new { uci = PoliceDept.TbPoliceDeptName }, PoliceDept);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePoliceDept(PoliceDept PoliceDept)
        {           
            PoliceDept dbPoliceDept = await _PoliceDeptService.UpdatePoliceDept(PoliceDept);

            if (dbPoliceDept == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{PoliceDept.TbPoliceDeptName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePoliceDept(PoliceDept PoliceDept)
        {            
            (bool status, string message) = await _PoliceDeptService.DeletePoliceDept(PoliceDept);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, PoliceDept);
        }
    }
}
