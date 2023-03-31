using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersTypeController : ControllerBase
    {
        private readonly IUsersTypeService _UsersTypeService;

        public UsersTypeController(IUsersTypeService UsersTypeService)
        {
            _UsersTypeService = UsersTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersTypeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var UsersTypes = await _UsersTypeService.GetUsersTypeListByValue(offset, limit, val);

            if (UsersTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No UsersTypes in database");
            }

            return StatusCode(StatusCodes.Status200OK, UsersTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersTypeList(string UsersType_name)
        {
            var UsersTypes = await _UsersTypeService.GetUsersTypeList(UsersType_name);

            if (UsersTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UsersType found for uci: {UsersType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UsersTypes);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersType(string UsersType_name)
        {
            var UsersTypes = await _UsersTypeService.GetUsersType(UsersType_name);

            if (UsersTypes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UsersType found for uci: {UsersType_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UsersTypes);
        }

        [HttpPost]
        public async Task<ActionResult<UsersType>> AddUsersType(UsersType UsersType)
        {
            var dbUsersType = await _UsersTypeService.AddUsersType(UsersType);

            if (dbUsersType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UsersType.TbUsersTypeName} could not be added."
                );
            }

            return CreatedAtAction("GetUsersType", new { uci = UsersType.TbUsersTypeName }, UsersType);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsersType(UsersType UsersType)
        {           
            UsersType dbUsersType = await _UsersTypeService.UpdateUsersType(UsersType);

            if (dbUsersType == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UsersType.TbUsersTypeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsersType(UsersType UsersType)
        {            
            (bool status, string message) = await _UsersTypeService.DeleteUsersType(UsersType);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, UsersType);
        }
    }
}
