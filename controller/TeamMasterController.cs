using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamMasterController : ControllerBase
    {
        private readonly ITeamMasterService _TeamMasterService;

        public TeamMasterController(ITeamMasterService TeamMasterService)
        {
            _TeamMasterService = TeamMasterService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamMasterList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TeamMasters = await _TeamMasterService.GetTeamMasterListByValue(offset, limit, val);

            if (TeamMasters == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TeamMasters in database");
            }

            return StatusCode(StatusCodes.Status200OK, TeamMasters);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamMasterList(string TeamMaster_name)
        {
            var TeamMasters = await _TeamMasterService.GetTeamMasterList(TeamMaster_name);

            if (TeamMasters == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TeamMaster found for uci: {TeamMaster_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TeamMasters);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamMaster(string TeamMaster_name)
        {
            var TeamMasters = await _TeamMasterService.GetTeamMaster(TeamMaster_name);

            if (TeamMasters == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TeamMaster found for uci: {TeamMaster_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TeamMasters);
        }

        [HttpPost]
        public async Task<ActionResult<TeamMaster>> AddTeamMaster(TeamMaster TeamMaster)
        {
            var dbTeamMaster = await _TeamMasterService.AddTeamMaster(TeamMaster);

            if (dbTeamMaster == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TeamMaster.TbTeamMasterName} could not be added."
                );
            }

            return CreatedAtAction("GetTeamMaster", new { uci = TeamMaster.TbTeamMasterName }, TeamMaster);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeamMaster(TeamMaster TeamMaster)
        {           
            TeamMaster dbTeamMaster = await _TeamMasterService.UpdateTeamMaster(TeamMaster);

            if (dbTeamMaster == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TeamMaster.TbTeamMasterName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeamMaster(TeamMaster TeamMaster)
        {            
            (bool status, string message) = await _TeamMasterService.DeleteTeamMaster(TeamMaster);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TeamMaster);
        }
    }
}
