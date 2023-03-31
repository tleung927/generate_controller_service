using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleSlIlBudgetController : ControllerBase
    {
        private readonly IViewScheduleSlIlBudgetService _ViewScheduleSlIlBudgetService;

        public ViewScheduleSlIlBudgetController(IViewScheduleSlIlBudgetService ViewScheduleSlIlBudgetService)
        {
            _ViewScheduleSlIlBudgetService = ViewScheduleSlIlBudgetService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleSlIlBudgetList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleSlIlBudgets = await _ViewScheduleSlIlBudgetService.GetViewScheduleSlIlBudgetListByValue(offset, limit, val);

            if (ViewScheduleSlIlBudgets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleSlIlBudgets in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleSlIlBudgets);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleSlIlBudgetList(string ViewScheduleSlIlBudget_name)
        {
            var ViewScheduleSlIlBudgets = await _ViewScheduleSlIlBudgetService.GetViewScheduleSlIlBudgetList(ViewScheduleSlIlBudget_name);

            if (ViewScheduleSlIlBudgets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleSlIlBudget found for uci: {ViewScheduleSlIlBudget_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleSlIlBudgets);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleSlIlBudget(string ViewScheduleSlIlBudget_name)
        {
            var ViewScheduleSlIlBudgets = await _ViewScheduleSlIlBudgetService.GetViewScheduleSlIlBudget(ViewScheduleSlIlBudget_name);

            if (ViewScheduleSlIlBudgets == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleSlIlBudget found for uci: {ViewScheduleSlIlBudget_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleSlIlBudgets);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleSlIlBudget>> AddViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget)
        {
            var dbViewScheduleSlIlBudget = await _ViewScheduleSlIlBudgetService.AddViewScheduleSlIlBudget(ViewScheduleSlIlBudget);

            if (dbViewScheduleSlIlBudget == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleSlIlBudget.TbViewScheduleSlIlBudgetName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleSlIlBudget", new { uci = ViewScheduleSlIlBudget.TbViewScheduleSlIlBudgetName }, ViewScheduleSlIlBudget);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget)
        {           
            ViewScheduleSlIlBudget dbViewScheduleSlIlBudget = await _ViewScheduleSlIlBudgetService.UpdateViewScheduleSlIlBudget(ViewScheduleSlIlBudget);

            if (dbViewScheduleSlIlBudget == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleSlIlBudget.TbViewScheduleSlIlBudgetName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleSlIlBudget(ViewScheduleSlIlBudget ViewScheduleSlIlBudget)
        {            
            (bool status, string message) = await _ViewScheduleSlIlBudgetService.DeleteViewScheduleSlIlBudget(ViewScheduleSlIlBudget);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleSlIlBudget);
        }
    }
}
