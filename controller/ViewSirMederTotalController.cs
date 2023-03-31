using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewSirMederTotalController : ControllerBase
    {
        private readonly IViewSirMederTotalService _ViewSirMederTotalService;

        public ViewSirMederTotalController(IViewSirMederTotalService ViewSirMederTotalService)
        {
            _ViewSirMederTotalService = ViewSirMederTotalService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSirMederTotalList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewSirMederTotals = await _ViewSirMederTotalService.GetViewSirMederTotalListByValue(offset, limit, val);

            if (ViewSirMederTotals == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewSirMederTotals in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSirMederTotals);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSirMederTotalList(string ViewSirMederTotal_name)
        {
            var ViewSirMederTotals = await _ViewSirMederTotalService.GetViewSirMederTotalList(ViewSirMederTotal_name);

            if (ViewSirMederTotals == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewSirMederTotal found for uci: {ViewSirMederTotal_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSirMederTotals);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewSirMederTotal(string ViewSirMederTotal_name)
        {
            var ViewSirMederTotals = await _ViewSirMederTotalService.GetViewSirMederTotal(ViewSirMederTotal_name);

            if (ViewSirMederTotals == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewSirMederTotal found for uci: {ViewSirMederTotal_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewSirMederTotals);
        }

        [HttpPost]
        public async Task<ActionResult<ViewSirMederTotal>> AddViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal)
        {
            var dbViewSirMederTotal = await _ViewSirMederTotalService.AddViewSirMederTotal(ViewSirMederTotal);

            if (dbViewSirMederTotal == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewSirMederTotal.TbViewSirMederTotalName} could not be added."
                );
            }

            return CreatedAtAction("GetViewSirMederTotal", new { uci = ViewSirMederTotal.TbViewSirMederTotalName }, ViewSirMederTotal);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal)
        {           
            ViewSirMederTotal dbViewSirMederTotal = await _ViewSirMederTotalService.UpdateViewSirMederTotal(ViewSirMederTotal);

            if (dbViewSirMederTotal == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewSirMederTotal.TbViewSirMederTotalName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewSirMederTotal(ViewSirMederTotal ViewSirMederTotal)
        {            
            (bool status, string message) = await _ViewSirMederTotalService.DeleteViewSirMederTotal(ViewSirMederTotal);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewSirMederTotal);
        }
    }
}
