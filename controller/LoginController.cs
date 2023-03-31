using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _LoginService;

        public LoginController(ILoginService LoginService)
        {
            _LoginService = LoginService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Logins = await _LoginService.GetLoginListByValue(offset, limit, val);

            if (Logins == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Logins in database");
            }

            return StatusCode(StatusCodes.Status200OK, Logins);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginList(string Login_name)
        {
            var Logins = await _LoginService.GetLoginList(Login_name);

            if (Logins == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Login found for uci: {Login_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Logins);
        }

        [HttpGet]
        public async Task<IActionResult> GetLogin(string Login_name)
        {
            var Logins = await _LoginService.GetLogin(Login_name);

            if (Logins == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Login found for uci: {Login_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Logins);
        }

        [HttpPost]
        public async Task<ActionResult<Login>> AddLogin(Login Login)
        {
            var dbLogin = await _LoginService.AddLogin(Login);

            if (dbLogin == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Login.TbLoginName} could not be added."
                );
            }

            return CreatedAtAction("GetLogin", new { uci = Login.TbLoginName }, Login);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLogin(Login Login)
        {           
            Login dbLogin = await _LoginService.UpdateLogin(Login);

            if (dbLogin == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Login.TbLoginName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLogin(Login Login)
        {            
            (bool status, string message) = await _LoginService.DeleteLogin(Login);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Login);
        }
    }
}
