using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersGroupController : ControllerBase
    {
        private readonly IUsersGroupService _UsersGroupService;

        public UsersGroupController(IUsersGroupService UsersGroupService)
        {
            _UsersGroupService = UsersGroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersGroupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var UsersGroups = await _UsersGroupService.GetUsersGroupListByValue(offset, limit, val);

            if (UsersGroups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No UsersGroups in database");
            }

            return StatusCode(StatusCodes.Status200OK, UsersGroups);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersGroupList(string UsersGroup_name)
        {
            var UsersGroups = await _UsersGroupService.GetUsersGroupList(UsersGroup_name);

            if (UsersGroups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UsersGroup found for uci: {UsersGroup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UsersGroups);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersGroup(string UsersGroup_name)
        {
            var UsersGroups = await _UsersGroupService.GetUsersGroup(UsersGroup_name);

            if (UsersGroups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No UsersGroup found for uci: {UsersGroup_name}");
            }

            return StatusCode(StatusCodes.Status200OK, UsersGroups);
        }

        [HttpPost]
        public async Task<ActionResult<UsersGroup>> AddUsersGroup(UsersGroup UsersGroup)
        {
            var dbUsersGroup = await _UsersGroupService.AddUsersGroup(UsersGroup);

            if (dbUsersGroup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UsersGroup.TbUsersGroupName} could not be added."
                );
            }

            return CreatedAtAction("GetUsersGroup", new { uci = UsersGroup.TbUsersGroupName }, UsersGroup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUsersGroup(UsersGroup UsersGroup)
        {           
            UsersGroup dbUsersGroup = await _UsersGroupService.UpdateUsersGroup(UsersGroup);

            if (dbUsersGroup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{UsersGroup.TbUsersGroupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUsersGroup(UsersGroup UsersGroup)
        {            
            (bool status, string message) = await _UsersGroupService.DeleteUsersGroup(UsersGroup);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, UsersGroup);
        }
    }
}
