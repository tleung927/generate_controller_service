using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedAppointmentController : ControllerBase
    {
        private readonly IMedAppointmentService _MedAppointmentService;

        public MedAppointmentController(IMedAppointmentService MedAppointmentService)
        {
            _MedAppointmentService = MedAppointmentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedAppointmentList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var MedAppointments = await _MedAppointmentService.GetMedAppointmentListByValue(offset, limit, val);

            if (MedAppointments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No MedAppointments in database");
            }

            return StatusCode(StatusCodes.Status200OK, MedAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedAppointmentList(string MedAppointment_name)
        {
            var MedAppointments = await _MedAppointmentService.GetMedAppointmentList(MedAppointment_name);

            if (MedAppointments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No MedAppointment found for uci: {MedAppointment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, MedAppointments);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedAppointment(string MedAppointment_name)
        {
            var MedAppointments = await _MedAppointmentService.GetMedAppointment(MedAppointment_name);

            if (MedAppointments == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No MedAppointment found for uci: {MedAppointment_name}");
            }

            return StatusCode(StatusCodes.Status200OK, MedAppointments);
        }

        [HttpPost]
        public async Task<ActionResult<MedAppointment>> AddMedAppointment(MedAppointment MedAppointment)
        {
            var dbMedAppointment = await _MedAppointmentService.AddMedAppointment(MedAppointment);

            if (dbMedAppointment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{MedAppointment.TbMedAppointmentName} could not be added."
                );
            }

            return CreatedAtAction("GetMedAppointment", new { uci = MedAppointment.TbMedAppointmentName }, MedAppointment);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedAppointment(MedAppointment MedAppointment)
        {           
            MedAppointment dbMedAppointment = await _MedAppointmentService.UpdateMedAppointment(MedAppointment);

            if (dbMedAppointment == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{MedAppointment.TbMedAppointmentName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedAppointment(MedAppointment MedAppointment)
        {            
            (bool status, string message) = await _MedAppointmentService.DeleteMedAppointment(MedAppointment);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, MedAppointment);
        }
    }
}
