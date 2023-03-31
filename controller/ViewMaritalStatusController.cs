using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewMaritalStatusController : ControllerBase
    {
        private readonly IViewMaritalStatusService _ViewMaritalStatusService;

        public ViewMaritalStatusController(IViewMaritalStatusService ViewMaritalStatusService)
        {
            _ViewMaritalStatusService = ViewMaritalStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewMaritalStatusList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewMaritalStatuss = await _ViewMaritalStatusService.GetViewMaritalStatusListByValue(offset, limit, val);

            if (ViewMaritalStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewMaritalStatuss in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewMaritalStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewMaritalStatusList(string ViewMaritalStatus_name)
        {
            var ViewMaritalStatuss = await _ViewMaritalStatusService.GetViewMaritalStatusList(ViewMaritalStatus_name);

            if (ViewMaritalStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewMaritalStatus found for uci: {ViewMaritalStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewMaritalStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewMaritalStatus(string ViewMaritalStatus_name)
        {
            var ViewMaritalStatuss = await _ViewMaritalStatusService.GetViewMaritalStatus(ViewMaritalStatus_name);

            if (ViewMaritalStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewMaritalStatus found for uci: {ViewMaritalStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewMaritalStatuss);
        }

        [HttpPost]
        public async Task<ActionResult<ViewMaritalStatus>> AddViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus)
        {
            var dbViewMaritalStatus = await _ViewMaritalStatusService.AddViewMaritalStatus(ViewMaritalStatus);

            if (dbViewMaritalStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewMaritalStatus.TbViewMaritalStatusName} could not be added."
                );
            }

            return CreatedAtAction("GetViewMaritalStatus", new { uci = ViewMaritalStatus.TbViewMaritalStatusName }, ViewMaritalStatus);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus)
        {           
            ViewMaritalStatus dbViewMaritalStatus = await _ViewMaritalStatusService.UpdateViewMaritalStatus(ViewMaritalStatus);

            if (dbViewMaritalStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewMaritalStatus.TbViewMaritalStatusName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewMaritalStatus(ViewMaritalStatus ViewMaritalStatus)
        {            
            (bool status, string message) = await _ViewMaritalStatusService.DeleteViewMaritalStatus(ViewMaritalStatus);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewMaritalStatus);
        }
    }
}
