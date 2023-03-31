using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtcitationController : ControllerBase
    {
        private readonly IFormMtcitationService _FormMtcitationService;

        public FormMtcitationController(IFormMtcitationService FormMtcitationService)
        {
            _FormMtcitationService = FormMtcitationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtcitationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtcitations = await _FormMtcitationService.GetFormMtcitationListByValue(offset, limit, val);

            if (FormMtcitations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtcitations in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtcitations);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtcitationList(string FormMtcitation_name)
        {
            var FormMtcitations = await _FormMtcitationService.GetFormMtcitationList(FormMtcitation_name);

            if (FormMtcitations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtcitation found for uci: {FormMtcitation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtcitations);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtcitation(string FormMtcitation_name)
        {
            var FormMtcitations = await _FormMtcitationService.GetFormMtcitation(FormMtcitation_name);

            if (FormMtcitations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtcitation found for uci: {FormMtcitation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtcitations);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtcitation>> AddFormMtcitation(FormMtcitation FormMtcitation)
        {
            var dbFormMtcitation = await _FormMtcitationService.AddFormMtcitation(FormMtcitation);

            if (dbFormMtcitation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtcitation.TbFormMtcitationName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtcitation", new { uci = FormMtcitation.TbFormMtcitationName }, FormMtcitation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtcitation(FormMtcitation FormMtcitation)
        {           
            FormMtcitation dbFormMtcitation = await _FormMtcitationService.UpdateFormMtcitation(FormMtcitation);

            if (dbFormMtcitation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtcitation.TbFormMtcitationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtcitation(FormMtcitation FormMtcitation)
        {            
            (bool status, string message) = await _FormMtcitationService.DeleteFormMtcitation(FormMtcitation);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtcitation);
        }
    }
}
