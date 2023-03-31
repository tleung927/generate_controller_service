using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormScmonthController : ControllerBase
    {
        private readonly IFormScmonthService _FormScmonthService;

        public FormScmonthController(IFormScmonthService FormScmonthService)
        {
            _FormScmonthService = FormScmonthService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonthList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormScmonths = await _FormScmonthService.GetFormScmonthListByValue(offset, limit, val);

            if (FormScmonths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormScmonths in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonths);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonthList(string FormScmonth_name)
        {
            var FormScmonths = await _FormScmonthService.GetFormScmonthList(FormScmonth_name);

            if (FormScmonths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScmonth found for uci: {FormScmonth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonths);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormScmonth(string FormScmonth_name)
        {
            var FormScmonths = await _FormScmonthService.GetFormScmonth(FormScmonth_name);

            if (FormScmonths == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormScmonth found for uci: {FormScmonth_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonths);
        }

        [HttpPost]
        public async Task<ActionResult<FormScmonth>> AddFormScmonth(FormScmonth FormScmonth)
        {
            var dbFormScmonth = await _FormScmonthService.AddFormScmonth(FormScmonth);

            if (dbFormScmonth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScmonth.TbFormScmonthName} could not be added."
                );
            }

            return CreatedAtAction("GetFormScmonth", new { uci = FormScmonth.TbFormScmonthName }, FormScmonth);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormScmonth(FormScmonth FormScmonth)
        {           
            FormScmonth dbFormScmonth = await _FormScmonthService.UpdateFormScmonth(FormScmonth);

            if (dbFormScmonth == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormScmonth.TbFormScmonthName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormScmonth(FormScmonth FormScmonth)
        {            
            (bool status, string message) = await _FormScmonthService.DeleteFormScmonth(FormScmonth);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormScmonth);
        }
    }
}
