using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIppoutController : ControllerBase
    {
        private readonly IFormIppoutService _FormIppoutService;

        public FormIppoutController(IFormIppoutService FormIppoutService)
        {
            _FormIppoutService = FormIppoutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppoutList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIppouts = await _FormIppoutService.GetFormIppoutListByValue(offset, limit, val);

            if (FormIppouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIppouts in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppouts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppoutList(string FormIppout_name)
        {
            var FormIppouts = await _FormIppoutService.GetFormIppoutList(FormIppout_name);

            if (FormIppouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppout found for uci: {FormIppout_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppouts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIppout(string FormIppout_name)
        {
            var FormIppouts = await _FormIppoutService.GetFormIppout(FormIppout_name);

            if (FormIppouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIppout found for uci: {FormIppout_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIppouts);
        }

        [HttpPost]
        public async Task<ActionResult<FormIppout>> AddFormIppout(FormIppout FormIppout)
        {
            var dbFormIppout = await _FormIppoutService.AddFormIppout(FormIppout);

            if (dbFormIppout == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppout.TbFormIppoutName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIppout", new { uci = FormIppout.TbFormIppoutName }, FormIppout);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIppout(FormIppout FormIppout)
        {           
            FormIppout dbFormIppout = await _FormIppoutService.UpdateFormIppout(FormIppout);

            if (dbFormIppout == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIppout.TbFormIppoutName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIppout(FormIppout FormIppout)
        {            
            (bool status, string message) = await _FormIppoutService.DeleteFormIppout(FormIppout);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIppout);
        }
    }
}
