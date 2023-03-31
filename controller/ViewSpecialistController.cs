using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewSpecialistController : ControllerBase
    {
        private readonly IViewSpecialistService _ViewSpecialistService;

        public ViewSpecialistController(IViewSpecialistService ViewSpecialistService)
        {
            _ViewSpecialistService = ViewSpecialistService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSpecialistList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewSpecialists = await _ViewSpecialistService.GetViewSpecialistListByValue(offset, limit, val);

            if (ViewSpecialists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewSpecialists in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSpecialists);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSpecialistList(string ViewSpecialist_name)
        {
            var ViewSpecialists = await _ViewSpecialistService.GetViewSpecialistList(ViewSpecialist_name);

            if (ViewSpecialists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewSpecialist found for uci: {ViewSpecialist_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSpecialists);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSpecialist(string ViewSpecialist_name)
        {
            var ViewSpecialists = await _ViewSpecialistService.GetViewSpecialist(ViewSpecialist_name);

            if (ViewSpecialists == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewSpecialist found for uci: {ViewSpecialist_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSpecialists);
        }

        [HttpPost]
        public async Task<ActionResult<ViewSpecialist>> AddViewSpecialist(ViewSpecialist ViewSpecialist)
        {
            var dbViewSpecialist = await _ViewSpecialistService.AddViewSpecialist(ViewSpecialist);

            if (dbViewSpecialist == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewSpecialist.TbViewSpecialistName} could not be added."
                );
            }

            return CreatedAtAction("GetViewSpecialist", new { uci = ViewSpecialist.TbViewSpecialistName }, ViewSpecialist);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewSpecialist(ViewSpecialist ViewSpecialist)
        {           
            ViewSpecialist dbViewSpecialist = await _ViewSpecialistService.UpdateViewSpecialist(ViewSpecialist);

            if (dbViewSpecialist == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewSpecialist.TbViewSpecialistName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewSpecialist(ViewSpecialist ViewSpecialist)
        {            
            (bool status, string message) = await _ViewSpecialistService.DeleteViewSpecialist(ViewSpecialist);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewSpecialist);
        }
    }
}
