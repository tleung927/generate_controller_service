using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamScheduleController : ControllerBase
    {
        private readonly ITeamScheduleService _TeamScheduleService;

        public TeamScheduleController(ITeamScheduleService TeamScheduleService)
        {
            _TeamScheduleService = TeamScheduleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamScheduleList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TeamSchedules = await _TeamScheduleService.GetTeamScheduleListByValue(offset, limit, val);

            if (TeamSchedules == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TeamSchedules in database");
            }

            return StatusCode(StatusCodes.Status200OK, TeamSchedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamScheduleList(string TeamSchedule_name)
        {
            var TeamSchedules = await _TeamScheduleService.GetTeamScheduleList(TeamSchedule_name);

            if (TeamSchedules == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TeamSchedule found for uci: {TeamSchedule_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TeamSchedules);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamSchedule(string TeamSchedule_name)
        {
            var TeamSchedules = await _TeamScheduleService.GetTeamSchedule(TeamSchedule_name);

            if (TeamSchedules == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TeamSchedule found for uci: {TeamSchedule_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TeamSchedules);
        }

        [HttpPost]
        public async Task<ActionResult<TeamSchedule>> AddTeamSchedule(TeamSchedule TeamSchedule)
        {
            var dbTeamSchedule = await _TeamScheduleService.AddTeamSchedule(TeamSchedule);

            if (dbTeamSchedule == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TeamSchedule.TbTeamScheduleName} could not be added."
                );
            }

            return CreatedAtAction("GetTeamSchedule", new { uci = TeamSchedule.TbTeamScheduleName }, TeamSchedule);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeamSchedule(TeamSchedule TeamSchedule)
        {           
            TeamSchedule dbTeamSchedule = await _TeamScheduleService.UpdateTeamSchedule(TeamSchedule);

            if (dbTeamSchedule == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TeamSchedule.TbTeamScheduleName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeamSchedule(TeamSchedule TeamSchedule)
        {            
            (bool status, string message) = await _TeamScheduleService.DeleteTeamSchedule(TeamSchedule);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TeamSchedule);
        }
    }
}
