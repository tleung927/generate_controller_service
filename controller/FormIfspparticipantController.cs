using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspparticipantController : ControllerBase
    {
        private readonly IFormIfspparticipantService _FormIfspparticipantService;

        public FormIfspparticipantController(IFormIfspparticipantService FormIfspparticipantService)
        {
            _FormIfspparticipantService = FormIfspparticipantService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspparticipantList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspparticipants = await _FormIfspparticipantService.GetFormIfspparticipantListByValue(offset, limit, val);

            if (FormIfspparticipants == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspparticipants in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspparticipants);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspparticipantList(string FormIfspparticipant_name)
        {
            var FormIfspparticipants = await _FormIfspparticipantService.GetFormIfspparticipantList(FormIfspparticipant_name);

            if (FormIfspparticipants == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspparticipant found for uci: {FormIfspparticipant_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspparticipants);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspparticipant(string FormIfspparticipant_name)
        {
            var FormIfspparticipants = await _FormIfspparticipantService.GetFormIfspparticipant(FormIfspparticipant_name);

            if (FormIfspparticipants == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspparticipant found for uci: {FormIfspparticipant_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspparticipants);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfspparticipant>> AddFormIfspparticipant(FormIfspparticipant FormIfspparticipant)
        {
            var dbFormIfspparticipant = await _FormIfspparticipantService.AddFormIfspparticipant(FormIfspparticipant);

            if (dbFormIfspparticipant == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspparticipant.TbFormIfspparticipantName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfspparticipant", new { uci = FormIfspparticipant.TbFormIfspparticipantName }, FormIfspparticipant);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfspparticipant(FormIfspparticipant FormIfspparticipant)
        {           
            FormIfspparticipant dbFormIfspparticipant = await _FormIfspparticipantService.UpdateFormIfspparticipant(FormIfspparticipant);

            if (dbFormIfspparticipant == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspparticipant.TbFormIfspparticipantName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfspparticipant(FormIfspparticipant FormIfspparticipant)
        {            
            (bool status, string message) = await _FormIfspparticipantService.DeleteFormIfspparticipant(FormIfspparticipant);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspparticipant);
        }
    }
}
