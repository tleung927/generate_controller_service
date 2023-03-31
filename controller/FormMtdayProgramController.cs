using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtdayProgramController : ControllerBase
    {
        private readonly IFormMtdayProgramService _FormMtdayProgramService;

        public FormMtdayProgramController(IFormMtdayProgramService FormMtdayProgramService)
        {
            _FormMtdayProgramService = FormMtdayProgramService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtdayProgramList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtdayPrograms = await _FormMtdayProgramService.GetFormMtdayProgramListByValue(offset, limit, val);

            if (FormMtdayPrograms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtdayPrograms in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdayPrograms);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtdayProgramList(string FormMtdayProgram_name)
        {
            var FormMtdayPrograms = await _FormMtdayProgramService.GetFormMtdayProgramList(FormMtdayProgram_name);

            if (FormMtdayPrograms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtdayProgram found for uci: {FormMtdayProgram_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdayPrograms);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtdayProgram(string FormMtdayProgram_name)
        {
            var FormMtdayPrograms = await _FormMtdayProgramService.GetFormMtdayProgram(FormMtdayProgram_name);

            if (FormMtdayPrograms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtdayProgram found for uci: {FormMtdayProgram_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdayPrograms);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtdayProgram>> AddFormMtdayProgram(FormMtdayProgram FormMtdayProgram)
        {
            var dbFormMtdayProgram = await _FormMtdayProgramService.AddFormMtdayProgram(FormMtdayProgram);

            if (dbFormMtdayProgram == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtdayProgram.TbFormMtdayProgramName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtdayProgram", new { uci = FormMtdayProgram.TbFormMtdayProgramName }, FormMtdayProgram);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtdayProgram(FormMtdayProgram FormMtdayProgram)
        {           
            FormMtdayProgram dbFormMtdayProgram = await _FormMtdayProgramService.UpdateFormMtdayProgram(FormMtdayProgram);

            if (dbFormMtdayProgram == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtdayProgram.TbFormMtdayProgramName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtdayProgram(FormMtdayProgram FormMtdayProgram)
        {            
            (bool status, string message) = await _FormMtdayProgramService.DeleteFormMtdayProgram(FormMtdayProgram);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdayProgram);
        }
    }
}
