using Microsoft.AspNetCore.Mvc;
using AppScalDB.Models;
using appscal_webapi.Services;

namespace appscal_webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SclirelController : ControllerBase
    {
        private readonly ISclirelService _SclirelService;

        public SclirelController(ISclirelService SclirelService)
        {
            _SclirelService = SclirelService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSclirelList(int offset, int limit, string val)
        {
            if (limit == 0)
                limit = 20;
            var Sclirels = await _SclirelService.GetSclirelListByValue(offset, limit, val);

            if (Sclirels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, "No Sclirels in database");
            }

            return StatusCode(StatusCodes.Status200OK, Sclirels);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclirelList(string Sclirel_name)
        {
            var Sclirels = await _SclirelService.GetSclirelList(Sclirel_name);

            if (Sclirels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclirel found for uci: {Sclirel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclirels);
        }

        [HttpGet]
        public async Task<IActionResult> GetSclirel(string Sclirel_name)
        {
            var Sclirels = await _SclirelService.GetSclirel(Sclirel_name);

            if (Sclirels == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No Sclirel found for uci: {Sclirel_name}");
            }

            return StatusCode(StatusCodes.Status200OK, Sclirels);
        }

        [HttpPost]
        public async Task<ActionResult<Sclirel>> AddSclirel(Sclirel Sclirel)
        {
            var dbSclirel = await _SclirelService.AddSclirel(Sclirel);

            if (dbSclirel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclirel.TbSclirelName} could not be added."
                );
            }

            return CreatedAtAction("GetSclirel", new { uci = Sclirel.TbSclirelName }, Sclirel);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSclirel(Sclirel Sclirel)
        {           
            Sclirel dbSclirel = await _SclirelService.UpdateSclirel(Sclirel);

            if (dbSclirel == null)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    $"{Sclirel.TbSclirelName} could not be updated"
                );
            }

            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSclirel(Sclirel Sclirel)
        {            
            (bool status, string message) = await _SclirelService.DeleteSclirel(Sclirel);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, Sclirel);
        }
    }
}
