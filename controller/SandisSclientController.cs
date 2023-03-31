using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SandisSclientController : ControllerBase
    {
        private readonly ISandisSclientService _SandisSclientService;

        public SandisSclientController(ISandisSclientService SandisSclientService)
        {
            _SandisSclientService = SandisSclientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSclientList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var SandisSclients = await _SandisSclientService.GetSandisSclientListByValue(offset, limit, val);

            if (SandisSclients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No SandisSclients in database");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclients);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSclientList(string SandisSclient_name)
        {
            var SandisSclients = await _SandisSclientService.GetSandisSclientList(SandisSclient_name);

            if (SandisSclients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSclient found for uci: {SandisSclient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclients);
        }

        [HttpGet]
        public async Task<IActionResult> GetSandisSclient(string SandisSclient_name)
        {
            var SandisSclients = await _SandisSclientService.GetSandisSclient(SandisSclient_name);

            if (SandisSclients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No SandisSclient found for uci: {SandisSclient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclients);
        }

        [HttpPost]
        public async Task<ActionResult<SandisSclient>> AddSandisSclient(SandisSclient SandisSclient)
        {
            var dbSandisSclient = await _SandisSclientService.AddSandisSclient(SandisSclient);

            if (dbSandisSclient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSclient.TbSandisSclientName} could not be added."
                );
            }

            return CreatedAtAction("GetSandisSclient", new { uci = SandisSclient.TbSandisSclientName }, SandisSclient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSandisSclient(SandisSclient SandisSclient)
        {           
            SandisSclient dbSandisSclient = await _SandisSclientService.UpdateSandisSclient(SandisSclient);

            if (dbSandisSclient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{SandisSclient.TbSandisSclientName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSandisSclient(SandisSclient SandisSclient)
        {            
            (bool status, string message) = await _SandisSclientService.DeleteSandisSclient(SandisSclient);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, SandisSclient);
        }
    }
}
