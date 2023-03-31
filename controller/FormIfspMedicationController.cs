using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspMedicationController : ControllerBase
    {
        private readonly IFormIfspMedicationService _FormIfspMedicationService;

        public FormIfspMedicationController(IFormIfspMedicationService FormIfspMedicationService)
        {
            _FormIfspMedicationService = FormIfspMedicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspMedicationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspMedications = await _FormIfspMedicationService.GetFormIfspMedicationListByValue(offset, limit, val);

            if (FormIfspMedications == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspMedications in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspMedications);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspMedicationList(string FormIfspMedication_name)
        {
            var FormIfspMedications = await _FormIfspMedicationService.GetFormIfspMedicationList(FormIfspMedication_name);

            if (FormIfspMedications == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspMedication found for uci: {FormIfspMedication_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspMedications);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspMedication(string FormIfspMedication_name)
        {
            var FormIfspMedications = await _FormIfspMedicationService.GetFormIfspMedication(FormIfspMedication_name);

            if (FormIfspMedications == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspMedication found for uci: {FormIfspMedication_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspMedications);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfspMedication>> AddFormIfspMedication(FormIfspMedication FormIfspMedication)
        {
            var dbFormIfspMedication = await _FormIfspMedicationService.AddFormIfspMedication(FormIfspMedication);

            if (dbFormIfspMedication == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspMedication.TbFormIfspMedicationName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfspMedication", new { uci = FormIfspMedication.TbFormIfspMedicationName }, FormIfspMedication);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfspMedication(FormIfspMedication FormIfspMedication)
        {           
            FormIfspMedication dbFormIfspMedication = await _FormIfspMedicationService.UpdateFormIfspMedication(FormIfspMedication);

            if (dbFormIfspMedication == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspMedication.TbFormIfspMedicationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfspMedication(FormIfspMedication FormIfspMedication)
        {            
            (bool status, string message) = await _FormIfspMedicationService.DeleteFormIfspMedication(FormIfspMedication);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspMedication);
        }
    }
}
