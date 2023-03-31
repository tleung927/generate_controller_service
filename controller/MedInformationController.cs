using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MedInformationController : ControllerBase
    {
        private readonly IMedInformationService _MedInformationService;

        public MedInformationController(IMedInformationService MedInformationService)
        {
            _MedInformationService = MedInformationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetMedInformationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var MedInformations = await _MedInformationService.GetMedInformationListByValue(offset, limit, val);

            if (MedInformations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No MedInformations in database");
            }

            return StatusCode(StatusCodes.Status200OK, MedInformations);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedInformationList(string MedInformation_name)
        {
            var MedInformations = await _MedInformationService.GetMedInformationList(MedInformation_name);

            if (MedInformations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No MedInformation found for uci: {MedInformation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, MedInformations);
        }

        [HttpGet]
        public async Task<IActionResult> GetMedInformation(string MedInformation_name)
        {
            var MedInformations = await _MedInformationService.GetMedInformation(MedInformation_name);

            if (MedInformations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No MedInformation found for uci: {MedInformation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, MedInformations);
        }

        [HttpPost]
        public async Task<ActionResult<MedInformation>> AddMedInformation(MedInformation MedInformation)
        {
            var dbMedInformation = await _MedInformationService.AddMedInformation(MedInformation);

            if (dbMedInformation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{MedInformation.TbMedInformationName} could not be added."
                );
            }

            return CreatedAtAction("GetMedInformation", new { uci = MedInformation.TbMedInformationName }, MedInformation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMedInformation(MedInformation MedInformation)
        {           
            MedInformation dbMedInformation = await _MedInformationService.UpdateMedInformation(MedInformation);

            if (dbMedInformation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{MedInformation.TbMedInformationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMedInformation(MedInformation MedInformation)
        {            
            (bool status, string message) = await _MedInformationService.DeleteMedInformation(MedInformation);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, MedInformation);
        }
    }
}
