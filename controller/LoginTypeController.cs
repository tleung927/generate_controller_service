using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginTypeController : ControllerBase
    {
        private readonly ILoginTypeService _LoginTypeService;

        public LoginTypeController(ILoginTypeService LoginTypeService)
        {
            _LoginTypeService = LoginTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginTypeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var LoginTypes = await _LoginTypeService.GetLoginTypeListByValue(offset, limit, val);

            if (LoginTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No LoginTypes in database");
            }

            return StatusCode(StatusCodes.Status200OK, LoginTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginTypeList(string LoginType_name)
        {
            var LoginTypes = await _LoginTypeService.GetLoginTypeList(LoginType_name);

            if (LoginTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginType found for uci: {LoginType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetLoginType(string LoginType_name)
        {
            var LoginTypes = await _LoginTypeService.GetLoginType(LoginType_name);

            if (LoginTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No LoginType found for uci: {LoginType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, LoginTypes);
        }

        [HttpPost]
        public async Task<ActionResult<LoginType>> AddLoginType(LoginType LoginType)
        {
            var dbLoginType = await _LoginTypeService.AddLoginType(LoginType);

            if (dbLoginType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginType.TbLoginTypeName} could not be added."
                );
            }

            return CreatedAtAction("GetLoginType", new { uci = LoginType.TbLoginTypeName }, LoginType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateLoginType(LoginType LoginType)
        {           
            LoginType dbLoginType = await _LoginTypeService.UpdateLoginType(LoginType);

            if (dbLoginType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{LoginType.TbLoginTypeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLoginType(LoginType LoginType)
        {            
            (bool status, string message) = await _LoginTypeService.DeleteLoginType(LoginType);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, LoginType);
        }
    }
}
