using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _GroupService;

        public GroupController(IGroupService GroupService)
        {
            _GroupService = GroupService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Groups = await _GroupService.GetGroupListByValue(offset, limit, val);

            if (Groups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Groups in database");
            }

            return StatusCode(StatusCodes.Status200OK, Groups);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroupList(string Group_name)
        {
            var Groups = await _GroupService.GetGroupList(Group_name);

            if (Groups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Group found for uci: {Group_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Groups);
        }

        [HttpGet]
        public async Task<IActionResult> GetGroup(string Group_name)
        {
            var Groups = await _GroupService.GetGroup(Group_name);

            if (Groups == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Group found for uci: {Group_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Groups);
        }

        [HttpPost]
        public async Task<ActionResult<Group>> AddGroup(Group Group)
        {
            var dbGroup = await _GroupService.AddGroup(Group);

            if (dbGroup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Group.TbGroupName} could not be added."
                );
            }

            return CreatedAtAction("GetGroup", new { uci = Group.TbGroupName }, Group);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGroup(Group Group)
        {           
            Group dbGroup = await _GroupService.UpdateGroup(Group);

            if (dbGroup == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Group.TbGroupName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteGroup(Group Group)
        {            
            (bool status, string message) = await _GroupService.DeleteGroup(Group);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Group);
        }
    }
}
