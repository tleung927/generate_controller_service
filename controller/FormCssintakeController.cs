using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormCssintakeController : ControllerBase
    {
        private readonly IFormCssintakeService _FormCssintakeService;

        public FormCssintakeController(IFormCssintakeService FormCssintakeService)
        {
            _FormCssintakeService = FormCssintakeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormCssintakeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormCssintakes = await _FormCssintakeService.GetFormCssintakeListByValue(offset, limit, val);

            if (FormCssintakes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormCssintakes in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormCssintakes);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormCssintakeList(string FormCssintake_name)
        {
            var FormCssintakes = await _FormCssintakeService.GetFormCssintakeList(FormCssintake_name);

            if (FormCssintakes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormCssintake found for uci: {FormCssintake_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormCssintakes);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormCssintake(string FormCssintake_name)
        {
            var FormCssintakes = await _FormCssintakeService.GetFormCssintake(FormCssintake_name);

            if (FormCssintakes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormCssintake found for uci: {FormCssintake_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormCssintakes);
        }

        [HttpPost]
        public async Task<ActionResult<FormCssintake>> AddFormCssintake(FormCssintake FormCssintake)
        {
            var dbFormCssintake = await _FormCssintakeService.AddFormCssintake(FormCssintake);

            if (dbFormCssintake == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormCssintake.TbFormCssintakeName} could not be added."
                );
            }

            return CreatedAtAction("GetFormCssintake", new { uci = FormCssintake.TbFormCssintakeName }, FormCssintake);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormCssintake(FormCssintake FormCssintake)
        {           
            FormCssintake dbFormCssintake = await _FormCssintakeService.UpdateFormCssintake(FormCssintake);

            if (dbFormCssintake == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormCssintake.TbFormCssintakeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormCssintake(FormCssintake FormCssintake)
        {            
            (bool status, string message) = await _FormCssintakeService.DeleteFormCssintake(FormCssintake);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormCssintake);
        }
    }
}
