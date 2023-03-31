using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormFairHearingController : ControllerBase
    {
        private readonly IFormFairHearingService _FormFairHearingService;

        public FormFairHearingController(IFormFairHearingService FormFairHearingService)
        {
            _FormFairHearingService = FormFairHearingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormFairHearingList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormFairHearings = await _FormFairHearingService.GetFormFairHearingListByValue(offset, limit, val);

            if (FormFairHearings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormFairHearings in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormFairHearings);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormFairHearingList(string FormFairHearing_name)
        {
            var FormFairHearings = await _FormFairHearingService.GetFormFairHearingList(FormFairHearing_name);

            if (FormFairHearings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormFairHearing found for uci: {FormFairHearing_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormFairHearings);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormFairHearing(string FormFairHearing_name)
        {
            var FormFairHearings = await _FormFairHearingService.GetFormFairHearing(FormFairHearing_name);

            if (FormFairHearings == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormFairHearing found for uci: {FormFairHearing_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormFairHearings);
        }

        [HttpPost]
        public async Task<ActionResult<FormFairHearing>> AddFormFairHearing(FormFairHearing FormFairHearing)
        {
            var dbFormFairHearing = await _FormFairHearingService.AddFormFairHearing(FormFairHearing);

            if (dbFormFairHearing == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormFairHearing.TbFormFairHearingName} could not be added."
                );
            }

            return CreatedAtAction("GetFormFairHearing", new { uci = FormFairHearing.TbFormFairHearingName }, FormFairHearing);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormFairHearing(FormFairHearing FormFairHearing)
        {           
            FormFairHearing dbFormFairHearing = await _FormFairHearingService.UpdateFormFairHearing(FormFairHearing);

            if (dbFormFairHearing == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormFairHearing.TbFormFairHearingName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormFairHearing(FormFairHearing FormFairHearing)
        {            
            (bool status, string message) = await _FormFairHearingService.DeleteFormFairHearing(FormFairHearing);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormFairHearing);
        }
    }
}
