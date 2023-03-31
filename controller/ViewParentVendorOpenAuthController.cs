using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ViewParentVendorOpenAuthController : ControllerBase
    {
        private readonly IViewParentVendorOpenAuthService _ViewParentVendorOpenAuthService;

        public ViewParentVendorOpenAuthController(IViewParentVendorOpenAuthService ViewParentVendorOpenAuthService)
        {
            _ViewParentVendorOpenAuthService = ViewParentVendorOpenAuthService;
        }

        [HttpGet]
        public async Task<IActionResult> GetViewParentVendorOpenAuthList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var ViewParentVendorOpenAuths = await _ViewParentVendorOpenAuthService.GetViewParentVendorOpenAuthListByValue(offset, limit, val);

            if (ViewParentVendorOpenAuths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No ViewParentVendorOpenAuths in database");
            }

            return StatusCode(StatusCodes.Status200OK, ViewParentVendorOpenAuths);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewParentVendorOpenAuthList(string ViewParentVendorOpenAuth_name)
        {
            var ViewParentVendorOpenAuths = await _ViewParentVendorOpenAuthService.GetViewParentVendorOpenAuthList(ViewParentVendorOpenAuth_name);

            if (ViewParentVendorOpenAuths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewParentVendorOpenAuth found for uci: {ViewParentVendorOpenAuth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewParentVendorOpenAuths);
        }

        [HttpGet]
        public async Task<IActionResult> GetViewParentVendorOpenAuth(string ViewParentVendorOpenAuth_name)
        {
            var ViewParentVendorOpenAuths = await _ViewParentVendorOpenAuthService.GetViewParentVendorOpenAuth(ViewParentVendorOpenAuth_name);

            if (ViewParentVendorOpenAuths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No ViewParentVendorOpenAuth found for uci: {ViewParentVendorOpenAuth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, ViewParentVendorOpenAuths);
        }

        [HttpPost]
        public async Task<ActionResult<ViewParentVendorOpenAuth>> AddViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth)
        {
            var dbViewParentVendorOpenAuth = await _ViewParentVendorOpenAuthService.AddViewParentVendorOpenAuth(ViewParentVendorOpenAuth);

            if (dbViewParentVendorOpenAuth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewParentVendorOpenAuth.TbViewParentVendorOpenAuthName} could not be added."
                );
            }

            return CreatedAtAction("GetViewParentVendorOpenAuth", new { uci = ViewParentVendorOpenAuth.TbViewParentVendorOpenAuthName }, ViewParentVendorOpenAuth);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth)
        {           
            ViewParentVendorOpenAuth dbViewParentVendorOpenAuth = await _ViewParentVendorOpenAuthService.UpdateViewParentVendorOpenAuth(ViewParentVendorOpenAuth);

            if (dbViewParentVendorOpenAuth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{ViewParentVendorOpenAuth.TbViewParentVendorOpenAuthName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteViewParentVendorOpenAuth(ViewParentVendorOpenAuth ViewParentVendorOpenAuth)
        {            
            (bool status, string message) = await _ViewParentVendorOpenAuthService.DeleteViewParentVendorOpenAuth(ViewParentVendorOpenAuth);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, ViewParentVendorOpenAuth);
        }
    }
}
