using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspoutController : ControllerBase
    {
        private readonly IFormIfspoutService _FormIfspoutService;

        public FormIfspoutController(IFormIfspoutService FormIfspoutService)
        {
            _FormIfspoutService = FormIfspoutService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspoutList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspouts = await _FormIfspoutService.GetFormIfspoutListByValue(offset, limit, val);

            if (FormIfspouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspouts in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspouts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspoutList(string FormIfspout_name)
        {
            var FormIfspouts = await _FormIfspoutService.GetFormIfspoutList(FormIfspout_name);

            if (FormIfspouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspout found for uci: {FormIfspout_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspouts);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspout(string FormIfspout_name)
        {
            var FormIfspouts = await _FormIfspoutService.GetFormIfspout(FormIfspout_name);

            if (FormIfspouts == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspout found for uci: {FormIfspout_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspouts);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfspout>> AddFormIfspout(FormIfspout FormIfspout)
        {
            var dbFormIfspout = await _FormIfspoutService.AddFormIfspout(FormIfspout);

            if (dbFormIfspout == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspout.TbFormIfspoutName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfspout", new { uci = FormIfspout.TbFormIfspoutName }, FormIfspout);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfspout(FormIfspout FormIfspout)
        {           
            FormIfspout dbFormIfspout = await _FormIfspoutService.UpdateFormIfspout(FormIfspout);

            if (dbFormIfspout == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspout.TbFormIfspoutName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfspout(FormIfspout FormIfspout)
        {            
            (bool status, string message) = await _FormIfspoutService.DeleteFormIfspout(FormIfspout);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspout);
        }
    }
}
