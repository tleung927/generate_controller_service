using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewStatusController : ControllerBase
    {
        private readonly IViewStatusService _ViewStatusService;

        public ViewStatusController(IViewStatusService ViewStatusService)
        {
            _ViewStatusService = ViewStatusService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewStatusList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewStatuss = await _ViewStatusService.GetViewStatusListByValue(offset, limit, val);

            if (ViewStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewStatuss in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewStatusList(string ViewStatus_name)
        {
            var ViewStatuss = await _ViewStatusService.GetViewStatusList(ViewStatus_name);

            if (ViewStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewStatus found for uci: {ViewStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewStatuss);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewStatus(string ViewStatus_name)
        {
            var ViewStatuss = await _ViewStatusService.GetViewStatus(ViewStatus_name);

            if (ViewStatuss == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewStatus found for uci: {ViewStatus_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewStatuss);
        }

        [HttpPost]
        public async Task<ActionResult<ViewStatus>> AddViewStatus(ViewStatus ViewStatus)
        {
            var dbViewStatus = await _ViewStatusService.AddViewStatus(ViewStatus);

            if (dbViewStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewStatus.TbViewStatusName} could not be added."
                );
            }

            return CreatedAtAction("GetViewStatus", new { uci = ViewStatus.TbViewStatusName }, ViewStatus);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewStatus(ViewStatus ViewStatus)
        {           
            ViewStatus dbViewStatus = await _ViewStatusService.UpdateViewStatus(ViewStatus);

            if (dbViewStatus == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewStatus.TbViewStatusName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewStatus(ViewStatus ViewStatus)
        {            
            (bool status, string message) = await _ViewStatusService.DeleteViewStatus(ViewStatus);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewStatus);
        }
    }
}
