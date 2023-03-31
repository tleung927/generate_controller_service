using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginTrxXrfController : ControllerBase
    {
        private readonly ILoginTrxXrfService _LoginTrxXrfService;

        public LoginTrxXrfController(ILoginTrxXrfService LoginTrxXrfService)
        {
            _LoginTrxXrfService = LoginTrxXrfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginTrxXrfList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var LoginTrxXrfs = await _LoginTrxXrfService.GetLoginTrxXrfListByValue(offset, limit, val);

            if (LoginTrxXrfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No LoginTrxXrfs in database");
            }

            return StatusCode(StatusCodes.Status200OK, LoginTrxXrfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginTrxXrfList(string LoginTrxXrf_name)
        {
            var LoginTrxXrfs = await _LoginTrxXrfService.GetLoginTrxXrfList(LoginTrxXrf_name);

            if (LoginTrxXrfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginTrxXrf found for uci: {LoginTrxXrf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginTrxXrfs);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginTrxXrf(string LoginTrxXrf_name)
        {
            var LoginTrxXrfs = await _LoginTrxXrfService.GetLoginTrxXrf(LoginTrxXrf_name);

            if (LoginTrxXrfs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginTrxXrf found for uci: {LoginTrxXrf_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginTrxXrfs);
        }

        [HttpPost]
        public async Task<ActionResult<LoginTrxXrf>> AddLoginTrxXrf(LoginTrxXrf LoginTrxXrf)
        {
            var dbLoginTrxXrf = await _LoginTrxXrfService.AddLoginTrxXrf(LoginTrxXrf);

            if (dbLoginTrxXrf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginTrxXrf.TbLoginTrxXrfName} could not be added."
                );
            }

            return CreatedAtAction("GetLoginTrxXrf", new { uci = LoginTrxXrf.TbLoginTrxXrfName }, LoginTrxXrf);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoginTrxXrf(LoginTrxXrf LoginTrxXrf)
        {           
            LoginTrxXrf dbLoginTrxXrf = await _LoginTrxXrfService.UpdateLoginTrxXrf(LoginTrxXrf);

            if (dbLoginTrxXrf == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginTrxXrf.TbLoginTrxXrfName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLoginTrxXrf(LoginTrxXrf LoginTrxXrf)
        {            
            (bool status, string message) = await _LoginTrxXrfService.DeleteLoginTrxXrf(LoginTrxXrf);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, LoginTrxXrf);
        }
    }
}
