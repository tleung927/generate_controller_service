using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewControllerCodeController : ControllerBase
    {
        private readonly IViewControllerCodeService _ViewControllerCodeService;

        public ViewControllerCodeController(IViewControllerCodeService ViewControllerCodeService)
        {
            _ViewControllerCodeService = ViewControllerCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewControllerCodeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewControllerCodes = await _ViewControllerCodeService.GetViewControllerCodeListByValue(offset, limit, val);

            if (ViewControllerCodes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewControllerCodes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewControllerCodes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewControllerCodeList(string ViewControllerCode_name)
        {
            var ViewControllerCodes = await _ViewControllerCodeService.GetViewControllerCodeList(ViewControllerCode_name);

            if (ViewControllerCodes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewControllerCode found for uci: {ViewControllerCode_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewControllerCodes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewControllerCode(string ViewControllerCode_name)
        {
            var ViewControllerCodes = await _ViewControllerCodeService.GetViewControllerCode(ViewControllerCode_name);

            if (ViewControllerCodes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewControllerCode found for uci: {ViewControllerCode_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewControllerCodes);
        }

        [HttpPost]
        public async Task<ActionResult<ViewControllerCode>> AddViewControllerCode(ViewControllerCode ViewControllerCode)
        {
            var dbViewControllerCode = await _ViewControllerCodeService.AddViewControllerCode(ViewControllerCode);

            if (dbViewControllerCode == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewControllerCode.TbViewControllerCodeName} could not be added."
                );
            }

            return CreatedAtAction("GetViewControllerCode", new { uci = ViewControllerCode.TbViewControllerCodeName }, ViewControllerCode);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewControllerCode(ViewControllerCode ViewControllerCode)
        {           
            ViewControllerCode dbViewControllerCode = await _ViewControllerCodeService.UpdateViewControllerCode(ViewControllerCode);

            if (dbViewControllerCode == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewControllerCode.TbViewControllerCodeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewControllerCode(ViewControllerCode ViewControllerCode)
        {            
            (bool status, string message) = await _ViewControllerCodeService.DeleteViewControllerCode(ViewControllerCode);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewControllerCode);
        }
    }
}
