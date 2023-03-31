using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewPosCountController : ControllerBase
    {
        private readonly IViewPosCountService _ViewPosCountService;

        public ViewPosCountController(IViewPosCountService ViewPosCountService)
        {
            _ViewPosCountService = ViewPosCountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewPosCountList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewPosCounts = await _ViewPosCountService.GetViewPosCountListByValue(offset, limit, val);

            if (ViewPosCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewPosCounts in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewPosCounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewPosCountList(string ViewPosCount_name)
        {
            var ViewPosCounts = await _ViewPosCountService.GetViewPosCountList(ViewPosCount_name);

            if (ViewPosCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewPosCount found for uci: {ViewPosCount_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewPosCounts);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewPosCount(string ViewPosCount_name)
        {
            var ViewPosCounts = await _ViewPosCountService.GetViewPosCount(ViewPosCount_name);

            if (ViewPosCounts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewPosCount found for uci: {ViewPosCount_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewPosCounts);
        }

        [HttpPost]
        public async Task<ActionResult<ViewPosCount>> AddViewPosCount(ViewPosCount ViewPosCount)
        {
            var dbViewPosCount = await _ViewPosCountService.AddViewPosCount(ViewPosCount);

            if (dbViewPosCount == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewPosCount.TbViewPosCountName} could not be added."
                );
            }

            return CreatedAtAction("GetViewPosCount", new { uci = ViewPosCount.TbViewPosCountName }, ViewPosCount);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewPosCount(ViewPosCount ViewPosCount)
        {           
            ViewPosCount dbViewPosCount = await _ViewPosCountService.UpdateViewPosCount(ViewPosCount);

            if (dbViewPosCount == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewPosCount.TbViewPosCountName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewPosCount(ViewPosCount ViewPosCount)
        {            
            (bool status, string message) = await _ViewPosCountService.DeleteViewPosCount(ViewPosCount);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewPosCount);
        }
    }
}
