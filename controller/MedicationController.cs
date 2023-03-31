using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedicationController : ControllerBase
    {
        private readonly IMedicationService _MedicationService;

        public MedicationController(IMedicationService MedicationService)
        {
            _MedicationService = MedicationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Medications = await _MedicationService.GetMedicationListByValue(offset, limit, val);

            if (Medications == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Medications in database");
            }

            return StatusCode(StatusCodes.Status200OK, Medications);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedicationList(string Medication_name)
        {
            var Medications = await _MedicationService.GetMedicationList(Medication_name);

            if (Medications == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Medication found for uci: {Medication_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Medications);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedication(string Medication_name)
        {
            var Medications = await _MedicationService.GetMedication(Medication_name);

            if (Medications == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Medication found for uci: {Medication_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Medications);
        }

        [HttpPost]
        public async Task<ActionResult<Medication>> AddMedication(Medication Medication)
        {
            var dbMedication = await _MedicationService.AddMedication(Medication);

            if (dbMedication == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Medication.TbMedicationName} could not be added."
                );
            }

            return CreatedAtAction("GetMedication", new { uci = Medication.TbMedicationName }, Medication);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedication(Medication Medication)
        {           
            Medication dbMedication = await _MedicationService.UpdateMedication(Medication);

            if (dbMedication == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Medication.TbMedicationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedication(Medication Medication)
        {            
            (bool status, string message) = await _MedicationService.DeleteMedication(Medication);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Medication);
        }
    }
}
