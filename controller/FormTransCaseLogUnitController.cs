using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormTransCaseLogUnitController : ControllerBase
    {
        private readonly IFormTransCaseLogUnitService _FormTransCaseLogUnitService;

        public FormTransCaseLogUnitController(IFormTransCaseLogUnitService FormTransCaseLogUnitService)
        {
            _FormTransCaseLogUnitService = FormTransCaseLogUnitService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransCaseLogUnitList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormTransCaseLogUnits = await _FormTransCaseLogUnitService.GetFormTransCaseLogUnitListByValue(offset, limit, val);

            if (FormTransCaseLogUnits == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormTransCaseLogUnits in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogUnits);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransCaseLogUnitList(string FormTransCaseLogUnit_name)
        {
            var FormTransCaseLogUnits = await _FormTransCaseLogUnitService.GetFormTransCaseLogUnitList(FormTransCaseLogUnit_name);

            if (FormTransCaseLogUnits == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormTransCaseLogUnit found for uci: {FormTransCaseLogUnit_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogUnits);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormTransCaseLogUnit(string FormTransCaseLogUnit_name)
        {
            var FormTransCaseLogUnits = await _FormTransCaseLogUnitService.GetFormTransCaseLogUnit(FormTransCaseLogUnit_name);

            if (FormTransCaseLogUnits == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormTransCaseLogUnit found for uci: {FormTransCaseLogUnit_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogUnits);
        }

        [HttpPost]
        public async Task<ActionResult<FormTransCaseLogUnit>> AddFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit)
        {
            var dbFormTransCaseLogUnit = await _FormTransCaseLogUnitService.AddFormTransCaseLogUnit(FormTransCaseLogUnit);

            if (dbFormTransCaseLogUnit == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormTransCaseLogUnit.TbFormTransCaseLogUnitName} could not be added."
                );
            }

            return CreatedAtAction("GetFormTransCaseLogUnit", new { uci = FormTransCaseLogUnit.TbFormTransCaseLogUnitName }, FormTransCaseLogUnit);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit)
        {           
            FormTransCaseLogUnit dbFormTransCaseLogUnit = await _FormTransCaseLogUnitService.UpdateFormTransCaseLogUnit(FormTransCaseLogUnit);

            if (dbFormTransCaseLogUnit == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormTransCaseLogUnit.TbFormTransCaseLogUnitName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormTransCaseLogUnit(FormTransCaseLogUnit FormTransCaseLogUnit)
        {            
            (bool status, string message) = await _FormTransCaseLogUnitService.DeleteFormTransCaseLogUnit(FormTransCaseLogUnit);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormTransCaseLogUnit);
        }
    }
}
