using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewClientStatusController : ControllerBase
    {
        private readonly IViewClientStatusService _ViewClientStatusService;

        public ViewClientStatusController(IViewClientStatusService ViewClientStatusService)
        {
            _ViewClientStatusService = ViewClientStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewClientStatusList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewClientStatuss = await _ViewClientStatusService.GetViewClientStatusListByValue(offset, limit, val);

            if (ViewClientStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewClientStatuss in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewClientStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewClientStatusList(string ViewClientStatus_name)
        {
            var ViewClientStatuss = await _ViewClientStatusService.GetViewClientStatusList(ViewClientStatus_name);

            if (ViewClientStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewClientStatus found for uci: {ViewClientStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewClientStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewClientStatus(string ViewClientStatus_name)
        {
            var ViewClientStatuss = await _ViewClientStatusService.GetViewClientStatus(ViewClientStatus_name);

            if (ViewClientStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewClientStatus found for uci: {ViewClientStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewClientStatuss);
        }

        [HttpPost]
        public async Task<ActionResult<ViewClientStatus>> AddViewClientStatus(ViewClientStatus ViewClientStatus)
        {
            var dbViewClientStatus = await _ViewClientStatusService.AddViewClientStatus(ViewClientStatus);

            if (dbViewClientStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewClientStatus.TbViewClientStatusName} could not be added."
                );
            }

            return CreatedAtAction("GetViewClientStatus", new { uci = ViewClientStatus.TbViewClientStatusName }, ViewClientStatus);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewClientStatus(ViewClientStatus ViewClientStatus)
        {           
            ViewClientStatus dbViewClientStatus = await _ViewClientStatusService.UpdateViewClientStatus(ViewClientStatus);

            if (dbViewClientStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewClientStatus.TbViewClientStatusName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewClientStatus(ViewClientStatus ViewClientStatus)
        {            
            (bool status, string message) = await _ViewClientStatusService.DeleteViewClientStatus(ViewClientStatus);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewClientStatus);
        }
    }
}
