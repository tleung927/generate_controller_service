using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewCderEpilepsyController : ControllerBase
    {
        private readonly IViewCderEpilepsyService _ViewCderEpilepsyService;

        public ViewCderEpilepsyController(IViewCderEpilepsyService ViewCderEpilepsyService)
        {
            _ViewCderEpilepsyService = ViewCderEpilepsyService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderEpilepsyList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewCderEpilepsys = await _ViewCderEpilepsyService.GetViewCderEpilepsyListByValue(offset, limit, val);

            if (ViewCderEpilepsys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewCderEpilepsys in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderEpilepsys);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderEpilepsyList(string ViewCderEpilepsy_name)
        {
            var ViewCderEpilepsys = await _ViewCderEpilepsyService.GetViewCderEpilepsyList(ViewCderEpilepsy_name);

            if (ViewCderEpilepsys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCderEpilepsy found for uci: {ViewCderEpilepsy_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderEpilepsys);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderEpilepsy(string ViewCderEpilepsy_name)
        {
            var ViewCderEpilepsys = await _ViewCderEpilepsyService.GetViewCderEpilepsy(ViewCderEpilepsy_name);

            if (ViewCderEpilepsys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCderEpilepsy found for uci: {ViewCderEpilepsy_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderEpilepsys);
        }

        [HttpPost]
        public async Task<ActionResult<ViewCderEpilepsy>> AddViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy)
        {
            var dbViewCderEpilepsy = await _ViewCderEpilepsyService.AddViewCderEpilepsy(ViewCderEpilepsy);

            if (dbViewCderEpilepsy == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCderEpilepsy.TbViewCderEpilepsyName} could not be added."
                );
            }

            return CreatedAtAction("GetViewCderEpilepsy", new { uci = ViewCderEpilepsy.TbViewCderEpilepsyName }, ViewCderEpilepsy);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy)
        {           
            ViewCderEpilepsy dbViewCderEpilepsy = await _ViewCderEpilepsyService.UpdateViewCderEpilepsy(ViewCderEpilepsy);

            if (dbViewCderEpilepsy == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCderEpilepsy.TbViewCderEpilepsyName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewCderEpilepsy(ViewCderEpilepsy ViewCderEpilepsy)
        {            
            (bool status, string message) = await _ViewCderEpilepsyService.DeleteViewCderEpilepsy(ViewCderEpilepsy);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderEpilepsy);
        }
    }
}
