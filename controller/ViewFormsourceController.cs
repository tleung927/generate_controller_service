using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewFormsourceController : ControllerBase
    {
        private readonly IViewFormsourceService _ViewFormsourceService;

        public ViewFormsourceController(IViewFormsourceService ViewFormsourceService)
        {
            _ViewFormsourceService = ViewFormsourceService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewFormsourceList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewFormsources = await _ViewFormsourceService.GetViewFormsourceListByValue(offset, limit, val);

            if (ViewFormsources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewFormsources in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewFormsources);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewFormsourceList(string ViewFormsource_name)
        {
            var ViewFormsources = await _ViewFormsourceService.GetViewFormsourceList(ViewFormsource_name);

            if (ViewFormsources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewFormsource found for uci: {ViewFormsource_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewFormsources);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewFormsource(string ViewFormsource_name)
        {
            var ViewFormsources = await _ViewFormsourceService.GetViewFormsource(ViewFormsource_name);

            if (ViewFormsources == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewFormsource found for uci: {ViewFormsource_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewFormsources);
        }

        [HttpPost]
        public async Task<ActionResult<ViewFormsource>> AddViewFormsource(ViewFormsource ViewFormsource)
        {
            var dbViewFormsource = await _ViewFormsourceService.AddViewFormsource(ViewFormsource);

            if (dbViewFormsource == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewFormsource.TbViewFormsourceName} could not be added."
                );
            }

            return CreatedAtAction("GetViewFormsource", new { uci = ViewFormsource.TbViewFormsourceName }, ViewFormsource);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewFormsource(ViewFormsource ViewFormsource)
        {           
            ViewFormsource dbViewFormsource = await _ViewFormsourceService.UpdateViewFormsource(ViewFormsource);

            if (dbViewFormsource == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewFormsource.TbViewFormsourceName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewFormsource(ViewFormsource ViewFormsource)
        {            
            (bool status, string message) = await _ViewFormsourceService.DeleteViewFormsource(ViewFormsource);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewFormsource);
        }
    }
}
