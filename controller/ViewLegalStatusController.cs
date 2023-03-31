using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewLegalStatusController : ControllerBase
    {
        private readonly IViewLegalStatusService _ViewLegalStatusService;

        public ViewLegalStatusController(IViewLegalStatusService ViewLegalStatusService)
        {
            _ViewLegalStatusService = ViewLegalStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLegalStatusList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewLegalStatuss = await _ViewLegalStatusService.GetViewLegalStatusListByValue(offset, limit, val);

            if (ViewLegalStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewLegalStatuss in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLegalStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLegalStatusList(string ViewLegalStatus_name)
        {
            var ViewLegalStatuss = await _ViewLegalStatusService.GetViewLegalStatusList(ViewLegalStatus_name);

            if (ViewLegalStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewLegalStatus found for uci: {ViewLegalStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLegalStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewLegalStatus(string ViewLegalStatus_name)
        {
            var ViewLegalStatuss = await _ViewLegalStatusService.GetViewLegalStatus(ViewLegalStatus_name);

            if (ViewLegalStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewLegalStatus found for uci: {ViewLegalStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewLegalStatuss);
        }

        [HttpPost]
        public async Task<ActionResult<ViewLegalStatus>> AddViewLegalStatus(ViewLegalStatus ViewLegalStatus)
        {
            var dbViewLegalStatus = await _ViewLegalStatusService.AddViewLegalStatus(ViewLegalStatus);

            if (dbViewLegalStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewLegalStatus.TbViewLegalStatusName} could not be added."
                );
            }

            return CreatedAtAction("GetViewLegalStatus", new { uci = ViewLegalStatus.TbViewLegalStatusName }, ViewLegalStatus);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewLegalStatus(ViewLegalStatus ViewLegalStatus)
        {           
            ViewLegalStatus dbViewLegalStatus = await _ViewLegalStatusService.UpdateViewLegalStatus(ViewLegalStatus);

            if (dbViewLegalStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewLegalStatus.TbViewLegalStatusName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewLegalStatus(ViewLegalStatus ViewLegalStatus)
        {            
            (bool status, string message) = await _ViewLegalStatusService.DeleteViewLegalStatus(ViewLegalStatus);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewLegalStatus);
        }
    }
}
