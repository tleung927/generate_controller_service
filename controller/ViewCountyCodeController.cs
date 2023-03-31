using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewCountyCodeController : ControllerBase
    {
        private readonly IViewCountyCodeService _ViewCountyCodeService;

        public ViewCountyCodeController(IViewCountyCodeService ViewCountyCodeService)
        {
            _ViewCountyCodeService = ViewCountyCodeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCountyCodeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewCountyCodes = await _ViewCountyCodeService.GetViewCountyCodeListByValue(offset, limit, val);

            if (ViewCountyCodes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewCountyCodes in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCountyCodes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCountyCodeList(string ViewCountyCode_name)
        {
            var ViewCountyCodes = await _ViewCountyCodeService.GetViewCountyCodeList(ViewCountyCode_name);

            if (ViewCountyCodes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCountyCode found for uci: {ViewCountyCode_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCountyCodes);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewCountyCode(string ViewCountyCode_name)
        {
            var ViewCountyCodes = await _ViewCountyCodeService.GetViewCountyCode(ViewCountyCode_name);

            if (ViewCountyCodes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewCountyCode found for uci: {ViewCountyCode_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewCountyCodes);
        }

        [HttpPost]
        public async Task<ActionResult<ViewCountyCode>> AddViewCountyCode(ViewCountyCode ViewCountyCode)
        {
            var dbViewCountyCode = await _ViewCountyCodeService.AddViewCountyCode(ViewCountyCode);

            if (dbViewCountyCode == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCountyCode.TbViewCountyCodeName} could not be added."
                );
            }

            return CreatedAtAction("GetViewCountyCode", new { uci = ViewCountyCode.TbViewCountyCodeName }, ViewCountyCode);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewCountyCode(ViewCountyCode ViewCountyCode)
        {           
            ViewCountyCode dbViewCountyCode = await _ViewCountyCodeService.UpdateViewCountyCode(ViewCountyCode);

            if (dbViewCountyCode == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewCountyCode.TbViewCountyCodeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewCountyCode(ViewCountyCode ViewCountyCode)
        {            
            (bool status, string message) = await _ViewCountyCodeService.DeleteViewCountyCode(ViewCountyCode);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewCountyCode);
        }
    }
}
