using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewScheduleControllerDescController : ControllerBase
    {
        private readonly IViewScheduleControllerDescService _ViewScheduleControllerDescService;

        public ViewScheduleControllerDescController(IViewScheduleControllerDescService ViewScheduleControllerDescService)
        {
            _ViewScheduleControllerDescService = ViewScheduleControllerDescService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleControllerDescList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewScheduleControllerDescs = await _ViewScheduleControllerDescService.GetViewScheduleControllerDescListByValue(offset, limit, val);

            if (ViewScheduleControllerDescs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewScheduleControllerDescs in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerDescs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleControllerDescList(string ViewScheduleControllerDesc_name)
        {
            var ViewScheduleControllerDescs = await _ViewScheduleControllerDescService.GetViewScheduleControllerDescList(ViewScheduleControllerDesc_name);

            if (ViewScheduleControllerDescs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleControllerDesc found for uci: {ViewScheduleControllerDesc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerDescs);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewScheduleControllerDesc(string ViewScheduleControllerDesc_name)
        {
            var ViewScheduleControllerDescs = await _ViewScheduleControllerDescService.GetViewScheduleControllerDesc(ViewScheduleControllerDesc_name);

            if (ViewScheduleControllerDescs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewScheduleControllerDesc found for uci: {ViewScheduleControllerDesc_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerDescs);
        }

        [HttpPost]
        public async Task<ActionResult<ViewScheduleControllerDesc>> AddViewScheduleControllerDesc(ViewScheduleControllerDesc ViewScheduleControllerDesc)
        {
            var dbViewScheduleControllerDesc = await _ViewScheduleControllerDescService.AddViewScheduleControllerDesc(ViewScheduleControllerDesc);

            if (dbViewScheduleControllerDesc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleControllerDesc.TbViewScheduleControllerDescName} could not be added."
                );
            }

            return CreatedAtAction("GetViewScheduleControllerDesc", new { uci = ViewScheduleControllerDesc.TbViewScheduleControllerDescName }, ViewScheduleControllerDesc);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewScheduleControllerDesc(ViewScheduleControllerDesc ViewScheduleControllerDesc)
        {           
            ViewScheduleControllerDesc dbViewScheduleControllerDesc = await _ViewScheduleControllerDescService.UpdateViewScheduleControllerDesc(ViewScheduleControllerDesc);

            if (dbViewScheduleControllerDesc == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewScheduleControllerDesc.TbViewScheduleControllerDescName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewScheduleControllerDesc(ViewScheduleControllerDesc ViewScheduleControllerDesc)
        {            
            (bool status, string message) = await _ViewScheduleControllerDescService.DeleteViewScheduleControllerDesc(ViewScheduleControllerDesc);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewScheduleControllerDesc);
        }
    }
}
