using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsppylController : ControllerBase
    {
        private readonly IPsppylService _PsppylService;

        public PsppylController(IPsppylService PsppylService)
        {
            _PsppylService = PsppylService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPsppylList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Psppyls = await _PsppylService.GetPsppylListByValue(offset, limit, val);

            if (Psppyls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Psppyls in database");
            }

            return StatusCode(StatusCodes.Status200OK, Psppyls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPsppylList(string Psppyl_name)
        {
            var Psppyls = await _PsppylService.GetPsppylList(Psppyl_name);

            if (Psppyls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Psppyl found for uci: {Psppyl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Psppyls);
        }

        [HttpGet]
        public async Task<IActionResult> GetPsppyl(string Psppyl_name)
        {
            var Psppyls = await _PsppylService.GetPsppyl(Psppyl_name);

            if (Psppyls == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Psppyl found for uci: {Psppyl_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Psppyls);
        }

        [HttpPost]
        public async Task<ActionResult<Psppyl>> AddPsppyl(Psppyl Psppyl)
        {
            var dbPsppyl = await _PsppylService.AddPsppyl(Psppyl);

            if (dbPsppyl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Psppyl.TbPsppylName} could not be added."
                );
            }

            return CreatedAtAction("GetPsppyl", new { uci = Psppyl.TbPsppylName }, Psppyl);
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePsppyl(Psppyl Psppyl)
        {           
            Psppyl dbPsppyl = await _PsppylService.UpdatePsppyl(Psppyl);

            if (dbPsppyl == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Psppyl.TbPsppylName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeletePsppyl(Psppyl Psppyl)
        {            
            (bool status, string message) = await _PsppylService.DeletePsppyl(Psppyl);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Psppyl);
        }
    }
}
