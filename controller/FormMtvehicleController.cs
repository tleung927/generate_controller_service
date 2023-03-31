using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormMtvehicleController : ControllerBase
    {
        private readonly IFormMtvehicleService _FormMtvehicleService;

        public FormMtvehicleController(IFormMtvehicleService FormMtvehicleService)
        {
            _FormMtvehicleService = FormMtvehicleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtvehicleList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormMtvehicles = await _FormMtvehicleService.GetFormMtvehicleListByValue(offset, limit, val);

            if (FormMtvehicles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormMtvehicles in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtvehicles);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtvehicleList(string FormMtvehicle_name)
        {
            var FormMtvehicles = await _FormMtvehicleService.GetFormMtvehicleList(FormMtvehicle_name);

            if (FormMtvehicles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtvehicle found for uci: {FormMtvehicle_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtvehicles);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormMtvehicle(string FormMtvehicle_name)
        {
            var FormMtvehicles = await _FormMtvehicleService.GetFormMtvehicle(FormMtvehicle_name);

            if (FormMtvehicles == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormMtvehicle found for uci: {FormMtvehicle_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormMtvehicles);
        }

        [HttpPost]
        public async Task<ActionResult<FormMtvehicle>> AddFormMtvehicle(FormMtvehicle FormMtvehicle)
        {
            var dbFormMtvehicle = await _FormMtvehicleService.AddFormMtvehicle(FormMtvehicle);

            if (dbFormMtvehicle == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtvehicle.TbFormMtvehicleName} could not be added."
                );
            }

            return CreatedAtAction("GetFormMtvehicle", new { uci = FormMtvehicle.TbFormMtvehicleName }, FormMtvehicle);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormMtvehicle(FormMtvehicle FormMtvehicle)
        {           
            FormMtvehicle dbFormMtvehicle = await _FormMtvehicleService.UpdateFormMtvehicle(FormMtvehicle);

            if (dbFormMtvehicle == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormMtvehicle.TbFormMtvehicleName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormMtvehicle(FormMtvehicle FormMtvehicle)
        {            
            (bool status, string message) = await _FormMtvehicleService.DeleteFormMtvehicle(FormMtvehicle);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormMtvehicle);
        }
    }
}
