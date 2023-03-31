using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtdriverController : ControllerBase
    {
        private readonly IFormMtdriverService _FormMtdriverService;

        public FormMtdriverController(IFormMtdriverService FormMtdriverService)
        {
            _FormMtdriverService = FormMtdriverService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtdriverList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtdrivers = await _FormMtdriverService.GetFormMtdriverListByValue(offset, limit, val);

            if (FormMtdrivers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtdrivers in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdrivers);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtdriverList(string FormMtdriver_name)
        {
            var FormMtdrivers = await _FormMtdriverService.GetFormMtdriverList(FormMtdriver_name);

            if (FormMtdrivers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtdriver found for uci: {FormMtdriver_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdrivers);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtdriver(string FormMtdriver_name)
        {
            var FormMtdrivers = await _FormMtdriverService.GetFormMtdriver(FormMtdriver_name);

            if (FormMtdrivers == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtdriver found for uci: {FormMtdriver_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdrivers);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtdriver>> AddFormMtdriver(FormMtdriver FormMtdriver)
        {
            var dbFormMtdriver = await _FormMtdriverService.AddFormMtdriver(FormMtdriver);

            if (dbFormMtdriver == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtdriver.TbFormMtdriverName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtdriver", new { uci = FormMtdriver.TbFormMtdriverName }, FormMtdriver);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtdriver(FormMtdriver FormMtdriver)
        {           
            FormMtdriver dbFormMtdriver = await _FormMtdriverService.UpdateFormMtdriver(FormMtdriver);

            if (dbFormMtdriver == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtdriver.TbFormMtdriverName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtdriver(FormMtdriver FormMtdriver)
        {            
            (bool status, string message) = await _FormMtdriverService.DeleteFormMtdriver(FormMtdriver);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtdriver);
        }
    }
}
