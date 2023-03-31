using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewPrimProgNameController : ControllerBase
    {
        private readonly IViewPrimProgNameService _ViewPrimProgNameService;

        public ViewPrimProgNameController(IViewPrimProgNameService ViewPrimProgNameService)
        {
            _ViewPrimProgNameService = ViewPrimProgNameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewPrimProgNameList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewPrimProgNames = await _ViewPrimProgNameService.GetViewPrimProgNameListByValue(offset, limit, val);

            if (ViewPrimProgNames == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewPrimProgNames in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewPrimProgNames);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewPrimProgNameList(string ViewPrimProgName_name)
        {
            var ViewPrimProgNames = await _ViewPrimProgNameService.GetViewPrimProgNameList(ViewPrimProgName_name);

            if (ViewPrimProgNames == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewPrimProgName found for uci: {ViewPrimProgName_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewPrimProgNames);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewPrimProgName(string ViewPrimProgName_name)
        {
            var ViewPrimProgNames = await _ViewPrimProgNameService.GetViewPrimProgName(ViewPrimProgName_name);

            if (ViewPrimProgNames == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewPrimProgName found for uci: {ViewPrimProgName_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewPrimProgNames);
        }

        [HttpPost]
        public async Task<ActionResult<ViewPrimProgName>> AddViewPrimProgName(ViewPrimProgName ViewPrimProgName)
        {
            var dbViewPrimProgName = await _ViewPrimProgNameService.AddViewPrimProgName(ViewPrimProgName);

            if (dbViewPrimProgName == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewPrimProgName.TbViewPrimProgNameName} could not be added."
                );
            }

            return CreatedAtAction("GetViewPrimProgName", new { uci = ViewPrimProgName.TbViewPrimProgNameName }, ViewPrimProgName);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewPrimProgName(ViewPrimProgName ViewPrimProgName)
        {           
            ViewPrimProgName dbViewPrimProgName = await _ViewPrimProgNameService.UpdateViewPrimProgName(ViewPrimProgName);

            if (dbViewPrimProgName == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewPrimProgName.TbViewPrimProgNameName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewPrimProgName(ViewPrimProgName ViewPrimProgName)
        {            
            (bool status, string message) = await _ViewPrimProgNameService.DeleteViewPrimProgName(ViewPrimProgName);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewPrimProgName);
        }
    }
}
