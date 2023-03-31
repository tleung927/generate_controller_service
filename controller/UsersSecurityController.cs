using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersSecurityController : ControllerBase
    {
        private readonly IUsersSecurityService _UsersSecurityService;

        public UsersSecurityController(IUsersSecurityService UsersSecurityService)
        {
            _UsersSecurityService = UsersSecurityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersSecurityList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var UsersSecuritys = await _UsersSecurityService.GetUsersSecurityListByValue(offset, limit, val);

            if (UsersSecuritys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No UsersSecuritys in database");
            }

            return StatusCode(StatusCodes.Status200OK, UsersSecuritys);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersSecurityList(string UsersSecurity_name)
        {
            var UsersSecuritys = await _UsersSecurityService.GetUsersSecurityList(UsersSecurity_name);

            if (UsersSecuritys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UsersSecurity found for uci: {UsersSecurity_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UsersSecuritys);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersSecurity(string UsersSecurity_name)
        {
            var UsersSecuritys = await _UsersSecurityService.GetUsersSecurity(UsersSecurity_name);

            if (UsersSecuritys == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UsersSecurity found for uci: {UsersSecurity_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UsersSecuritys);
        }

        [HttpPost]
        public async Task<ActionResult<UsersSecurity>> AddUsersSecurity(UsersSecurity UsersSecurity)
        {
            var dbUsersSecurity = await _UsersSecurityService.AddUsersSecurity(UsersSecurity);

            if (dbUsersSecurity == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UsersSecurity.TbUsersSecurityName} could not be added."
                );
            }

            return CreatedAtAction("GetUsersSecurity", new { uci = UsersSecurity.TbUsersSecurityName }, UsersSecurity);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsersSecurity(UsersSecurity UsersSecurity)
        {           
            UsersSecurity dbUsersSecurity = await _UsersSecurityService.UpdateUsersSecurity(UsersSecurity);

            if (dbUsersSecurity == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UsersSecurity.TbUsersSecurityName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsersSecurity(UsersSecurity UsersSecurity)
        {            
            (bool status, string message) = await _UsersSecurityService.DeleteUsersSecurity(UsersSecurity);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, UsersSecurity);
        }
    }
}
