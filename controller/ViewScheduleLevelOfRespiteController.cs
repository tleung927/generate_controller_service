using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleLevelOfRespiteController : ControllerBase
    {
        private readonly IViewScheduleLevelOfRespiteService _ViewScheduleLevelOfRespiteService;

        public ViewScheduleLevelOfRespiteController(IViewScheduleLevelOfRespiteService ViewScheduleLevelOfRespiteService)
        {
            _ViewScheduleLevelOfRespiteService = ViewScheduleLevelOfRespiteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleLevelOfRespiteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleLevelOfRespites = await _ViewScheduleLevelOfRespiteService.GetViewScheduleLevelOfRespiteListByValue(offset, limit, val);

            if (ViewScheduleLevelOfRespites == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleLevelOfRespites in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleLevelOfRespites);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleLevelOfRespiteList(string ViewScheduleLevelOfRespite_name)
        {
            var ViewScheduleLevelOfRespites = await _ViewScheduleLevelOfRespiteService.GetViewScheduleLevelOfRespiteList(ViewScheduleLevelOfRespite_name);

            if (ViewScheduleLevelOfRespites == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleLevelOfRespite found for uci: {ViewScheduleLevelOfRespite_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleLevelOfRespites);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleLevelOfRespite(string ViewScheduleLevelOfRespite_name)
        {
            var ViewScheduleLevelOfRespites = await _ViewScheduleLevelOfRespiteService.GetViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite_name);

            if (ViewScheduleLevelOfRespites == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleLevelOfRespite found for uci: {ViewScheduleLevelOfRespite_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleLevelOfRespites);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleLevelOfRespite>> AddViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite)
        {
            var dbViewScheduleLevelOfRespite = await _ViewScheduleLevelOfRespiteService.AddViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite);

            if (dbViewScheduleLevelOfRespite == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleLevelOfRespite.TbViewScheduleLevelOfRespiteName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleLevelOfRespite", new { uci = ViewScheduleLevelOfRespite.TbViewScheduleLevelOfRespiteName }, ViewScheduleLevelOfRespite);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite)
        {           
            ViewScheduleLevelOfRespite dbViewScheduleLevelOfRespite = await _ViewScheduleLevelOfRespiteService.UpdateViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite);

            if (dbViewScheduleLevelOfRespite == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleLevelOfRespite.TbViewScheduleLevelOfRespiteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite ViewScheduleLevelOfRespite)
        {            
            (bool status, string message) = await _ViewScheduleLevelOfRespiteService.DeleteViewScheduleLevelOfRespite(ViewScheduleLevelOfRespite);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleLevelOfRespite);
        }
    }
}
