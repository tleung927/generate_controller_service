using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewTablesGController : ControllerBase
    {
        private readonly IViewTablesGService _ViewTablesGService;

        public ViewTablesGController(IViewTablesGService ViewTablesGService)
        {
            _ViewTablesGService = ViewTablesGService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTablesGList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewTablesGs = await _ViewTablesGService.GetViewTablesGListByValue(offset, limit, val);

            if (ViewTablesGs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewTablesGs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesGs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTablesGList(string ViewTablesG_name)
        {
            var ViewTablesGs = await _ViewTablesGService.GetViewTablesGList(ViewTablesG_name);

            if (ViewTablesGs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewTablesG found for uci: {ViewTablesG_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesGs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTablesG(string ViewTablesG_name)
        {
            var ViewTablesGs = await _ViewTablesGService.GetViewTablesG(ViewTablesG_name);

            if (ViewTablesGs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewTablesG found for uci: {ViewTablesG_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesGs);
        }

        [HttpPost]
        public async Task<ActionResult<ViewTablesG>> AddViewTablesG(ViewTablesG ViewTablesG)
        {
            var dbViewTablesG = await _ViewTablesGService.AddViewTablesG(ViewTablesG);

            if (dbViewTablesG == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewTablesG.TbViewTablesGName} could not be added."
                );
            }

            return CreatedAtAction("GetViewTablesG", new { uci = ViewTablesG.TbViewTablesGName }, ViewTablesG);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewTablesG(ViewTablesG ViewTablesG)
        {           
            ViewTablesG dbViewTablesG = await _ViewTablesGService.UpdateViewTablesG(ViewTablesG);

            if (dbViewTablesG == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewTablesG.TbViewTablesGName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewTablesG(ViewTablesG ViewTablesG)
        {            
            (bool status, string message) = await _ViewTablesGService.DeleteViewTablesG(ViewTablesG);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesG);
        }
    }
}
