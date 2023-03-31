using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginPermissionController : ControllerBase
    {
        private readonly ILoginPermissionService _LoginPermissionService;

        public LoginPermissionController(ILoginPermissionService LoginPermissionService)
        {
            _LoginPermissionService = LoginPermissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginPermissionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var LoginPermissions = await _LoginPermissionService.GetLoginPermissionListByValue(offset, limit, val);

            if (LoginPermissions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No LoginPermissions in database");
            }

            return StatusCode(StatusCodes.Status200OK, LoginPermissions);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginPermissionList(string LoginPermission_name)
        {
            var LoginPermissions = await _LoginPermissionService.GetLoginPermissionList(LoginPermission_name);

            if (LoginPermissions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginPermission found for uci: {LoginPermission_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginPermissions);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginPermission(string LoginPermission_name)
        {
            var LoginPermissions = await _LoginPermissionService.GetLoginPermission(LoginPermission_name);

            if (LoginPermissions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginPermission found for uci: {LoginPermission_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginPermissions);
        }

        [HttpPost]
        public async Task<ActionResult<LoginPermission>> AddLoginPermission(LoginPermission LoginPermission)
        {
            var dbLoginPermission = await _LoginPermissionService.AddLoginPermission(LoginPermission);

            if (dbLoginPermission == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginPermission.TbLoginPermissionName} could not be added."
                );
            }

            return CreatedAtAction("GetLoginPermission", new { uci = LoginPermission.TbLoginPermissionName }, LoginPermission);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoginPermission(LoginPermission LoginPermission)
        {           
            LoginPermission dbLoginPermission = await _LoginPermissionService.UpdateLoginPermission(LoginPermission);

            if (dbLoginPermission == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginPermission.TbLoginPermissionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLoginPermission(LoginPermission LoginPermission)
        {            
            (bool status, string message) = await _LoginPermissionService.DeleteLoginPermission(LoginPermission);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, LoginPermission);
        }
    }
}
