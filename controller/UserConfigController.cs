using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserConfigController : ControllerBase
    {
        private readonly IUserConfigService _UserConfigService;

        public UserConfigController(IUserConfigService UserConfigService)
        {
            _UserConfigService = UserConfigService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserConfigList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var UserConfigs = await _UserConfigService.GetUserConfigListByValue(offset, limit, val);

            if (UserConfigs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No UserConfigs in database");
            }

            return StatusCode(StatusCodes.Status200OK, UserConfigs);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserConfigList(string UserConfig_name)
        {
            var UserConfigs = await _UserConfigService.GetUserConfigList(UserConfig_name);

            if (UserConfigs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UserConfig found for uci: {UserConfig_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UserConfigs);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserConfig(string UserConfig_name)
        {
            var UserConfigs = await _UserConfigService.GetUserConfig(UserConfig_name);

            if (UserConfigs == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UserConfig found for uci: {UserConfig_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UserConfigs);
        }

        [HttpPost]
        public async Task<ActionResult<UserConfig>> AddUserConfig(UserConfig UserConfig)
        {
            var dbUserConfig = await _UserConfigService.AddUserConfig(UserConfig);

            if (dbUserConfig == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UserConfig.TbUserConfigName} could not be added."
                );
            }

            return CreatedAtAction("GetUserConfig", new { uci = UserConfig.TbUserConfigName }, UserConfig);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserConfig(UserConfig UserConfig)
        {           
            UserConfig dbUserConfig = await _UserConfigService.UpdateUserConfig(UserConfig);

            if (dbUserConfig == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UserConfig.TbUserConfigName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUserConfig(UserConfig UserConfig)
        {            
            (bool status, string message) = await _UserConfigService.DeleteUserConfig(UserConfig);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, UserConfig);
        }
    }
}
