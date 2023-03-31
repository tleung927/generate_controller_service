using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginStatController : ControllerBase
    {
        private readonly ILoginStatService _LoginStatService;

        public LoginStatController(ILoginStatService LoginStatService)
        {
            _LoginStatService = LoginStatService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginStatList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var LoginStats = await _LoginStatService.GetLoginStatListByValue(offset, limit, val);

            if (LoginStats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No LoginStats in database");
            }

            return StatusCode(StatusCodes.Status200OK, LoginStats);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginStatList(string LoginStat_name)
        {
            var LoginStats = await _LoginStatService.GetLoginStatList(LoginStat_name);

            if (LoginStats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginStat found for uci: {LoginStat_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginStats);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginStat(string LoginStat_name)
        {
            var LoginStats = await _LoginStatService.GetLoginStat(LoginStat_name);

            if (LoginStats == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginStat found for uci: {LoginStat_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginStats);
        }

        [HttpPost]
        public async Task<ActionResult<LoginStat>> AddLoginStat(LoginStat LoginStat)
        {
            var dbLoginStat = await _LoginStatService.AddLoginStat(LoginStat);

            if (dbLoginStat == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginStat.TbLoginStatName} could not be added."
                );
            }

            return CreatedAtAction("GetLoginStat", new { uci = LoginStat.TbLoginStatName }, LoginStat);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoginStat(LoginStat LoginStat)
        {           
            LoginStat dbLoginStat = await _LoginStatService.UpdateLoginStat(LoginStat);

            if (dbLoginStat == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginStat.TbLoginStatName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLoginStat(LoginStat LoginStat)
        {            
            (bool status, string message) = await _LoginStatService.DeleteLoginStat(LoginStat);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, LoginStat);
        }
    }
}
