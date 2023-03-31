using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsrateController : ControllerBase
    {
        private readonly IPsrateService _PsrateService;

        public PsrateController(IPsrateService PsrateService)
        {
            _PsrateService = PsrateService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPsrateList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Psrates = await _PsrateService.GetPsrateListByValue(offset, limit, val);

            if (Psrates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Psrates in database");
            }

            return StatusCode(StatusCodes.Status200OK, Psrates);
        }

        [HttpGet]
        public async Task<IActionResult> GetPsrateList(string Psrate_name)
        {
            var Psrates = await _PsrateService.GetPsrateList(Psrate_name);

            if (Psrates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Psrate found for uci: {Psrate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Psrates);
        }

        [HttpGet]
        public async Task<IActionResult> GetPsrate(string Psrate_name)
        {
            var Psrates = await _PsrateService.GetPsrate(Psrate_name);

            if (Psrates == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Psrate found for uci: {Psrate_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Psrates);
        }

        [HttpPost]
        public async Task<ActionResult<Psrate>> AddPsrate(Psrate Psrate)
        {
            var dbPsrate = await _PsrateService.AddPsrate(Psrate);

            if (dbPsrate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Psrate.TbPsrateName} could not be added."
                );
            }

            return CreatedAtAction("GetPsrate", new { uci = Psrate.TbPsrateName }, Psrate);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePsrate(Psrate Psrate)
        {           
            Psrate dbPsrate = await _PsrateService.UpdatePsrate(Psrate);

            if (dbPsrate == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Psrate.TbPsrateName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePsrate(Psrate Psrate)
        {            
            (bool status, string message) = await _PsrateService.DeletePsrate(Psrate);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Psrate);
        }
    }
}
