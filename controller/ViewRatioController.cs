using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewRatioController : ControllerBase
    {
        private readonly IViewRatioService _ViewRatioService;

        public ViewRatioController(IViewRatioService ViewRatioService)
        {
            _ViewRatioService = ViewRatioService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRatioList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewRatios = await _ViewRatioService.GetViewRatioListByValue(offset, limit, val);

            if (ViewRatios == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewRatios in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRatios);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRatioList(string ViewRatio_name)
        {
            var ViewRatios = await _ViewRatioService.GetViewRatioList(ViewRatio_name);

            if (ViewRatios == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewRatio found for uci: {ViewRatio_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRatios);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewRatio(string ViewRatio_name)
        {
            var ViewRatios = await _ViewRatioService.GetViewRatio(ViewRatio_name);

            if (ViewRatios == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewRatio found for uci: {ViewRatio_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewRatios);
        }

        [HttpPost]
        public async Task<ActionResult<ViewRatio>> AddViewRatio(ViewRatio ViewRatio)
        {
            var dbViewRatio = await _ViewRatioService.AddViewRatio(ViewRatio);

            if (dbViewRatio == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewRatio.TbViewRatioName} could not be added."
                );
            }

            return CreatedAtAction("GetViewRatio", new { uci = ViewRatio.TbViewRatioName }, ViewRatio);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewRatio(ViewRatio ViewRatio)
        {           
            ViewRatio dbViewRatio = await _ViewRatioService.UpdateViewRatio(ViewRatio);

            if (dbViewRatio == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewRatio.TbViewRatioName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewRatio(ViewRatio ViewRatio)
        {            
            (bool status, string message) = await _ViewRatioService.DeleteViewRatio(ViewRatio);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewRatio);
        }
    }
}
