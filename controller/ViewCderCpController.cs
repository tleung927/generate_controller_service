using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewCderCpController : ControllerBase
    {
        private readonly IViewCderCpService _ViewCderCpService;

        public ViewCderCpController(IViewCderCpService ViewCderCpService)
        {
            _ViewCderCpService = ViewCderCpService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderCpList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewCderCps = await _ViewCderCpService.GetViewCderCpListByValue(offset, limit, val);

            if (ViewCderCps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewCderCps in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderCps);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderCpList(string ViewCderCp_name)
        {
            var ViewCderCps = await _ViewCderCpService.GetViewCderCpList(ViewCderCp_name);

            if (ViewCderCps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCderCp found for uci: {ViewCderCp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderCps);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCderCp(string ViewCderCp_name)
        {
            var ViewCderCps = await _ViewCderCpService.GetViewCderCp(ViewCderCp_name);

            if (ViewCderCps == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCderCp found for uci: {ViewCderCp_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderCps);
        }

        [HttpPost]
        public async Task<ActionResult<ViewCderCp>> AddViewCderCp(ViewCderCp ViewCderCp)
        {
            var dbViewCderCp = await _ViewCderCpService.AddViewCderCp(ViewCderCp);

            if (dbViewCderCp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCderCp.TbViewCderCpName} could not be added."
                );
            }

            return CreatedAtAction("GetViewCderCp", new { uci = ViewCderCp.TbViewCderCpName }, ViewCderCp);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewCderCp(ViewCderCp ViewCderCp)
        {           
            ViewCderCp dbViewCderCp = await _ViewCderCpService.UpdateViewCderCp(ViewCderCp);

            if (dbViewCderCp == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCderCp.TbViewCderCpName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewCderCp(ViewCderCp ViewCderCp)
        {            
            (bool status, string message) = await _ViewCderCpService.DeleteViewCderCp(ViewCderCp);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewCderCp);
        }
    }
}
