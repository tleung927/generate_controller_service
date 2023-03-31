using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AbbreviationController : ControllerBase
    {
        private readonly IAbbreviationService _AbbreviationService;

        public AbbreviationController(IAbbreviationService AbbreviationService)
        {
            _AbbreviationService = AbbreviationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAbbreviationList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Abbreviations = await _AbbreviationService.GetAbbreviationListByValue(offset, limit, val);

            if (Abbreviations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Abbreviations in database");
            }

            return StatusCode(StatusCodes.Status200OK, Abbreviations);
        }

        [HttpGet]
        public async Task<IActionResult> GetAbbreviationList(string Abbreviation_name)
        {
            var Abbreviations = await _AbbreviationService.GetAbbreviationList(Abbreviation_name);

            if (Abbreviations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Abbreviation found for uci: {Abbreviation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Abbreviations);
        }

        [HttpGet]
        public async Task<IActionResult> GetAbbreviation(string Abbreviation_name)
        {
            var Abbreviations = await _AbbreviationService.GetAbbreviation(Abbreviation_name);

            if (Abbreviations == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Abbreviation found for uci: {Abbreviation_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Abbreviations);
        }

        [HttpPost]
        public async Task<ActionResult<Abbreviation>> AddAbbreviation(Abbreviation Abbreviation)
        {
            var dbAbbreviation = await _AbbreviationService.AddAbbreviation(Abbreviation);

            if (dbAbbreviation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Abbreviation.TbAbbreviationName} could not be added."
                );
            }

            return CreatedAtAction("GetAbbreviation", new { uci = Abbreviation.TbAbbreviationName }, Abbreviation);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAbbreviation(Abbreviation Abbreviation)
        {           
            Abbreviation dbAbbreviation = await _AbbreviationService.UpdateAbbreviation(Abbreviation);

            if (dbAbbreviation == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Abbreviation.TbAbbreviationName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAbbreviation(Abbreviation Abbreviation)
        {            
            (bool status, string message) = await _AbbreviationService.DeleteAbbreviation(Abbreviation);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Abbreviation);
        }
    }
}
