using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclientController : ControllerBase
    {
        private readonly ISclientService _SclientService;

        public SclientController(ISclientService SclientService)
        {
            _SclientService = SclientService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclients = await _SclientService.GetSclientListByValue(offset, limit, val);

            if (Sclients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclients in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclients);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclientList(string Sclient_name)
        {
            var Sclients = await _SclientService.GetSclientList(Sclient_name);

            if (Sclients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclient found for uci: {Sclient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclients);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclient(string Sclient_name)
        {
            var Sclients = await _SclientService.GetSclient(Sclient_name);

            if (Sclients == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclient found for uci: {Sclient_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclients);
        }

        [HttpPost]
        public async Task<ActionResult<Sclient>> AddSclient(Sclient Sclient)
        {
            var dbSclient = await _SclientService.AddSclient(Sclient);

            if (dbSclient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclient.TbSclientName} could not be added."
                );
            }

            return CreatedAtAction("GetSclient", new { uci = Sclient.TbSclientName }, Sclient);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclient(Sclient Sclient)
        {           
            Sclient dbSclient = await _SclientService.UpdateSclient(Sclient);

            if (dbSclient == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclient.TbSclientName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclient(Sclient Sclient)
        {            
            (bool status, string message) = await _SclientService.DeleteSclient(Sclient);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclient);
        }
    }
}
