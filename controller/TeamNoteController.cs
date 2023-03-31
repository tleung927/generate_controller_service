using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamNoteController : ControllerBase
    {
        private readonly ITeamNoteService _TeamNoteService;

        public TeamNoteController(ITeamNoteService TeamNoteService)
        {
            _TeamNoteService = TeamNoteService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamNoteList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var TeamNotes = await _TeamNoteService.GetTeamNoteListByValue(offset, limit, val);

            if (TeamNotes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No TeamNotes in database");
            }

            return StatusCode(StatusCodes.Status200OK, TeamNotes);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamNoteList(string TeamNote_name)
        {
            var TeamNotes = await _TeamNoteService.GetTeamNoteList(TeamNote_name);

            if (TeamNotes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TeamNote found for uci: {TeamNote_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TeamNotes);
        }

        [HttpGet]
        public async Task<IActionResult> GetTeamNote(string TeamNote_name)
        {
            var TeamNotes = await _TeamNoteService.GetTeamNote(TeamNote_name);

            if (TeamNotes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No TeamNote found for uci: {TeamNote_name}");
            }

            return StatusCode(StatusCodes.Status200OK, TeamNotes);
        }

        [HttpPost]
        public async Task<ActionResult<TeamNote>> AddTeamNote(TeamNote TeamNote)
        {
            var dbTeamNote = await _TeamNoteService.AddTeamNote(TeamNote);

            if (dbTeamNote == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TeamNote.TbTeamNoteName} could not be added."
                );
            }

            return CreatedAtAction("GetTeamNote", new { uci = TeamNote.TbTeamNoteName }, TeamNote);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeamNote(TeamNote TeamNote)
        {           
            TeamNote dbTeamNote = await _TeamNoteService.UpdateTeamNote(TeamNote);

            if (dbTeamNote == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{TeamNote.TbTeamNoteName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTeamNote(TeamNote TeamNote)
        {            
            (bool status, string message) = await _TeamNoteService.DeleteTeamNote(TeamNote);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, TeamNote);
        }
    }
}
