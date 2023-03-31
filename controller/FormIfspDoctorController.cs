using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FormIfspDoctorController : ControllerBase
    {
        private readonly IFormIfspDoctorService _FormIfspDoctorService;

        public FormIfspDoctorController(IFormIfspDoctorService FormIfspDoctorService)
        {
            _FormIfspDoctorService = FormIfspDoctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspDoctorList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var FormIfspDoctors = await _FormIfspDoctorService.GetFormIfspDoctorListByValue(offset, limit, val);

            if (FormIfspDoctors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No FormIfspDoctors in database");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspDoctors);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspDoctorList(string FormIfspDoctor_name)
        {
            var FormIfspDoctors = await _FormIfspDoctorService.GetFormIfspDoctorList(FormIfspDoctor_name);

            if (FormIfspDoctors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspDoctor found for uci: {FormIfspDoctor_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspDoctors);
        }

        [HttpGet]
        public async Task<IActionResult> GetFormIfspDoctor(string FormIfspDoctor_name)
        {
            var FormIfspDoctors = await _FormIfspDoctorService.GetFormIfspDoctor(FormIfspDoctor_name);

            if (FormIfspDoctors == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No FormIfspDoctor found for uci: {FormIfspDoctor_name}");
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspDoctors);
        }

        [HttpPost]
        public async Task<ActionResult<FormIfspDoctor>> AddFormIfspDoctor(FormIfspDoctor FormIfspDoctor)
        {
            var dbFormIfspDoctor = await _FormIfspDoctorService.AddFormIfspDoctor(FormIfspDoctor);

            if (dbFormIfspDoctor == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspDoctor.TbFormIfspDoctorName} could not be added."
                );
            }

            return CreatedAtAction("GetFormIfspDoctor", new { uci = FormIfspDoctor.TbFormIfspDoctorName }, FormIfspDoctor);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateFormIfspDoctor(FormIfspDoctor FormIfspDoctor)
        {           
            FormIfspDoctor dbFormIfspDoctor = await _FormIfspDoctorService.UpdateFormIfspDoctor(FormIfspDoctor);

            if (dbFormIfspDoctor == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{FormIfspDoctor.TbFormIfspDoctorName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFormIfspDoctor(FormIfspDoctor FormIfspDoctor)
        {            
            (bool status, string message) = await _FormIfspDoctorService.DeleteFormIfspDoctor(FormIfspDoctor);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, FormIfspDoctor);
        }
    }
}
