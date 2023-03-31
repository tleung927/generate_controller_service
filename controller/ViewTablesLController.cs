using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewTablesLController : ControllerBase
    {
        private readonly IViewTablesLService _ViewTablesLService;

        public ViewTablesLController(IViewTablesLService ViewTablesLService)
        {
            _ViewTablesLService = ViewTablesLService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTablesLList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewTablesLs = await _ViewTablesLService.GetViewTablesLListByValue(offset, limit, val);

            if (ViewTablesLs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewTablesLs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesLs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTablesLList(string ViewTablesL_name)
        {
            var ViewTablesLs = await _ViewTablesLService.GetViewTablesLList(ViewTablesL_name);

            if (ViewTablesLs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewTablesL found for uci: {ViewTablesL_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesLs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewTablesL(string ViewTablesL_name)
        {
            var ViewTablesLs = await _ViewTablesLService.GetViewTablesL(ViewTablesL_name);

            if (ViewTablesLs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewTablesL found for uci: {ViewTablesL_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesLs);
        }

        [HttpPost]
        public async Task<ActionResult<ViewTablesL>> AddViewTablesL(ViewTablesL ViewTablesL)
        {
            var dbViewTablesL = await _ViewTablesLService.AddViewTablesL(ViewTablesL);

            if (dbViewTablesL == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewTablesL.TbViewTablesLName} could not be added."
                );
            }

            return CreatedAtAction("GetViewTablesL", new { uci = ViewTablesL.TbViewTablesLName }, ViewTablesL);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewTablesL(ViewTablesL ViewTablesL)
        {           
            ViewTablesL dbViewTablesL = await _ViewTablesLService.UpdateViewTablesL(ViewTablesL);

            if (dbViewTablesL == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewTablesL.TbViewTablesLName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewTablesL(ViewTablesL ViewTablesL)
        {            
            (bool status, string message) = await _ViewTablesLService.DeleteViewTablesL(ViewTablesL);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewTablesL);
        }
    }
}
