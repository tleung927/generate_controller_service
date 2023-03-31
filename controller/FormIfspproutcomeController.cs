using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspproutcomeController : ControllerBase
    {
        private readonly IFormIfspproutcomeService _FormIfspproutcomeService;

        public FormIfspproutcomeController(IFormIfspproutcomeService FormIfspproutcomeService)
        {
            _FormIfspproutcomeService = FormIfspproutcomeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspproutcomeList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspproutcomes = await _FormIfspproutcomeService.GetFormIfspproutcomeListByValue(offset, limit, val);

            if (FormIfspproutcomes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspproutcomes in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspproutcomes);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspproutcomeList(string FormIfspproutcome_name)
        {
            var FormIfspproutcomes = await _FormIfspproutcomeService.GetFormIfspproutcomeList(FormIfspproutcome_name);

            if (FormIfspproutcomes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspproutcome found for uci: {FormIfspproutcome_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspproutcomes);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspproutcome(string FormIfspproutcome_name)
        {
            var FormIfspproutcomes = await _FormIfspproutcomeService.GetFormIfspproutcome(FormIfspproutcome_name);

            if (FormIfspproutcomes == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspproutcome found for uci: {FormIfspproutcome_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspproutcomes);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfspproutcome>> AddFormIfspproutcome(FormIfspproutcome FormIfspproutcome)
        {
            var dbFormIfspproutcome = await _FormIfspproutcomeService.AddFormIfspproutcome(FormIfspproutcome);

            if (dbFormIfspproutcome == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspproutcome.TbFormIfspproutcomeName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfspproutcome", new { uci = FormIfspproutcome.TbFormIfspproutcomeName }, FormIfspproutcome);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfspproutcome(FormIfspproutcome FormIfspproutcome)
        {           
            FormIfspproutcome dbFormIfspproutcome = await _FormIfspproutcomeService.UpdateFormIfspproutcome(FormIfspproutcome);

            if (dbFormIfspproutcome == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspproutcome.TbFormIfspproutcomeName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfspproutcome(FormIfspproutcome FormIfspproutcome)
        {            
            (bool status, string message) = await _FormIfspproutcomeService.DeleteFormIfspproutcome(FormIfspproutcome);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspproutcome);
        }
    }
}
