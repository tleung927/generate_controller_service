using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AcronymController : ControllerBase
    {
        private readonly IAcronymService _AcronymService;

        public AcronymController(IAcronymService AcronymService)
        {
            _AcronymService = AcronymService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAcronymList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Acronyms = await _AcronymService.GetAcronymListByValue(offset, limit, val);

            if (Acronyms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Acronyms in database");
            }

            return StatusCode(StatusCodes.Status200OK, Acronyms);
        }

        [HttpGet]
        public async Task<IActionResult> GetAcronymList(string Acronym_name)
        {
            var Acronyms = await _AcronymService.GetAcronymList(Acronym_name);

            if (Acronyms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Acronym found for uci: {Acronym_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Acronyms);
        }

        [HttpGet]
        public async Task<IActionResult> GetAcronym(string Acronym_name)
        {
            var Acronyms = await _AcronymService.GetAcronym(Acronym_name);

            if (Acronyms == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Acronym found for uci: {Acronym_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Acronyms);
        }

        [HttpPost]
        public async Task<ActionResult<Acronym>> AddAcronym(Acronym Acronym)
        {
            var dbAcronym = await _AcronymService.AddAcronym(Acronym);

            if (dbAcronym == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Acronym.TbAcronymName} could not be added."
                );
            }

            return CreatedAtAction("GetAcronym", new { uci = Acronym.TbAcronymName }, Acronym);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAcronym(Acronym Acronym)
        {           
            Acronym dbAcronym = await _AcronymService.UpdateAcronym(Acronym);

            if (dbAcronym == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Acronym.TbAcronymName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAcronym(Acronym Acronym)
        {            
            (bool status, string message) = await _AcronymService.DeleteAcronym(Acronym);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Acronym);
        }
    }
}
