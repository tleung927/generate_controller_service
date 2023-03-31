using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleCurrentControllerController : ControllerBase
    {
        private readonly IViewScheduleCurrentControllerService _ViewScheduleCurrentControllerService;

        public ViewScheduleCurrentControllerController(IViewScheduleCurrentControllerService ViewScheduleCurrentControllerService)
        {
            _ViewScheduleCurrentControllerService = ViewScheduleCurrentControllerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleCurrentControllerList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleCurrentControllers = await _ViewScheduleCurrentControllerService.GetViewScheduleCurrentControllerListByValue(offset, limit, val);

            if (ViewScheduleCurrentControllers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleCurrentControllers in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleCurrentControllers);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleCurrentControllerList(string ViewScheduleCurrentController_name)
        {
            var ViewScheduleCurrentControllers = await _ViewScheduleCurrentControllerService.GetViewScheduleCurrentControllerList(ViewScheduleCurrentController_name);

            if (ViewScheduleCurrentControllers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleCurrentController found for uci: {ViewScheduleCurrentController_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleCurrentControllers);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleCurrentController(string ViewScheduleCurrentController_name)
        {
            var ViewScheduleCurrentControllers = await _ViewScheduleCurrentControllerService.GetViewScheduleCurrentController(ViewScheduleCurrentController_name);

            if (ViewScheduleCurrentControllers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleCurrentController found for uci: {ViewScheduleCurrentController_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleCurrentControllers);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleCurrentController>> AddViewScheduleCurrentController(ViewScheduleCurrentController ViewScheduleCurrentController)
        {
            var dbViewScheduleCurrentController = await _ViewScheduleCurrentControllerService.AddViewScheduleCurrentController(ViewScheduleCurrentController);

            if (dbViewScheduleCurrentController == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleCurrentController.TbViewScheduleCurrentControllerName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleCurrentController", new { uci = ViewScheduleCurrentController.TbViewScheduleCurrentControllerName }, ViewScheduleCurrentController);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleCurrentController(ViewScheduleCurrentController ViewScheduleCurrentController)
        {           
            ViewScheduleCurrentController dbViewScheduleCurrentController = await _ViewScheduleCurrentControllerService.UpdateViewScheduleCurrentController(ViewScheduleCurrentController);

            if (dbViewScheduleCurrentController == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleCurrentController.TbViewScheduleCurrentControllerName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleCurrentController(ViewScheduleCurrentController ViewScheduleCurrentController)
        {            
            (bool status, string message) = await _ViewScheduleCurrentControllerService.DeleteViewScheduleCurrentController(ViewScheduleCurrentController);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleCurrentController);
        }
    }
}
