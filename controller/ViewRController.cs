using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewRController : ControllerBase
    {
        private readonly IViewRService _ViewRService;

        public ViewRController(IViewRService ViewRService)
        {
            _ViewRService = ViewRService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewRs = await _ViewRService.GetViewRListByValue(offset, limit, val);

            if (ViewRs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewRs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRList(string ViewR_name)
        {
            var ViewRs = await _ViewRService.GetViewRList(ViewR_name);

            if (ViewRs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewR found for uci: {ViewR_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewR(string ViewR_name)
        {
            var ViewRs = await _ViewRService.GetViewR(ViewR_name);

            if (ViewRs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewR found for uci: {ViewR_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRs);
        }

        [HttpPost]
        public async Task<ActionResult<ViewR>> AddViewR(ViewR ViewR)
        {
            var dbViewR = await _ViewRService.AddViewR(ViewR);

            if (dbViewR == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewR.TbViewRName} could not be added."
                );
            }

            return CreatedAtAction("GetViewR", new { uci = ViewR.TbViewRName }, ViewR);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewR(ViewR ViewR)
        {           
            ViewR dbViewR = await _ViewRService.UpdateViewR(ViewR);

            if (dbViewR == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewR.TbViewRName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewR(ViewR ViewR)
        {            
            (bool status, string message) = await _ViewRService.DeleteViewR(ViewR);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewR);
        }
    }
}
