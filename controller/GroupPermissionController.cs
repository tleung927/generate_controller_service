using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupPermissionController : ControllerBase
    {
        private readonly IGroupPermissionService _GroupPermissionService;

        public GroupPermissionController(IGroupPermissionService GroupPermissionService)
        {
            _GroupPermissionService = GroupPermissionService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupPermissionList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var GroupPermissions = await _GroupPermissionService.GetGroupPermissionListByValue(offset, limit, val);

            if (GroupPermissions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No GroupPermissions in database");
            }

            return StatusCode(StatusCodes.Status200OK, GroupPermissions);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupPermissionList(string GroupPermission_name)
        {
            var GroupPermissions = await _GroupPermissionService.GetGroupPermissionList(GroupPermission_name);

            if (GroupPermissions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GroupPermission found for uci: {GroupPermission_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GroupPermissions);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupPermission(string GroupPermission_name)
        {
            var GroupPermissions = await _GroupPermissionService.GetGroupPermission(GroupPermission_name);

            if (GroupPermissions == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No GroupPermission found for uci: {GroupPermission_name}");
            }

            return StatusCode(StatusCodes.Status200OK, GroupPermissions);
        }

        [HttpPost]
        public async Task<ActionResult<GroupPermission>> AddGroupPermission(GroupPermission GroupPermission)
        {
            var dbGroupPermission = await _GroupPermissionService.AddGroupPermission(GroupPermission);

            if (dbGroupPermission == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GroupPermission.TbGroupPermissionName} could not be added."
                );
            }

            return CreatedAtAction("GetGroupPermission", new { uci = GroupPermission.TbGroupPermissionName }, GroupPermission);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroupPermission(GroupPermission GroupPermission)
        {           
            GroupPermission dbGroupPermission = await _GroupPermissionService.UpdateGroupPermission(GroupPermission);

            if (dbGroupPermission == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{GroupPermission.TbGroupPermissionName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroupPermission(GroupPermission GroupPermission)
        {            
            (bool status, string message) = await _GroupPermissionService.DeleteGroupPermission(GroupPermission);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, GroupPermission);
        }
    }
}
