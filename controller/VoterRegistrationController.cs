using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VoterRegistrationController : ControllerBase
    {
        private readonly IVoterRegistrationService _VoterRegistrationService;

        public VoterRegistrationController(IVoterRegistrationService VoterRegistrationService)
        {
            _VoterRegistrationService = VoterRegistrationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetVoterRegistrationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var VoterRegistrations = await _VoterRegistrationService.GetVoterRegistrationListByValue(offset, limit, val);

            if (VoterRegistrations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No VoterRegistrations in database");
            }

            return StatusCode(StatusCodes.Status200OK, VoterRegistrations);
        }

        [HttpGet]
        public async Task<IActionResult> GetVoterRegistrationList(string VoterRegistration_name)
        {
            var VoterRegistrations = await _VoterRegistrationService.GetVoterRegistrationList(VoterRegistration_name);

            if (VoterRegistrations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No VoterRegistration found for uci: {VoterRegistration_name}");
            }

            return StatusCode(StatusCodes.Status200OK, VoterRegistrations);
        }

        [HttpGet]
        public async Task<IActionResult> GetVoterRegistration(string VoterRegistration_name)
        {
            var VoterRegistrations = await _VoterRegistrationService.GetVoterRegistration(VoterRegistration_name);

            if (VoterRegistrations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No VoterRegistration found for uci: {VoterRegistration_name}");
            }

            return StatusCode(StatusCodes.Status200OK, VoterRegistrations);
        }

        [HttpPost]
        public async Task<ActionResult<VoterRegistration>> AddVoterRegistration(VoterRegistration VoterRegistration)
        {
            var dbVoterRegistration = await _VoterRegistrationService.AddVoterRegistration(VoterRegistration);

            if (dbVoterRegistration == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{VoterRegistration.TbVoterRegistrationName} could not be added."
                );
            }

            return CreatedAtAction("GetVoterRegistration", new { uci = VoterRegistration.TbVoterRegistrationName }, VoterRegistration);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateVoterRegistration(VoterRegistration VoterRegistration)
        {           
            VoterRegistration dbVoterRegistration = await _VoterRegistrationService.UpdateVoterRegistration(VoterRegistration);

            if (dbVoterRegistration == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{VoterRegistration.TbVoterRegistrationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVoterRegistration(VoterRegistration VoterRegistration)
        {            
            (bool status, string message) = await _VoterRegistrationService.DeleteVoterRegistration(VoterRegistration);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, VoterRegistration);
        }
    }
}
